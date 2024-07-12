using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly BookContext _context;

    public BooksController(BookContext context)
    {
        _context = context;
    }

    // GET: api/Books
    // Retrieves all books from the database asynchronously
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
    {
        return await _context.Books.ToListAsync();
    }

    // GET: api/Books/5
    // Retrieves a specific book by its ID asynchronously
    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> GetBook(int id)
    {
        var book = await _context.Books.FindAsync(id);

        if (book == null)
        {
            return NotFound(); // Returns 404 Not Found if the book with the given ID doesn't exist
        }

        return book;
    }

    // POST: api/Books
    // Adds a new book to the database asynchronously
    [HttpPost]
    public async Task<ActionResult<Book>> PostBook(Book book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        // Returns a 201 Created status and the newly created book's details
        return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
    }

    // PUT: api/Books/5
    // Updates an existing book's details asynchronously
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBook(int id, Book book)
    {
        if (id != book.Id)
        {
            return BadRequest(); // Returns 400 Bad Request if the provided ID doesn't match the book's ID
        }

        _context.Entry(book).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BookExists(id))
            {
                return NotFound(); // Returns 404 Not Found if the book with the given ID doesn't exist
            }
            else
            {
                throw;
            }
        }

        return NoContent(); // Returns 204 No Content after a successful update
    }

    // DELETE: api/Books/5
    // Deletes a book by its ID asynchronously
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            return NotFound(); // Returns 404 Not Found if the book with the given ID doesn't exist
        }

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

        return NoContent(); // Returns 204 No Content after successful deletion
    }

    // Checks if a book with the specified ID exists
    private bool BookExists(int id)
    {
        return _context.Books.Any(e => e.Id == id);
    }
}
