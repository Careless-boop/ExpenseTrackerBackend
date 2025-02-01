using AutoMapper;
using ExpenseTracker.Application.Common.Interfaces.Repositories;
using MediatR;

namespace ExpenseTracker.Application.Mediator.Categories.Commands
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Guid>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public UpdateCategoryCommandHandler(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<Guid> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _repositoryWrapper.CategoryRepository
                 .GetByIdAsync(request.CategoryId);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category '{request.CategoryId}' not found.");
            }
            if (category.UserId != request.UserId)
            {
                throw new UnauthorizedAccessException(
                    $"Category '{request.CategoryId}' does not belong to user '{request.UserId}'."
                );
            }

            category.Name = request.Name;

            await _repositoryWrapper.CategoryRepository.UpdateAsync(category);
            await _repositoryWrapper.SaveAsync();

            return category.Id;
        }
    }
}
