using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoListApi.Api.Controllers;
using TodoListApi.Application.DTOs;
using TodoListApi.Application.Interfaces;
using TodoListApi.Domain.Entities;
using Xunit;

namespace TodoListApi.Api.Tests.Controllers
{
    public class TodoControllerTests
    {
        private readonly Mock<ITodoService> _mockTodoService;
        private readonly TodoController _controller;

        public TodoControllerTests()
        {
            _mockTodoService = new Mock<ITodoService>();
            _controller = new TodoController(_mockTodoService.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResultWithAllTasks()
        {
            // Arrange
            var tasks = new List<TodoItemDto>
            {
                new TodoItemDto { Id = Guid.NewGuid(), Title = "Task 1", IsCompleted = false },
                new TodoItemDto { Id = Guid.NewGuid(), Title = "Task 2", IsCompleted = true }
            };
            _mockTodoService.Setup(service => service.GetAllTasks()).ReturnsAsync(tasks);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<TodoItemDto>>(okResult.Value);
            Assert.Equal(2, returnValue.Count());
        }

        [Fact]
        public async Task GetById_WithValidId_ReturnsOkResultWithTask()
        {
            // Arrange
            var id = Guid.NewGuid();
            var task = new TodoItemDto { Id = id, Title = "Task 1", IsCompleted = false };
            _mockTodoService.Setup(service => service.GetTask(id)).ReturnsAsync(task);

            // Act
            var result = await _controller.GetById(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<TodoItemDto>(okResult.Value);
            Assert.Equal(id, returnValue.Id);
        }

        [Fact]
        public async Task GetById_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockTodoService.Setup(service => service.GetTask(id)).ReturnsAsync((TodoItemDto)null);

            // Act
            var result = await _controller.GetById(id);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Create_WithValidData_ReturnsCreatedAtAction()
        {
            // Arrange
            var createDto = new CreateTodoItemDto { Title = "New Task", Description = "Description" };
            var createdTask = new TodoItemDto { Id = Guid.NewGuid(), Title = "New Task", Description = "Description" };
            _mockTodoService.Setup(service => service.CreateTask(createDto)).ReturnsAsync(createdTask);

            // Act
            var result = await _controller.Create(createDto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal("GetById", createdAtActionResult.ActionName);
            var returnValue = Assert.IsType<TodoItemDto>(createdAtActionResult.Value);
            Assert.Equal(createdTask.Id, returnValue.Id);
        }

        [Fact]
        public async Task Update_WithValidIdAndData_ReturnsOkResult()
        {
            // Arrange
            var id = Guid.NewGuid();
            var updateDto = new UpdateTodoItemDto { Id = id, Title = "Updated Task" };
            var updatedTask = new TodoItemDto { Id = id, Title = "Updated Task" };
            _mockTodoService.Setup(service => service.UpdateTask(updateDto)).ReturnsAsync(updatedTask);

            // Act
            var result = await _controller.Update(id, updateDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<TodoItemDto>(okResult.Value);
            Assert.Equal(id, returnValue.Id);
        }

        [Fact]
        public async Task Update_WithMismatchedIds_ReturnsBadRequest()
        {
            // Arrange
            var id = Guid.NewGuid();
            var updateDto = new UpdateTodoItemDto { Id = Guid.NewGuid(), Title = "Updated Task" };

            // Act
            var result = await _controller.Update(id, updateDto);

            // Assert
            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public async Task Update_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            var updateDto = new UpdateTodoItemDto { Id = id, Title = "Updated Task" };
            _mockTodoService.Setup(service => service.UpdateTask(updateDto)).ReturnsAsync((TodoItemDto)null);

            // Act
            var result = await _controller.Update(id, updateDto);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Delete_WithValidId_ReturnsNoContent()
        {
            // Arrange
            var id = Guid.NewGuid();
            var task = new TodoItemDto { Id = id, Title = "Task to delete" };
            _mockTodoService.Setup(service => service.GetTask(id)).ReturnsAsync(task);
            _mockTodoService.Setup(service => service.DeleteTask(id)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(id);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            _mockTodoService.Setup(service => service.GetTask(id)).ReturnsAsync((TodoItemDto)null);

            // Act
            var result = await _controller.Delete(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Filter_WithIsCompletedParam_CallsFilterTasksByStatus()
        {
            // Arrange
            bool isCompleted = true;
            var tasks = new List<TodoItemDto>
            {
                new TodoItemDto { Id = Guid.NewGuid(), Title = "Task 1", IsCompleted = true }
            };
            _mockTodoService.Setup(service => service.FilterTasksByStatus(isCompleted)).ReturnsAsync(tasks);

            // Act
            var result = await _controller.Filter(isCompleted: isCompleted);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<TodoItemDto>>(okResult.Value);
            Assert.Single(returnValue);
            _mockTodoService.Verify(service => service.FilterTasksByStatus(isCompleted), Times.Once);
        }

        [Fact]
        public async Task Filter_WithPriorityParam_CallsFilterTasksByPriority()
        {
            // Arrange
            int priority = (int)Priority.High;
            var tasks = new List<TodoItemDto>
            {
                new TodoItemDto { Id = Guid.NewGuid(), Title = "Task 1", Priority = Priority.High }
            };
            _mockTodoService.Setup(service => service.FilterTasksByPriority(Priority.High)).ReturnsAsync(tasks);

            // Act
            var result = await _controller.Filter(priority: priority);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<TodoItemDto>>(okResult.Value);
            Assert.Single(returnValue);
            _mockTodoService.Verify(service => service.FilterTasksByPriority(Priority.High), Times.Once);
        }

        [Fact]
        public async Task Filter_WithSortByDueDate_CallsSortTasksByDueDate()
        {
            // Arrange
            var tasks = new List<TodoItemDto>
            {
                new TodoItemDto { Id = Guid.NewGuid(), Title = "Task 1", DueDate = DateTime.Now.AddDays(1) },
                new TodoItemDto { Id = Guid.NewGuid(), Title = "Task 2", DueDate = DateTime.Now.AddDays(2) }
            };
            _mockTodoService.Setup(service => service.SortTasksByDueDate()).ReturnsAsync(tasks);

            // Act
            var result = await _controller.Filter(sortByDueDate: true);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<TodoItemDto>>(okResult.Value);
            Assert.Equal(2, returnValue.Count());
            _mockTodoService.Verify(service => service.SortTasksByDueDate(), Times.Once);
        }

        [Fact]
        public async Task Filter_WithNoParams_CallsGetAllTasks()
        {
            // Arrange
            var tasks = new List<TodoItemDto>
            {
                new TodoItemDto { Id = Guid.NewGuid(), Title = "Task 1" },
                new TodoItemDto { Id = Guid.NewGuid(), Title = "Task 2" }
            };
            _mockTodoService.Setup(service => service.GetAllTasks()).ReturnsAsync(tasks);

            // Act
            var result = await _controller.Filter();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<TodoItemDto>>(okResult.Value);
            Assert.Equal(2, returnValue.Count());
            _mockTodoService.Verify(service => service.GetAllTasks(), Times.Once);
        }

        [Fact]
        public void GetPrimes_ReturnsOkResultWithPrimeNumber()
        {
            // Act
            var result = _controller.GetPrimes();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            dynamic returnValue = okResult.Value;
            Assert.NotNull(returnValue.Prime);
            Assert.True(returnValue.ExecutionTimeMs > 0);
        }
    }
}
