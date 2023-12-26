﻿using Models_Todo;
using System.Collections.Generic;
using System.Linq;

namespace task0102_Todo.Repo
{
    public class TodoRepo
    {
        private readonly TodoContext _context;

        public TodoRepo(TodoContext context)
        {
            _context = context;
        }

        public void AddTodo(Todo todo)
        {
            _context.Todos.Add(todo);
            _context.SaveChanges();
        }

        public IEnumerable<Todo> GetTodos()
        {
            return _context.Todos.ToList();
        }

        // Add other repository methods for CRUD operations
    }
}