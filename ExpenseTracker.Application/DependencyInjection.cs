using ExpenseTracker.Application.AutoMapper.Profiles;
using ExpenseTracker.Application.Behaviors;
using ExpenseTracker.Application.Mediator.Transactions.Commands;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseTracker.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(typeof(CreateTransactionCommandHandler).Assembly);
            });

            services.AddAutoMapper(typeof(TransactionProfile).Assembly);

            services.AddValidatorsFromAssembly(typeof(CreateTransactionCommand).Assembly);

            services.AddTransient(
                typeof(IPipelineBehavior<,>),
                typeof(ValidationBehavior<,>));

            // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        }
    }
}
