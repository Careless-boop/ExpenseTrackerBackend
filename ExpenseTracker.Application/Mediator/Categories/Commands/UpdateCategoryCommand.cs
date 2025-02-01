using MediatR;

namespace ExpenseTracker.Application.Mediator.Categories.Commands
{
    public record UpdateCategoryCommand(
        string UserId,
        Guid CategoryId,
        string Name
    ) : IRequest<Guid>;
}
