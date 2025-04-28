using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ToDoApi.DTOs.Request;
using ToDoApi.Models;
using ToDoApi.Services;
using ToDoApi.Services.Interfaces;

namespace ToDoApi.Controllers
{
    
    [ApiController]
    [Route("api/todos")]
    public class TodoController : ControllerBase
    {
        private readonly ItodoService itodoService;
        private readonly ILogger<TodoController> logger;
        public TodoController(ItodoService itodoService, ILogger<TodoController> logger)
        {
            this.itodoService = itodoService;
            this.logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TodoCreateDto item)
        {
            var userIdClaim = item.UserId;
            if (userIdClaim != null)
            { 
                logger.LogInformation($"UserId: {userIdClaim}");
            }
            else
            {
                logger.LogWarning("UserId claim not found in token");
                return Unauthorized();
            }
            var todoItem = new TodoItem
            {
                UserId = userIdClaim,
                Title = item.Title,
                Description = item.Description
            };
            var created = await itodoService.CreateAsync(todoItem, userIdClaim);
            return CreatedAtAction(nameof(GetById),new {id = created.Id},created);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            if (id == null) return Unauthorized();
            var todo = await itodoService.GetByIdAsync(id);
            if (todo == null) return NotFound();
            return Ok(todo);    
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            if (userId == null) return Unauthorized();
            var success = await itodoService.DeleteAsync(id,userId);
            if (!success) return NotFound();
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] TodoItem item)
        {

            var updated = await itodoService.UpdateAsync(item);
            if (updated == null) return NotFound();
            return Ok(updated);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll(string userId)
        {
            var list = await itodoService.GetUserTodoAsync(userId);
            return Ok(list);
        }


    }
}
