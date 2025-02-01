using AutoMapper;
using ExpenseTracker.Application.Common.Interfaces.Repositories;
using ExpenseTracker.Domain.Entities;
using MediatR;

namespace ExpenseTracker.Application.Mediator.Transactions.Commands
{
    public class CreateTransactionCommandHandler
        : IRequestHandler<CreateTransactionCommand, Guid>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public CreateTransactionCommandHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var categoryId = await GetTransactionCategoryAsync(request.CategoryId, request.UserId);

            var transaction = _mapper.Map<Transaction>(request);

            transaction.CategoryId = categoryId;

            await _repositoryWrapper.TransactionRepository.AddAsync(transaction);

            await _repositoryWrapper.SaveAsync();

            return transaction.Id;
        }

        private async Task<Guid> GetTransactionCategoryAsync(Guid? categoryId, string userId)
        {
            if (categoryId == null)
            {
                var category = await _repositoryWrapper.CategoryRepository.GetByNameAndUserAsync("Others", userId);
                if(category == null)
                {
                    category = new Category
                    {
                        Name = "Others",
                        UserId = userId
                    };

                    await _repositoryWrapper.CategoryRepository.AddAsync(category);
                }

                return category.Id;
            }
            else
            {
                var category = await _repositoryWrapper.CategoryRepository.GetByIdAsync(categoryId.Value);
                if(category == null)
                {
                    throw new KeyNotFoundException($"Category '{categoryId}' not found.");
                }
                if(category.UserId != userId)
                {
                    throw new UnauthorizedAccessException($"Category '{categoryId}' does not belong to user '{userId}'.");
                }
            }

            return categoryId.Value;
        }
    }
}