using Microsoft.AspNetCore.Mvc;
using Todo.API.DTOs.Todo;
using Todo.API.Filters;
using Todo.API.IServices;
using Todo.API.Models;

namespace Todo.API.Controllers
{
    [ApiController]
    [Route("api/todo")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;
        private readonly ILogger<TodoController> _logger;

        public TodoController(ILogger<TodoController> logger, ITodoService todoService)
        {
            _logger = logger;
            _todoService = todoService;
        }

        [HttpGet]
        public Task<List<TodoItem>> GetAsync()
        {
            return _todoService.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<TodoItem> GetByIdAsync(string id)
        {
            var item = await _todoService.GetByIdAsync(id);
            return item ?? throw new InternalErrorException("item not found");
        }

        [HttpPost]
        public Task<TodoItem> CreateAsync([FromBody] CreateTodoDto dto)
        {
            return _todoService.AddAsync(dto);
        }

        [HttpPut("{id}")]
        public Task<TodoItem> UpdateAsync([FromBody] UpdateTodoDto dto, string id)
        {
            dto.Id = id;
            return _todoService.UpdateAsync(dto);
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync(string id)
        {
            if (await _todoService.GetByIdAsync(id) == null)
            {
                throw new InternalErrorException("item not found");
            }

            await _todoService.DeleteByIdAsync(id);
        }
    }
}