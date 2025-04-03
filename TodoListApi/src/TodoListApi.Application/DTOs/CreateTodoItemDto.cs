using System;
using TodoListApi.Domain.Entities;

namespace TodoListApi.Application.DTOs
{
    public class CreateTodoItemDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public Priority Priority { get; set; }
    }
}