using System.ComponentModel.DataAnnotations.Schema;

namespace Todo.API.Models
{
    [Table("TodoItems")]
    public class TodoItem : BaseModel
    {
        public string? Title { get; set; }

        public string? Description { get; set; }
    }
}
