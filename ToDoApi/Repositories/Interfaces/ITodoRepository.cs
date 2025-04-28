

using ToDoApi.Models;

namespace ToDoApi.Repositories.Interfaces
{
    public interface ITodoRepository<T> where T : class
    {
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveChangesAsync();
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(string id);
        Task<IEnumerable<T>> GetByUserAsync(string userId);
    }
}
