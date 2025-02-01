namespace ExpenseTracker.Application.DTOs.Transaction
{
    public record TransactionDto(Guid Id, Guid CategoryId, decimal Amount, DateTime Date, bool IsExpense, DateTime CreatedAt, string Note);
}
