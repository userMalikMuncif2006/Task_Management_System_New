using Task_Management_System_New.DTOs;
using Task_Management_System_New.Models;

namespace Task_Management_System_New.Interfaces
{
    public interface ITaskRepository
    {
        IEnumerable<TaskItemResponseDto> GetAllTasks();
        TaskItemResponseDto? GetTaskById(int id);
        IEnumerable<TaskItemResponseDto> SearchTasks(string name);
        TaskItemResponseDto AddTask(TaskItem task);
        bool UpdateTask(int id, UpdateTaskItemDto task);
        bool UpdateStatus(int id, string status);
        bool DeleteTask(int id);
    }
}