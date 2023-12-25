using System;
using Microsoft.Extensions.DependencyInjection;
using Models_Todo;

public static class InMemoryDbInitializer
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<TodoContext>();

            // Ensure the database is created
            dbContext.Database.EnsureCreated();

            // Check if there are existing records, and add initial data if the database is empty
            if (!dbContext.Todos.Any())
            {
                // Add initial data
                dbContext.Todos.Add(new Todo { Title = "Sample Todo", Content = "This is a sample todo.", IsDone = false, IsDeleted = false });

                // Save changes
                dbContext.SaveChanges();
            }
        }
    }
}
