using Master.Dto.Masters;
using Master.Dto.Shared;
using Master.Repository.UnitOfwork;
using static Core.Common;

namespace Master.ServiceBus.PoliceThanaService;

public class PoliceThanaServiceBus(IUnitOfWorkRepository IUnitOfWorkRepository) : IPoliceThanaServiceBus
{
    private readonly AsyncLocker<string> userLock = new AsyncLocker<string>();
    private readonly IUnitOfWorkRepository _IUnitOfWorkRepository = IUnitOfWorkRepository;

    public async Task<ResponseModel> GetPoliceRange(PoliceRangeFilterModel objModel)
    {
        try
        {
            var data = _IUnitOfWorkRepository.PoliceThana.GetPoliceRange(objModel);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseWithoutPaginationModel> PoliceRangeDropdownList(int DistrictId)
    {
        try
        {
            var data = _IUnitOfWorkRepository.PoliceThana.PoliceRangeDropdownList(DistrictId);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseModel> GetPoliceDistrict(PoliceDistrictFilterModel objModel)
    {
        try
        {
            var data = _IUnitOfWorkRepository.PoliceThana.GetPoliceDistrict(objModel);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseWithoutPaginationModel> PoliceDistrictDropdownList(int DistrictId, int RangeId)
    {
        try
        {
            var data = _IUnitOfWorkRepository.PoliceThana.PoliceDistrictDropdownList(DistrictId, RangeId);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseModel> GetPoliceCircle(PoliceCircleFilterModel objModel)
    {
        try
        {
            var data = _IUnitOfWorkRepository.PoliceThana.GetPoliceCircle(objModel);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseWithoutPaginationModel> PoliceCircleDropdownList(int PdId)
    {
        try
        {
            var data = _IUnitOfWorkRepository.PoliceThana.PoliceCircleDropdownList(PdId);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<ResponseModel> GetPoliceStation(PoliceStationFilterModel objModel)
    {
        try
        {
            var data = _IUnitOfWorkRepository.PoliceThana.GetPoliceStation(objModel);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseWithoutPaginationModel> PoliceStationDropdownList(int PcId)
    {
        try
        {
            var data = _IUnitOfWorkRepository.PoliceThana.PoliceStationDropdownList(PcId);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<ResponseWithoutPaginationModel> AllPoliceStationDropdownList()
    {
        try
        {
            var data = _IUnitOfWorkRepository.PoliceThana.AllPoliceStationDropdownList();
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }

}
