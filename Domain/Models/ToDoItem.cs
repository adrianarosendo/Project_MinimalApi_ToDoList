
namespace Domain.Models
{
    public class ToDoItem
    {

        public ToDoItem() { }

        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsComplete { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime LastUpdate { get; set; }

    }
}
