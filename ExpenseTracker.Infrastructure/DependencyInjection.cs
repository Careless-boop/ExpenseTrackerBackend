using ExpenseTracker.Application.Common.Interfaces.Jwt;
using ExpenseTracker.Application.Common.Interfaces.Repositories;
using ExpenseTracker.Infrastructure.Persistense;
using ExpenseTracker.Infrastructure.Persistense.Repositories;
using ExpenseTracker.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseTracker.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ExpenseTrackerContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

            services.AddScoped<IJwtTokenService, JwtTokenService>();
        }
    }
}
