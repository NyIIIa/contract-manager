using ContractManager.Domain.Common;
using ContractManager.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ContractManager.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            return services.AddPersistence(configuration);
        }   
        
        private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseConnectionString = configuration.GetConnectionString("Database");
            Ensure.NotNullOrEmpty(databaseConnectionString);
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(databaseConnectionString));
            
            return services;
        }
    }
}