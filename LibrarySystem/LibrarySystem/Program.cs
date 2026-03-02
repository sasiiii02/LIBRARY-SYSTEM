// File: Program.cs
// Purpose: Configures and starts the application

using Microsoft.EntityFrameworkCore;      // For database
using LibrarySystem.Data;                  // Our database context

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
// This enables controllers (like BooksController)
builder.Services.AddControllers();

// Configure SQLite Database
// This tells our app to use SQLite with a file named "library.db"
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=library.db"));

// Learn more about configuring Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// IMPORTANT: Configure CORS to allow React app to call this API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            // Allow requests from React app (default port 3000)
            policy.AllowAnyOrigin()
      .AllowAnyMethod()
      .AllowAnyHeader();     // Allow any headers
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    // Swagger helps test API (available at /swagger)
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redirect HTTP to HTTPS
app.UseHttpsRedirection();

// Enable CORS (must be before MapControllers)
app.UseCors("AllowReactApp");

// Enable authorization (not using yet, but keep it)
app.UseAuthorization();

// Map controllers to routes
app.MapControllers();

// Create database on startup if it doesn't exist
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    // This creates the database file and tables
    dbContext.Database.EnsureCreated();
}

// Start the application
app.Run();