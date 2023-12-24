using Models_Todo;

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

        // Add other repository methods for CRUD operations
    }
}



