using Email.Dto.Email;

namespace Email.ServiceBus.EmailService
{
    public interface IEmailServices
    {
        Task SendEmailAsync(string mailTo, string mailSubject, string filePath);
        Task RunEmailSender();
        Task<ResponseModel> GetSubjectList(DateTime Date);
        Task<ResponseModel> GetNodalEmailList(DateTime Date);
        Task<ResponseModel> GetRoleList(DateTime Date);
        Task<List<EmailDataModel>> GetEmailHistoryData(DateTime Date, string email, string role, string subject);
        List<string> GetDistinctNodalEmailAsync(string date);
    }
}
