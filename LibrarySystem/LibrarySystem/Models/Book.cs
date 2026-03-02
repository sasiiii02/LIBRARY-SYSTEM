// File: Models/Book.cs
// Purpose: Defines what a Book looks like in our database

using System;  // For DateTime

namespace LibrarySystem.Models;

public class Book
{
    // Every book needs a unique ID (primary key)
    // Database will auto-generate this
    public int Id { get; set; }

    // Book title - can't be empty
    public string Title { get; set; }

    // Author name
    public string Author { get; set; }

    // Book description
    public string Description { get; set; }

    // When the book was added to library
    // DateTime is a built-in type for dates
    public DateTime CreatedAt { get; set; }
}