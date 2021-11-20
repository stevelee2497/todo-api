namespace Todo.API.DTOs.Todo
{
    public class UpdateTodoDto
    {
        public string Id { get; set; } = String.Empty;

        public string? Title { get; set; }

        public string? Description { get; set; }
    }
}
