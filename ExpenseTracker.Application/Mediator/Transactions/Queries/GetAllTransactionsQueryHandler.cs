using ExpenseTracker.Application.Common.Interfaces.Repositories;
using ExpenseTracker.Application.DTOs.Transaction;
using MediatR;

namespace ExpenseTracker.Application.Mediator.Transactions.Queries
{
    public class GetAllTransactionsQueryHandler
        : IRequestHandler<GetAllTransactionsQuery, IEnumerable<TransactionDto>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public GetAllTransactionsQueryHandler(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<IEnumerable<TransactionDto>> Handle(
            GetAllTransactionsQuery request,
            CancellationToken cancellationToken)
        {
            var transactions = await _repositoryWrapper.TransactionRepository.GetAllForUserAsync(request.UserId);

            var result = transactions.Select(t => new TransactionDto(t.Id, t.CategoryId, t.Amount, t.Date, t.IsExpense, t.CreatedAt, t.Note));

            return result;
        }
    }
}
