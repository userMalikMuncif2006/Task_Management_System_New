using Task_Management_System_New.Repositories;
using Task_Management_System_New.Services; // For Services
using Task_Management_System_New.Interfaces; // If you have separate interface files


namespace Task_Management_System_New
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1. DATABASE INITIALIZATION
            // This stays here to ensure DB is ready before the app starts
            var initializer = new Task_Management_System.Repositories.DbInitializer();
            initializer.InitializeDatabase();

            // 2. ADD SERVICES TO THE CONTAINER

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // --- MISSING PART: DEPENDENCY INJECTION REGISTRATION ---
            // This tells C# which class to use when an Interface is requested

            // Register Repositories


            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ITaskRepository, TaskRepository>();

            // Register Services


            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ITaskService, TaskService>();


            // -------------------------------------------------------

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            // 3. CONFIGURE THE HTTP REQUEST PIPELINE
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Important: CORS must be before MapControllers
            app.UseCors("AllowFrontend");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}