using System.ComponentModel.DataAnnotations;

namespace ToDoApi.DTOs.Request
{
    public class UpdateUserDto
    {
        [EmailAddress] public string? Email { get; set; }
        public string? FullName { get; set; }
    }
}
