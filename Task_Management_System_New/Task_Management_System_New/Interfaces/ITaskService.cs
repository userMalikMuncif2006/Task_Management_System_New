using Task_Management_System_New.DTOs;

namespace Task_Management_System_New.Services
{
    public interface ITaskService
    {
        ApiResponse<IEnumerable<TaskItemResponseDto>> GetAllTasks();
        ApiResponse<TaskItemResponseDto> GetTaskById(int id);
        ApiResponse<IEnumerable<TaskItemResponseDto>> SearchTasks(string name);
        ApiResponse<TaskItemResponseDto> AddTask(CreateTaskItemDto dto);
        ApiResponse<bool> UpdateTask(int id, UpdateTaskItemDto dto);
        ApiResponse<bool> ChangeStatus(int id, ChangeStatusDto dto);
        ApiResponse<bool> DeleteTask(int id);
    }
}