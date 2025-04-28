using ToDoApi.DTOs.Request;
using ToDoApi.DTOs.Response;

namespace ToDoApi.Services.Interfaces
{
    public interface IUserServices
    {
        Task<UserDto> CreateAsync (CreateUserDto dto);
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto?> GetByIdAsync(string id);
        Task<UserDto?> UpdateAsync(string id, UpdateUserDto dto);
        Task<bool> DeleteAsync(string id);
    }
}
