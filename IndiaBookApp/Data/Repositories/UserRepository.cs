using IndiaBookApp.Data.Interfaces;
using IndiaBookApp.Models;
using Microsoft.EntityFrameworkCore;

namespace IndiaBookApp.Data.Repositories
{
    public class UserRepository : IUser
    {
        private readonly ApplicationDbContext applicationDbContext;

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task AddAsync(User user)
        {
            applicationDbContext.Add(user);
            await applicationDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            applicationDbContext.Users.Remove(user);
            await applicationDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await applicationDbContext.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(string id)
        {
            return await applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(User user)
        {
            applicationDbContext.Update(user);
            await applicationDbContext.SaveChangesAsync();
        }
    }
}
