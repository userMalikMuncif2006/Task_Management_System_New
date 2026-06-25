using Microsoft.AspNetCore.Mvc;
using Task_Management_System_New.DTOs;
using Task_Management_System_New.Interfaces;
using Task_Management_System_New.Services;

namespace Task_Management_System_New.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var result = _userService.GetAllUsers();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var result = _userService.GetUserById(id);
            if (!result.Success) return NotFound(result);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] CreateUserDto dto)
        {
            var result = _userService.AddUser(dto);
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("{id}/tasks")]
        public IActionResult GetUserWithTasks(int id)
        {
            var result = _userService.GetUserWithTasks(id);
            if (!result.Success) return NotFound(result);
            return Ok(result);
        }
    }
}