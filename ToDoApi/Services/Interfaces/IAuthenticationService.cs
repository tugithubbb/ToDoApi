using ToDoApi.Models;

namespace ToDoApi.Services.Interfaces        
{
    public interface IAuthenticationService
    {
        Task<string> GenerateTokenAsync(ApplicationUser user);
    }
}
