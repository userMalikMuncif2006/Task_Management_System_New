using Task_Management_System_New.DTOs;
using Task_Management_System_New.Interfaces;
using Task_Management_System_New.Repositories;

namespace Task_Management_System_New.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ApiResponse<bool> AddUser(CreateUserDto dto)
        {
            // Rule: Check if email already exists
            var existingUser = _userRepository.GetUserByEmail(dto.Email);
            if (existingUser != null)
                return ApiResponse<bool>.FailureResponse("Email is already registered.");

            bool isAdded = _userRepository.AddUser(dto);
            return ApiResponse<bool>.SuccessResponse(isAdded, "User created successfully.");
        }

        public ApiResponse<IEnumerable<UserWithTasksDto>> GetAllUsers()
        {
            var users = _userRepository.GetAllUsers();
            return ApiResponse<IEnumerable<UserWithTasksDto>>.SuccessResponse(users);
        }

        public ApiResponse<UserWithTasksDto> GetUserById(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null) return ApiResponse<UserWithTasksDto>.FailureResponse("User not found.");
            return ApiResponse<UserWithTasksDto>.SuccessResponse(user);
        }

        public ApiResponse<UserWithTasksDto> GetUserWithTasks(int id)
        {
            var userWithTasks = _userRepository.GetUserWithTasks(id);
            if (userWithTasks == null) return ApiResponse<UserWithTasksDto>.FailureResponse("User not found.");
            return ApiResponse<UserWithTasksDto>.SuccessResponse(userWithTasks);
        }
    }
}