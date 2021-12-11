using System;
using System.Threading.Tasks;
using backend.Core.IConfiguration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using backend.Models;
using Newtonsoft.Json.Linq;

namespace backend.Controllers
{
    [Route("/user")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public UsersController(
            ILogger<UsersController> logger,
            IUnitOfWork unitOfWork
        )
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public class PlayerObject 
        {   
            public Guid FrontManId {get; set;}
            public User User {get; set;}
            public Player Player {get; set;}
        }

        [HttpPost("/player")]
        public async Task<IActionResult> CreatePlayer(PlayerObject playerObject)
        {   
            try
            {
                playerObject.User.Id = Guid.NewGuid();
                playerObject.User.FrontManId = playerObject.FrontManId;
                await _unitOfWork.Users.Add(playerObject.User);
                playerObject.Player.UserId = playerObject.User.Id;
                await _unitOfWork.Players.Add(playerObject.Player);
                await _unitOfWork.CompleteAsync();

                return CreatedAtAction("GetItem", new {playerObject.Player.UserId}, playerObject.Player);
                 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All method error");
                return new JsonResult("Something went wrong") {StatusCode = 500};
            }
        }

        [HttpPost("/guard")]
        public async Task<IActionResult> CreateGuard(User user)
        {
            if (ModelState.IsValid)
            {
                user.Id = Guid.NewGuid();
                await _unitOfWork.Users.Add(user);
                await _unitOfWork.CompleteAsync();

                return CreatedAtAction("GetItem", new {user.Id}, user);
            }

            return new JsonResult("Something went wrong") {StatusCode = 500};
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(Guid id)
        {
            var user = await _unitOfWork.Users.GetById(id);

            if (user == null) 
                return NotFound();

            return Ok(user);
        }
    }
}