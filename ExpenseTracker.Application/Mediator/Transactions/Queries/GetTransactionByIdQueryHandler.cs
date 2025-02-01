using AutoMapper;
using ExpenseTracker.Application.Common.Interfaces.Repositories;
using ExpenseTracker.Application.DTOs.Transaction;
using MediatR;

namespace ExpenseTracker.Application.Mediator.Transactions.Queries
{
    public class GetTransactionByIdQueryHandler
        : IRequestHandler<GetTransactionByIdQuery, TransactionDto>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public GetTransactionByIdQueryHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        public async Task<TransactionDto> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
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

            var transactionDto = _mapper.Map<TransactionDto>(transaction);

            return transactionDto;
        }
    }
}
