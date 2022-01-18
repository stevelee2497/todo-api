using MongoDB.Driver;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using Todo.API.Filters;
using Todo.API.Models;

namespace Todo.API.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        private readonly IMongoCollection<T> _colection;

        public Repository(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            var table = typeof(T).GetCustomAttributes(typeof(TableAttribute), true).FirstOrDefault() as TableAttribute;
            _colection = database.GetCollection<T>(table?.Name ?? nameof(T));
            if (_colection == null) throw new InternalErrorException("Could not get database colection");
        }

        public async Task<T> AddAsync(T item)
        {
            item.CreatedOn = item.UpdatedOn = DateTimeOffset.Now;
            item.CreatedBy = item.UpdatedBy = "sa";
            await _colection.InsertOneAsync(item);
            return item;
        }

        public Task DeleteByIdAsync(string id)
        {
            return _colection.FindOneAndDeleteAsync(x => x.Id == id);
        }

        public Task<List<T>> GetAllAsync()
        {
            return _colection.Find(x => true).ToListAsync();
        }

        public Task<T> GetByIdAsync(string id)
        {
            return _colection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task<List<T>> Where(Expression<Func<T, bool>> filter)
        {
            return _colection.Find(filter).ToListAsync();
        }

        public async Task<T> UpdateAsync(T item)
        {
            item.CreatedOn = item.UpdatedOn = DateTimeOffset.Now;
            item.UpdatedBy = "sa";
            await _colection.ReplaceOneAsync(x => x.Id == item.Id, item);
            return item;
        }
    }
}
