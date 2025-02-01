using ExpenseTracker.Application.DTOs.Transaction;
using MediatR;

namespace ExpenseTracker.Application.Mediator.Transactions.Queries
{
    public record GetTransactionByIdQuery(
        string UserId,
        Guid TransactionId
    ) : IRequest<TransactionDto>;
}
