using System.Linq;
using System.Threading.Tasks;
using backend.DAL;
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
        private readonly ILogger<UserController> _logger;

        public UserController(
            UserManager<ApplicationUser> userManager, 
            DatabaseContext databaseContext,
            ILogger<UserController> logger
            )
        {
            _userManager = userManager;
            _databaseContext = databaseContext;
            _logger = logger;
        
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
            return(Ok());
        }

        [HttpGet]
        public async Task<IActionResult> GetUserData(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if(user == null)
            {
                return BadRequest("Email not registered");
            }
            return(Ok(user));
        }
    }
}