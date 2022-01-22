using System;
using System.Threading.Tasks;
using backend.Core.IConfiguration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TodoController : ControllerBase
    {
        private readonly ILogger<TodoController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public TodoController(
            ILogger<TodoController> logger,
            IUnitOfWork unitOfWork
        )
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodos()
        {
            var todos = await _unitOfWork.Todos.All();
            if (todos == null) 
                return NotFound();
            return Ok(todos);
        }

        [HttpPost]
        [Route("Create")]
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

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateTodo(Todo todo)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.Todos.Update(todo);
                await _unitOfWork.CompleteAsync();

                return Ok(todo);
            }
            return new JsonResult("Something went wrong") {StatusCode = 500};
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTodoById(Guid id)
        {
            var todo = await _unitOfWork.Todos.GetById(id);

            if (todo == null) 
                return NotFound();

            return Ok(todo);
        }
    }
}