using Master.Dto.Masters;
using Master.Dto.Shared;

namespace Master.Repository.FirStatus
{
    public interface IFirStatus  
    {
        Task<ResponseModel> GetFirStatus(FIRStatusFilterModel objModel);
        Task<ResponseWithoutPaginationModel> GetFirStatusDropdownList();
        Task<ResponseModel> AddEditFirStatus(AddEditFIRStatusModel objModel, int UserId);
        Task<ResponseModel> ActiveDeactiveFirStatus(ActiveDeactiveFIRStatusModel objModel, int UserId);


    }
}
