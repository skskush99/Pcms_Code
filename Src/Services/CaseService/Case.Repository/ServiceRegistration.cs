using Case.Repository.UnitOfwork;
using Common.Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Case.Repository.CaseDecision;
using Case.Repository.CaseFileRegister;
using Case.Repository.CaseHearings;
using Case.Repository.CaseRegistrations;
using Case.Repository.CasesDecidedOnIstHearing;
using Case.Repository.DierRegistrations;


namespace Case.Repository
{
    public static class ServiceRegistration
    {
        public static void AddRepositoryInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region Repository
            services.AddTransient<IGenericRepository, SqlRepository>();
            services.AddTransient<IUnitOfWorkRepository, UnitOfWorkRepository>();
            services.AddTransient<ICaseDecisionRepository, CaseDecisionRepository>();
            services.AddTransient<ICaseFileRegisterRepository, CaseFileRegisterRepository>();
            services.AddTransient<ICaseHearingsRepository, CaseHearingsRepository>();
            services.AddTransient<ICaseRegistrationsRepository, CaseRegistrationsRepository>();
            services.AddTransient<ICasesDecidedOnIstHearingRepository, CasesDecidedOnIstHearingRepository>();
            services.AddTransient<IDierRegistrationsRepository, DierRegistrationsRepository>();
            #endregion Repository
        }
    }
}
