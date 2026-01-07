using Master.Dto.Masters;
using Master.Dto.Shared;
using Master.Repository.UnitOfwork;
using static Core.Common;

namespace Master.ServiceBus.CourtTypesService;

public class CourtTypesServiceBus(IUnitOfWorkRepository IUnitOfWorkRepository) : ICourtTypesServiceBus
{
    private readonly AsyncLocker<string> userLock = new AsyncLocker<string>();
    private readonly IUnitOfWorkRepository _IUnitOfWorkRepository = IUnitOfWorkRepository;

    public async Task<ResponseModel> GetCourtTypes(CourtTypesFilterModel objModel)
    {
        try
        {
            var data = _IUnitOfWorkRepository.CourtTypes.GetCourtTypes(objModel);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseWithoutPaginationModel> GetCourtTypesDropdownList(int CourtTypeId)
    {
        try
        {
            var data = _IUnitOfWorkRepository.CourtTypes.GetCourtTypesDropdownList(CourtTypeId);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseModel> AddEditCourtTypes(CourtTypesModel objModel, int UserId)
    {
        try
        {
            var data = _IUnitOfWorkRepository.CourtTypes.AddEditCourtTypes(objModel, UserId);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseModel> ActiveDeactiveCourtTypes(CourtTypesActiveDeactiveModel objModel, int UserId)
    {
        try
        {
            var data = _IUnitOfWorkRepository.CourtTypes.ActiveDeactiveCourtTypes(objModel, UserId);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }

}
