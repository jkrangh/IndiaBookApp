using IndiaBookApp.Data;
using IndiaBookApp.Data.Repositories;
using IndiaBookApp.Models;
using Microsoft.EntityFrameworkCore;

namespace IndiaBookApp.Test
{
    public class BookRepositoryTest : IDisposable
    {
        private static  DbContextOptions<ApplicationDbContext> dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "BookDbContext")
            .Options;

        private ApplicationDbContext dbContext;
        private BookRepository bookRepository;
        public BookRepositoryTest()
        {
            dbContext = new ApplicationDbContext(dbContextOptions);
            dbContext.Database.EnsureCreated();
            bookRepository = new BookRepository(dbContext);        
        }

        [Fact]
        public async Task CreateNewBook()
        {
            //Arrange
            var book = TestBook();
            var result = new Book();
            //Act
            await bookRepository.AddAsync(book);
            var books = await bookRepository.GetAllAsync();
            result = books.FirstOrDefault(x => x.Author == book.Author);
            //Assert
            Assert.Equal(book, result);
        }

        [Fact]
        public async Task UpdateBook()
        {
            // Arrange
            var book = TestBook();
            await bookRepository.AddAsync(book);

            // Act
            book.Country = "Spain";

            // Using a different context to ensure we're testing persistence
            using (var dbContextUpdate = new ApplicationDbContext(dbContextOptions))
            {
                var bookRepoUpdate = new BookRepository(dbContextUpdate);
                await bookRepoUpdate.UpdateAsync(book);
            }

            // Assert
            using (var dbContextCheck = new ApplicationDbContext(dbContextOptions))
            {
                var bookRepoCheck = new BookRepository(dbContextCheck);
                var updatedBook = await bookRepoCheck.GetByIdAsync(book.Id);
                Assert.Equal("Spain", updatedBook.Country);
            }
        }

        [Fact]
        public void DeleteBook()
        {
            //Arrange
            var book = TestBook();
            //Act

            //Assert
        }

        [Fact]
        public void GetBooks()
        {
            //Arrange
            var book = TestBook();
            //Act

            //Assert
        }

        [Fact]
        public void GetBookById()
        {
            //Arrange
            var book = TestBook();
            //Act

            //Assert
        }

        private Book TestBook()
        {
            Book book = new Book()
            {
                Author = "Jeff",
                Country = "England",
                ImageLink = "EmptyImage",
                Language = "English",
                Link = "EmptyWikiLink",
                Pages = 404,
                Title = "NoPageFound",
                Year = 2024
            };
            
            return book;
        }

        public void Dispose()
        {
            dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task SearchBooks()
        {
            // Arrange
            var book = TestBook();
            await bookRepository.AddAsync(book);

            // Act
            var searchString = "Jeff";
            var result = await bookRepository.SearchAsync(searchString);

            // Assert
            Assert.Single(result);
            Assert.Equal("NoPageFound", result.First().Title);
        }

        //Arrange

        //Act

        //Assert
    }
}