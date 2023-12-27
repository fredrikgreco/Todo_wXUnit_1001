using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Models_Todo;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using Todo_wXUnit_1001.Controllers;

namespace Todo_wXUnit_1001.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly TodoContext _dbContext;

        public List<Todo> Todos { get; set; }
        [BindProperty] public Todo Todo { get; set; }
        //public bool ShowDeletedTodos { get; set; } = false;

        public IndexModel(TodoContext dbContext, ILogger<IndexModel> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task OnGet()
        {
            Console.WriteLine("OnGet method called");
            Todo = new Todo();
            //ShowDeletedTodos = false;
            UpdateTodos(); 
            Console.WriteLine($"Number of Todos: {Todos?.Count}");
        }


        private void UpdateTodos()
        {
            Todos = _dbContext.Todos.ToList();
        }


        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            _dbContext.Todos.Add(Todo);
            _dbContext.SaveChanges();

            UpdateTodos();

            return RedirectToPage("/Index");
        }
        [HttpPost("UpdateIsDone")]
        public IActionResult UpdateIsDone([FromBody] UpdateTodoRequest request)
        {
            var todo = _dbContext.Todos.FirstOrDefault(t => t.Id == request.TodoId);
            if (todo != null)
            {
                _dbContext.Todos.Remove(todo);
                _dbContext.SaveChanges();
                UpdateTodos();
                return Page();   
            }

            return NotFound(new { Message = "Todo not found" });
        }







    }
}
