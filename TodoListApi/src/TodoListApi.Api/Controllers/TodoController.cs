using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoListApi.Application.DTOs;
using TodoListApi.Application.Interfaces;
using TodoListApi.Domain.Entities;

namespace TodoListApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }


        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetAll()
        {
            var tasks = await _todoService.GetAllTasks();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDto>> GetById(Guid id)
        {
            var task = await _todoService.GetTask(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDto>> Create(CreateTodoItemDto createDto)
        {
            var createdTask = await _todoService.CreateTask(createDto);
            return CreatedAtAction(nameof(GetById), new { id = createdTask.Id }, createdTask);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TodoItemDto>> Update(Guid id, UpdateTodoItemDto updateDto)
        {
            if (id != updateDto.Id)
            {
                return BadRequest();
            }

            var updatedTask = await _todoService.UpdateTask(updateDto);
            if (updatedTask == null)
            {
                return NotFound();
            }
            return Ok(updatedTask);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var task = await _todoService.GetTask(id);
            if (task == null)
            {
                return NotFound();
            }

            await _todoService.DeleteTask(id);
            return NoContent();
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<TodoItemDto>>> Filter(bool? isCompleted = null, int? priority = null, bool? sortByDueDate = null)
        {
            IEnumerable<TodoItemDto> tasks = null;
            
            if (isCompleted.HasValue)
            {
                tasks = await _todoService.FilterTasksByStatus(isCompleted.Value);
            }
            else if (priority.HasValue)
            {
                tasks = await _todoService.FilterTasksByPriority((Priority)priority.Value);
            }
            else if (sortByDueDate.HasValue && sortByDueDate.Value)
            {
                tasks = await _todoService.SortTasksByDueDate();
            }
            else
            {
                tasks = await _todoService.GetAllTasks();
            }
            
            // Ensure we never return null
            return Ok(tasks ?? Enumerable.Empty<TodoItemDto>());
        }

        [HttpGet("prime")]
        public ActionResult<int> GetPrimes()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var prime = GetPrimeNumber();
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            
            // Log execution time to console output
            Console.WriteLine($"Time to calculate 30000th prime number: {elapsedMs}ms");
            
            return Ok(new { Prime = prime, ExecutionTimeMs = elapsedMs });
        }

        private int GetPrimeNumber()
        {
            int count = 0;
            int number = 2;
            while (count < 30000)
            {
                if (IsPrime(number))
                {
                    count++;
                }
                number++;
            }
            return number - 1;
        }

        private bool IsPrime(int number)
        {
            if (number < 2)
            {
                return false;
            }
            for (int i = 2; i <= number / 2; i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}