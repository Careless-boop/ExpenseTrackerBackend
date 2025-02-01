using ExpenseTracker.Application.DTOs.Category;
using MediatR;

namespace ExpenseTracker.Application.Mediator.Categories.Queries
{
    public record GetAllCategoriesQuery(string UserId) : IRequest<IEnumerable<CategoryDto>>;
}
