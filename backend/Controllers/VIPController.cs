using System;
using System.Threading.Tasks;
using backend.Core.IConfiguration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using backend.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.AspNetCore.Authorization;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "FrontMan")]
    public class VIPController : ControllerBase
    {
        private readonly ILogger<VIPController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public VIPController(
            ILogger<VIPController> logger,
            IUnitOfWork unitOfWork
        )
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetVIPs()
        {
            var vips = await _unitOfWork.VIPS.All();
            if (vips == null) 
                return NotFound();
            return Ok(vips);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVIPById(Guid id)
        {
            var vip = await _unitOfWork.VIPS.GetById(id);

            if (vip == null) 
                return NotFound();

            return Ok(vip);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateTodo(VIP vip)
        {
            if (ModelState.IsValid)
            {
                vip.Id = Guid.NewGuid();
                await _unitOfWork.VIPS.Add(vip);
                await _unitOfWork.CompleteAsync();

                return CreatedAtAction("GetItem", new {vip.Id}, vip);
            }

            return new JsonResult("Something went wrong") {StatusCode = 500};
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateVIP(VIP vip)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.VIPS.Update(vip);
                await _unitOfWork.CompleteAsync();

                return Ok(vip);
            }
            return new JsonResult("Something went wrong") {StatusCode = 500};
            
        }

    }
}