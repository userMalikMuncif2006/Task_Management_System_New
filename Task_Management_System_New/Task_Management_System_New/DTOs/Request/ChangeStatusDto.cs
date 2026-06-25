using System.ComponentModel.DataAnnotations;

namespace Task_Management_System_New.DTOs
{
    public class ChangeStatusDto
    {
        [Required]
        [RegularExpression("Todo|In Progress|Done", ErrorMessage = "Invalid Status")]
        public string Status { get; set; } = string.Empty;
    }
}