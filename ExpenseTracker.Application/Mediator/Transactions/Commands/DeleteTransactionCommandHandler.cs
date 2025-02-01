using ExpenseTracker.Application.Common.Interfaces.Repositories;
using MediatR;

namespace ExpenseTracker.Application.Mediator.Transactions.Commands
{
    public class DeleteTransactionCommandHandler
        : IRequestHandler<DeleteTransactionCommand, Unit>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public DeleteTransactionCommandHandler(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<Unit> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _repositoryWrapper.TransactionRepository
                .GetByIdAsync(request.TransactionId);

            if (transaction == null)
            {
                throw new KeyNotFoundException($"Transaction '{request.TransactionId}' not found.");
            }

            var category = await _repositoryWrapper.CategoryRepository
                .GetByIdAsync(transaction.CategoryId);

            if (category == null || category.UserId != request.UserId)
            {
                throw new UnauthorizedAccessException(
                    $"Transaction '{request.TransactionId}' does not belong to user '{request.UserId}'."
                );
            }

            await _repositoryWrapper.TransactionRepository.RemoveAsync(transaction);
            await _repositoryWrapper.SaveAsync();

            return Unit.Value;
        }
    }
}
