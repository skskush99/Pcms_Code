using Master.Dto.Masters;
using Master.Dto.Shared;

namespace Master.ServiceBus.CrimeActService
{
    public interface ICrimeActServiceBus
    {
        Task<ResponseModel> GetCrimeAct(CrimeActFilterModel objModel);
        Task<ResponseWithoutPaginationModel> GetCrimeActDropdownList(int CrimeClsId);
        Task<ResponseModel> AddEditCrimeAct(AddEditCrimeActModel objModel, int UserId);
        Task<ResponseModel> ActiveDeactiveCrimeAct(ActiveDeactiveCrimeActModel objModel, int UserId);

    }
}
