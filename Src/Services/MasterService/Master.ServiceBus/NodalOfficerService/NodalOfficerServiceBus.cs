using Master.Dto.Masters;
using Master.Dto.Shared;
using Master.Repository.UnitOfwork;
using static Core.Common;

namespace Master.ServiceBus.NodalOfficerService;

public class NodalOfficerServiceBus(IUnitOfWorkRepository IUnitOfWorkRepository) : INodalOfficerServiceBus
{
    private readonly AsyncLocker<string> userLock = new AsyncLocker<string>();
    private readonly IUnitOfWorkRepository _IUnitOfWorkRepository = IUnitOfWorkRepository;

    public async Task<ResponseModel> GetNodalOfficer(NodalOfficerFilterModel objModel, int LoginRoleId)
    {
        try
        {
            var data = _IUnitOfWorkRepository.NodalOfficer.GetNodalOfficer(objModel, LoginRoleId);
            return await data;
        }
        catch (Exception)
        {

            throw;
        }
    }
    public async Task<ResponseWithoutPaginationModel> GetNodalOfficerDropdownList()
    {
        try
        {
            var data = _IUnitOfWorkRepository.NodalOfficer.GetNodalOfficerDropdownList();
            return await data;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<ResponseWithoutPaginationModel> AddEditNodalOfficer(NodalOfficerModel objModel)
    {
        try
        {
            var data = _IUnitOfWorkRepository.NodalOfficer.AddEditNodalOfficer(objModel);
            return await data;
        }
        catch (Exception)
        {

            throw;
        }
    }
    public async Task<ResponseWithoutPaginationModel> ActiveDeactiveNodalOfficer(NodalOfficerActiveDeactiveModel objModel, int UserId)
    {
        try
        {
            var data = _IUnitOfWorkRepository.NodalOfficer.ActiveDeactiveNodalOfficer(objModel, UserId);
            return await data;
        }
        catch (Exception)
        {

            throw;
        }
    }

}
