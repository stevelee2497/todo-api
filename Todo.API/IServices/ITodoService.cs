using Todo.API.DTOs.Todo;
using Todo.API.Models;

namespace Todo.API.IServices
{
    public interface ITodoService
    {
        Task<TodoItem> AddAsync(CreateTodoDto dto);
        Task<List<TodoItem>> GetAllAsync();
        Task<TodoItem> GetByIdAsync(string id);
        Task<TodoItem> UpdateAsync(UpdateTodoDto item);
        Task DeleteByIdAsync(string id);
    }
}
