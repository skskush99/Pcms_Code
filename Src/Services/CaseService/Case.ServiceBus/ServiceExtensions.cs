using Case.ServiceBus.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Case.ServiceBus.DierRegistrationsService;
using Case.ServiceBus.ComplaintRegisterService;
using Case.ServiceBus.DierRegistrations_NewService;

namespace Case.ServiceBus
{
    public static class ServiceExtensions
    {
        public static void AddServiceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUnitOfWorkService, UnitOfWorkService>();
            services.AddTransient<IDierRegistrationsServiceBus, DierRegistrationsServiceBus>();
            services.AddTransient<IComplaintRegisterServiceBus, ComplaintRegisterServiceBus>();
            services.AddTransient<IDierRegistrations_NewServiceBus, DierRegistrations_NewServiceBus>();
        }
    }
}
