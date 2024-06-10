using IndiaBookApp.Models;

namespace IndiaBookApp.Data.Interfaces
{
    public interface IUser
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(string id);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
    }
}
