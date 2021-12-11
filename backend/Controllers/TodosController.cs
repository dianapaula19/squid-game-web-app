using System;
using System.Threading.Tasks;
using backend.Core.IConfiguration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using backend.Models;

namespace backend.Controllers
{
    [ApiController]
    [Route("todo")]
    public class TodosController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public TodosController(
            ILogger<UsersController> logger,
            IUnitOfWork unitOfWork
        )
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTodo(Todo todo)
        {
            if (ModelState.IsValid)
            {
                todo.Id = Guid.NewGuid();
                await _unitOfWork.Todos.Add(todo);
                await _unitOfWork.CompleteAsync();

                return CreatedAtAction("GetItem", new {todo.Id}, todo);
            }

            return new JsonResult("Something went wrong") {StatusCode = 500};
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(Guid id)
        {
            var todo = await _unitOfWork.Todos.GetById(id);

            if (todo == null) 
                return NotFound();

            return Ok(todo);
        }
    }
}