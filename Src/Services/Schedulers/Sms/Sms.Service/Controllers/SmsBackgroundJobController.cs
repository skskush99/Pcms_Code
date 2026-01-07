using Sms.Dto.SmsModel;
using Sms.ServiceBus.UnitOfWork;
using System.Net.Http.Headers;
using System.Text.Json;
using Common.Repository;
using System.Net.Mail;
using System.Net;

namespace Sms.Service.Controllers
{
    public class SmsBackgroundJobController : BackgroundService
    {
        private readonly LogsService _logsService;
        private IConfiguration Configuration;
        private readonly IServiceProvider serviceProvider;
        private readonly IUnitOfWorkService unitOfWorkService;
        public SmsBackgroundJobController(LogsService logsService, IConfiguration _configuration, IServiceProvider serviceProvider, IUnitOfWorkService unitOfWorkService)
        {
            _logsService = logsService;
            Configuration = _configuration;
            this.serviceProvider = serviceProvider;
            this.unitOfWorkService = unitOfWorkService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logsService.Logs("Information", "SmsService", "SmsService started on: " + System.DateTime.Now.ToString("dd-MMM-yyyy HH:mm"));
            Console.WriteLine("SmsService started on: " + System.DateTime.Now.ToString("dd-MMM-yyyy HH:mm"));
            while (!stoppingToken.IsCancellationRequested)
            {
                //Create sms for send
                await ExecuteJobCreateAsync(stoppingToken);
                
                //Created sms send
                await ExecuteJobAsync(stoppingToken);

                // Calculate the time until the next 6:00 AM
                DateTime now = DateTime.Now;
                DateTime nextRun = now.Date.AddDays(1).AddHours(6); // Next day at 6 AM
                if (now.Hour < 6)
                {
                    nextRun = now.Date.AddHours(6); // Today at 6 AM
                }
                TimeSpan delay = nextRun - now;
                _logsService.Logs("Information", "SmsService", $"SmsService will run again in {delay.TotalHours} hours at " + System.DateTime.Now.AddHours(delay.TotalHours).ToString("dd-MMM-yyyy HH:mm"));
                Console.WriteLine($"SmsService will run again in {delay.TotalHours} hours at " + System.DateTime.Now.AddHours(delay.TotalHours).ToString("dd-MMM-yyyy HH:mm"));
                await Task.Delay(delay, stoppingToken);  // Wait until next execution time
            }
            _logsService.Logs("Information", "SmsService", "SmsService stopping on: " + System.DateTime.Now.ToString("dd-MMM-yyyy HH:mm"));
            Console.WriteLine("SmsService stopping on: " + System.DateTime.Now.ToString("dd-MMM-yyyy HH:mm"));
        }

        /// <summary>
        /// Create sms method
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        private async Task ExecuteJobCreateAsync(CancellationToken stoppingToken)
        {
            try
            {
                _logsService.Logs("Information", "SmsService", "SmsCreateService started on: " + System.DateTime.Now.ToString("dd-MMM-yyyy HH:mm"));
                Console.WriteLine("SmsCreateService started on: " + System.DateTime.Now.ToString("dd-MMM-yyyy HH:mm"));
                using (var scope = serviceProvider.CreateScope())
                {
                    var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWorkService>();
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
                _logsService.Logs("Information", "SmsService", "SmsCreateService completed on: " + System.DateTime.Now.ToString("dd-MMM-yyyy HH:mm"));
                Console.WriteLine("SmsCreateService completed on: " + System.DateTime.Now.ToString("dd-MMM-yyyy HH:mm"));
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "SmsService", ex.Message, ex.StackTrace, ex.Source, "SmsServiceScheduler/Sms.Service/SmsBackgroundJobController/ExecuteJobCreateAsync");
                Console.WriteLine("ExecuteJobCreateAsync", "Error", ex.Message);
            }
        }

