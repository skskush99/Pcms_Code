using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NextHearing.ServiceBus.NextHearingService;
using NextHearing.ServiceBus.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextHearing.ServiceBus
{
    public static class ServiceExtensions
    {
        public static void AddServiceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUnitOfWorkService, UnitOfWorkService>();
            services.AddTransient<INextHearingService, NextHearingServices>();
        }
    }
}
