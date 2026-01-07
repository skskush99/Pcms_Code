using Master.Dto.Masters;
using Master.Dto.Shared;
using Master.Repository.UnitOfwork;
using static Core.Common;

namespace Master.ServiceBus.OfficeService;

public class OfficeServiceBus(IUnitOfWorkRepository IUnitOfWorkRepository) : IOfficeServiceBus
{
    private readonly AsyncLocker<string> userLock = new AsyncLocker<string>();
    private readonly IUnitOfWorkRepository _IUnitOfWorkRepository = IUnitOfWorkRepository;

    public async Task<ResponseModel> GetOffices(OfficesFilterModel objModel)
    {
        try
        {
            var data = _IUnitOfWorkRepository.Offices.GetOffices(objModel);
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
            var data = _IUnitOfWorkRepository.Offices.GetOfficesDropdownList(OfficeId);
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseModel> AddEditOffices(OfficesModel objModel, int UserId)
    {
        try
        {
            var data = _IUnitOfWorkRepository.Offices.AddEditOffices(objModel, UserId);
            return await data;
        }
        catch (Exception)
        {

            throw;
        }
    }
    public async Task<ResponseModel> ActiveDeactiveOffices(OfficesActiveDeactiveModel objModel, int UserId)
    {
        try
        {
            var data = _IUnitOfWorkRepository.Offices.ActiveDeactiveOffices(objModel, UserId);
            return await data;
        }
        catch (Exception)
        {

            throw;
        }
    }

}
