using MediatR;

namespace ExpenseTracker.Application.Mediator.Transactions.Commands
{
    public record CreateTransactionCommand(
        string UserId,
        Guid? CategoryId,
        decimal Amount,
        bool IsExpense,
        DateTime Date,
        string Note
    ) : IRequest<Guid>;
}
