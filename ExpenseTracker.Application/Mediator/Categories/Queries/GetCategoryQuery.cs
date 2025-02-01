using ExpenseTracker.Application.DTOs.Category;
using MediatR;

namespace ExpenseTracker.Application.Mediator.Categories.Queries
{
    public record GetCategoryQuery(
        string UserId,
        Guid CategoryId
    ) : IRequest<CategoryDto>;
}
