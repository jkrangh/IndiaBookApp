using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace IndiaBookApp.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid Phone Number")]
        public override string PhoneNumber { get; set; }
    }
}
