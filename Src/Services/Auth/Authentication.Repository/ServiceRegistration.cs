using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Authentication.Repository.UserToken;
using Authentication.Repository.UnitOfwork;
using Authentication.Repository.Esign;
using Authentication.Repository.DropDowns;

namespace Authentication.Repository
{
    public static class ServiceRegistration
    {
        public static void AddRepositoryInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUserLogin, UserLoginRepository>();
            services.AddTransient<IUnitOfWorkRepository, UnitOfWorkRepository>();
            services.AddTransient<IEsignRepository, EsignRepository>();
            services.AddTransient<IDropDowns, DropDownsRepositoty>();
        }
    }
}
