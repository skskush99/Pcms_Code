using Common.Repository;
using NextHearing.Service.Controllers;
using NextHearing.ServiceBus.NextHearingService;
using NextHearing.ServiceBus.UnitOfWork;

namespace NextHearing.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.WebHost.UseUrls("http://localhost:63064", "https://localhost:63063");
            builder.Host.UseWindowsService();

            builder.Services.AddControllers();
            builder.Services.AddTransient<LogsService>();
            builder.Services.AddTransient<INextHearingService, NextHearingServices>();
            builder.Services.AddTransient<IUnitOfWorkService, UnitOfWorkService>();
            builder.Services.AddHostedService<NextHearingBackgroundJobController>();

            var app = builder.Build();

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
