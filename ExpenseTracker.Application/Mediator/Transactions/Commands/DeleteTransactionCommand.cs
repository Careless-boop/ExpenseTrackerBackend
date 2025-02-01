using MediatR;

namespace ExpenseTracker.Application.Mediator.Transactions.Commands
{
    public record DeleteTransactionCommand(
        string UserId,
        Guid TransactionId
    ) : IRequest<Unit>;
}
