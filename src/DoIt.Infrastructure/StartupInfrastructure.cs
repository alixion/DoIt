using DoIt.Domain.Common;
using DoIt.Infrastructure.Data;
using DoIt.Infrastructure.Data.EfRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DoIt.Infrastructure
{
    public static class StartupInfrastructure
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext(connectionString);
            services.AddScoped(typeof(IRepository<,>), typeof(EfRepository<,>));
            return services;
        }
        
        private static IServiceCollection AddDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DoItDbContext>(options =>
            {
                options
                    .UseNpgsql(connectionString)
                    .UseSnakeCaseNamingConvention();
            });
            return services;
        }
    }
}