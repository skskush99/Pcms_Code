using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Common;
using Authentication.Dto.Shared;
using Authentication.Repository.UnitOfwork;

namespace Authentication.ServiceBus.DropDownsService;

public class DropDownsServiceBus(IUnitOfWorkRepository IUnitOfWorkRepository) : IDropDownsServiceBus
{
    private readonly AsyncLocker<string> userLock = new AsyncLocker<string>();
    private readonly IUnitOfWorkRepository _IUnitOfWorkRepository = IUnitOfWorkRepository;

    public async Task<ResponseWithoutPaginationModel> GetLevelDropdownList()
    {
        try
        {
            var data = _IUnitOfWorkRepository.DropDowns.GetLevelDropdownList();
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseWithoutPaginationModel> GetRolesDropdownList()
    {
        try
        {
            var data = _IUnitOfWorkRepository.DropDowns.GetRolesDropdownList();
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
            var data = _IUnitOfWorkRepository.DropDowns.GetDivisionsList();
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
            var data = _IUnitOfWorkRepository.DropDowns.GetDistrictsList(DivisionId, StateId);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseWithoutPaginationModel> GetOfficesDropdownList(int OfficeId)
    {
        try
        {
            var data = _IUnitOfWorkRepository.DropDowns.GetOfficesDropdownList(OfficeId);
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
            var data = _IUnitOfWorkRepository.DropDowns.GetDesignationDropdownList();
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
            var data = _IUnitOfWorkRepository.DropDowns.GetCourtNamesDropdownList(JCourtId);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseWithoutPaginationModel> AddEditUserMapReq(UserMapReqModel objModel, int UserId)
    {
        try
        {
            var data = _IUnitOfWorkRepository.DropDowns.AddEditUserMapReq(objModel, UserId);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }


}

