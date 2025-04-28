using System.ComponentModel.DataAnnotations;

namespace ToDoApi.Models
{
    public class InvalidatedToken
    {
        [Key]
        public required string Id { get; set; }  
        public required string Token { get; set; } 
        public DateTime ExpiryDate { get; set; }  
        public DateTime InvalidatedAt { get; set; }

        public string UserId { get; set; }

        // 🔗 Navigation
        public ApplicationUser? User { get; set; }
    }
}
