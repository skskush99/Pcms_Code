using Sms.Sender.Controllers;
using Sms.ServiceBus.SmsService;
using Sms.ServiceBus.UnitOfWork;

namespace Sms.Sender
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.UseWindowsService();
            builder.Services.AddControllers();
            builder.Services.AddTransient<ISmsService, SmsServices>();
            builder.Services.AddTransient<IUnitOfWorkService, UnitOfWorkService>();
            builder.Services.AddHostedService<SmsCreateBackgroundJobController>();
            var app = builder.Build();
            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
