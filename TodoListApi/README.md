# ToDo List API

## Overview

This project is a simple CRUD (Create, Read, Update, Delete) ToDo list application built with .NET 9. It provides a RESTful API for managing tasks in a todo list, with data stored in-memory, eliminating the need for an external database.

## Project Structure

The project is organized into several layers following the Clean Architecture pattern:

- **Domain Layer**: Contains the core business logic and entities.
  - `TodoItem.cs`: Represents a task in the ToDo list.
  - `Priority.cs`: Enum for categorizing task priority levels.
  - `TodoListApi.Domain.csproj`: Project file for the Domain layer.

- **Application Layer**: Contains the application logic, DTOs, and service interfaces.
  - `DTOs`: Contains data transfer objects for task management.
    - `TodoItemDto.cs`: DTO for transferring `TodoItem` data.
    - `CreateTodoItemDto.cs`: DTO for creating new tasks.
    - `UpdateTodoItemDto.cs`: DTO for updating existing tasks.
  - `Interfaces`: Contains service interfaces for task management.
    - `IRepository.cs`: Interface for data access operations.
    - `ITodoService.cs`: Interface for managing tasks.
  - `Services`: Contains implementations of the service interfaces.
    - `TodoService.cs`: Implements task management logic.
  - `TodoListApi.Application.csproj`: Project file for the Application layer.

- **Infrastructure Layer**: Contains implementations for data storage and access.
  - `Repositories`: Contains repository implementations.
    - `InMemoryTodoRepository.cs`: In-memory data storage for `TodoItem` objects.
  - `TodoListApi.Infrastructure.csproj`: Project file for the Infrastructure layer.

- **API Layer**: Contains the web API components.
  - `Controllers`: Contains API controllers for handling HTTP requests.
    - `TodoController.cs`: Handles requests related to tasks.
  - `Program.cs`: Entry point of the API application.
  - `appsettings.json`: Configuration settings for the application.
  - `appsettings.Development.json`: Development-specific configuration settings.
  - `TodoListApi.Api.csproj`: Project file for the API layer.

## Getting Started

### Prerequisites

- .NET 9 SDK installed on your machine.

### Setup Instructions

1. Clone the repository or download the project files.
2. Open the solution file `TodoListApi.sln` in your preferred IDE.
3. Restore the project dependencies by running:
   ```
   dotnet restore
   ```
4. Build the project:
   ```
   dotnet build
   ```
5. Run the application:
   ```
   dotnet run --project src/TodoListApi.Api/TodoListApi.Api.csproj
   ```

### Usage

Once the application is running, you can interact with the ToDo List API using tools like Postman or curl. The API supports the following endpoints:

- `POST /api/todo`: Create a new task.
- `GET /api/todo`: Retrieve all tasks.
- `GET /api/todo/{id}`: Retrieve a specific task by ID.
- `PUT /api/todo/{id}`: Update an existing task.
- `PATCH /api/todo/{id}/complete`: Mark a task as completed.
- `DELETE /api/todo/{id}`: Delete a task.
- Additional filtering and sorting options are available.

## Contributing

Contributions are welcome! Please feel free to submit a pull request or open an issue for any suggestions or improvements.

## License

This project is licensed under the MIT License. See the LICENSE file for more details.