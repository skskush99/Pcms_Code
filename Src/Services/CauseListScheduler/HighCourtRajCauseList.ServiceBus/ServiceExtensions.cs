using HighCourtRajCauseList.ServiceBus.HighCourtRajCauseList;
using HighCourtRajCauseList.ServiceBus.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighCourtRajCauseList.ServiceBus
{
    public static class ServiceExtensions
    {
        public static void AddServiceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUnitOfWorkService, UnitOfWorkService>();
            services.AddTransient<IHighCourtRajCauseListService, HighCourtRajCauseListService>();
        }
    }
}
