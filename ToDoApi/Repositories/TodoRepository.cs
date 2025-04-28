
using Microsoft.EntityFrameworkCore;
using Polly;
using System.Runtime.CompilerServices;
using ToDoApi.Data;
using ToDoApi.Models;
using ToDoApi.Repositories.Interfaces;

namespace ToDoApi.Repositories
{
    public class TodoRepository<T> : ITodoRepository<T> where T : class, IUserOwnedEntity
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;
        public TodoRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
             await _dbSet.AddAsync(entity);
        }
            

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var entities = await _dbSet.ToListAsync();
            return entities;

        }

       

        public async Task<T> GetByIdAsync(string id)
        {
            var entity = await _dbSet.FindAsync(id);
            return entity;
        }

        public async Task<IEnumerable<T>> GetByUserAsync(string userId)
        {
           return await _dbSet.Where(t => t.UserId == userId).ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
           await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
