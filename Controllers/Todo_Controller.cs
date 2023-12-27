using Microsoft.AspNetCore.Mvc;
using Models_Todo;
using System.Linq;

namespace Todo_wXUnit_1001.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _dbContext;

        public TodoController(TodoContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetTodos(bool showDeleted = false)
        {
            var todosQuery = showDeleted
                ? _dbContext.Todos
                : _dbContext.Todos.Where(t => !t.IsDeleted);

            var todos = todosQuery.ToList();

            return new JsonResult(new { Todos = todos });
        }

        [HttpPost("UpdateIsDone")]
        public IActionResult UpdateIsDone([FromBody] UpdateTodoRequest request)
        {
            var todo = _dbContext.Todos.FirstOrDefault(t => t.Id == request.TodoId);
            if (todo != null)
            {
                todo.IsDone = request.IsDone;
                todo.Title = request.Title;
                todo.Content = request.Content;

                // Add logic for marking as deleted
                if (request.IsDeleted)
                {
                    todo.IsDeleted = true;
                    // Optionally, move the deleted todo to a list of deleted todos
                    // _dbContext.DeletedTodos.Add(todo);
                }

                _dbContext.SaveChanges();
                return Ok(new { Message = "Todo updated successfully" });
            }

            return NotFound(new { Message = "Todo not found" });
        }

        [HttpPost("DeleteTodo/{todoId}")]
        public IActionResult DeleteTodo(int todoId)
        {
            var todo = _dbContext.Todos.FirstOrDefault(t => t.Id == todoId);
            if (todo != null)
            {
                _dbContext.Todos.Remove(todo);
                _dbContext.SaveChanges();
                return Ok(new { Message = "Todo deleted successfully" });
            }

            return NotFound(new { Message = "Todo not found" });
        }
    }



    public class UpdateTodoRequest
    {
        public int TodoId { get; set; }
        public bool IsDone { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsDeleted { get; set; }
    }
}
