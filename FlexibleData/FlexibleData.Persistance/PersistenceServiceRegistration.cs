using FlexibleData.Application.Contracts.Persistence;
using FlexibleData.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FlexibleData.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
        {
            //register repositories
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IFlexibleDataRepository, FlexibleDataRepository>();
            services.AddScoped<IStatisticsRepository, StatisticsRepository>();

            return services;
        }
    }
}
