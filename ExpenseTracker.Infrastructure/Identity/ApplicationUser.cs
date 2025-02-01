using ExpenseTracker.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace ExpenseTracker.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime? BirthDate { get; set; }

        public ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}
