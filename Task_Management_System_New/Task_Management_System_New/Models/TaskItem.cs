namespace Task_Management_System_New.Models
{
    public class TaskItem
    {
        public int TaskId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Status { get; set; } = "Todo"; // Todo, In Progress, Done
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int UserId { get; set; }
    }
}