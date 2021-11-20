using System.Linq.Expressions;
using Todo.API.Models;

namespace Todo.API.Repositories
{
    public interface IRepository<T> where T : BaseModel
    {
        Task<T> AddAsync(T item);
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(string id);
        Task<List<T>> Where(Expression<Func<T, bool>> filter);
        Task<T> UpdateAsync(T item);
        Task DeleteByIdAsync(string id);
    }
}
