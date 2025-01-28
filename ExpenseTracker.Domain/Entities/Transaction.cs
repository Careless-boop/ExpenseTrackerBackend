using ExpenseTracker.Domain.Interfaces;

namespace ExpenseTracker.Domain.Entities
{
    public class Transaction : IEntity
    {
        public Guid Id { get; set; }
        public string Note { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public bool IsExpense { get; set; }

        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }

        public string UserId { get; set; } = null!;
    }
}
