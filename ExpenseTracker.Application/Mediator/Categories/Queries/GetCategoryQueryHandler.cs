using AutoMapper;
using ExpenseTracker.Application.Common.Interfaces.Repositories;
using ExpenseTracker.Application.DTOs.Category;
using MediatR;

namespace ExpenseTracker.Application.Mediator.Categories.Queries
{
    public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, CategoryDto>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public GetCategoryQueryHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        public async Task<CategoryDto> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            var category = await _repositoryWrapper.CategoryRepository.GetByIdAsync(request.CategoryId);
         
            if(category == null)
            {
                throw new KeyNotFoundException($"Category '{request.CategoryId}' not found.");
            }
            if(category.UserId != request.UserId)
            {
                throw new UnauthorizedAccessException(
                    $"Category '{request.CategoryId}' does not belong to user '{request.UserId}'."
                );
            }

            var result = _mapper.Map<CategoryDto>(category);

            return result;
        }
    }
}
