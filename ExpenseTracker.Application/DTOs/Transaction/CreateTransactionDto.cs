namespace ExpenseTracker.Application.DTOs.Transaction
{
    public record CreateTransactionDto(
        Guid? CategoryId,
        decimal Amount,
        DateTime Date,
        bool IsExpense,
        string? Note
    );
}
