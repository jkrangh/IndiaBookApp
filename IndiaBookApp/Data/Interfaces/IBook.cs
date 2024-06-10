using IndiaBookApp.Models;

namespace IndiaBookApp.Data.Interfaces
{
    public interface IBook
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book> GetByIdAsync(int? id);
        Task AddAsync(Book book);
        Task UpdateAsync(Book book);
        Task DeleteAsync(Book book);
        Task<IEnumerable<Book>> SearchAsync(string searchString);
    }
}