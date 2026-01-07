using ClosedXML.Excel;
using Email.Dto.Email;
using Email.ServiceBus.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Globalization;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace Email.Service.Controllers
{
    public class EmailBackgroundJobController : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider serviceProvider;
        private readonly IUnitOfWorkService unitOfWorkService;
        public EmailBackgroundJobController(IServiceProvider serviceProvider,
            IUnitOfWorkService unitOfWorkService,
            IConfiguration configuration)
        {
            this.serviceProvider = serviceProvider;
            this.unitOfWorkService = unitOfWorkService;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("DailyJob is starting.");

            while (!stoppingToken.IsCancellationRequested)
            {
                await ExecuteJobAsync(stoppingToken);

                // Calculate the time until the next 8:00 AM
                DateTime now = DateTime.Now;
                DateTime nextRun = now.Date.AddDays(1).AddHours(8); // Next day at 8 AM
                if (now.Hour < 8)
                {
                    nextRun = now.Date.AddHours(8); // Today at 8 AM
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
                        await unitOfWork.EmailServices.RunEmailSender();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error : Email Entry Not Today" + ex.ToString());
                    }

                    DateTime date = DateTime.Now.Date;
                    string dateString = date.ToString("yyyy-MM-dd");
                    IFormatProvider culture = new CultureInfo("en-US", true);
                    DateTime dateVal = DateTime.ParseExact(dateString, "yyyy-MM-dd", culture);
                    try
                    {
                        var emailData1 = unitOfWork.EmailServices.GetDistinctNodalEmailAsync(dateString);
                        var emailData = await unitOfWork.EmailServices.GetNodalEmailList(dateVal);
                        var subData = await unitOfWork.EmailServices.GetSubjectList(dateVal);
                        var roleData = await unitOfWork.EmailServices.GetRoleList(dateVal);

                        if (emailData.Status && subData.Status && roleData.Status)
                        {
                            var emailid = emailData?.Data;
                            var sub = subData?.Data;
                            var role = roleData?.Data;

                            for (int it = 0; it < emailid.ToList().Count; it++)
                            {
                                string email = Convert.ToString(emailid.ToList()[it]);
                                for (int irole = 0; irole < role.ToList().Count; irole++)
                                {
                                    string ro = Convert.ToString(role.ToList()[irole]);
                                    for (int s = 0; s < sub.ToList().Count; s++)
                                    {
                                        string subj = Convert.ToString(sub.ToList()[s]);
                                        var result = await unitOfWork.EmailServices.GetEmailHistoryData(dateVal, Convert.ToString(email), Convert.ToString(ro), Convert.ToString(subj));
                                        if (result.Count > 0)
                                        {
                                            var dt = new DataTable("Email_History");
                                            dt.Columns.Add("Administrative Deptt. Name", typeof(string));
                                            dt.Columns.Add("Unit/HOD Name", typeof(string));
                                            dt.Columns.Add("Office Name", typeof(string));
                                            dt.Columns.Add("Abbreviation/Case No./Year", typeof(string));
                                            dt.Columns.Add("Court Name & Place", typeof(string));
                                            dt.Columns.Add("Appealant Name & Designation", typeof(string));
                                            dt.Columns.Add("Respondent Name & Designation", typeof(string));
                                            dt.Columns.Add("Lawyer's Name & MobileNo.", typeof(string));
                                            dt.Columns.Add("OIC Name & MobileNo.", typeof(string));
                                            dt.Columns.Add("Case Priority", typeof(string));
                                            dt.Columns.Add("Decision/Hearing Date", typeof(string));
                                            dt.Columns.Add("Status", typeof(string));
                                            dt.Columns.Add("Reply Filed", typeof(string));
                                            dt.Columns.Add("Decision Date", typeof(string));
                                            dt.Columns.Add("Next Hearing Date", typeof(string));
                                            dt.Columns.Add("Case Registration Date", typeof(string));
                                            dt.Columns.Add("Role", typeof(string));
                                            string subject = "";
                                            foreach (var item in result)
                                            {
                                                DataRow row = dt.NewRow();
                                                row["Administrative Deptt. Name"] = item.AdmDeptName;
                                                row["Unit/HOD Name"] = item.UnitName;
                                                row["Office Name"] = item.OfficeName;
                                                row["Abbreviation/Case No./Year"] = item.AbCaseNoYear;
                                                row["Court Name & Place"] = item.CourtNamePlace;
                                                row["Appealant Name & Designation"] = item.AppealantDesg;
                                                row["Respondent Name & Designation"] = item.RespondentDesg;
                                                row["Lawyer's Name & MobileNo."] = item.LawyersMobileNo;
                                                row["OIC Name & MobileNo."] = item.OICNameMobileNo;
                                                row["Case Priority"] = item.PriorityName;
                                                row["Decision/Hearing Date"] = item.DecisionHearing;
                                                row["Status"] = item.Status;
                                                row["Reply Filed"] = item.ReplyFiled;
                                                row["Decision Date"] = item.DecisionDate;
                                                row["Next Hearing Date"] = item.NextHearing_Date;
                                                row["Case Registration Date"] = item.CaseRegistrationDate;
                                                row["Role"] = item.Role;
                                                subject = item.Subject;
                                                dt.Rows.Add(row);
                                            }
                                            if (dt.Rows.Count > 0)
                                            {
                                                await ExportDataSetToExcelAsync(dt, ro, s, dateString, "maheshsaini0020@gmail.com", subject); // Replace with actual email
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("InLoop", "Error", " records not updated. " + ex.Message);
                    }                                                                                                                             // 3. Resolve Dependency

                }

                Console.WriteLine("DailyJob execution completed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("", "Error", ex.Message);
                // Consider adding retry logic or other error handling here.
            }
        }

        private async Task ExportDataSetToExcelAsync(DataTable dataTable, string role,int count,string date, string email, string subject)
        {
            try
            {
                // Save the Excel file
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "ExcelExports");
                string newdate = date;
                string path = "\\DataFile" + role + count + email + ".xlsx";
                string file = folderPath + "\\ExcelFiles\\" + newdate + path;
                var fileName = $"EmailHistory_{role}_{subject}_{date}.xlsx";
                // Customize path as needed
                Directory.CreateDirectory(folderPath); // Ensure directory exists
                var filePath = Path.Combine(folderPath, fileName);

                using (var workbook = new XLWorkbook()) // Using statement ensures disposal
                {
                    var worksheet = workbook.Worksheets.Add("Sheet1");

                    // Add headers
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        worksheet.Cell(1, i + 1).Value = dataTable.Columns[i].ColumnName;
                    }

                    // Add data rows
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataTable.Columns.Count; j++)
                        {
                            worksheet.Cell(i + 2, j + 1).Value = (string)dataTable.Rows[i][j];
                        }
                    }
                    worksheet.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Style.Font.Bold = true;

                    using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    {
                        workbook.SaveAs(stream); //Save the file to the stream
                    }
                } // The workbook closes and disposes here.

                // Now, attach the file to the email (send the email)
                await SendEmailAsync(email, subject, filePath);
            }
            catch (Exception ex)
            {

            }

        }

        public async Task SendEmailAsync(string mailTo, string mailSubject, string filePath)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    using (SmtpClient smtpClient = new SmtpClient())
                    {
                        // Configuration from appsettings.json or user secrets
                        var data = _configuration.GetSection("Smtp");
                        smtpClient.Host = _configuration["Smtp:Host"];
                        smtpClient.Port = int.Parse(_configuration["Smtp:Port"]);
                        if (bool.Parse(_configuration["Smtp:EnableSsl"]) == true)
                        {
                            smtpClient.EnableSsl = bool.Parse(_configuration["Smtp:EnableSsl"]);
                        }

                        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtpClient.UseDefaultCredentials = false;

                        // Credentials
                        smtpClient.Credentials = new NetworkCredential(
                            _configuration["Smtp:Username"],
                            _configuration["Smtp:Password"]
                        );

                        mail.From = new MailAddress(_configuration["Smtp:FromAddress"]);
                        mail.To.Add(mailTo);
                        mail.Subject = mailSubject;
                        mail.IsBodyHtml = true;

                        string body = $"Dear Sir/Madam,<br /><br />Please find the attached details of {mailSubject}.<br /><br /> *This is an automatically generated email, please do not reply.*<br/><br/>For any further assistance regarding LITES, please contact email-id:-<a href='mailto:justice-deptt@rajasthan.gov.in'>justice-deptt@rajasthan.gov.in</a>";
                        mail.Body = body;

                        // Attachment
                        Attachment attachment = new Attachment(filePath);
                        attachment.ContentDisposition.FileName = $"LITES Alert {DateTime.Now:ddMMyyyy}.xlsx";
                        mail.Attachments.Add(attachment);

                        await smtpClient.SendMailAsync(mail);

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }
    }
}
