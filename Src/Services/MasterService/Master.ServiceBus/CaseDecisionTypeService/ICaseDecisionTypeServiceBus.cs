using Master.Dto.Masters;
using Master.Dto.Shared;

namespace Master.ServiceBus.CaseDecisionTypeService
{
    public interface ICaseDecisionTypeServiceBus
    {
        Task<ResponseModel> GetCaseDecisionType(CaseDecisionTypeFilterModel objModel);
        Task<ResponseWithoutPaginationModel> GetCaseDecisionTypeDropdownList();
        Task<ResponseModel> AddEditCaseDecisionType(AddEditCaseDecisionTypeModel objModel, int UserId);
        Task<ResponseModel> ActiveDeactiveCaseDecisionType(ActiveDeactiveCaseDecisionTypeModel objModel, int UserId);
    }

}
