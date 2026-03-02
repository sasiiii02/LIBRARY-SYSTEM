// File: Models/Book.cs
// Purpose: Defines the structure of a Book entity in our database
// This class maps directly to the Books table in SQLite

using System;  // For DateTime type

namespace LibrarySystem.Models;

// This is our main domain model - represents a book in the library
public class Book
{
    // Primary key - unique identifier for each book
    // By convention, Entity Framework treats 'Id' or 'BookId' as the primary key
    // The database will auto-generate this value when inserting new records
    public int Id { get; set; }

    // Book title - required field
    // In a real app, we might add [Required] attribute for validation
    public string Title { get; set; }

    // Author name - who wrote the book
    public string Author { get; set; }

    // Book description - short summary of the book
    // This can be null/empty if user doesn't provide one
    public string Description { get; set; }

    // Timestamp of when the book was added to the database
    // This is set automatically by the server when creating a book
    // Useful for sorting by newest additions or tracking purposes
    public DateTime CreatedAt { get; set; }
}