using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using CrewInfo.Persistence.Interfaces.Repositories;
using CrewInfo.Persistence.Repositories;

namespace CrewInfo.Persistence
{
    public static class PersistenceExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services
                , IConfiguration configuration)
        {
            services.AddDbContext<CrewInfoDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString(nameof(CrewInfoDbContext)));
            });

            services.AddScoped<IPilotRepository, PilotRepository>();
            services.AddScoped<IStewardRepository, StewardRepository>();

            return services;
        }
    }
}
