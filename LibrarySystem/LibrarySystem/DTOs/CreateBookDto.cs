// File: DTOs/CreateBookDto.cs
// Purpose: Defines what data the client sends when creating/updating a book
// This DTO represents the data coming from the frontend form

namespace LibrarySystem.DTOs;

// This DTO is used when receiving book data from the client
// It matches the form fields in the React frontend
public class CreateBookDto
{
    // Book title - required field from the form
    // The frontend validates that this isn't empty
    public string Title { get; set; }

    // Author name - also required in the form
    public string Author { get; set; }

    // Book description - optional field
    // Can be empty string if user doesn't provide one
    public string Description { get; set; }

    // Note: Id is NOT included because the database generates it
    // CreatedAt is NOT included because the server sets it
    // The client only needs to provide the actual book data
}