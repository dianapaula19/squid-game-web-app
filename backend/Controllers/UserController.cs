using System;
using System.Linq;
using System.Threading.Tasks;
using backend.Core.IConfiguration;
using backend.DAL;
using backend.Models.DTOS.Responses;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace backend.Models
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly DatabaseContext _databaseContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserController> _logger;

        public UserController(
            UserManager<ApplicationUser> userManager, 
            DatabaseContext databaseContext,
            ILogger<UserController> logger,
            IUnitOfWork unitOfWork 
            )
        {
            _userManager = userManager;
            _databaseContext = databaseContext;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Authorize(Roles = "FrontMan")]
        public async Task<IActionResult> GetUsersByStatus()
        {
            var users = await _userManager.Users.ToListAsync();
            if (users == null)
                return NotFound();
            var query = users.GroupBy(
                user => user.Status,
                user => user.Status,
                (status, statues) => new
                {
                    Key = status,
                    Count = statues.Count()
                }
            );
            return(Ok(query));
        }

        [HttpPut]
        [Authorize(Roles = "FrontMan")]
        public async Task<IActionResult> SetStatusToDead(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if(user == null)
            {
                return BadRequest("Email not registered");
            }
            user.Status = "dead";
            await _userManager.UpdateAsync(user);
            return(Ok("Updated the status"));
        }

        [HttpGet]
        [Route("Profile")]
        public async Task<IActionResult> GetUserData(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if(user == null)
            {
                return BadRequest("Email not registered");
            }
            var response = new ApplicationUserProfileResponse(){
                Username = user.UserName,
                Country = user.Country,
            };
            if(user.ApplicationUserRole == "Profile"){
                var player = await _unitOfWork.Players.GetById(user.PlayerInfoForeignKey);
                response.FirstName = player.FirstName;
                response.LastName = player.LastName;
                response.Gender = player.Gender;
                return(Ok(response));
            }
            if(user.ApplicationUserRole == "Guard"){
                var guard = await _unitOfWork.Guards.GetById(user.GuardInfoForeignKey);
                response.Type = guard.Type;
                return(Ok(response));
            }
            return(Ok(response));
        }

        [HttpGet]
        [Authorize(Roles = "FrontMan")]
        public async Task<IActionResult> GetUsersByCountry(string email)
        {
            var frontman = await _userManager.FindByEmailAsync(email);
            if(frontman == null)
            {
                return BadRequest("No such user exists");
            }
            var users = await _userManager.Users.ToListAsync();
            return(Ok(users.Where(u => u.Country == frontman.Country)));
        }

        [HttpDelete]
        [Authorize(Roles = "Player, Guard")]
        [Route("Delete")]
        public async Task<IActionResult> DeleteAccount(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if(user == null)
            {
                return BadRequest("No such user exists");
            }
            if (user.ApplicationUserRole == "Player") 
            {
                await _unitOfWork.Players.Delete(user.PlayerInfoForeignKey);
            }
            if (user.ApplicationUserRole == "Guard") 
            {
                await _unitOfWork.Players.Delete(user.GuardInfoForeignKey);
            }
            await _userManager.DeleteAsync(user);
            return Ok("Account deleted succesfully");
            
        }
    }
}