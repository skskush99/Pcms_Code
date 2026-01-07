using Sms.Dto.SmsModel;

namespace Sms.ServiceBus.SmsService
{
    public interface ISmsService
    {
        Task<List<SmsListModel>> GetSmsRequestList(SmsRequestModel data);
        Task RunSmsSender();
        Task RunSmsSenderNodalOfficer();
    }
}
