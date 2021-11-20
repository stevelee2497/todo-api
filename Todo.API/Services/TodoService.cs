using Todo.API.Repositories;
using Todo.API.DTOs.Todo;
using Todo.API.IServices;
using Todo.API.Models;

namespace Todo.API.Services
{
    public class TodoService : ITodoService
    {
        private readonly IRepository<TodoItem> _todoItems;

        public TodoService(IRepository<TodoItem> todoItems)
        {
            _todoItems = todoItems;
        }

        public Task<TodoItem> AddAsync(CreateTodoDto dto)
        {
            var todo = new TodoItem { Title = dto.Title, Description = dto.Description };
            return _todoItems.AddAsync(todo);
        }

        public Task DeleteByIdAsync(string id)
        {
            return _todoItems.DeleteByIdAsync(id);
        }

        public Task<List<TodoItem>> GetAllAsync()
        {
            return _todoItems.GetAllAsync();
        }

        public Task<TodoItem> GetByIdAsync(string id)
        {
            return _todoItems.GetByIdAsync(id);
        }

        public async Task<TodoItem> UpdateAsync(UpdateTodoDto dto)
        {
            var todo = await GetByIdAsync(dto.Id);
            todo.Title = dto.Title;
            todo.Description = dto.Description; 
            return await _todoItems.UpdateAsync(todo);
        }
    }
}
