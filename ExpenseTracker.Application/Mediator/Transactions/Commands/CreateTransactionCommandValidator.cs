using FluentValidation;

namespace ExpenseTracker.Application.Mediator.Transactions.Commands
{
    public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
    {
        public CreateTransactionCommandValidator()
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage("Amount must be greater than 0");
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User is required");
        }
    }
}
