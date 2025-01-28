using ExpenseTracker.Application.Common.Interfaces.Repositories;
using ExpenseTracker.Domain.Entities;
using MediatR;

namespace ExpenseTracker.Application.Mediator.Transactions.Commands
{
    public class CreateTransactionCommandHandler
        : IRequestHandler<CreateTransactionCommand, Guid>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public CreateTransactionCommandHandler(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<Guid> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = new Transaction
            {
                UserId = request.UserId,
                CategoryId = request.CategoryId,
                Amount = request.Amount,
                IsExpense = request.IsExpense,
                Date = request.Date,
                Note = request.Note
            };

            await _repositoryWrapper.TransactionRepository.AddAsync(transaction);

            return transaction.Id;
        }
    }
}