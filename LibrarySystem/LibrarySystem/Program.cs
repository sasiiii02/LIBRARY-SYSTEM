// File: Program.cs
// Purpose: Application entry point - configures and starts the web server
// This is where we set up all services, middleware, and dependencies

using Microsoft.EntityFrameworkCore;      // For database functionality
using LibrarySystem.Data;                  // Our custom database context

// Create a builder object that helps configure our application
var builder = WebApplication.CreateBuilder(args);

// ========== ADD SERVICES TO THE CONTAINER ==========
// Services are reusable components that can be injected into controllers

// Add controller services - this enables MVC controllers (like BooksController)
// Controllers handle HTTP requests and return responses
builder.Services.AddControllers();

// Configure SQLite database connection
// AddDbContext registers our AppDbContext with dependency injection
// It tells the app to use SQLite with a database file named "library.db"
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=library.db"));

// Add Swagger/OpenAPI services for API documentation
// This automatically generates documentation at /swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS (Cross-Origin Resource Sharing)
// This allows our React frontend (running on a different port) to call this API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            // AllowAnyOrigin() means any domain can call this API
            // In production, you'd restrict this to your actual frontend URL
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()      // Allow GET, POST, PUT, DELETE, etc.
                  .AllowAnyHeader();      // Allow any HTTP headers
        });
});

// Build the application with all configured services
var app = builder.Build();

// ========== CONFIGURE THE HTTP REQUEST PIPELINE ==========
// This determines how the app handles incoming HTTP requests

// Enable Swagger only in development environment
// This ensures API documentation isn't exposed in production
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();           // Generate Swagger JSON
    app.UseSwaggerUI();         // Serve Swagger UI at /swagger
}

// Redirect HTTP requests to HTTPS for security
app.UseHttpsRedirection();

// Enable CORS - must be called before MapControllers
// This applies the CORS policy we defined above
app.UseCors("AllowReactApp");

// Enable authorization middleware
// Even though we're not using auth now, it's good to have for future
app.UseAuthorization();

// Map controller endpoints to routes
// This connects URLs like /api/books to the appropriate controller actions
app.MapControllers();

// ========== DATABASE INITIALIZATION ==========
// Create the database on startup if it doesn't already exist
using (var scope = app.Services.CreateScope())
{
    // Get our database context from the service scope
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    
    // EnsureCreated creates the database file and tables if they don't exist
    // It also runs the seed data from OnModelCreating
    dbContext.Database.EnsureCreated();
    // Note: In production, you'd use Migrations instead of EnsureCreated
}

// Start the application and begin listening for HTTP requests
app.Run();