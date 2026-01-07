using Azure;
using Master.Dto.Roles;
using Master.Dto.Shared;
using Master.Repository.UnitOfwork;
using static Core.Common;

namespace Master.ServiceBus.Roles
{
    public class RolesServiceBus(IUnitOfWorkRepository IUnitOfWorkRepository) : IRolesServiceBus
    {
        private readonly AsyncLocker<string> userLock = new AsyncLocker<string>();
        private readonly IUnitOfWorkRepository _IUnitOfWorkRepository = IUnitOfWorkRepository;
        public async Task<ResponseModel> GetRoles(int PageNo, int PageSize)
        {
            try
            {
                var data = _IUnitOfWorkRepository.Roles.GetRoles(PageNo, PageSize);
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
                var data = _IUnitOfWorkRepository.Roles.GetRolesDropdownList();
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseWithoutPaginationModel> GetRolesNodelOfficerDropdownList(int RoleId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.Roles.GetRolesNodelOfficerDropdownList(RoleId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseWithoutPaginationModel> AddRole(RoleModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.Roles.AddRole(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseWithoutPaginationModel> ActiveDeactiveRole(RoleActiveDeactiveModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.Roles.ActiveDeactiveRole(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseWithoutPaginationModel> DBAction(DBActionModel objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.Roles.DBAction(objModel);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
