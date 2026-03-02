// File: Controllers/BooksController.cs
// Purpose: Handles all HTTP requests for book operations
// This controller defines the API endpoints that the frontend will call

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibrarySystem.Data;
using LibrarySystem.Models;
using LibrarySystem.DTOs;

namespace LibrarySystem.Controllers;

// This attribute sets the base route for all endpoints in this controller
// [controller] gets replaced by the controller name (Books) -> URL: /api/books
[Route("api/[controller]")]
// This attribute enables automatic HTTP 400 responses for invalid models
[ApiController]
public class BooksController : ControllerBase
{
    // Database context private field - will be injected via constructor
    private readonly AppDbContext _context;

    // Constructor - receives database context through dependency injection
    // ASP.NET Core automatically provides the AppDbContext instance
    public BooksController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/books
    // Returns a list of all books in the database
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookResponseDto>>> GetBooks()
    {
        // ToListAsync() executes the query and returns all books as a List
        // The 'await' keyword is used because database operations are asynchronous
        var books = await _context.Books.ToListAsync();

        // Convert each Book entity to a BookResponseDto
        // This ensures we only send the fields we want to expose to the client
        var bookDtos = books.Select(book => new BookResponseDto
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            Description = book.Description
            // CreatedAt is intentionally excluded - it's internal data
        }).ToList();

        // Return 200 OK status with the list of books
        return Ok(bookDtos);
    }

    // GET: api/books/5
    // Returns a single book by its ID
    [HttpGet("{id}")]
    public async Task<ActionResult<BookResponseDto>> GetBook(int id)
    {
        // FindAsync searches for a book with the matching primary key (Id)
        var book = await _context.Books.FindAsync(id);

        // If no book found with that ID, return 404 Not Found
        if (book == null)
        {
            return NotFound();
        }

        // Convert the found book to DTO format before sending to client
        var bookDto = new BookResponseDto
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            Description = book.Description
        };

        // Return 200 OK with the book data
        return Ok(bookDto);
    }

    // POST: api/books
    // Creates a new book from data sent by the client
    [HttpPost]
    public async Task<ActionResult<BookResponseDto>> CreateBook(CreateBookDto createBookDto)
    {
        // Basic validation - ensure title is provided
        // Title is required because it's the main identifier for the book
        if (string.IsNullOrWhiteSpace(createBookDto.Title))
        {
            // Return 400 Bad Request with error message
            return BadRequest("Title is required");
        }

        // Create a new Book entity from the DTO data
        var book = new Book
        {
            Title = createBookDto.Title,
            Author = createBookDto.Author,
            Description = createBookDto.Description,
            CreatedAt = DateTime.Now  // Server sets the creation timestamp
        };

        // Add the new book to the database context
        // This stages the insert but doesn't execute yet
        _context.Books.Add(book);
        
        // SaveChangesAsync actually executes the INSERT command
        // The database generates and assigns the Id at this point
        await _context.SaveChangesAsync();

        // Convert the saved book (now with an Id) to response DTO
        var bookDto = new BookResponseDto
        {
            Id = book.Id,  // Id is now populated by the database
            Title = book.Title,
            Author = book.Author,
            Description = book.Description
        };

        // Return 201 Created status with:
        // - Location header pointing to the new resource (api/books/{id})
        // - The newly created book data in the response body
        return CreatedAtAction(nameof(GetBook), new { id = book.Id }, bookDto);
    }

    // PUT: api/books/5
    // Fully updates an existing book (replaces all fields)
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(int id, CreateBookDto updateBookDto)
    {
        // First, check if the book exists in the database
        var book = await _context.Books.FindAsync(id);

        // If book not found, return 404 Not Found
        if (book == null)
        {
            return NotFound();
        }

        // Update the existing book's properties with new values
        book.Title = updateBookDto.Title;
        book.Author = updateBookDto.Author;
        book.Description = updateBookDto.Description;
        // CreatedAt is NOT updated - it stays as the original creation date

        // Mark the entity as modified so Entity Framework knows to update it
        // This is optional here because we're directly modifying tracked entity
        _context.Entry(book).State = EntityState.Modified;
        
        // SaveChangesAsync executes the UPDATE command
        await _context.SaveChangesAsync();

        // Return 204 No Content (success with no response body)
        return NoContent();
    }

    // DELETE: api/books/5
    // Removes a book from the database
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        // Find the book to delete
        var book = await _context.Books.FindAsync(id);

        // If book doesn't exist, return 404 Not Found
        if (book == null)
        {
            return NotFound();
        }

        // Remove the book from the database context
        // This stages the delete operation
        _context.Books.Remove(book);
        
        // SaveChangesAsync executes the DELETE command
        await _context.SaveChangesAsync();

        // Return 204 No Content (success with no response body)
        return NoContent();
    }
}