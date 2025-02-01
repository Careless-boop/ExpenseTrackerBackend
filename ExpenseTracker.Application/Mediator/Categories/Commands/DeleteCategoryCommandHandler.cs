using ExpenseTracker.Application.Common.Interfaces.Repositories;
using MediatR;

namespace ExpenseTracker.Application.Mediator.Categories.Commands
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Unit>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public DeleteCategoryCommandHandler(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _repositoryWrapper.CategoryRepository.GetByIdAsync(request.CategoryId);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category '{request.CategoryId}' not found");
            }

            if(category.UserId != request.UserId)
            {
                throw new UnauthorizedAccessException($"Category '{request.CategoryId}' does not belong to user '{request.UserId}'");
            }

            await _repositoryWrapper.CategoryRepository.RemoveAsync(category);
            await _repositoryWrapper.SaveAsync();

            return Unit.Value;
        }
    }
}
