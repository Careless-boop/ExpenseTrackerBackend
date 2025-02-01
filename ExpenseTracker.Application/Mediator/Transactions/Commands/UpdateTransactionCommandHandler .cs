using ExpenseTracker.Application.Common.Interfaces.Repositories;
using MediatR;

namespace ExpenseTracker.Application.Mediator.Transactions.Commands
{
    public class UpdateTransactionCommandHandler
        : IRequestHandler<UpdateTransactionCommand, Guid>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public UpdateTransactionCommandHandler(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<Guid> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = await _repositoryWrapper.TransactionRepository
                 .GetByIdAsync(request.TransactionId);
            if (transaction == null)
            {
                throw new KeyNotFoundException($"Transaction '{request.TransactionId}' not found.");
            }

            var currentCategory = await _repositoryWrapper.CategoryRepository
                .GetByIdAsync(transaction.CategoryId);

            if (currentCategory == null || currentCategory.UserId != request.UserId)
            {
                throw new UnauthorizedAccessException($"Transaction does not belong to user '{request.UserId}'.");
            }

            if (request.CategoryId.HasValue && request.CategoryId.Value != transaction.CategoryId)
            {
                var newCategory = await _repositoryWrapper.CategoryRepository
                    .GetByIdAsync(request.CategoryId.Value);
                if (newCategory == null)
                {
                    throw new KeyNotFoundException($"Category '{request.CategoryId.Value}' not found.");
                }
                if (newCategory.UserId != request.UserId)
                {
                    throw new UnauthorizedAccessException(
                        $"Category '{request.CategoryId.Value}' does not belong to user '{request.UserId}'.");
                }
                transaction.CategoryId = newCategory.Id;
            }

            if (request.Amount.HasValue)
                transaction.Amount = request.Amount.Value;

            if (request.Date.HasValue)
                transaction.Date = request.Date.Value;

            if (request.IsExpense.HasValue)
                transaction.IsExpense = request.IsExpense.Value;

            if (request.Note != null)
                transaction.Note = request.Note;

            await _repositoryWrapper.TransactionRepository.UpdateAsync(transaction);
            await _repositoryWrapper.SaveAsync();

            return transaction.Id;
        }
    }
}
