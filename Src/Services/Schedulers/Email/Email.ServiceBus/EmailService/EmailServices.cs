using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using Common.Dapper;
using Dapper;
using System.Data;
using Email.Dto.Email;
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;
using System.Reflection.PortableExecutable;

namespace Email.ServiceBus.EmailService
{
    public class EmailServices : SqlRepository, IEmailServices
    {
        private readonly IConfiguration _configuration;
        private readonly System.Data.IDbConnection Con;
        public EmailServices(IConfiguration Configuration) : base(Configuration)
        {
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
                        if (bool.Parse(_configuration["Smtp:EnableSsl"])==true) {
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

        public async Task RunEmailSender()
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    var objData = await Con.QueryAsync("sp_EmailSender", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<string> GetDistinctNodalEmailAsync(string date)
        {
            var values = new List<string>();
            try
            {
                using (var connection = GetOpenConnection())
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = $"SELECT distinct(NodalEmail) FROM Email_History WHERE CONVERT(date, CreatedDate) = '{date}' AND NodalEmail IS NOT NULL AND NodalEmail != ''";
                        using var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            values.Add(reader["NodalEmail"].ToString());
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            
            return values;
        }
        public async Task<ResponseModel> GetSubjectList(DateTime Date)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetSubjectList");
                    parmeters.Add("@Date", Date);
                    var objData = await Con.QueryMultipleAsync("sp_GetEmailHistory", parmeters, commandType: CommandType.StoredProcedure);
               
                    ResponseModel objResut = new()
                    {
                        Status = true,
                        Message = "",
                        Data = objData?.Read<string>()
                    };
                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseModel> GetNodalEmailList(DateTime Date)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetNodalEmailList");
                    parmeters.Add("@Date", Date);
                    var objData = await Con.QueryAsync<string>("sp_GetEmailHistory", parmeters, commandType: CommandType.StoredProcedure);

                    ResponseModel objResut = new()
                    {
                        Status = true,
                        Message = "",
                        Data = objData
                    };
                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseModel> GetRoleList(DateTime Date)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetRoleList");
                    parmeters.Add("@Date", Date);
                    var objData = await Con.QueryMultipleAsync("sp_GetEmailHistory", parmeters, commandType: CommandType.StoredProcedure);

                    ResponseModel objResut = new()
                    {
                        Status = true,
                        Message = "",
                        Data = objData?.Read<string>()
                    };
                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<EmailDataModel>> GetEmailHistoryData(DateTime Date,string email, string role, string subject)
        {
            var data = new List<EmailDataModel>();
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetEmailHistoryData");
                    parmeters.Add("@Date", Date);
                    parmeters.Add("@Email", email);
                    parmeters.Add("@Role", role);
                    parmeters.Add("@Subject", subject);
                    var objData = await Con.QueryAsync<EmailDataModel>("sp_GetEmailHistory", parmeters, commandType: CommandType.StoredProcedure);
                    data.AddRange(objData);
                    
                    DisposeCurrentSqlConnection();
                    return data;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
