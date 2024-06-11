using IndiaBookApp.Data.Interfaces;
using IndiaBookApp.Models;
using Microsoft.EntityFrameworkCore;

namespace IndiaBookApp.Data.Repositories
{
    public class BookRepository : IBook
    {
        private readonly ApplicationDbContext applicationDbContext;

        public BookRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task AddAsync(Book book)
        {
            applicationDbContext.Add(book);
            await applicationDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Book book)
        {
            applicationDbContext.Books.Remove(book);
            await applicationDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await applicationDbContext.Books.ToListAsync();
        }

        public async Task<Book> GetByIdAsync(int? id)
        {
            return await applicationDbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
        }
            
        public async Task UpdateAsync(Book book)
        {
            applicationDbContext.Update(book);
            await applicationDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Book>> SearchAsync(string searchString)
        {
            // Implement search logic here
            var books = await applicationDbContext.Books.ToListAsync();
            return books.Where(b =>
                b.Author.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                b.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                b.Country.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                b.Language.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                b.Year.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase));
        }
    }
}
