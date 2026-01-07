using Master.Dto.Masters;
using Master.Dto.Shared;
using Master.Repository.UnitOfwork;
using static Core.Common;

namespace Master.ServiceBus.ReqInformationService;

public class ReqInformationServiceBus(IUnitOfWorkRepository IUnitOfWorkRepository) : IReqInformationServiceBus
{
    private readonly AsyncLocker<string> userLock = new AsyncLocker<string>();
    private readonly IUnitOfWorkRepository _IUnitOfWorkRepository = IUnitOfWorkRepository;

    public async Task<ResponseModel> GetReqInformation(ReqInformationFilterModel objModel)
    {
        try
        {
            var data = _IUnitOfWorkRepository.ReqInformation.GetReqInformation(objModel);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseModel> GetReqInformationPopUp(GetReqInformationPopUpFilterModel objModel)
    {
        try
        {
            var data = _IUnitOfWorkRepository.ReqInformation.GetReqInformationPopUp(objModel);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseWithoutPaginationModel> GetReqInformationDropdownList()
    {
        try
        {
            var data = _IUnitOfWorkRepository.ReqInformation.GetReqInformationDropdownList();
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseModel> AddEditReqInformation(ReqInformationModel objModel, int UserId)
    {
        try
        {
            var data = _IUnitOfWorkRepository.ReqInformation.AddEditReqInformation(objModel, UserId);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseModel> ActiveDeactiveReqInformation(ReqInformationActiveDeactiveModel objModel, int UserId)
    {
        try
        {
            var data = _IUnitOfWorkRepository.ReqInformation.ActiveDeactiveReqInformation(objModel, UserId);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseModel> ReqInformationUpdate(ReqInformationUpdateModel objModel, int UserId)
    {
        try
        {
            var data = _IUnitOfWorkRepository.ReqInformation.ReqInformationUpdate(objModel, UserId);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<ResponseModel> ReqInformationReset(ReqInformationUpdateModel objModel, int UserId)
    {
        try
        {
            var data = _IUnitOfWorkRepository.ReqInformation.ReqInformationReset(objModel, UserId);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }

}
