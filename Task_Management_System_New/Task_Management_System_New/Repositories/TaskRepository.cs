using Microsoft.Data.SqlClient;
using Task_Management_System_New.DTOs;
using Task_Management_System_New.Interfaces;
using Task_Management_System_New.Models;

namespace Task_Management_System_New.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly string _connectionString;

        public TaskRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public IEnumerable<TaskItemResponseDto> GetAllTasks()
        {
            var tasks = new List<TaskItemResponseDto>();
            using var conn = new SqlConnection(_connectionString);
            string sql = @"SELECT t.*, u.UserName FROM Tasks t 
                           INNER JOIN Users u ON t.UserId = u.UserId";
            using var cmd = new SqlCommand(sql, conn);
            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                tasks.Add(MapToDto(reader));
            }
            return tasks;
        }

        public TaskItemResponseDto AddTask(TaskItem task)
        {
            using var conn = new SqlConnection(_connectionString);
            string sql = @"INSERT INTO Tasks (Title, Description, Status, UserId) 
                           VALUES (@Title, @Desc, @Status, @UserId);
                           SELECT SCOPE_IDENTITY();";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Title", task.Title);
            cmd.Parameters.AddWithValue("@Desc", task.Description ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Status", task.Status);
            cmd.Parameters.AddWithValue("@UserId", task.UserId);
            conn.Open();
            int newId = Convert.ToInt32(cmd.ExecuteScalar());
            return GetTaskById(newId)!;
        }

        public TaskItemResponseDto? GetTaskById(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            string sql = @"SELECT t.*, u.UserName FROM Tasks t 
                           INNER JOIN Users u ON t.UserId = u.UserId 
                           WHERE t.TaskId = @Id";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            conn.Open();
            using var reader = cmd.ExecuteReader();
            return reader.Read() ? MapToDto(reader) : null;
        }

        public bool UpdateTask(int id, UpdateTaskItemDto task)
        {
            using var conn = new SqlConnection(_connectionString);
            string sql = @"UPDATE Tasks SET Title=@Title, Description=@Desc, 
                           Status=@Status, UserId=@UserId WHERE TaskId=@Id";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@Title", task.Title);
            cmd.Parameters.AddWithValue("@Desc", task.Description ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Status", task.Status);
            cmd.Parameters.AddWithValue("@UserId", task.UserId);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool UpdateStatus(int id, string status)
        {
            using var conn = new SqlConnection(_connectionString);
            string sql = "UPDATE Tasks SET Status = @Status WHERE TaskId = @Id";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@Status", status);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool DeleteTask(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            string sql = "DELETE FROM Tasks WHERE TaskId = @Id";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        public IEnumerable<TaskItemResponseDto> SearchTasks(string name)
        {
            var tasks = new List<TaskItemResponseDto>();
            using var conn = new SqlConnection(_connectionString);
            string sql = @"SELECT t.*, u.UserName FROM Tasks t 
                           INNER JOIN Users u ON t.UserId = u.UserId 
                           WHERE t.Title LIKE @Name";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Name", "%" + name + "%");
            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read()) { tasks.Add(MapToDto(reader)); }
            return tasks;
        }

        // Helper method to reduce code duplication
        private TaskItemResponseDto MapToDto(SqlDataReader reader)
        {
            return new TaskItemResponseDto
            {
                TaskId = (int)reader["TaskId"],
                Title = reader["Title"].ToString()!,
                Description = reader["Description"]?.ToString(),
                Status = reader["Status"].ToString()!,
                CreatedDate = (DateTime)reader["CreatedDate"],
                UserId = (int)reader["UserId"],
                UserName = reader["UserName"].ToString()!
            };
        }
    }
}