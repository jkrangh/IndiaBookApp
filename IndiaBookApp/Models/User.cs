using Microsoft.AspNetCore.Identity;

namespace IndiaBookApp.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
