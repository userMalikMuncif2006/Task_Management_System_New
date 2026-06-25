using Microsoft.Data.SqlClient;

namespace Task_Management_System.Repositories
{
    public class DbInitializer
    {
        // Change this string if your SQL instance name is different (e.g., .\SQLEXPRESS)
        private const string ServerName = @"(localdb)\MSSQLLocalDB";
        private const string DatabaseName = "TaskManagementDb";

        private readonly string masterConnectionString =
            $@"Server={ServerName};Database=master;Trusted_Connection=True;TrustServerCertificate=True";

        private readonly string dbConnectionString =
            $@"Server={ServerName};Database={DatabaseName};Trusted_Connection=True;TrustServerCertificate=True";

        public void InitializeDatabase()
        {
            CreateDatabaseIfNotExists();
            CreateTablesIfNotExists();
            SeedData();
        }

        private void CreateDatabaseIfNotExists()
        {
            using SqlConnection conn = new SqlConnection(masterConnectionString);
            conn.Open();

            // Check if database exists in system catalog
            string query = $@"
                IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = '{DatabaseName}')
                BEGIN
                    CREATE DATABASE [{DatabaseName}]
                END";

            using SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteNonQuery();
        }

        private void CreateTablesIfNotExists()
        {
            using SqlConnection conn = new SqlConnection(dbConnectionString);
            conn.Open();

            // Note: Users table must be created before Tasks table because of Foreign Key
            string query = @"
                IF OBJECT_ID('Users', 'U') IS NULL
                BEGIN
                    CREATE TABLE Users (
                        UserId INT IDENTITY(1,1) PRIMARY KEY,
                        UserName NVARCHAR(100) NOT NULL,
                        Email NVARCHAR(100) NOT NULL UNIQUE
                    );
                END

                IF OBJECT_ID('Tasks', 'U') IS NULL
                BEGIN
                    CREATE TABLE Tasks (
                        TaskId INT IDENTITY(1,1) PRIMARY KEY,
                        Title NVARCHAR(200) NOT NULL,
                        Description NVARCHAR(500) NULL,
                        Status NVARCHAR(20) NOT NULL DEFAULT 'Todo',
                        CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
                        UserId INT NOT NULL,
                        CONSTRAINT FK_Tasks_Users FOREIGN KEY (UserId)
                            REFERENCES Users(UserId),
                        CONSTRAINT CK_Tasks_Status CHECK (Status IN ('Todo', 'In Progress', 'Done'))
                    );
                END";

            using SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteNonQuery();
        }

        private void SeedData()
        {
            using SqlConnection conn = new SqlConnection(dbConnectionString);
            conn.Open();

            // Using the exact sample data from your Task Management System guide
            string query = @"
                IF NOT EXISTS (SELECT 1 FROM Users)
                BEGIN
                    -- Insert Users
                    INSERT INTO Users (UserName, Email)
                    VALUES
                    ('Abdul Baasith', 'abdul@example.com'),
                    ('Student One', 'student1@example.com'),
                    ('Student Two', 'student2@example.com');

                    -- Insert Tasks assigned to those users
                    INSERT INTO Tasks (Title, Description, Status, UserId)
                    VALUES
                    ('Create database', 'Create Users and Tasks tables', 'Done', 1),
                    ('Build user API', 'Create user endpoints', 'In Progress', 1),
                    ('Create frontend', 'Build HTML pages', 'Todo', 2),
                    ('Test API', 'Use Swagger to test endpoints', 'Todo', 3);
                END";

            using SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteNonQuery();
        }
    }
}