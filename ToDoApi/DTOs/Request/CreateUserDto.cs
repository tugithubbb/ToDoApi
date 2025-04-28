using System.ComponentModel.DataAnnotations;

namespace ToDoApi.DTOs.Request
{
    public class CreateUserDto
    {
        [Required] public string UserName { get; set; }
        [Required][EmailAddress] public string Email { get; set; }
        [Required] public string Password { get; set; }
        public string? FullName { get; set; }
    }
}
