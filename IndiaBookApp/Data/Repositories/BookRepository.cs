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
            //await applicationDbContext.SaveChangesAsync();
        }
    }
}
