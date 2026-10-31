# Task Management System — ASP.NET Core REST API

A backend learning project for managing users and assigned tasks using C#, ASP.NET Core 8, ADO.NET and SQL Server LocalDB.

> Status: Learning project. The source was reviewed for documentation; build and runtime behavior have not been independently tested.

## Technology Stack
- C# / .NET 8 / ASP.NET Core Web API
- SQL Server LocalDB and Microsoft.Data.SqlClient
- Swagger / OpenAPI in Development
- Layered organization: Controller → Service → Repository → Database

## Features
- Create and retrieve users
- List tasks assigned to a user
- Create, list, search, update and delete tasks
- Update task status
- Initialize database tables and sample data on startup
- Request and response DTOs

## API Endpoints

| Method | Route | Action |
|---|---|---|
| GET | /api/User | List users |
| GET | /api/User/{id} | Get user |
| POST | /api/User | Create user |
| GET | /api/User/{id}/tasks | Get assigned tasks |
| GET | /api/Task | List tasks |
| GET | /api/Task/{id} | Get task |
| GET | /api/Task/search?name=... | Search tasks |
| POST | /api/Task | Create task |
| PUT | /api/Task/{id} | Update task |
| PUT | /api/Task/{id}/status | Change status |
| DELETE | /api/Task/{id} | Delete task |

Routes are documented from source code; runtime responses are not verified.

## Architecture

Controller → Service → Repository → SQL Server

The repository contains Controllers, DTOs, Interfaces, Models, Repositories and Services.

## Getting Started (Windows)

1. Install the .NET 8 SDK and SQL Server LocalDB.
2. Clone the repository.
3. Open Task_Management_System_New/Task_Management_System_New.sln.
4. Check LocalDB configuration in appsettings.json and Repositories/dbInitializer.cs.
5. Open a terminal in the folder containing the .csproj file.

Run:

~~~bash
dotnet restore
dotnet run
~~~

Use the local URL printed in the terminal. In Development, try the /swagger endpoint.

The app attempts database initialization on startup. LocalDB is Windows-specific.

## Task Status

Todo | In Progress | Done

## Security and Development Notes

- The current CORS policy permits any origin; restrict before public hosting.
- Authentication is not configured in the inspected startup code.
- Validate inputs and use parameterized SQL.
- Add automated tests and environment-specific configuration before production use.

## Author

Malik Munshif
GitHub: https://github.com/userMalikMuncif2006
