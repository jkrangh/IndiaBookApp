using IndiaBookApp.Models;

namespace IndiaBookApp.Data.Interfaces
{
    public interface IBookLoan
    {
        Task<IEnumerable<BookLoan>> GetAllAsync();
        Task<BookLoan> GetByIdAsync(int? id);
        Task AddAsync(BookLoan bookLoan);
        Task UpdateAsync(BookLoan bookLoan);
        Task DeleteAsync(BookLoan bookLoan);
    }
}
