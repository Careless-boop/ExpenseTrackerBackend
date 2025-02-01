using MediatR;

namespace ExpenseTracker.Application.Mediator.Transactions.Commands
{
    public record UpdateTransactionCommand(
        string UserId,       
        Guid TransactionId,  
        Guid? CategoryId,    
        decimal? Amount,
        DateTime? Date,
        bool? IsExpense,
        string? Note
    ) : IRequest<Guid>;
}