        /// <summary>
        /// Send sms method
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        private async Task ExecuteJobAsync(CancellationToken stoppingToken)
        {
            try
            {
                _logsService.Logs("Information", "SmsService", "SmsSendService started on: " + System.DateTime.Now.ToString("dd-MMM-yyyy HH:mm"));
                Console.WriteLine("SmsSendService started on: " + System.DateTime.Now.ToString("dd-MMM-yyyy HH:mm"));
                int totalSendSms = 0;
                int totalSms = 0;
                int totalSendEmail = 0;
                int totaldept = 0;
                int totalunit = 0;
                int totaloic = 0;
                int totalsad = 0;
                int totaldeptd = 0;
                int totalcoll = 0;
                int totalnodal = 0;

                if (Convert.ToBoolean(this.Configuration["IsStopSmsSend"]))
                {
                    _logsService.Logs("Information", "SmsService", "SmsSendService setting is stop.");
                    Console.WriteLine("SmsSendService setting is stop.");
                }
                if (Convert.ToBoolean(this.Configuration["IsStopEmailSend"]))
                {
                    _logsService.Logs("Information", "SmsService", "EmailSendService setting is stop.");
                    Console.WriteLine("EmailSendService setting is stop.");
                }
                if (!Convert.ToBoolean(this.Configuration["IsStopSmsSend"]) || !Convert.ToBoolean(this.Configuration["IsStopEmailSend"]))
                {
                    using (var scope = serviceProvider.CreateScope())
                    {
                        string newtext = ". - Justice Department";
                        bool isSent = false;
                        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWorkService>(); // Example
                        var objdata = new SmsRequestModel { Date = DateTime.Now.Date };
                        var smsList = await unitOfWork.SmsService.GetSmsRequestList(objdata);
                        totalSms = smsList.Count;
                        string TemplateID;
                        foreach (var item in smsList)
                        {
                            string Mess = "";
                            if (!string.IsNullOrEmpty(item.Message))
                            {
                                if (item.Message.Length <= 150)
                                {
                                    Mess = item.Message + newtext;
                                }
                                else
                                {
                                    Mess = item.Message + newtext;
                                }
                            }

                            if (Convert.ToBoolean(this.Configuration["IsDevelopment"]))
                            {
                                item.MobileNo = this.Configuration["DevelopmentMobile"] + "";
                                item.EmailId = this.Configuration["DevelopmentEmail"] + "";
                            }

                            if (!string.IsNullOrEmpty(item.MobileNo) && !Convert.ToBoolean(this.Configuration["IsStopSmsSend"]))
                            {
                                string mobile = item.MobileNo.Replace("-", string.Empty);
                                TemplateID = item.TemplateID;
                                try
                                {
                                    isSent = true;//await SendESMSAsync(mobile, Mess, TemplateID);
                                    if (isSent)
                                    {
                                        totalSendSms = totalSendSms + 1;
                                        if (item.RoleId == "2")
                                            totaldept = totaldept + 1;
                                        else if (item.RoleId == "3")
                                            totalunit = totalunit + 1;
                                        else if (item.RoleId == "5")
                                            totaloic = totaloic + 1;
                                        else if (item.RoleId == "6")
                                            totalsad = totalsad + 1;
                                        else if (item.RoleId == "7")
                                            totaldeptd = totaldeptd + 1;
                                        else if (item.RoleId == "13")
                                            totalcoll = totalcoll + 1;
                                        else if (item.RoleId == "8")
                                            totalnodal = totalnodal + 1;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    _logsService.Logs("Error", "SmsService", ex.Message, ex.StackTrace, ex.Source, "SmsServiceScheduler/Sms.Service/SmsBackgroundJobController/ExecuteJobAsync/SendESMSAsync");
                                    Console.WriteLine("ExecuteJobAsync/SendESMSAsync", "Error", ex.Message);
                                }
                            }

                            if (!string.IsNullOrEmpty(item.EmailId) && !Convert.ToBoolean(this.Configuration["IsStopEmailSend"]))
                            {
                                try
                                {
                                    string mailTo = item.EmailId;
                                    string mailSubject = item.ShortDescription;
                                    string mailBody = $"Dear {item.RecieverName ?? "Sir/Madam"},<br /><br />{item.Message}.<br /><br />Thanks & Regards<br />Justice Department<br /><br /><br />*This is an automatically generated email, please do not reply.*<br/>For any further assistance regarding LITES, please contact email-id:-<a href='mailto:justice-deptt@rajasthan.gov.in'>justice-deptt@rajasthan.gov.in</a>";

                                    isSent = await SendEmailAsync(mailTo, mailSubject, mailBody);
                                    if (isSent)
                                        totalSendEmail = totalSendEmail + 1;
                                }
                                catch (Exception ex)
                                {
                                    _logsService.Logs("Error", "SmsService", ex.Message, ex.StackTrace, ex.Source, "SmsServiceScheduler/Sms.Service/SmsBackgroundJobController/ExecuteJobAsync/SendESMSAsync");
                                    Console.WriteLine("ExecuteJobAsync/SendESMSAsync", "Error", ex.Message);
                                }
                            }
                        }
                    }
                    _logsService.Logs("Information", "SmsService", "Sms Email Send Service completed on: " + System.DateTime.Now.ToString("dd-MMM-yyyy HH:mm") + " with send to sms: " + totalSendSms + " and email: " + totalSendEmail + ", in total of :" + totalSms);
                    Console.WriteLine("SmsSendService completed on: " + System.DateTime.Now.ToString("dd-MMM-yyyy HH:mm") + " with send to sms: " + totalSendSms + " and email: " + totalSendEmail + ", in total of :" + totalSms);
                }
                if (!Convert.ToBoolean(this.Configuration["IsStopSmsSend"]))
                {
                    string[] nos = this.Configuration["NOSMobileNo"]?.Split(',') ?? Array.Empty<string>();
                    foreach (var mob_no in nos)
                    {
                        if (!string.IsNullOrEmpty(mob_no) && Convert.ToString(mob_no).Length == 10)
                            await SendESMSAsync(mob_no, "Total SMS Sent Today from LITES are: " + totalSendSms + ".1)Dept:" + totaldept + ",2)Unit:" + totalunit + ",3)OIC:" + totaloic + ",4)SAD:" + totalsad + ",5)DeptD:" + totaldeptd + ",6)NodalAdmin:" + totalnodal + ",7)Dist.Collector:" + totalcoll + ". - Justice Department", "1707160500349109332");
                    }
                }

            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "SmsService", ex.Message, ex.StackTrace, ex.Source, "SmsServiceScheduler/Sms.Service/SmsBackgroundJobController/ExecuteJobAsync");
                Console.WriteLine("ExecuteJobAsync", "Error", ex.Message);
            }
        }

