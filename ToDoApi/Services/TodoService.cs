using System.Formats.Asn1;
using ToDoApi.Models;
using ToDoApi.Repositories;
using ToDoApi.Repositories.Interfaces;
using ToDoApi.Services.Interfaces;

namespace ToDoApi.Services
{
    public class TodoService : ItodoService
    {
       private readonly ITodoRepository<TodoItem> todoRepository;
        public TodoService(ITodoRepository<TodoItem> todoRepository)
        {
            this.todoRepository = todoRepository;
        }

        public async Task<TodoItem> CreateAsync(TodoItem item, string userId)
        {
            item.UserId = userId;
            await todoRepository.AddAsync(item);
            await todoRepository.SaveChangesAsync();
            return item;

        }

        public async Task<bool> DeleteAsync(string id, string userId)
        {
            var exitsting = await todoRepository.GetByIdAsync(id);
            if(exitsting == null) return false;
            todoRepository.Delete(exitsting);
            await todoRepository.SaveChangesAsync();    
            return true;
        }

        public async Task<TodoItem?> GetByIdAsync(string id)
        {
            var todo = await  todoRepository.GetByIdAsync(id);
            if (todo == null)
            {
                return null;
            }

            return todo;
        }

        public async Task<IEnumerable<TodoItem>> GetUserTodoAsync(string userId)
        {
            return await todoRepository.GetByUserAsync(userId);
           
        }

        public async Task<TodoItem?> UpdateAsync( TodoItem item)
        {
            var existingItem = await todoRepository.GetByIdAsync(item.Id);
            if (existingItem == null)
                return null; 

            existingItem.Title = item.Title;
            existingItem.Description = item.Description;
            existingItem.IsComplete = item.IsComplete;
            existingItem.CreatedAt = item.CreatedAt;

            await todoRepository.SaveChangesAsync();
            return existingItem;
        }
    }


}
