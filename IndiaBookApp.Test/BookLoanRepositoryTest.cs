using IndiaBookApp.Data;
using IndiaBookApp.Data.Repositories;
using IndiaBookApp.Models;
using Microsoft.EntityFrameworkCore;

namespace IndiaBookApp.Test
{
    public class BookLoanRepositoryTest : IDisposable
    {
        private static DbContextOptions<ApplicationDbContext> dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "BookLoanDbContext")
            .Options;

        private ApplicationDbContext dbContext;
        private BookLoanRepository bookLoanRepository;
        private readonly BookRepositoryTest bookRepositoryTest;

        public BookLoanRepositoryTest()
        {
            dbContext = new ApplicationDbContext(dbContextOptions);
            dbContext.Database.EnsureCreated();
            bookLoanRepository = new BookLoanRepository(dbContext);
            bookRepositoryTest = new BookRepositoryTest();
        }
        [Fact]
        public async Task CreateNewBookLoan()
        {
            //Arrange
            var bookLoan = TestBookLoan();
            var result = new BookLoan();
            //Act
            await bookLoanRepository.AddAsync(bookLoan);
            var bookLoans = await bookLoanRepository.GetAllAsync();
            result = bookLoans.FirstOrDefault(x => x.User == bookLoan.User);
            //Assert
            Assert.Equal(bookLoan, result);
        }
        [Fact]
        public async void UpdateBookLoan()
        {
            //Arrange
            var bookLoan = TestBookLoan();
            var bookLoan2 = TestBookLoan();
            var result = new BookLoan();
            //Act
            await bookLoanRepository.AddAsync(bookLoan);
            await bookLoanRepository.AddAsync(bookLoan2);
            bookLoan.LoanExpires = DateTime.Now.AddDays(30);
            await bookLoanRepository.UpdateAsync(bookLoan);

            var bookLoans = await bookLoanRepository.GetAllAsync();
            result = bookLoans.Single(x => x.Id == bookLoan.Id);
            //Assert
            Assert.Equal(bookLoan.LoanExpires, result.LoanExpires);
        }
        [Fact]
        public async void DeleteBookLoan()
        {
            //Arrange
            var bookLoan = TestBookLoan();
            IEnumerable<BookLoan> result;
            //Act
            await bookLoanRepository.AddAsync(bookLoan);
            await bookLoanRepository.DeleteAsync(bookLoan);
            result = await bookLoanRepository.GetAllAsync();
            //Assert
            Assert.Empty(result);
        }
        [Fact]
        public async void GetAllBookLoans()
        {
            //Arrange
            var bookLoan = TestBookLoan();
            var bookLoan2 = TestBookLoan();
            bookLoan2.LoanExpires = DateTime.Now.AddDays(30);
            var bookLoan3 = TestBookLoan();
            bookLoan3.LoanExpires = DateTime.Now.AddDays(50);
            IEnumerable<BookLoan> result;
            //Act
            await bookLoanRepository.AddAsync(bookLoan);
            await bookLoanRepository.AddAsync(bookLoan2);
            await bookLoanRepository.AddAsync(bookLoan3);
            result = await bookLoanRepository.GetAllAsync();
            //Assert
            Assert.NotEmpty(result);
        }
        [Fact]
        public async void GetBookLoanById()
        {
            //Arrange
            var bookLoan = TestBookLoan();
            var result = new BookLoan();
            //Act
            await bookLoanRepository.AddAsync(bookLoan);
            result = await bookLoanRepository.GetByIdAsync(bookLoan.Id);
            //Assert
            Assert.Equal(bookLoan.Id, result.Id);
        }
        private BookLoan TestBookLoan()
        {
            BookLoan bookLoan = new BookLoan()
            {
                Book = bookRepositoryTest.TestBook(),
                User = new User() { FirstName = "Lisa", LastName = "Svensson" }
            };
            return bookLoan;
        }
        public Book asdasTestBook()
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
    }

}
