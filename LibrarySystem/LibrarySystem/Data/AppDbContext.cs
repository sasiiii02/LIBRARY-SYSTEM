// File: Data/AppDbContext.cs
// Purpose: Handles database connection and operations

using LibrarySystem.Models;            // To use our Book model
using Microsoft.EntityFrameworkCore;  // For database functionality

namespace LibrarySystem.Data;

// This class inherits from DbContext (gets all database features)
public class AppDbContext : DbContext
{
    // Constructor - runs when creating a new AppDbContext
    // DbContextOptions tells it how to connect to database
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)  // Pass options to parent class
    {
    }

    // This creates a "Books" table in database
    // DbSet<Book> means "a set of Book objects"
    public DbSet<Book> Books { get; set; }

    // Optional: Add some initial data when database is created
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Tell Entity Framework to add this data when creating database
        modelBuilder.Entity<Book>().HasData(
            new Book
            {
                Id = 1,
                Title = "The Hobbit",
                Author = "J.R.R. Tolkien",
                Description = "A fantasy novel about Bilbo Baggins",
                CreatedAt = DateTime.Now
            },
            new Book
            {
                Id = 2,
                Title = "1984",
                Author = "George Orwell",
                Description = "A dystopian social science fiction novel",
                CreatedAt = DateTime.Now
            }
        );
    }
}