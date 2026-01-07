using Master.Dto.Masters;
using Master.Dto.Shared;
using Master.Repository.UnitOfwork;
using static Core.Common;

namespace Master.ServiceBus.RajMasterService;

public class RajMasterServiceBus(IUnitOfWorkRepository IUnitOfWorkRepository) : IRajMasterServiceBus
{
    private readonly AsyncLocker<string> userLock = new AsyncLocker<string>();
    private readonly IUnitOfWorkRepository _IUnitOfWorkRepository = IUnitOfWorkRepository;

    public async Task<ResponseModel> AddStateRajMaster(RajMasterModel objModel, int MasterDataID)
    {
        try
        {
            var data = _IUnitOfWorkRepository.RajMaster.AddStateRajMaster(objModel, MasterDataID);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseModel> AddDivisionRajMaster(RajMasterModel objModel, int MasterDataID)
    {
        try
        {
            var data = _IUnitOfWorkRepository.RajMaster.AddDivisionRajMaster(objModel, MasterDataID);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseModel> AddDistrictRajMaster(RajMasterModel objModel, int MasterDataID)
    {
        try
        {
            var data = _IUnitOfWorkRepository.RajMaster.AddDistrictRajMaster(objModel, MasterDataID);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseModel> AddPoliceRangeRajMaster(RajMasterModel objModel, int MasterDataID)
    {
        try
        {
            var data = _IUnitOfWorkRepository.RajMaster.AddPoliceRangeRajMaster(objModel, MasterDataID);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseModel> AddPoliceDistrictRajMaster(RajMasterModel objModel, int MasterDataID)
    {
        try
        {
            var data = _IUnitOfWorkRepository.RajMaster.AddPoliceDistrictRajMaster(objModel, MasterDataID);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseModel> AddPoliceCircleRajMaster(RajMasterModel objModel, int MasterDataID)
    {
        try
        {
            var data = _IUnitOfWorkRepository.RajMaster.AddPoliceCircleRajMaster(objModel, MasterDataID);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseModel> AddPoliceStationRajMaster(RajMasterModel objModel, int MasterDataID)
    {
        try
        {
            var data = _IUnitOfWorkRepository.RajMaster.AddPoliceStationRajMaster(objModel, MasterDataID);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }

    


    //public async Task<ResponseModel> AddCityRajMaster(RajMasterModel objModel, int MasterDataID)
    //{
    //    try
    //    {
    //        var data = _IUnitOfWorkRepository.RajMaster.AddCityRajMaster(objModel, MasterDataID);
    //        return await data;
    //    }
    //    catch (Exception)
    //    {
    //        throw;
    //    }
    //}
    //public async Task<ResponseModel> AddSubDivisionRajMaster(RajMasterModel objModel, int MasterDataID)
    //{
    //    try
    //    {
    //        var data = _IUnitOfWorkRepository.RajMaster.AddSubDivisionRajMaster(objModel, MasterDataID);
    //        return await data;
    //    }
    //    catch (Exception)
    //    {
    //        throw;
    //    }
    //}
    //public async Task<ResponseModel> AddTehsilRajMaster(RajMasterModel objModel, int MasterDataID)
    //{
    //    try
    //    {
    //        var data = _IUnitOfWorkRepository.RajMaster.AddTehsilRajMaster(objModel, MasterDataID);
    //        return await data;
    //    }
    //    catch (Exception)
    //    {
    //        throw;
    //    }
    //}
    //public async Task<ResponseModel> AddDesignationRajMaster(RajMasterModel objModel, int MasterDataID)
    //{
    //    try
    //    {
    //        var data = _IUnitOfWorkRepository.RajMaster.AddDesignationRajMaster(objModel, MasterDataID);
    //        return await data;
    //    }
    //    catch (Exception)
    //    {
    //        throw;
    //    }
    //}
    //public async Task<ResponseModel> AddAdminDepartmentRajMaster(RajMasterModel objModel, int MasterDataID)
    //{
    //    try
    //    {
    //        var data = _IUnitOfWorkRepository.RajMaster.AddAdminDepartmentRajMaster(objModel, MasterDataID);
    //        return await data;
    //    }
    //    catch (Exception)
    //    {
    //        throw;
    //    }
    //}
    //public async Task<ResponseModel> AddAdminUnitsDepartmentRajMaster(RajMasterModel objModel, int MasterDataID)
    //{
    //    try
    //    {
    //        var data = _IUnitOfWorkRepository.RajMaster.AddAdminUnitsDepartmentRajMaster(objModel, MasterDataID);
    //        return await data;
    //    }
    //    catch (Exception)
    //    {
    //        throw;
    //    }
    //}

}
