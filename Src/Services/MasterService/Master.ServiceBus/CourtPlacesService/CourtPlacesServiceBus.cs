using Master.Dto.Masters;
using Master.Dto.Shared;
using Master.Repository.UnitOfwork;
using static Core.Common;

namespace Master.ServiceBus.CourtPlacesService;

public class CourtPlacesServiceBus(IUnitOfWorkRepository IUnitOfWorkRepository) : ICourtPlacesServiceBus
{
    private readonly AsyncLocker<string> userLock = new AsyncLocker<string>();
    private readonly IUnitOfWorkRepository _IUnitOfWorkRepository = IUnitOfWorkRepository;

    public async Task<ResponseModel> GetCourtPlaces(CourtPlacesFilterModel objModel)
    {
        try
        {
            var data = _IUnitOfWorkRepository.CourtPlaces.GetCourtPlaces(objModel);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseWithoutPaginationModel> GetCourtPlacesDropdownList(int CourtTypeId, int TehsilId)
    {
        try
        {
            var data = _IUnitOfWorkRepository.CourtPlaces.GetCourtPlacesDropdownList(CourtTypeId, TehsilId);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseModel> AddEditCourtPlaces(CourtPlacesModel objModel, int UserId)
    {
        try
        {
            var data = _IUnitOfWorkRepository.CourtPlaces.AddEditCourtPlaces(objModel, UserId);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseModel> ActiveDeactiveCourtPlaces(CourtPlacesActiveDeactiveModel objModel, int UserId)
    {
        try
        {
            var data = _IUnitOfWorkRepository.CourtPlaces.ActiveDeactiveCourtPlaces(objModel, UserId);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }

}