        /// <summary>
        /// Send sms method
        /// </summary>
        /// <param name="mobileNo"></param>
        /// <param name="message"></param>
        /// <param name="TemplateID"></param>
        /// <returns></returns>
        public static async Task<bool> SendESMSAsync(string mobileNo, string message, string TemplateID)
        {
            try
            {
                using var client = new HttpClient();
                client.BaseAddress = new Uri("https://api.sewadwaar.rajasthan.gov.in/app/live/eSanchar/Prod/api/OBD/CreateSMS/Request?client_id=77aa51df-1b37-4f61-a334-d750c8a07f0c");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("username", "LitesSMS");
                client.DefaultRequestHeaders.Add("password", "Just#Lit@s03");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var inputparams = new ExternalsmsApiInfo
                {
                    UniqueID = "LITES_SMS",
                    serviceName = "justice_sms",
                    language = "ENG",
                    message = message,
                    TemplateID = TemplateID,
                    mobileNo = new List<string> { mobileNo } // Use List<string> directly
                };

                // Serialize to JSON using System.Text.Json (recommended)
                var json = JsonSerializer.Serialize(inputparams);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json"); // Specify encoding and content type

                using var response = await client.PostAsync(client.BaseAddress, content); // Use PostAsync
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Response: " + result); // Improved logging
                    return true;
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync(); // Get error details
                    Console.WriteLine($"Error: {response.StatusCode} - {errorContent}"); // Log the error
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("SMSDispatcher SendESMS Error: " + ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Send email method
        /// </summary>
        /// <param name="mailTo"></param>
        /// <param name="mailSubject"></param>
        /// <param name="mailBody"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public async Task<bool> SendEmailAsync(string mailTo, string mailSubject, string mailBody, string filePath="")
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    using (SmtpClient smtpClient = new SmtpClient())
                    {
                        // Configuration from appsettings.json or user secrets
                        smtpClient.Host = this.Configuration["Smtp:Host"];
                        smtpClient.Port = int.Parse(Configuration["Smtp:Port"]);
                        if (bool.Parse(Configuration["Smtp:EnableSsl"]) == true)
                        {
                            smtpClient.EnableSsl = bool.Parse(Configuration["Smtp:EnableSsl"]);
                        }

                        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtpClient.UseDefaultCredentials = false;

                        // Credentials
                        smtpClient.Credentials = new NetworkCredential(
                            Configuration["Smtp:Username"],
                            Configuration["Smtp:Password"]
                        );

                        mail.From = new MailAddress(Configuration["Smtp:FromAddress"]);
                        mail.To.Add(mailTo);
                        mail.Subject = mailSubject;
                        mail.IsBodyHtml = true;
                        mail.Body = mailBody;

                        // Attachment
                        if (!string.IsNullOrEmpty(filePath))
                        {
                            Attachment attachment = new Attachment(filePath);
                            mail.Attachments.Add(attachment);
                        }
                        await smtpClient.SendMailAsync(mail);

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
            return false;
        }
    }
}
