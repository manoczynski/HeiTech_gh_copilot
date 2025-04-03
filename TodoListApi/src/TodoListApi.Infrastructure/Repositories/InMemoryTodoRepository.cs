using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;
using TodoListApi.Domain.Entities;
using TodoListApi.Application.Interfaces;

namespace TodoListApi.Infrastructure.Repositories
{
    public class InMemoryTodoRepository : IRepository<TodoItem>
    {
        private readonly List<TodoItem> _todoItems = new List<TodoItem>();
        private readonly string _filePath;
        private readonly object _lock = new object();

        public InMemoryTodoRepository(string filePath = null)
        {
            _filePath = filePath ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "todos.txt");
            LoadFromFile();
        }

        private void LoadFromFile()
        {
            lock (_lock)
            {
                if (File.Exists(_filePath))
                {
                    try
                    {
                        var json = File.ReadAllText(_filePath);
                        var items = JsonSerializer.Deserialize<List<TodoItem>>(json);
                        if (items != null)
                        {
                            _todoItems.Clear();
                            _todoItems.AddRange(items);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error loading from file: {ex.Message}");
                    }
                }
            }
        }

        private void SaveToFile()
        {
            lock (_lock)
            {
                try
                {
                    var directory = Path.GetDirectoryName(_filePath);
                    if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    var json = JsonSerializer.Serialize(_todoItems, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(_filePath, json);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving to file: {ex.Message}");
                }
            }
        }

        public TodoItem Add(TodoItem item)
        {
            lock (_lock)
            {
                item.Id = Guid.NewGuid();
                _todoItems.Add(item);
                SaveToFile();
                return item;
            }
        }

        public TodoItem Get(Guid id)
        {
            lock (_lock)
            {
                return _todoItems.FirstOrDefault(x => x.Id == id);
            }
        }

        public IEnumerable<TodoItem> GetAll()
        {
            lock (_lock)
            {
                return _todoItems.ToList();
            }
        }

        public TodoItem Update(TodoItem item)
        {
            lock (_lock)
            {
                var index = _todoItems.FindIndex(x => x.Id == item.Id);
                if (index != -1)
                {
                    _todoItems[index] = item;
                    SaveToFile();
                }
                return item;
            }
        }

        public void Delete(Guid id)
        {
            lock (_lock)
            {
                var item = _todoItems.FirstOrDefault(x => x.Id == id);
                if (item != null)
                {
                    _todoItems.Remove(item);
                    SaveToFile();
                }
            }
        }
    }
}