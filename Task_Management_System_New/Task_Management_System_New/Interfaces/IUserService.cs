using Task_Management_System_New.DTOs;

namespace Task_Management_System_New.Services
{
    public interface IUserService
    {
        ApiResponse<IEnumerable<UserWithTasksDto>> GetAllUsers(); // Simplified list
        ApiResponse<UserWithTasksDto> GetUserById(int id);
        ApiResponse<bool> AddUser(CreateUserDto dto);
        ApiResponse<UserWithTasksDto> GetUserWithTasks(int id);
    }
}