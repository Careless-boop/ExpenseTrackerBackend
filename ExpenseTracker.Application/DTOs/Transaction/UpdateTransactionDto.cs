namespace ExpenseTracker.Application.DTOs.Transaction
{
    public record UpdateTransactionDto(Guid TransactionId, Guid? CategoryId, decimal? Amount, DateTime? Date, bool? IsExpense, string? Note);
}
