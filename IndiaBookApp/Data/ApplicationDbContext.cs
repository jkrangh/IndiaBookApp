using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IndiaBookApp.Models;
using Newtonsoft.Json;

namespace IndiaBookApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; } = default!;

        public void SeedDatabase()
        {
            if (!Books.Any())
            {
                var books = GetBooksFromJson();
                Books.AddRange(books);
                SaveChanges();
            }
        }

        private List<Book> GetBooksFromJson()
        {
            var jsonData = File.ReadAllText(@"wwwroot/seed-data/books.json");
            var books = JsonConvert.DeserializeObject<List<Book>>(jsonData);
            return books;
        }
    }
}
