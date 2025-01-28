using ExpenseTracker.Application.DTOs;
using MediatR;

namespace ExpenseTracker.Application.Mediator.Transactions.Queries
{
    public record GetAllTransactionsQuery(string UserId) : IRequest<IEnumerable<TransactionDto>>;
}
