// File: DTOs/BookResponseDto.cs
// Purpose: Defines what data the server sends back to the client
// DTO = Data Transfer Object - used to shape data for API responses

namespace LibrarySystem.DTOs;

// This DTO is used when sending book data from server to client
// It only includes fields that the frontend needs to see
public class BookResponseDto
{
    // Unique identifier for the book
    // This comes from the database primary key
    public int Id { get; set; }

    // Book title - shown in the frontend list
    public string Title { get; set; }

    // Author name - displayed in the book card
    public string Author { get; set; }

    // Book description - optional field shown when available
    public string Description { get; set; }
    
    // Note: CreatedAt is intentionally omitted
    // The frontend doesn't need to know when the book was added
    // This keeps internal implementation details private
}