using System.Threading.Tasks;
using backend.Configuration;
using backend.Models.DTOS.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System;
using backend.Models;
using backend.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using backend.Core.IConfiguration;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtConfig _jwtConfig;
        private readonly TokenValidationParameters _tokenValidationParams;
        private readonly DatabaseContext _databaseContext;
        private readonly ILogger<AuthController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public AuthController(
            UserManager<ApplicationUser> userManager, 
            IOptionsMonitor<JwtConfig> optionsMonitor,
            TokenValidationParameters tokenValidationParams,
            DatabaseContext databaseContext,
            ILogger<AuthController> logger,
            IUnitOfWork unitOfWork
            )
        {
            _userManager = userManager;
            _jwtConfig = optionsMonitor.CurrentValue;
            _tokenValidationParams = tokenValidationParams;
            _databaseContext = databaseContext;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto user)
        {
            if(ModelState.IsValid)
            {
                // We can utilise the model
                var existingUser = await _userManager.FindByEmailAsync(user.Email);

                if(existingUser != null)
                {
                    return BadRequest(new RegistrationResponse(){
                            Errors = new List<string>() {
                                "Email already in use"
                            },
                            Success = false
                    });
                }
                var newUser = new ApplicationUser()
                {
                    UserName = user.Username,
                    Email = user.Email,
                    Country = user.Country,
                };
                if (user.Role.ToUpper() == "PLAYER" || user.Role.ToUpper() == "GUARD")
                {
                    newUser.Status = "alive";
                    
                    if (user.Role.ToUpper() == "PLAYER")
                    {
                        var newPlayer = new Player();
                        newPlayer.FirstName = user.PlayerRegistrationDTO.FirstName;
                        newPlayer.LastName = user.PlayerRegistrationDTO.LastName;
                        newPlayer.Gender = user.PlayerRegistrationDTO.Gender;
                        newPlayer.ApplicationUser = newUser;
                        newPlayer.ApplicationUserForeignKey = newUser.Id;
                        await _unitOfWork.Players.Add(newPlayer);
                        newUser.PlayerInfo = newPlayer;
                    } else if (user.Role.ToUpper() == "Guard")
                    {
                        var newGuard = new Guard();
                        newGuard.ApplicationUser = newUser;
                        newGuard.ApplicationUserForeignKey = newUser.Id;
                        await _unitOfWork.Guards.Add(newGuard);
                        newUser.GuardInfo = newGuard;
                    }
                }
                
                var isCreated = await _userManager.CreateAsync(newUser, user.Password);
                if(isCreated.Succeeded)
                {
                   var jwtToken = await GenerateJwtToken(newUser);

                   return Ok(jwtToken);
                } else {
                    return BadRequest(new RegistrationResponse(){
                            Errors = isCreated.Errors.Select(x => x.Description).ToList(),
                            Success = false
                    });
                }
            }

            return BadRequest(new RegistrationResponse(){
                    Errors = new List<string>() {
                        "Invalid payload"
                    },
                    Success = false
            });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest user)
        {
            if(ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Email);

                if(existingUser == null) {
                        return BadRequest(new RegistrationResponse(){
                            Errors = new List<string>() {
                                "Invalid login request"
                            },
                            Success = false
                    });
                }

                var isCorrect = await _userManager.CheckPasswordAsync(existingUser, user.Password);

                if(!isCorrect) {
                      return BadRequest(new RegistrationResponse(){
                            Errors = new List<string>() {
                                "Invalid login request"
                            },
                            Success = false
                    });
                }

                var jwtToken  = await GenerateJwtToken(existingUser);

                return Ok(jwtToken);
            }

            return BadRequest(new RegistrationResponse(){
                    Errors = new List<string>() {
                        "Invalid payload"
                    },
                    Success = false
            });
        }

        [HttpPost]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequest tokenRequest)
        {
            if(ModelState.IsValid)
            {
                var result = await VerifyAndGenerateToken(tokenRequest);

                if(result == null) {
                    return BadRequest(new RegistrationResponse() {
                        Errors = new List<string>() {
                            "Invalid tokens"
                        },
                        Success = false
                    });
                }

                return Ok(result);
            }

            return BadRequest(new RegistrationResponse() {
                Errors = new List<string>() {
                    "Invalid payload"
                },
                Success = false
            });
        }


        private async Task<AuthResult> GenerateJwtToken(ApplicationUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new []
                {
                    new Claim("Id", user.Id), 
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddSeconds(30), // 5-10 
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            var refreshToken = new RefreshToken()
            {
                JwtId = token.Id,
                IsUsed = false,
                IsRevoked = false,
                UserId = user.Id,
                AddedDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(6),
                Token = RandomString(35) + Guid.NewGuid()
            };

            await _databaseContext.RefreshTokens.AddAsync(refreshToken);
            await _databaseContext.SaveChangesAsync();

            return new AuthResult() {
                Token = jwtToken,
                Success = true,
                RefreshToken = refreshToken.Token
            };
        }

        private async Task<AuthResult> VerifyAndGenerateToken(TokenRequest tokenRequest)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            try
            {   
                // Validation 1 - Validation JWT token format
                var tokenInVerification = jwtTokenHandler.ValidateToken(tokenRequest.Token, _tokenValidationParams, out var validatedToken);

                // Validation 2 - Validate encryption alg
                if(validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

                    if(result == false) {
                        return null;
                    }
                }

                // Validation 3 - validate expiry date
                var utcExpiryDate = long.Parse(tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

                var expiryDate = UnixTimeStampToDateTime(utcExpiryDate);

                if(expiryDate > DateTime.UtcNow) {
                    return new AuthResult() {
                        Success = false,
                        Errors = new List<string>() {
                            "Token has not yet expired"
                        }
                    };
                }

                // validation 4 - validate existence of the token
                var storedToken = await _databaseContext.RefreshTokens.FirstOrDefaultAsync(x => x.Token == tokenRequest.RefreshToken);

                if(storedToken == null)
                {
                    return new AuthResult() {
                        Success = false,
                        Errors = new List<string>() {
                            "Token does not exist"
                        }
                    };
                }

                // Validation 5 - validate if used
                if(storedToken.IsUsed)
                {
                    return new AuthResult() {
                        Success = false,
                        Errors = new List<string>() {
                            "Token has been used"
                        }
                    };
                }

                // Validation 6 - validate if revoked
                if(storedToken.IsRevoked)
                {
                    return new AuthResult() {
                        Success = false,
                        Errors = new List<string>() {
                            "Token has been revoked"
                        }
                    };
                }

                // Validation 7 - validate the id
                var jti = tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

                if(storedToken.JwtId != jti)
                {
                    return new AuthResult() {
                        Success = false,
                        Errors = new List<string>() {
                            "Token doesn't match"
                        }
                    };
                }

                // update current token 

                storedToken.IsUsed = true;
                _databaseContext.RefreshTokens.Update(storedToken);
                await _databaseContext.SaveChangesAsync();
                
                // Generate a new token
                var dbUser = await _userManager.FindByIdAsync(storedToken.UserId);
                return await GenerateJwtToken(dbUser);
            }
            catch(Exception ex)
            {
                if(ex.Message.Contains("Lifetime validation failed. The token is expired.")) {

                      return new AuthResult() {
                        Success = false,
                        Errors = new List<string>() {
                            "Token has expired please re-login"
                        }
                    };
                
                } else {
                      return new AuthResult() {
                        Success = false,
                        Errors = new List<string>() {
                            "Something went wrong."
                        }
                    };
                }
            }    
        }

        private DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            var dateTimeVal = new DateTime(1970, 1,1,0,0,0,0, DateTimeKind.Utc);
            dateTimeVal = dateTimeVal.AddSeconds(unixTimeStamp).ToUniversalTime();

            return dateTimeVal;
        }

        private string RandomString(int length)
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(x => x[random.Next(x.Length)]).ToArray());
        }

    }

}
