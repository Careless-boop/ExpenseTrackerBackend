namespace ExpenseTracker.Application.DTOs
{
    public class TransactionDto
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public bool IsExpense { get; set; }
        public string Note { get; set; } = string.Empty;
    }
}
