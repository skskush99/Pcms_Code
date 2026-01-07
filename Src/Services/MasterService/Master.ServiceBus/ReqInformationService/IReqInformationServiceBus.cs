using Master.Dto.Masters;
using Master.Dto.Shared;

namespace Master.ServiceBus.ReqInformationService
{
    public interface IReqInformationServiceBus
    {
        Task<ResponseModel> GetReqInformation(ReqInformationFilterModel objModel);
        Task<ResponseModel> GetReqInformationPopUp(GetReqInformationPopUpFilterModel objModel);
        Task<ResponseWithoutPaginationModel> GetReqInformationDropdownList();
        Task<ResponseModel> AddEditReqInformation(ReqInformationModel objModel, int UserId);
        Task<ResponseModel> ActiveDeactiveReqInformation(ReqInformationActiveDeactiveModel objModel, int UserId);
        Task<ResponseModel> ReqInformationUpdate(ReqInformationUpdateModel objModel, int UserId);
        Task<ResponseModel> ReqInformationReset(ReqInformationUpdateModel objModel, int UserId);
    }
}
