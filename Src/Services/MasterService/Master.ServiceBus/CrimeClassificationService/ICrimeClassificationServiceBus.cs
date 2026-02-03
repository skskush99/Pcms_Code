using Master.Dto.Masters;
using Master.Dto.Shared;

namespace Master.ServiceBus.CrimeClassificationService
{
    public interface ICrimeClassificationServiceBus
    {
        Task<ResponseModel> GetCrimeClassification(CrimeClassificationFilterModel objModel);
        Task<ResponseWithoutPaginationModel> GetCrimeClassificationDropdownList();
        Task<ResponseModel> AddEditCrimeClassification(AddEditCrimeClassificationModel objModel, int UserId);
        Task<ResponseModel> ActiveDeactiveCrimeClassification(ActiveDeactiveCrimeClassificationModel objModel, int UserId);
    }
}
