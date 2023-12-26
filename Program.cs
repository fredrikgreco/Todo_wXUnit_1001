using Microsoft.EntityFrameworkCore;
using Models_Todo;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();  // Add this line to enable controllers - this is something i have no idea about

builder.Services.AddDbContext<TodoContext>(options =>
{
    options.UseInMemoryDatabase("YourDatabaseName");
});

InMemoryDbInitializer.Initialize(builder.Services.BuildServiceProvider());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();  // Add this line to map controllers - also a thing i don't know anything about

app.Run();
