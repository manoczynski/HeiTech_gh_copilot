using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoListApi.Domain.Entities;
using TodoListApi.Application.Interfaces;
using TodoListApi.Application.DTOs;

namespace TodoListApi.Application.Services
{
    public class TodoService : ITodoService
    {
        private readonly IRepository<TodoItem> _todoRepository;

        public TodoService(IRepository<TodoItem> todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public Task<TodoItemDto> CreateTask(CreateTodoItemDto createTodoItemDto)
        {
            var todoItem = new TodoItem
            {
                Id = Guid.NewGuid(),
                Title = createTodoItemDto.Title,
                Description = createTodoItemDto.Description,
                DueDate = createTodoItemDto.DueDate,
                Priority = createTodoItemDto.Priority,
                IsCompleted = false
            };

            var result = _todoRepository.Add(todoItem);
            return Task.FromResult(MapToDto(result));
        }

        public Task<TodoItemDto> GetTask(Guid id)
        {
            var todoItem = _todoRepository.Get(id);
            if (todoItem == null)
            {
                return Task.FromResult<TodoItemDto>(null);
            }
            return Task.FromResult(MapToDto(todoItem));
        }

        public Task<IEnumerable<TodoItemDto>> GetAllTasks()
        {
            var todoItems = _todoRepository.GetAll();
            return Task.FromResult(todoItems.Select(item => MapToDto(item)));
        }

        public Task<TodoItemDto> UpdateTask(UpdateTodoItemDto updateTodoItemDto)
        {
            var todoItem = _todoRepository.Get(updateTodoItemDto.Id);
            if (todoItem == null)
                return Task.FromResult<TodoItemDto>(null);

            todoItem.Title = updateTodoItemDto.Title;
            todoItem.Description = updateTodoItemDto.Description;
            todoItem.DueDate = updateTodoItemDto.DueDate;
            todoItem.Priority = updateTodoItemDto.Priority;
            todoItem.IsCompleted = updateTodoItemDto.IsCompleted;

            var result = _todoRepository.Update(todoItem);
            return Task.FromResult(MapToDto(result));
        }

        public Task DeleteTask(Guid id)
        {
            _todoRepository.Delete(id);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<TodoItemDto>> FilterTasksByStatus(bool isCompleted)
        {
            var filteredTasks = _todoRepository.GetAll()
                .Where(t => t.IsCompleted == isCompleted)
                .Select(item => MapToDto(item));
            
            return Task.FromResult(filteredTasks);
        }

        public Task<IEnumerable<TodoItemDto>> FilterTasksByPriority(Priority priority)
        {
            var filteredTasks = _todoRepository.GetAll()
                .Where(t => t.Priority == priority)
                .Select(item => MapToDto(item));
            
            return Task.FromResult(filteredTasks);
        }

        public Task<IEnumerable<TodoItemDto>> SortTasksByDueDate()
        {
            var sortedTasks = _todoRepository.GetAll()
                .OrderBy(t => t.DueDate)
                .Select(item => MapToDto(item));
            
            return Task.FromResult(sortedTasks);
        }

        private TodoItemDto MapToDto(TodoItem todoItem)
        {
            if (todoItem == null)
            {
                return null;
            }
            
            return new TodoItemDto
            {
                Id = todoItem.Id,
                Title = todoItem.Title,
                Description = todoItem.Description,
                DueDate = todoItem.DueDate,
                Priority = todoItem.Priority,
                IsCompleted = todoItem.IsCompleted
            };
        }
    }
}