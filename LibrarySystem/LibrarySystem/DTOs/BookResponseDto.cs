// File: DTOs/BookResponseDto.cs
// Purpose: Defines what data server sends back to client

namespace LibrarySystem.DTOs;

public class BookResponseDto
{
    // When sending book data to client, include these fields
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }
    // Note: We don't send CreatedAt to client
}