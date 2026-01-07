using Master.Dto.Users;
using Master.Dto.Shared;
using Master.Repository.UnitOfwork;
using static Core.Common;
using Azure;

namespace Master.ServiceBus.Users
{
    public class UserLoginServiceBus(IUnitOfWorkRepository IUnitOfWorkRepository) : IUserLoginServiceBus
    {
        private readonly AsyncLocker<string> userLock = new AsyncLocker<string>();
        private readonly IUnitOfWorkRepository _IUnitOfWorkRepository = IUnitOfWorkRepository;
        public async Task<ResponseWithoutPaginationModel> Login(LoginModel objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.UserLogins.Login(objModel);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseModel> GetUserMapReqList(UsersMapReqFilterModel objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.UserLogins.GetUserMapReqList(objModel);
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
                var data = _IUnitOfWorkRepository.UserLogins.AddEditUserMapReq(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseWithoutPaginationModel> MappedUserBySA(ApprovelUserModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.UserLogins.MappedUserBySA(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseModel> GetUserList(UsersFilterModel objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.UserLogins.GetUserList(objModel);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseWithoutPaginationModel> AddEditUser(UserLoginModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.UserLogins.AddEditUser(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseWithoutPaginationModel> ActiveDeactiveUser(ActiveDeactiveModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.UserLogins.ActiveDeactiveUser(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseWithoutPaginationModel> MappedUser(MappedUserModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.UserLogins.MappedUser(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseWithoutPaginationModel> DemapUser(DemapUserModel objModel, int UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.UserLogins.DemapUser(objModel, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseWithoutPaginationModel> SSOLogin(LoginModel objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.UserLogins.SSOLogin(objModel);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseWithoutPaginationModel> SsoProfile(SsoProfileRequestModel objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.UserLogins.SsoProfile(objModel);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }




        public async Task<ResponseWithoutPaginationModel> Loginlogs(TokenAuthModel authUser)
        {
            try
            {
                var data = _IUnitOfWorkRepository.UserLogins.Loginlogs(authUser);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseWithoutPaginationModel> GetUserMenulist(long RoleId, long UserId)
        {
            try
            {
                var data = _IUnitOfWorkRepository.UserLogins.GetUserMenulist(RoleId, UserId);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseWithoutPaginationModel> Logout(TokenAuthModel authUser)
        {
            try
            {
                var data = _IUnitOfWorkRepository.UserLogins.Logout(authUser);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }




    }
}
