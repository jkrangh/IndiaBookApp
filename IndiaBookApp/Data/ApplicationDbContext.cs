using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IndiaBookApp.Models;
using Newtonsoft.Json;

namespace IndiaBookApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<IndiaBookApp.Models.Book> Books { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(SeedBookData());
        }

        public List<Book> SeedBookData()
        {
            var books = new List<Book>();
            using (StreamReader r = new StreamReader(@"wwwroot/seed-data/books.json"))
            {
                string json = r.ReadToEnd();
                books = JsonConvert.DeserializeObject<List<Book>>(json);
            }
            return books;
        }
    }
}
