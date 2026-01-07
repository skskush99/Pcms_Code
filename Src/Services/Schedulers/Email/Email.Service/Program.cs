using Email.Service.Controllers;
using Email.ServiceBus.EmailService;
using Email.ServiceBus.UnitOfWork;

namespace Email.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Host.UseWindowsService();
            builder.Services.AddControllers();
            builder.Services.AddTransient<IEmailServices, EmailServices>();
            builder.Services.AddTransient<IUnitOfWorkService, UnitOfWorkService>();
            builder.Services.AddHostedService<EmailBackgroundJobController>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
