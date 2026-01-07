using Master.Dto.Masters;
using Master.Dto.Shared;
using Master.Repository.UnitOfwork;
using static Core.Common;

namespace Master.ServiceBus.StateService;

public class StateServiceBus(IUnitOfWorkRepository IUnitOfWorkRepository) : IStateServiceBus
{
    private readonly AsyncLocker<string> userLock = new AsyncLocker<string>();
    private readonly IUnitOfWorkRepository _IUnitOfWorkRepository = IUnitOfWorkRepository;
    public async Task<ResponseModel> StateList(StateFilterModel objModel)
    {
        try
        {
            var data = _IUnitOfWorkRepository.State.StateList(objModel);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseWithoutPaginationModel> GetStateList()
    {
        try
        {
            var data = _IUnitOfWorkRepository.State.GetStateList();
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseModel> DivisionsList(StateFilterModel objModel)
    {
        try
        {
            var data = _IUnitOfWorkRepository.State.DivisionsList(objModel);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseWithoutPaginationModel> GetDivisionsList()
    {
        try
        {
            var data = _IUnitOfWorkRepository.State.GetDivisionsList();
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseModel> DistrictsList(DistrictsFilterModel objModel)
    {
        try
        {
            var data = _IUnitOfWorkRepository.State.DistrictsList(objModel);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseWithoutPaginationModel> GetDistrictsList(int DivisionId, int StateId)
    {
        try
        {
            var data = _IUnitOfWorkRepository.State.GetDistrictsList(DivisionId, StateId);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    
    
    //public async Task<ResponseModel> CityList(CityFilterModel objModel)
    //{
    //    try
    //    {
    //        var data = _IUnitOfWorkRepository.State.CityList(objModel);
    //        return await data;
    //    }
    //    catch (Exception)
    //    {
    //        throw;
    //    }
    //}
    //public async Task<ResponseModel> GetCityList(int DistrictId)
    //{
    //    try
    //    {
    //        var data = _IUnitOfWorkRepository.State.GetCityList(DistrictId);
    //        return await data;
    //    }
    //    catch (Exception)
    //    {
    //        throw;
    //    }
    //}
    //public async Task<ResponseModel> TehsilsList(CityFilterModel objModel)
    //{
    //    try
    //    {
    //        var data = _IUnitOfWorkRepository.State.TehsilsList(objModel);
    //        return await data;
    //    }
    //    catch (Exception)
    //    {
    //        throw;
    //    }
    //}
    //public async Task<ResponseModel> GetTehsilsList(int DistrictId)
    //{
    //    try
    //    {
    //        var data = _IUnitOfWorkRepository.State.GetTehsilsList(DistrictId);
    //        return await data;
    //    }
    //    catch (Exception)
    //    {
    //        throw;
    //    }
    //}
    //public async Task<ResponseModel> SubDivisionsList(SubDivisionsFilterModel objModel)
    //{
    //    try
    //    {
    //        var data = _IUnitOfWorkRepository.State.SubDivisionsList(objModel);
    //        return await data;
    //    }
    //    catch (Exception)
    //    {
    //        throw;
    //    }
    //}
    //public async Task<ResponseWithoutPaginationModel> GetSubDivisionsList(int DivisionId, int DistrictId)
    //{
    //    try
    //    {
    //        var data = _IUnitOfWorkRepository.State.GetSubDivisionsList(DivisionId, DistrictId);
    //        return await data;
    //    }
    //    catch (Exception)
    //    {
    //        throw;
    //    }
    //}



}
