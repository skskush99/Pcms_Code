using Master.Dto.Masters;
using Master.Dto.Shared;
using Master.Repository.UnitOfwork;
using static Core.Common;

namespace Master.ServiceBus.DesignationService;

public class DesignationServiceBus(IUnitOfWorkRepository IUnitOfWorkRepository) : IDesignationServiceBus
{
    private readonly AsyncLocker<string> userLock = new AsyncLocker<string>();
    private readonly IUnitOfWorkRepository _IUnitOfWorkRepository = IUnitOfWorkRepository;

    public async Task<ResponseModel> GetDesignation(DesignationFilterModel objModel)
    {
        try
        {
            var data = _IUnitOfWorkRepository.Designation.GetDesignation(objModel);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseWithoutPaginationModel> GetDesignationDropdownList()
    {
        try
        {
            var data = _IUnitOfWorkRepository.Designation.GetDesignationDropdownList();
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseModel> AddEditDesignation(DesignationModel objModel, int UserId)
    {
        try
        {
            var data = _IUnitOfWorkRepository.Designation.AddEditDesignation(objModel, UserId);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseModel> ActiveDeactiveDesignation(DesignationActiveDeactiveModel objModel, int UserId)
    {
        try
        {
            var data = _IUnitOfWorkRepository.Designation.ActiveDeactiveDesignation(objModel, UserId);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    //public async Task<ResponseModel> GetDesignationRajmaster(DesignationFilterModel objModel)
    //{
    //    try
    //    {
    //        var data = _IUnitOfWorkRepository.Designation.GetDesignationRajmaster(objModel);
    //        return await data;
    //    }
    //    catch (Exception)
    //    {
    //        throw;
    //    }
    //}
    //public async Task<ResponseWithoutPaginationModel> GetDesignationRajmasterDropdownList()
    //{
    //    try
    //    {
    //        var data = _IUnitOfWorkRepository.Designation.GetDesignationRajmasterDropdownList();
    //        return await data;
    //    }
    //    catch (Exception)
    //    {
    //        throw;
    //    }
    //}


    /////////// OISc Designation Mapping Start
    //public async Task<ResponseModel> GetOICSDesigMapping(OICSDesigMappingFilterModel objModel)
    //{
    //    try
    //    {
    //        var data = _IUnitOfWorkRepository.Designation.GetOICSDesigMapping(objModel);
    //        return await data;
    //    }
    //    catch (Exception)
    //    {
    //        throw;
    //    }
    //}
    //public async Task<ResponseWithoutPaginationModel> GetOICSDesigMappingDropdownList(int AdminDeptId, int UnitId)
    //{
    //    try
    //    {
    //        var data = _IUnitOfWorkRepository.Designation.GetOICSDesigMappingDropdownList(AdminDeptId, UnitId);
    //        return await data;
    //    }
    //    catch (Exception)
    //    {
    //        throw;
    //    }
    //}
    //public async Task<ResponseModel> AddEditOICSDesigMapping(OICSDesigMappingModel objModel, int UserId)
    //{
    //    try
    //    {
    //        var data = _IUnitOfWorkRepository.Designation.AddEditOICSDesigMapping(objModel, UserId);
    //        return await data;
    //    }
    //    catch (Exception)
    //    {
    //        throw;
    //    }
    //}
    //public async Task<ResponseModel> ActiveDeactiveOICSDesigMapping(OICsDesigActiveDeactiveModel objModel, int UserId)
    //{
    //    try
    //    {
    //        var data = _IUnitOfWorkRepository.Designation.ActiveDeactiveOICSDesigMapping(objModel, UserId);
    //        return await data;
    //    }
    //    catch (Exception)
    //    {
    //        throw;
    //    }
    //}

    /////////// OISc Designation Mapping End

    /////////// OISc Designation Section Start
    //public async Task<ResponseModel> GetSection(SectionFilterModel objModel)
    //{
    //    try
    //    {
    //        var data = _IUnitOfWorkRepository.Designation.GetSection(objModel);
    //        return await data;
    //    }
    //    catch (Exception)
    //    {
    //        throw;
    //    }
    //}
    //public async Task<ResponseWithoutPaginationModel> GetSectionDropdownList(int AdmDeptId, int UnitId)
    //{
    //    try
    //    {
    //        var data = _IUnitOfWorkRepository.Designation.GetSectionDropdownList(AdmDeptId, UnitId);
    //        return await data;
    //    }
    //    catch (Exception)
    //    {
    //        throw;
    //    }
    //}
    //public async Task<ResponseModel> AddEditSection(SectionModel objModel, int UserId)
    //{
    //    try
    //    {
    //        var data = _IUnitOfWorkRepository.Designation.AddEditSection(objModel, UserId);
    //        return await data;
    //    }
    //    catch (Exception)
    //    {
    //        throw;
    //    }
    //}
    //public async Task<ResponseModel> ActiveDeactiveSection(SectionActiveDeactiveModel objModel, int UserId)
    //{
    //    try
    //    {
    //        var data = _IUnitOfWorkRepository.Designation.ActiveDeactiveSection(objModel, UserId);
    //        return await data;
    //    }
    //    catch (Exception)
    //    {
    //        throw;
    //    }
    //}
    /////////// OISc Designation Section End

}
