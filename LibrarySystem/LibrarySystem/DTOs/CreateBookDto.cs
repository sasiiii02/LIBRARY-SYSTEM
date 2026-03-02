// File: DTOs/CreateBookDto.cs
// Purpose: Defines what data client sends when creating/updating a book

namespace LibrarySystem.DTOs;

public class CreateBookDto
{
    // When creating a book, client sends these 3 fields
    // No Id - database generates it
    // No CreatedAt - server sets it
    public string Title { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }
}