using Todo.API.Repositories;
using Todo.API.DTOs.Todo;
using Todo.API.IServices;
using Todo.API.Models;

namespace Todo.API.Services
{
    public class TodoService : ITodoService
    {
        private readonly IRepository<TodoItem> _todoRepository;

        public TodoService(IRepository<TodoItem> todoItems)
        {
            _todoRepository = todoItems;
        }

        public Task<TodoItem> AddAsync(CreateTodoDto dto)
        {
            var todo = new TodoItem { Title = dto.Title, Description = dto.Description };
            return _todoRepository.AddAsync(todo);
        }

        public Task DeleteByIdAsync(string id)
        {
            return _todoRepository.DeleteByIdAsync(id);
        }

        public Task<List<TodoItem>> GetAllAsync()
        {
            return _todoRepository.GetAllAsync();
        }

        public Task<TodoItem> GetByIdAsync(string id)
        {
            return _todoRepository.GetByIdAsync(id);
        }

        public async Task<TodoItem> UpdateAsync(UpdateTodoDto dto)
        {
            var todo = await GetByIdAsync(dto.Id);
            todo.Title = dto.Title;
            todo.Description = dto.Description; 
            return await _todoRepository.UpdateAsync(todo);
        }
    }
}
