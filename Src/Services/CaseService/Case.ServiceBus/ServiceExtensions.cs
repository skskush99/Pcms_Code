using Case.ServiceBus.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Case.ServiceBus.CaseDecision;
using Case.ServiceBus.CaseFileRegister;
using Case.ServiceBus.CaseHearings;
using Case.ServiceBus.CaseRegistrations;
using Case.ServiceBus.CasesDecidedOnIstHearing;
using Case.ServiceBus.DierRegistrationsService;

namespace Case.ServiceBus
{
    public static class ServiceExtensions
    {
        public static void AddServiceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUnitOfWorkService, UnitOfWorkService>();
            services.AddTransient<ICaseDecisionServiceBus, CaseDecisionServiceBus>();
            services.AddTransient<ICaseFileRegisterServiceBus, CaseFileRegisterServiceBus>();
            services.AddTransient<ICaseHearingsServiceBus, CaseHearingsServiceBus>();
            services.AddTransient<ICaseRegistrationsServiceBus, CaseRegistrationsServiceBus>();            
            services.AddTransient<ICasesDecidedOnIstHearingServiceBus, CasesDecidedOnIstHearingServiceBus>();
            services.AddTransient<IDierRegistrationsServiceBus, DierRegistrationsServiceBus>();
        }
    }
}
