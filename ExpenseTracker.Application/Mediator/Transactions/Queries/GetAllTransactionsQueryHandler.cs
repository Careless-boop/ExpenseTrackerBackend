using ExpenseTracker.Application.Common.Interfaces.Repositories;
using ExpenseTracker.Application.DTOs;
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

            var result = transactions.Select(t => new TransactionDto
            {
                Id = t.Id,
                Amount = t.Amount,
                Date = t.Date,
                Note = t.Note,
                CategoryId = t.CategoryId,
                IsExpense = t.IsExpense
            });

            return result;
        }
    }
}
