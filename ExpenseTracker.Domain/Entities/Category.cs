using ExpenseTracker.Domain.Interfaces;

namespace ExpenseTracker.Domain.Entities
{
    public class Category : IEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string UserId { get; set; } = default!;

        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
