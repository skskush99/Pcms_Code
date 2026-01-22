using Master.Dto.Masters;
using Master.Dto.Shared;

namespace Master.Repository.CrimeAct
{
    public interface ICrimeAct
    {
        Task<ResponseModel> GetCrimeAct(CrimeActFilterModel objModel);
        Task<ResponseWithoutPaginationModel> GetCrimeActDropdownList(int CrimeClsId);
        Task<ResponseModel> AddEditCrimeAct(AddEditCrimeActModel objModel, int UserId);
        Task<ResponseModel> ActiveDeactiveCrimeAct(ActiveDeactiveCrimeActModel objModel, int UserId);

    }
}
