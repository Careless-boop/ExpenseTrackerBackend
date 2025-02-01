using ExpenseTracker.Application.Common.Interfaces.Repositories;
using ExpenseTracker.Application.DTOs.Category;
using MediatR;

namespace ExpenseTracker.Application.Mediator.Categories.Queries
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryDto>>
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public GetAllCategoriesQueryHandler(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<IEnumerable<CategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _repositoryWrapper.CategoryRepository.GetAllForUserAsync(request.UserId);
            var result = categories.Select(c => new CategoryDto(c.Id, c.Name, c.UserId));

            return result;
        }
    }
}
