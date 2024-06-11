using IndiaBookApp.Data;
using IndiaBookApp.Data.Repositories;
using IndiaBookApp.Models;
using Microsoft.EntityFrameworkCore;

namespace IndiaBookApp.Test
{
    public class BookRepositoryTest : IDisposable
    {
        private static DbContextOptions<ApplicationDbContext> dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
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
        public async void UpdateBook() //Doesnt work properly. Works even if SaveChanges is commented out in UpdateAsync method
        {
            //Arrange
            var book = TestBook();
            var book2 = TestBook();
            var result = new Book();
            //Act
            await bookRepository.AddAsync(book);
            await bookRepository.AddAsync(book2);
            book.Country = "Spain";
            await bookRepository.UpdateAsync(book);
            var books = await bookRepository.GetAllAsync();
            result = books.Single(x => x.Id == book.Id);
            //Assert
            Assert.Equal(book.Country, result.Country);
        }
        [Fact]
        public async void DeleteBook()
        {
            //Arrange
            var book = TestBook();
            IEnumerable<Book> result;
            //Act
            await bookRepository.AddAsync(book);
            await bookRepository.DeleteAsync(book);
            result = await bookRepository.GetAllAsync();
            //Assert
            Assert.Empty(result);
        }
        [Fact]
        public async void GetAllBooks()
        {
            //Arrange
            var book = TestBook();
            var book2 = TestBook();
            book2.Author = "Jeff2";
            var book3 = TestBook();
            book3.Author = "Jeff3";
            IEnumerable<Book> result;
            //Act
            await bookRepository.AddAsync(book);
            await bookRepository.AddAsync(book2);
            await bookRepository.AddAsync(book3);
            result = await bookRepository.GetAllAsync();
            //Assert
            Assert.NotEmpty(result);
        }
        [Fact]
        public async void GetBookById()
        {
            //Arrange
            var book = TestBook();
            var result = new Book();
            //Act
            await bookRepository.AddAsync(book);
            result = await bookRepository.GetByIdAsync(book.Id);
            //Assert
            Assert.Equal(book.Id, result.Id);
        }

        public Book TestBook()
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