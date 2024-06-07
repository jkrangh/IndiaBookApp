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
        public async void UpdateBook()
        {
            //Arrange
            var book = TestBook();
            var result = new Book();
            //Act
            await bookRepository.AddAsync(book);
            book.Country = "Spain";
            await bookRepository.UpdateAsync(book);
            var books = await bookRepository.GetAllAsync();
            result = books.FirstOrDefault(x => x.Author == book.Author);
            //Assert
            Assert.Equal(book, result);
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
                Language = "Enlgish",
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
        //Arrange

        //Act

        //Assert
    }
}