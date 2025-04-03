# HeiTech GitHub Copilot Demonstration

## Overview

This repository contains a ToDo List API project that was created specifically for the HeiTech Meetup. The primary purpose of this repository is to demonstrate the functionality and possibilities of GitHub Copilot as a developer productivity tool.

## About GitHub Copilot

GitHub Copilot is an AI pair programmer that helps you write code faster and with less effort. It draws context from comments and code to suggest individual lines and entire functions instantly. GitHub Copilot is powered by OpenAI's Codex, a descendant of GPT-3.

## ToDo List API Project

The ToDo List API is a RESTful web service built with .NET 9.x that allows users to manage their tasks. The project follows Clean Architecture principles, separating concerns into different layers:

- **Domain Layer**: Core business logic and entities
- **Application Layer**: Application services and DTOs
- **Infrastructure Layer**: Data persistence and external services 
- **API Layer**: REST endpoints and controllers

## Key Features Demonstrated with GitHub Copilot

In this demonstration, we showcase how GitHub Copilot helps with:

1. **Code Generation**: Writing boilerplate code and repetitive patterns
2. **Documentation**: Creating comprehensive comments and documentation
3. **Testing**: Generating test cases and test methods
4. **Refactoring**: Suggesting improvements to existing code
5. **Problem Solving**: Providing solutions to coding challenges
6. **API Design**: Helping create consistent REST endpoints

## Technologies Used

- .NET 9.x
- ASP.NET Core Web API
- Clean Architecture
- GitHub Actions (CI/CD pipeline)
- Swagger/OpenAPI for API documentation

## Getting Started

### Prerequisites

- .NET 9 SDK installed on your machine

### Setup Instructions

1. Clone the repository:
   ```
   git clone https://github.com/your-username/HeiTech_Demo_1.git
   ```

2. Navigate to the project directory:
   ```
   cd HeiTech_Demo_1/TodoListApi
   ```

3. Restore dependencies:
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

6. Access the API via:
   ```
   https://localhost:5001/swagger
   ```

## API Endpoints

The API provides the following endpoints:

- `GET /api/todo` - Get all todo items
- `GET /api/todo/{id}` - Get a specific todo item by ID
- `POST /api/todo` - Create a new todo item
- `PUT /api/todo/{id}` - Update an existing todo item
- `DELETE /api/todo/{id}` - Delete a todo item
- `GET /api/todo/filter` - Filter todo items by status, priority, or due date

## CI/CD Pipeline

This project uses GitHub Actions for CI/CD pipeline, demonstrating how GitHub Copilot can help with:

- Setting up workflow files
- Writing build and test scripts
- Configuring deployment processes
- Creating release procedures

## Using GitHub Copilot in This Project

Throughout the development of this API, GitHub Copilot was used to:

1. Generate controller methods
2. Write service implementations
3. Create entity classes and DTOs
4. Add data validation logic
5. Implement filtering and sorting algorithms
6. Generate unit and integration tests
7. Document the codebase

## Conclusion

This repository showcases the power and versatility of GitHub Copilot as an AI-powered coding assistant. By exploring the codebase, you can see examples of how GitHub Copilot can enhance developer productivity and help maintain coding standards across a project.

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Acknowledgments

- HeiTech Meetup organizers and participants
- GitHub Copilot team for creating an amazing developer tool
