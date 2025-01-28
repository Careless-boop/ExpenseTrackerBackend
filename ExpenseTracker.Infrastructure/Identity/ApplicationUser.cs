using Microsoft.AspNetCore.Identity;

namespace ExpenseTracker.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime? BirthDate { get; set; }
    }
}
