using System;
using System.Threading.Tasks;
using backend.Core.IConfiguration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using backend.Models;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FrontManController : ControllerBase
    {
        private readonly ILogger<FrontManController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public FrontManController(
            ILogger<FrontManController> logger,
            IUnitOfWork unitOfWork
        )
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFrontMan(FrontMan frontMan)
        {
            if (ModelState.IsValid)
            {
                frontMan.Id = Guid.NewGuid();
                await _unitOfWork.FrontMen.Add(frontMan);
                await _unitOfWork.CompleteAsync();

                return CreatedAtAction("GetItem", new {frontMan.Id}, frontMan);
            }

            return new JsonResult("Something went wrong") {StatusCode = 500};
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(Guid id)
        {
            var frontMan = await _unitOfWork.FrontMen.GetById(id);

            if (frontMan == null) 
                return NotFound();

            return Ok(frontMan);
        }
    }
}