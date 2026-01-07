using Microsoft.AspNetCore.Mvc;
using Sms.ServiceBus.UnitOfWork;

namespace Sms.Sender.Controllers
{
    public class SmsCreateBackgroundJobController : BackgroundService
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IUnitOfWorkService unitOfWorkService;
        public SmsCreateBackgroundJobController(IServiceProvider serviceProvider,
            IUnitOfWorkService unitOfWorkService)
        {
            this.serviceProvider = serviceProvider;
            this.unitOfWorkService = unitOfWorkService;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("DailyJob is starting.");

            while (!stoppingToken.IsCancellationRequested)
            {
                await ExecuteJobAsync(stoppingToken);

                // Calculate the time until the next 6:00 AM
                DateTime now = DateTime.Now;
                DateTime nextRun = now.Date.AddDays(1).AddHours(6); // Next day at 6 AM
                if (now.Hour < 6)
                {
                    nextRun = now.Date.AddHours(6); // Today at 6 AM
                }
                TimeSpan delay = nextRun - now;

                //DateTime now = DateTime.Now;
                //DateTime nextRun = now.Date.AddDays(1).AddHours(10).AddMinutes(30); // Next day at 6:30 PM (18:30 in 24-hour format)

                //if (now.Hour < 10 || (now.Hour == 10 && now.Minute < 30))
                //{
                //    nextRun = now.Date.AddHours(10).AddMinutes(30); // Today at 6:30 PM
                //}

                //TimeSpan delay = nextRun - now;

                //DateTime now = DateTime.Now;
                //DateTime nextRun = now.Date.AddMinutes(05); 

                //if (now.Minute < 05)
                //{
                //    nextRun = now.Date.AddMinutes(05); 
                //}

                //TimeSpan delay = nextRun - now;

                Console.WriteLine($"DailyJob will run again in {delay.TotalHours} hours.");

                await Task.Delay(delay, stoppingToken);  // Wait until next execution time
            }

            Console.WriteLine("DailyJob is stopping.");
        }

        private async Task ExecuteJobAsync(CancellationToken stoppingToken)
        {
            try
            {
                Console.WriteLine("DailyJob is executing.");

                // *** YOUR JOB LOGIC GOES HERE ***
                // Use _serviceProvider to resolve dependencies if needed.
                using (var scope = serviceProvider.CreateScope())
                {
                    var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWorkService>(); // Example

                    try
                    {
                        await unitOfWork.SmsService.RunSmsSender();
                        await unitOfWork.SmsService.RunSmsSenderNodalOfficer();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error : SMS Entry Not Today" + ex.ToString());
                    }
                }

                Console.WriteLine("DailyJob execution completed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                // Consider adding retry logic or other error handling here.
            }
        }
    }
}
