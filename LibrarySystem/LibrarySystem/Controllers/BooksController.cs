// File: Controllers/BooksController.cs
// Purpose: Handles all HTTP requests for books

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibrarySystem.Data;
using LibrarySystem.Models;
using LibrarySystem.DTOs;

namespace LibrarySystem.Controllers;

[Route("api/[controller]")]  // URL: /api/books
[ApiController]
public class BooksController : ControllerBase
{
    private readonly AppDbContext _context;

    // Constructor - gets database context
    public BooksController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/books
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookResponseDto>>> GetBooks()
    {
        // Get all books from database
        var books = await _context.Books.ToListAsync();

        // Convert each book to DTO (hides internal fields)
        var bookDtos = books.Select(book => new BookResponseDto
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            Description = book.Description
        }).ToList();

        return Ok(bookDtos);  // 200 OK with books
    }

    // GET: api/books/5
    [HttpGet("{id}")]
    public async Task<ActionResult<BookResponseDto>> GetBook(int id)
    {
        // Find book by ID
        var book = await _context.Books.FindAsync(id);

        if (book == null)
        {
            return NotFound();  // 404 Not Found
        }

        // Convert to DTO
        var bookDto = new BookResponseDto
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            Description = book.Description
        };

        return Ok(bookDto);  // 200 OK with book
    }

    // POST: api/books
    [HttpPost]
    public async Task<ActionResult<BookResponseDto>> CreateBook(CreateBookDto createBookDto)
    {
        // Validate: Title is required
        if (string.IsNullOrWhiteSpace(createBookDto.Title))
        {
            return BadRequest("Title is required");  // 400 Bad Request
        }

        // Create new book from DTO
        var book = new Book
        {
            Title = createBookDto.Title,
            Author = createBookDto.Author,
            Description = createBookDto.Description,
            CreatedAt = DateTime.Now  // Set creation time
        };

        // Add to database
        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        // Convert saved book to DTO (now includes ID)
        var bookDto = new BookResponseDto
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            Description = book.Description
        };

        // Return 201 Created with location of new book
        return CreatedAtAction(nameof(GetBook), new { id = book.Id }, bookDto);
    }

    // PUT: api/books/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(int id, CreateBookDto updateBookDto)
    {
        // Find book to update
        var book = await _context.Books.FindAsync(id);

        if (book == null)
        {
            return NotFound();  // 404 Not Found
        }

        // Update properties
        book.Title = updateBookDto.Title;
        book.Author = updateBookDto.Author;
        book.Description = updateBookDto.Description;

        // Mark as modified and save
        _context.Entry(book).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();  // 204 No Content (success)
    }

    // DELETE: api/books/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        // Find book to delete
        var book = await _context.Books.FindAsync(id);

        if (book == null)
        {
            return NotFound();  // 404 Not Found
        }

        // Remove from database
        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

        return NoContent();  // 204 No Content
    }
}