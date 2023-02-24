namespace TodoApi.Models
{
    public class TodoModel
    {
        public int Id { get; set; }
        public string Task { get; set; } = null!;
        public bool IsDone { get; set; }
    }
}
