using Case.Repository.UnitOfwork;
using Common.Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Case.Repository.DierRegistrations;
using Case.Repository.ComplaintRegister;


namespace Case.Repository
{
    public static class ServiceRegistration
    {
        public static void AddRepositoryInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region Repository
            services.AddTransient<IGenericRepository, SqlRepository>();
            services.AddTransient<IUnitOfWorkRepository, UnitOfWorkRepository>();
            services.AddTransient<IDierRegistrationsRepository, DierRegistrationsRepository>();
            services.AddTransient<IComplaintRegisterRepository, ComplaintRegisterRepository>();
            #endregion Repository
        }
    }
}
