using System.ComponentModel.DataAnnotations;

namespace Task_Management_System_New.DTOs
{
    public class CreateTaskItemDto
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        [RegularExpression("Todo|In Progress|Done", ErrorMessage = "Status must be: Todo, In Progress, or Done")]
        public string Status { get; set; } = "Todo";

        [Required]
        public int UserId { get; set; }
    }
}