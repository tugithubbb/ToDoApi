using ToDoApi.Models;

namespace ToDoApi.Services.Interfaces
{
    public interface ItodoService
    {
        Task<TodoItem> CreateAsync(TodoItem item,string userId);
        Task<TodoItem?> GetByIdAsync(string id);
        Task<IEnumerable<TodoItem>> GetUserTodoAsync(string userId);
        Task<TodoItem?> UpdateAsync(TodoItem item);
        Task<bool> DeleteAsync(string id, string userId);
    }
}
