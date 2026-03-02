// File: Data/AppDbContext.cs
// Purpose: Handles database connection and operations
// This class acts as the bridge between our code and the SQLite database

using LibrarySystem.Models;            // To use our Book model
using Microsoft.EntityFrameworkCore;  // For database functionality

namespace LibrarySystem.Data;

// This class inherits from DbContext, which gives us all the database features
// DbContext is part of Entity Framework Core
public class AppDbContext : DbContext
{
    // Constructor - called when creating a new AppDbContext instance
    // DbContextOptions contains configuration like connection string
    // The options parameter is passed in from Program.cs via dependency injection
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)  // Pass options to the base DbContext class
    {
    }

    // This creates a "Books" table in the database
    // DbSet<Book> represents the collection of Book entities in memory
    // Each Book in this DbSet corresponds to a row in the Books table
    public DbSet<Book> Books { get; set; }

    // This method is called when the database is being created
    // We use it to seed (add) some initial data
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // HasData tells Entity Framework to insert these records
        // when the database is first created
        modelBuilder.Entity<Book>().HasData(
            // First seed book
            new Book
            {
                Id = 1,  // Explicitly set Id for seed data
                Title = "The Hobbit",
                Author = "J.R.R. Tolkien",
                Description = "A fantasy novel about Bilbo Baggins",
                CreatedAt = DateTime.Now
            },
            // Second seed book
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