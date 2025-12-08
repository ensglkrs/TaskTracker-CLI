using TaskTracker.Enums;


namespace TaskTracker.Models
{
    public class AppTask
    {
        public int Id { get; set; }
        public string Description {  get; set; } = string.Empty;
        public Status TaskStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
