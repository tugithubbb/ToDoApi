namespace ToDoApi.DTOs.Request
{
    public class TodoCreateDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string UserId { get; set; }
    }
}
