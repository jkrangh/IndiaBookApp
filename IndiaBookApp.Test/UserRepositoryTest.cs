using IndiaBookApp.Data;
using IndiaBookApp.Data.Interfaces;
using IndiaBookApp.Data.Repositories;
using IndiaBookApp.Models;
using Microsoft.EntityFrameworkCore;

namespace IndiaBookApp.Test
{
    public class UserRepositoryTest : IDisposable
    {
        private static  DbContextOptions<ApplicationDbContext> dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "UserDbContext")
            .Options;

        private ApplicationDbContext dbContext;
        private UserRepository userRepository;
        public UserRepositoryTest()
        {
            dbContext = new ApplicationDbContext(dbContextOptions);
            dbContext.Database.EnsureCreated();
            userRepository = new UserRepository(dbContext);        
        }

        [Fact]
        public async Task CreateNewUser()
        {
            //Arrange
            var user = TestUser();
            var result = new User();
            //Act
            await userRepository.AddAsync(user);
            var books = await userRepository.GetAllAsync();
            result = books.FirstOrDefault(x => x.FirstName == user.FirstName);
            //Assert
            Assert.Equal(user, result);
        }

        [Fact]
        public async Task UpdateUser()
        {
            // Arrange
            var user = TestUser();
            await userRepository.AddAsync(user);

            // Act
            user.UserName = "Marten@hotmail.com";

            // Using a different context to ensure we're testing persistence
            using (var dbContextUpdate = new ApplicationDbContext(dbContextOptions))
            {
                var userRepoUpdate = new UserRepository(dbContextUpdate);
                await userRepoUpdate.UpdateAsync(user);
            }

            // Assert
            using (var dbContextCheck = new ApplicationDbContext(dbContextOptions))
            {
                var userRepoCheck = new UserRepository(dbContextCheck);
                var updatedUser = await userRepoCheck.GetByIdAsync(user.Id);
                Assert.Equal("Marten@hotmail.com", updatedUser.UserName);
            }
        }

        [Fact]
        public void DeleteUser()
        {
            //Arrange
            var user = TestUser();
            //Act

            //Assert
        }

        [Fact]
        public void GetUser()
        {
            //Arrange
            var user = TestUser();
            //Act

            //Assert
        }

        [Fact]
        public void GetUserById()
        {
            //Arrange
            var user = TestUser();
            //Act

            //Assert
        }

        public User TestUser()
        {
            User user = new User()
            { 
                UserName = "Martenson@hotmail.com",
                FirstName = "Jeff",
                LastName = "Holmden",
                Email = "Marten@hotmail.com",
                PhoneNumber = "0723232821"
            };
            return user;
        }

        public void Dispose()
        {
            dbContext.Database.EnsureDeleted();
        }
        //Arrange

        //Act

        //Assert
    }
}