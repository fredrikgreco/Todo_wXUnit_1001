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

            dbContext.Database.EnsureCreated();

            if (!dbContext.Todos.Any())
            {
                dbContext.Todos.Add(new Todo { Title = "Sample Todo", Content = "This is a sample todo.", IsDone = false, IsDeleted = false });

                dbContext.SaveChanges();
            }
        }
    }
}
