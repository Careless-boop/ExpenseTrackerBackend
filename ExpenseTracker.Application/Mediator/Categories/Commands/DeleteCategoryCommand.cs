using MediatR;

namespace ExpenseTracker.Application.Mediator.Categories.Commands
{
    public record DeleteCategoryCommand(
        string UserId,
        Guid CategoryId
    ) : IRequest<Unit>;
}
