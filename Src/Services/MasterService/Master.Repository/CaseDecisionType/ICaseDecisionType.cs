using Master.Dto.Masters;
using Master.Dto.Shared;

namespace Master.Repository.CaseDecisionType
{
    public interface ICaseDecisionType
    {
        Task<ResponseModel> GetCaseDecisionType(CaseDecisionTypeFilterModel objModel);
        Task<ResponseWithoutPaginationModel> GetCaseDecisionTypeDropdownList();
        Task<ResponseModel> AddEditCaseDecisionType(AddEditCaseDecisionTypeModel objModel, int UserId);
        Task<ResponseModel> ActiveDeactiveCaseDecisionType(ActiveDeactiveCaseDecisionTypeModel objModel, int UserId);


    }
}
