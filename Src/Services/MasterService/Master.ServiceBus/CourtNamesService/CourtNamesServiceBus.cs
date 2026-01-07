using Master.Dto.Masters;
using Master.Dto.Shared;
using Master.Repository.UnitOfwork;
using static Core.Common;

namespace Master.ServiceBus.CourtNamesService;

public class CourtNamesServiceBus(IUnitOfWorkRepository IUnitOfWorkRepository) : ICourtNamesServiceBus
{
    private readonly AsyncLocker<string> userLock = new AsyncLocker<string>();
    private readonly IUnitOfWorkRepository _IUnitOfWorkRepository = IUnitOfWorkRepository;

    public async Task<ResponseModel> GetCourtNames(CourtNamesFilterModel objModel)
    {
        try
        {
            var data = _IUnitOfWorkRepository.CourtNames.GetCourtNames(objModel);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseWithoutPaginationModel> GetCourtNamesDropdownList(int JCourtId)
    {
        try
        {
            var data = _IUnitOfWorkRepository.CourtNames.GetCourtNamesDropdownList(JCourtId);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseModel> AddEditCourtNames(CourtNamesModel objModel, int UserId)
    {
        try
        {
            var data = _IUnitOfWorkRepository.CourtNames.AddEditCourtNames(objModel, UserId);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseModel> ActiveDeactiveCourtNames(CourtNamesActiveDeactiveModel objModel, int UserId)
    {
        try
        {
            var data = _IUnitOfWorkRepository.CourtNames.ActiveDeactiveCourtNames(objModel, UserId);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }

}
