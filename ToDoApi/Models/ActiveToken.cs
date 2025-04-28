using System.ComponentModel.DataAnnotations;

namespace ToDoApi.Models
{
    public class ActiveToken
    {
        [Key]
        public required string Id { get; set; }  
        public string Token { get; set; }  = string.Empty;
        public DateTime CreatedAt { get; set; } 
        public DateTime ExpiryDate { get; set; }
        public string UserId { get; set; } = string.Empty;

        public required ApplicationUser User { get; set; }
    }
}
