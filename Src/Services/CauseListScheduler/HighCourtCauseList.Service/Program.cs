using Common.Repository;
using HighCourtCauseList.Service.Controllers;
using HighCourtRajCauseList.ServiceBus.HighCourtRajCauseList;
using HighCourtRajCauseList.ServiceBus.UnitOfWork;

namespace HighCourtCauseList.Service;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.WebHost.UseUrls("http://localhost:58737", "https://localhost:58736");
        // Add services to the container.
        builder.Host.UseWindowsService();
        builder.Services.AddControllers();
        builder.Services.AddTransient<LogsService>();
        builder.Services.AddTransient<IHighCourtRajCauseListService, HighCourtRajCauseListService>();
        builder.Services.AddTransient<IUnitOfWorkService, UnitOfWorkService>();
        builder.Services.AddHostedService<HighCourtCauseListBackgroundJob>(); // The background task

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
