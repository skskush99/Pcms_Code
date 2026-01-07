using Master.Dto.Menu;
using Master.Dto.Shared;
using Master.Repository.UnitOfwork;
using static Core.Common;

namespace Master.ServiceBus.Menu
{
    public class MenuServiceBus(IUnitOfWorkRepository IUnitOfWorkRepository) : IMenuServiceBus
    {

        private readonly AsyncLocker<string> userLock = new AsyncLocker<string>();
        private readonly IUnitOfWorkRepository _IUnitOfWorkRepository = IUnitOfWorkRepository;

        public async Task<ResponseModel> GetMenu(int PageNo, int PageSize)
        {
            try
            {
                var data = _IUnitOfWorkRepository.Menu.GetMenu(PageNo, PageSize);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseWithoutPaginationModel> GetParentMenusDropdownList()
        {
            try
            {
                var data = _IUnitOfWorkRepository.Menu.GetParentMenusDropdownList();
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseWithoutPaginationModel> AddMenu(MenuModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.Menu.AddMenu(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseWithoutPaginationModel> ActiveDeactiveMenu(MenuActiveDeactiveModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.Menu.ActiveDeactiveMenu(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseWithoutPaginationModel> GetMenuPageLink(MenuPageLinkFilterModel objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.Menu.GetMenuPageLink(objModel);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseWithoutPaginationModel> GetMenuMapping(int RoleId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.Menu.GetMenuMapping(RoleId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseWithoutPaginationModel> AddEditMenuMapping(IEnumerable<MenuMappingModel> objModel, int RoleId, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.Menu.AddEditMenuMapping(objModel, RoleId, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseWithoutPaginationModel> GetMenuMappingUser(int RoleId, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.Menu.GetMenuMappingUser(RoleId, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseWithoutPaginationModel> AddEditMenuMappingUser(IEnumerable<MenuMappingModel> objModel, int RoleId, int UserId, int ActionBy)
        {
            try
            {
                var data = _IUnitOfWorkRepository.Menu.AddEditMenuMappingUser(objModel, RoleId, UserId, ActionBy);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
