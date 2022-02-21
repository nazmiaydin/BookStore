using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.DbOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                {
                    return;
                }

                context.Authors.AddRange(
                    new Author
                    {
                        Name ="Yazar1_Name",
                        Surname ="Yazar1_Surname",
                        BirthDate = new DateTime(1960,12,03)
                    },
                    new Author
                    {
                        Name ="Yazar2_Name",
                        Surname ="Yazar2_Surname",
                        BirthDate = new DateTime(1955,01,12)
                    },
                    new Author
                    {
                        Name ="Yazar3_Name",
                        Surname ="Yazar3_Surname",
                        BirthDate = new DateTime(1978,07,04)
                    }
                    );

                context.Genres.AddRange(
                    new Genre
                    {
                        Name = "Personal Growth"
                    },
                    new Genre
                    {
                        Name = "Science Fiction"
                    },
                    new Genre
                    {
                        Name = "Romance"
                    }
                );

                context.Books.AddRange(
                    new Book
                    {
                        Title = "Lean Startup",
                        GenreId = 1,
                        PageCount = 100,
                        PublishDate = new DateTime(2001, 06, 12),
                        AuthorId = 1
                    },
                    new Book
                    {
                        Title = "Herland",
                        GenreId = 2,
                        PageCount = 400,
                        PublishDate = new DateTime(2020, 06, 12),
                        AuthorId = 1,
                    },
                    new Book
                    {
                        Title = "Dune",
                        GenreId = 3,
                        PageCount = 150,
                        PublishDate = new DateTime(1999, 06, 12),
                        AuthorId = 2,
                    }
                );
                context.SaveChanges();
            }
        }
    }
}