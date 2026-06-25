using Microsoft.Data.SqlClient;
using Task_Management_System_New.DTOs;
using Task_Management_System_New.Interfaces;
using Task_Management_System_New.Models;

namespace Task_Management_System_New.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public bool AddUser(CreateUserDto user)
        {
            using var conn = new SqlConnection(_connectionString);
            string sql = "INSERT INTO Users (UserName, Email) VALUES (@Name, @Email)";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Name", user.UserName);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public User? GetUserByEmail(string email)
        {
            using var conn = new SqlConnection(_connectionString);
            string sql = "SELECT * FROM Users WHERE Email = @Email";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Email", email);
            conn.Open();
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new User { UserId = (int)reader["UserId"], UserName = reader["UserName"].ToString()!, Email = reader["Email"].ToString()! };
            }
            return null;
        }

        public IEnumerable<UserWithTasksDto> GetAllUsers()
        {
            var users = new List<UserWithTasksDto>();
            using var conn = new SqlConnection(_connectionString);
            string sql = "SELECT UserId, UserName, Email FROM Users";
            using var cmd = new SqlCommand(sql, conn);
            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                users.Add(new UserWithTasksDto { UserId = (int)reader["UserId"], UserName = reader["UserName"].ToString()!, Email = reader["Email"].ToString()! });
            }
            return users;
        }

        public UserWithTasksDto? GetUserById(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            string sql = "SELECT UserId, UserName, Email FROM Users WHERE UserId = @Id";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            conn.Open();
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new UserWithTasksDto { UserId = (int)reader["UserId"], UserName = reader["UserName"].ToString()!, Email = reader["Email"].ToString()! };
            }
            return null;
        }

        public UserWithTasksDto? GetUserWithTasks(int id)
        {
            UserWithTasksDto? user = null;
            using var conn = new SqlConnection(_connectionString);
            // LEFT JOIN: Returns the user even if they have 0 tasks
            string sql = @"SELECT u.UserId, u.UserName, u.Email, t.TaskId, t.Title, t.Status 
                           FROM Users u 
                           LEFT JOIN Tasks t ON u.UserId = t.UserId 
                           WHERE u.UserId = @Id";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (user == null)
                {
                    user = new UserWithTasksDto
                    {
                        UserId = (int)reader["UserId"],
                        UserName = reader["UserName"].ToString()!,
                        Email = reader["Email"].ToString()!,
                        Tasks = new List<TaskItemResponseDto>()
                    };
                }
                if (reader["TaskId"] != DBNull.Value)
                {
                    user.Tasks.Add(new TaskItemResponseDto
                    {
                        TaskId = (int)reader["TaskId"],
                        Title = reader["Title"].ToString()!,
                        Status = reader["Status"].ToString()!
                    });
                }
            }
            return user;
        }
    }
}