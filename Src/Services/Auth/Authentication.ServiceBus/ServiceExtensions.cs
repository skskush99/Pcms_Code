using Authentication.ServiceBus.Esign;
using Authentication.ServiceBus.UnitOfWork;
using Authentication.ServiceBus.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Authentication.ServiceBus.DropDownsService;

namespace Authentication.ServiceBus
{
    public static class ServiceExtensions
    {
        public static void AddServiceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUnitOfWorkService, UnitOfWorkService>();
            services.AddTransient<IUserLoginServiceBus, UserLoginServiceBus>();
            services.AddTransient<IEsignServiceBus, EsignServiceBus>();
            services.AddTransient<IDropDownsServiceBus, DropDownsServiceBus>();
        }
    }
}
