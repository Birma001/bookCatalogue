using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new BookContext(
            serviceProvider.GetRequiredService<DbContextOptions<BookContext>>()))
        {
            // Look for any books.
            if (context.Books.Any())
            {
                return;   // DB has been seeded
            }

            context.Books.AddRange(
                new Book
                {
                    Title = "Book 1",
                    Author = "Author 1",
                    ISBN = "1234567890",
                    PublicationYear = 2020
                },
                new Book
                {
                    Title = "Book 2",
                    Author = "Author 2",
                    ISBN = "0987654321",
                    PublicationYear = 2021
                }
            );

            context.SaveChanges();
        }
    }
}
