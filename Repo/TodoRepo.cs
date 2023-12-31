﻿using Microsoft.EntityFrameworkCore;
using Models_Todo;
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

        public void RemoveTodo(int todoId)
        {
            var todoToRemove = _context.Todos.Find(todoId);

            if (todoToRemove != null)
            {
                _context.Todos.Remove(todoToRemove);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Todo> GetTodos()
        {
            return _context.Todos.ToList();
        }

        // maybe other repository methods for CRUD?
    }
}
