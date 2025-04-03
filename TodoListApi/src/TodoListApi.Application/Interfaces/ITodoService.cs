using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoListApi.Application.DTOs;
using TodoListApi.Domain.Entities;

namespace TodoListApi.Application.Interfaces
{
    public interface ITodoService
    {
        Task<TodoItemDto> CreateTask(CreateTodoItemDto createTodoItemDto);
        Task<TodoItemDto> GetTask(Guid id);
        Task<IEnumerable<TodoItemDto>> GetAllTasks();
        Task<TodoItemDto> UpdateTask(UpdateTodoItemDto updateTodoItemDto);
        Task DeleteTask(Guid id);
        Task<IEnumerable<TodoItemDto>> FilterTasksByStatus(bool isCompleted);
        Task<IEnumerable<TodoItemDto>> FilterTasksByPriority(Priority priority);
        Task<IEnumerable<TodoItemDto>> SortTasksByDueDate();
    }
}