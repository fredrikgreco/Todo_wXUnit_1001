using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Models_Todo;


namespace Todo_wXUnit_1001.Pages
{
    public class IndexModel : PageModel
    {
        public readonly ILogger<IndexModel> _logger;

        public readonly TodoContext _dbContext;

        public List<Todo> Todos { get; set; }

        [BindProperty]
        public Todo Todo { get; set; }


        public IndexModel(TodoContext dbContext, ILogger<IndexModel> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public void OnGet()
        {

            Todo = new Todo(); // Initialize Todo or retrieve it from your data source
            Todos = _dbContext.Todos.ToList();

        }


        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _dbContext.Todos.Add(Todo);
            _dbContext.SaveChanges();

            return RedirectToPage("/Index");
        }
    }
}


