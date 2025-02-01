using MediatR;

namespace ExpenseTracker.Application.Mediator.Categories.Commands
{
    public record CreateCategoryCommand(
        string UserId,
        string Name
    ) : IRequest<Guid>;
}
