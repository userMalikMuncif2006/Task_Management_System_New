using System.ComponentModel.DataAnnotations;

namespace Task_Management_System_New.DTOs
{
    public class CreateUserDto
    {
        [Required(ErrorMessage = "User name is required")]
        [StringLength(100)]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;
    }
}