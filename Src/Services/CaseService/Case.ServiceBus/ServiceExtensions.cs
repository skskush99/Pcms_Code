using Case.ServiceBus.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Case.ServiceBus.DierRegistrationsService;

namespace Case.ServiceBus
{
    public static class ServiceExtensions
    {
        public static void AddServiceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUnitOfWorkService, UnitOfWorkService>();
            services.AddTransient<IDierRegistrationsServiceBus, DierRegistrationsServiceBus>();
        }
    }
}
