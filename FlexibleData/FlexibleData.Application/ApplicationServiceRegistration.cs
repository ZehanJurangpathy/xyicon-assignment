using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FlexibleData.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, string connecitonString)
        {
            //register MediatR library
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            //register auto mapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddHangfire(cfg => cfg
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(connecitonString));

            services.AddHangfireServer();

            return services;
        }
    }
}
