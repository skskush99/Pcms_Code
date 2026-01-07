using Common.Repository;
using Sms.Service.Controllers;
using Sms.ServiceBus.SmsService;
using Sms.ServiceBus.UnitOfWork;

namespace Sms.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.WebHost.UseUrls("http://localhost:5121", "https://localhost:7049");
            builder.Host.UseWindowsService();
            builder.Services.AddControllers();
            builder.Services.AddTransient<LogsService>();
            builder.Services.AddTransient<ISmsService, SmsServices>();
            builder.Services.AddTransient<IUnitOfWorkService, UnitOfWorkService>();
            builder.Services.AddHostedService<SmsBackgroundJobController>();
            var app = builder.Build();
            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
