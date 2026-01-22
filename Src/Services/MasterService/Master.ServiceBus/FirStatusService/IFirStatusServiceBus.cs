using Master.Dto.Masters;
using Master.Dto.Shared;

namespace Master.ServiceBus.FirStatusService
{
    public interface IFirStatusServiceBus
    {
        Task<ResponseModel> GetFirStatus(FIRStatusFilterModel objModel);
        Task<ResponseWithoutPaginationModel> GetFirStatusDropdownList();
        Task<ResponseModel> AddEditFirStatus(AddEditFIRStatusModel objModel, int UserId);
        Task<ResponseModel> ActiveDeactiveFirStatus(ActiveDeactiveFIRStatusModel objModel, int UserId);

    }
}
