using Microsoft.AspNetCore.Identity;

namespace ToDoApi.Models
{
    public class ApplicationUser : IdentityUser<string>

    {
        public ApplicationUser()
        {
            Id = Guid.NewGuid().ToString(); 
        }
        public string? FullName { get; set; }
        public ICollection<TodoItem> Todos { get; set; } = new List<TodoItem>();
        public ICollection<ActiveToken> ActiveTokens { get; set; } = new List<ActiveToken>();
        public ICollection<InvalidatedToken>? InvalidatedTokens { get; set; }

    }
}
