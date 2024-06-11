using IndiaBookApp.Data.Interfaces;
using IndiaBookApp.Models;
using Microsoft.EntityFrameworkCore;

namespace IndiaBookApp.Data.Repositories
{
    public class BookLoanRepository : IBookLoan
    {
        private readonly ApplicationDbContext applicationDbContext;

        public BookLoanRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public async Task AddAsync(BookLoan bookLoan)
        {
            applicationDbContext.Add(bookLoan);
            await applicationDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(BookLoan bookLoan)
        {
            applicationDbContext.BookLoans.Remove(bookLoan);
            await applicationDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<BookLoan>> GetAllAsync()
        {
            return await applicationDbContext.BookLoans.ToListAsync();
        }

        public async Task<BookLoan> GetByIdAsync(int? id)
        {
            return await applicationDbContext.BookLoans.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(BookLoan bookLoan)
        {
            applicationDbContext.Update(bookLoan);
            await applicationDbContext.SaveChangesAsync();
        }
    }
}
