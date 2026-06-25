using Task_Management_System_New.DTOs;
using Task_Management_System_New.Models;

namespace Task_Management_System_New.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<UserWithTasksDto> GetAllUsers();
        UserWithTasksDto? GetUserById(int id);
        User? GetUserByEmail(string email); // Used for duplicate check
        bool AddUser(CreateUserDto user);
        UserWithTasksDto? GetUserWithTasks(int id);
    }
}