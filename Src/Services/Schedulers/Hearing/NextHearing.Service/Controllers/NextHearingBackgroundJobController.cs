using Common.Repository;
using Newtonsoft.Json.Linq;
using NextHearing.ServiceBus.UnitOfWork;
namespace NextHearing.Service.Controllers
{
    public class NextHearingBackgroundJobController : BackgroundService
    {
        private readonly LogsService _logsService;
        private IConfiguration Configuration;
        private readonly IServiceProvider serviceProvider;
        private readonly IUnitOfWorkService unitOfWorkService;
        public NextHearingBackgroundJobController(IConfiguration _configuration, IServiceProvider serviceProvider, IUnitOfWorkService unitOfWorkService, LogsService logsService)
        {
            _logsService = logsService;
            Configuration = _configuration;
            this.serviceProvider = serviceProvider;
            this.unitOfWorkService = unitOfWorkService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                _logsService.Logs("Information", "NextHearingScheduler", "NextHearingScheduler service started on: " + System.DateTime.Now.ToString("dd-MMM-yyyy HH:mm"));
                Console.WriteLine("NextHearingScheduler service started on: " + System.DateTime.Now.ToString("dd-MMM-yyyy HH:mm"));
                while (!stoppingToken.IsCancellationRequested)
                {
                    await ExecuteJobAsync(stoppingToken);

                    DateTime now = DateTime.Now;
                    DateTime nextRun = now.Date.AddDays(1).AddHours(18).AddMinutes(0); // Next day at 6:30 PM (18:30 in 24-hour format)
                    if (now.Hour < 18 || (now.Hour == 18 && now.Minute < 0))
                    {
                        nextRun = now.Date.AddHours(18).AddMinutes(0); // Today at 6:30 PM
                    }
                    TimeSpan delay = nextRun - now;
                    _logsService.Logs("Information", "NextHearingScheduler", $"NextHearingScheduler service will run again in {delay.TotalHours} hours on "+ System.DateTime.Now.AddHours(delay.TotalHours).ToString("dd-MMM-yyyy HH:mm"));
                    Console.WriteLine($"NextHearingScheduler service will run again in {delay.TotalHours} hours on " + System.DateTime.Now.AddHours(delay.TotalHours).ToString("dd-MMM-yyyy HH:mm"));
                    await Task.Delay(delay, stoppingToken);  // Wait until next execution time
                }
                _logsService.Logs("Information", "NextHearingScheduler", "NextHearingScheduler service stopping on: " + System.DateTime.Now.ToString("dd-MMM-yyyy HH:mm"));
                Console.WriteLine("NextHearingScheduler service stopping on: " + System.DateTime.Now.ToString("dd-MMM-yyyy HH:mm"));
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "NextHearingScheduler", ex.Message, ex.StackTrace, ex.Source, "NextHearingScheduler/NextHearing.Service/NextHearingBackgroundJob/ExecuteAsync");
            }
        }

        private async Task ExecuteJobAsync(CancellationToken stoppingToken)
        {
            try
            {
                _logsService.Logs("Information", "NextHearingScheduler", "NextHearingScheduler service started on: " + System.DateTime.Now.ToString("dd-MMM-yyyy HH:mm"));
                Console.WriteLine("NextHearingScheduler service started on: " + System.DateTime.Now.ToString("dd-MMM-yyyy HH:mm"));
                
                using (var scope = serviceProvider.CreateScope())
                {
                   var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWorkService>(); // Example
                   var cnrlist = await unitOfWork.NextHearingService.GetNextHearingListforUpdate();
                   try
                    {
                        if (cnrlist != null)
                        {
                            foreach (var item in cnrlist)
                            {
                                string nextHearingDate = await GetCnrData(unitOfWork, item.CRNNumber, item.CaseId, item.Hearing_SNo);
                                if (!string.IsNullOrEmpty(nextHearingDate))
                                {
                                    try
                                    {
                                        DateTime nextHearingDatetime = Convert.ToDateTime(nextHearingDate);
                                        if (nextHearingDatetime > DateTime.Now)
                                        {
                                            try
                                            {
                                                var result = await unitOfWork.NextHearingService.UpdateDecideDateUsingCNR1(item.CRNNumber, nextHearingDate, item.CaseId, item.Hearing_SNo);
                                                Console.WriteLine("CNR No: "+ item.CRNNumber + " - Update");
                                            }
                                            catch (Exception ex)
                                            {
                                                _logsService.Logs("Error", "NextHearingScheduler", ex.Message, ex.StackTrace, ex.Source, "NextHearingScheduler/NextHearing.Service/NextHearingBackgroundJob/ExecuteJobAsync");
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        _logsService.Logs("Error", "NextHearingScheduler", ex.Message, ex.StackTrace, ex.Source, "NextHearingScheduler/NextHearing.Service/NextHearingBackgroundJob/ExecuteJobAsync");
                                        Console.WriteLine("usp_UpdateNextHearing_UsingCNR", "Error", item.CRNNumber + " records not updated.- Date conversion Error " + ex.InnerException.Message);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logsService.Logs("Error", "NextHearingScheduler", ex.Message, ex.StackTrace, ex.Source, "NextHearingScheduler/NextHearing.Service/NextHearingBackgroundJob/ExecuteJobAsync");
                        Console.WriteLine("ExecuteJobAsync records not updated. Error: " + ex.Message);
                    }
                }
                
                _logsService.Logs("Information", "NextHearingScheduler", "DailyJob execution completed on: " + System.DateTime.Now.ToString("dd-MMM-yyyy HH:mm"));
                Console.WriteLine("DailyJob execution completed on: " + System.DateTime.Now.ToString("dd-MMM-yyyy HH:mm"));
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "NextHearingScheduler", ex.Message, ex.StackTrace, ex.Source, "NextHearingScheduler/NextHearing.Service/NextHearingBackgroundJob/ExecuteJobAsync");
                Console.WriteLine("usp_NextHearingListForUpdate Error: " + ex.Message);
            }
        }

        public async Task<string> GetCnrData(IUnitOfWorkService unitOfWork, string cnr, Int64 caseid, int Hearing_SNo)
        {
            Console.WriteLine("CNR No: " + cnr + " - Check");
            var result = "";
            try
            {
                string apiUrl = this.Configuration["apiUrl"] + cnr;
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = client.GetAsync(apiUrl).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var data = response.Content.ReadAsStringAsync().Result;
                        dynamic details = JObject.Parse(data);
                        var pend_disp = (string)(details.SelectToken("data.pend_disp") ?? "");
                        var date_of_decision = (string)(details.SelectToken("data.date_of_decision") ?? "");
                        var date_next_list = (string)(details.SelectToken("data.date_next_list") ?? "");

                        //Old Code///if (pend_disp == "D" && !string.IsNullOrEmpty(date_of_decision))
                        if ((pend_disp == "D" && !string.IsNullOrEmpty(date_of_decision)) /*|| (date_of_decision == date_next_list)*/)
                        {
                            string DecidedDate = date_of_decision;
                            var ok = await unitOfWork.NextHearingService.UpdateDecideDateUsingCNR(cnr, DecidedDate, caseid, 1);
                            Console.WriteLine("CNR No: " + cnr + " - Decide");
                        }
                        else if ((pend_disp == "D" && !string.IsNullOrEmpty(date_of_decision)) && !string.IsNullOrEmpty(date_next_list) && (date_of_decision == date_next_list))
                        {
                            string DecidedDate = date_of_decision;
                            var ok = await unitOfWork.NextHearingService.UpdateDecideDateUsingCNR(cnr, DecidedDate, caseid, 1);
                            Console.WriteLine("CNR No: " + cnr + " - Decide");
                        }
                        //Old Code///else if (!string.IsNullOrEmpty(date_next_list))
                        else if (!string.IsNullOrEmpty(date_next_list))
                        {
                            result = date_next_list;
                            return result;
                        }
                        else if ((!string.IsNullOrEmpty(date_next_list) && !string.IsNullOrEmpty(date_of_decision)) && (date_of_decision != date_next_list))
                        {
                            // EventLog.WriteError("check result", "check", details["date_next_list"]);
                            result = date_next_list;
                            return result;
                        }
                        return "";
                    }
                    else
                    {
                        return "";
                    }
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "NextHearingScheduler", ex.Message, ex.StackTrace, ex.Source, "NextHearingScheduler/NextHearing.Service/NextHearingBackgroundJob/GetCnrData");
                Console.WriteLine("Error: " + ex.Message);
                return "";
            }
        }
    }
}
