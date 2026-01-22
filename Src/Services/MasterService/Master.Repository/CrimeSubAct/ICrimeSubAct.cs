using Master.Dto.Masters;
using Master.Dto.Shared;

namespace Master.Repository.CrimeSubAct
{
    public interface ICrimeSubAct
    {
        Task<ResponseModel> GetCrimeSubAct(CrimeSubActFilterModel objModel);
        Task<ResponseWithoutPaginationModel> GetCrimeSubActDropdownList(int CrimeActId, int CrimeClsId);
        Task<ResponseModel> AddEditCrimeSubAct(AddEditCrimeSubActModel objModel, int UserId);
        Task<ResponseModel> ActiveDeactiveCrimeSubAct(ActiveDeactiveCrimeSubActModel objModel, int UserId);
    }
}
