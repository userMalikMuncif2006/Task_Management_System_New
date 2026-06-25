using Microsoft.AspNetCore.Mvc;
using Task_Management_System_New.DTOs;
using Task_Management_System_New.Interfaces;
using Task_Management_System_New.Services;

namespace Task_Management_System_New.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public IActionResult GetAllTasks()
        {
            var result = _taskService.GetAllTasks();
            return Ok(result); // Returns ApiResponse with List of Tasks
        }

        [HttpGet("{id}")]
        public IActionResult GetTaskById(int id)
        {
            var result = _taskService.GetTaskById(id);
            if (!result.Success) return NotFound(result);
            return Ok(result);
        }

        [HttpGet("search")]
        public IActionResult SearchTasks([FromQuery] string name)
        {
            var result = _taskService.SearchTasks(name);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddTask([FromBody] CreateTaskItemDto dto)
        {
            var result = _taskService.AddTask(dto);
            if (!result.Success) return BadRequest(result);
            return CreatedAtAction(nameof(GetTaskById), new { id = result.Data.TaskId }, result);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, [FromBody] UpdateTaskItemDto dto)
        {
            var result = _taskService.UpdateTask(id, dto);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPut("{id}/status")]
        public IActionResult ChangeStatus(int id, [FromBody] ChangeStatusDto dto)
        {
            var result = _taskService.ChangeStatus(id, dto);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var result = _taskService.DeleteTask(id);
            if (!result.Success) return NotFound(result);
            return Ok(result);
        }
    }
}