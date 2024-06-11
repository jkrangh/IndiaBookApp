namespace IndiaBookApp.Models
{
    public class BookLoan
    {
        public int Id { get; set; }
        public Book Book { get; set; }
        public User User { get; set; }
        public DateTime LoanDate { get; set; } = DateTime.Now;
        public DateTime LoanExpires { get; set; } = DateTime.Now.AddDays(14);
    }
}
