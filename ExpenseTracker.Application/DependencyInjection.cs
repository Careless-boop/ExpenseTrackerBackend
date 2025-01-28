using ExpenseTracker.Application.AutoMapper.Profiles;
using ExpenseTracker.Application.Mediator.Transactions.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseTracker.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(typeof(CreateTransactionCommandHandler).Assembly);
            });

            services.AddAutoMapper(typeof(TransactionProfile).Assembly);

            // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            return services;
        }
    }
}
