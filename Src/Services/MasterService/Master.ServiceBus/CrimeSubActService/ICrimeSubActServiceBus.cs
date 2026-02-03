using Master.Dto.Masters;
using Master.Dto.Shared;

namespace Master.ServiceBus.CrimeSubActService
{
    public interface ICrimeSubActServiceBus
    {
        Task<ResponseModel> GetCrimeSubAct(CrimeSubActFilterModel objModel);
        Task<ResponseWithoutPaginationModel> GetCrimeSubActDropdownList(int CrimeActId, int CrimeClsId);
        Task<ResponseModel> AddEditCrimeSubAct(AddEditCrimeSubActModel objModel, int UserId);
        Task<ResponseModel> ActiveDeactiveCrimeSubAct(ActiveDeactiveCrimeSubActModel objModel, int UserId);
    }
}
