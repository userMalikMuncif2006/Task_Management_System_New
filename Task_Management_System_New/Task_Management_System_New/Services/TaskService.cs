using Task_Management_System_New.DTOs;
using Task_Management_System_New.Interfaces;
using Task_Management_System_New.Repositories;
using Task_Management_System_New.Models;

namespace Task_Management_System_New.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUserRepository _userRepository;

        public TaskService(ITaskRepository taskRepository, IUserRepository userRepository)
        {
            _taskRepository = taskRepository;
            _userRepository = userRepository;
        }

        public ApiResponse<TaskItemResponseDto> AddTask(CreateTaskItemDto dto)
        {
            // Rule: Check if the assigned user exists
            var user = _userRepository.GetUserById(dto.UserId);
            if (user == null)
                return ApiResponse<TaskItemResponseDto>.FailureResponse("Assigned user does not exist.");

            var newTask = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                Status = dto.Status,
                UserId = dto.UserId
            };

            var createdTask = _taskRepository.AddTask(newTask);
            return ApiResponse<TaskItemResponseDto>.SuccessResponse(createdTask, "Task added successfully.");
        }

        public ApiResponse<IEnumerable<TaskItemResponseDto>> GetAllTasks()
        {
            var tasks = _taskRepository.GetAllTasks();
            return ApiResponse<IEnumerable<TaskItemResponseDto>>.SuccessResponse(tasks);
        }

        public ApiResponse<TaskItemResponseDto> GetTaskById(int id)
        {
            var task = _taskRepository.GetTaskById(id);
            if (task == null) return ApiResponse<TaskItemResponseDto>.FailureResponse("Task not found.");
            return ApiResponse<TaskItemResponseDto>.SuccessResponse(task);
        }

        public ApiResponse<IEnumerable<TaskItemResponseDto>> SearchTasks(string name)
        {
            var tasks = _taskRepository.SearchTasks(name);
            return ApiResponse<IEnumerable<TaskItemResponseDto>>.SuccessResponse(tasks);
        }

        public ApiResponse<bool> UpdateTask(int id, UpdateTaskItemDto dto)
        {
            var existingTask = _taskRepository.GetTaskById(id);
            if (existingTask == null) return ApiResponse<bool>.FailureResponse("Task not found.");

            var user = _userRepository.GetUserById(dto.UserId);
            if (user == null) return ApiResponse<bool>.FailureResponse("Assigned user does not exist.");

            bool updated = _taskRepository.UpdateTask(id, dto);
            return ApiResponse<bool>.SuccessResponse(updated, "Task updated successfully.");
        }

        public ApiResponse<bool> ChangeStatus(int id, ChangeStatusDto dto)
        {
            bool updated = _taskRepository.UpdateStatus(id, dto.Status);
            if (!updated) return ApiResponse<bool>.FailureResponse("Task not found or status update failed.");
            return ApiResponse<bool>.SuccessResponse(true, "Status updated successfully.");
        }

        public ApiResponse<bool> DeleteTask(int id)
        {
            bool deleted = _taskRepository.DeleteTask(id);
            if (!deleted) return ApiResponse<bool>.FailureResponse("Task not found.");
            return ApiResponse<bool>.SuccessResponse(true, "Task deleted successfully.");
        }
    }
}