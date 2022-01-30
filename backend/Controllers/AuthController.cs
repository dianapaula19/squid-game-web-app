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
using Microsoft.Extensions.Logging;
using backend.Core.IConfiguration;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtConfig _jwtConfig;
        private readonly TokenValidationParameters _tokenValidationParams;
        private readonly DatabaseContext _databaseContext;
        private readonly ILogger<AuthController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IDictionary<int, string> _types;
        public AuthController(
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager,
            IOptionsMonitor<JwtConfig> optionsMonitor,
            TokenValidationParameters tokenValidationParams,
            DatabaseContext databaseContext,
            ILogger<AuthController> logger,
            IUnitOfWork unitOfWork
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtConfig = optionsMonitor.CurrentValue;
            _tokenValidationParams = tokenValidationParams;
            _databaseContext = databaseContext;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _types = new Dictionary<int, string>();
            _types.Add(0,"Manager"); 
            _types.Add(1,"Soldier");
            _types.Add(2,"Worker");
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto user)
        {
            if(ModelState.IsValid)
            {
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

                var newUser = new ApplicationUser() { 
                    Email = user.Email, 
                    UserName = user.Username, 
                    Country = user.Country,
                    ApplicationUserRole = user.Role
                };
                var newPlayer = new Player();
                var newGuard = new Guard();

                if (user.Role != "FrontMan")
                {
                    newUser.Status = "alive";
                    if (user.Role == "Player")
                    {
                        newPlayer.FirstName = user.PlayerInfo.FirstName;
                        newPlayer.LastName = user.PlayerInfo.LastName;
                        newPlayer.Gender = user.PlayerInfo.Gender;
                    }
                    if (user.Role == "Guard")
                    {
                        var rand = new Random();
                        newGuard.Type = _types[rand.Next(3)];
                    }
                }

                if (user.Role == "Player") {
                    newPlayer.ApplicationUserForeignKey = newUser.Id;
                    await _unitOfWork.Players.Add(newPlayer);
                    newUser.PlayerInfoForeignKey = newPlayer.PlayerId;
                }

                if (user.Role == "Guard") {
                    newGuard.ApplicationUserForeignKey = newUser.Id;
                    await _unitOfWork.Guards.Add(newGuard);
                    newUser.GuardInfoForeignKey = newGuard.GuardId;
                }

                var isCreated = await _userManager.CreateAsync(newUser, user.Password);
                
                if(isCreated.Succeeded)
                {
                    var roleExist = await _roleManager.RoleExistsAsync(user.Role);

                    if(!roleExist) // checks on the role exist status
                    {
                        _logger.LogInformation($"The role does not exist");
                        return BadRequest(new {
                            error = "Role does not exist"
                        });
                    }

                    var result = await _userManager.AddToRoleAsync(newUser, user.Role);

                    // Check if the user is assigned to the role successfully
                    if(!result.Succeeded)
                    {
                        _logger.LogInformation($"The user was not able to be added to the role");
                        return BadRequest(new {
                            error = "The user was not able to be added to the role"
                        });
                    }

                    var jwtToken = GenerateJwtToken(newUser);

                    return Ok(new RegistrationResponse() {
                        Success = true,
                        Token = jwtToken,
                        Role = user.Role,
                        Email = user.Email,
                    });
                    
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
                        return BadRequest(new LoginResponse(){
                            Errors = new List<string>() {
                                "Invalid login request"
                            },
                            Success = false
                    });
                }

                var isCorrect = await _userManager.CheckPasswordAsync(existingUser, user.Password);

                if(!isCorrect) {
                      return BadRequest(new LoginResponse(){
                        Errors = new List<string>() {
                            "Invalid login request"
                        },
                        Success = false
                    });
                }

                var jwtToken = GenerateJwtToken(existingUser);

                return Ok(new LoginResponse() {
                    Success = true,
                    Token = jwtToken,
                    Email = existingUser.Email,
                    Role = existingUser.ApplicationUserRole,
                    Status = existingUser.Status,
                });
            }

            return BadRequest(new LoginResponse(){
                    Errors = new List<string>() {
                        "Invalid payload"
                    },
                    Success = false
            });
        }

        private string GenerateJwtToken(ApplicationUser user)
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
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }

    }

}
