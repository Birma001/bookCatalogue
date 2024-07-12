using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

// Represents the database context for managing Book entities
public class BookContext : DbContext
{
    // Constructor that accepts DbContextOptions to initialize the context
    public BookContext(DbContextOptions<BookContext> options)
        : base(options)
    {
    }

    // DbSet property for accessing and managing Book entities in the database
    public DbSet<Book> Books { get; set; }
}
