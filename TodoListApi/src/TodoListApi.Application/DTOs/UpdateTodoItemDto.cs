using System;
using TodoListApi.Domain.Entities;

namespace TodoListApi.Application.DTOs
{
    public class UpdateTodoItemDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public Priority Priority { get; set; }
        public bool IsCompleted { get; set; }
    }
}