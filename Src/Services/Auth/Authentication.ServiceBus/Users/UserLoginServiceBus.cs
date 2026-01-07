using Authentication.Dto.Shared;
using Authentication.Repository.UnitOfwork;
using static Core.Common;

namespace Authentication.ServiceBus.Users
{
    public class UserLoginServiceBus(IUnitOfWorkRepository IUnitOfWorkRepository) : IUserLoginServiceBus
    {
        private readonly AsyncLocker<string> userLock = new AsyncLocker<string>();
        private readonly IUnitOfWorkRepository _IUnitOfWorkRepository = IUnitOfWorkRepository;

        public async Task<ResponseUserMappingModel> SSOLogin(LoginModel objModel)
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

        public async Task<ResponseWithoutPaginationModel> SSOLoginBypass(LoginModel objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.UserLogins.SSOLoginBypass(objModel);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseWithoutPaginationModel> SSOLoginForMobleApp(LoginModel objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.UserLogins.SSOLoginForMobleApp(objModel);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseWithoutPaginationModel> SSOIDMapped(SSOIDMappedModel objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.UserLogins.SSOIDMapped(objModel);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ResponseWithoutPaginationModel_New> AuthenticateMapping(LoginModel_New objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.UserLogins.AuthenticateMapping(objModel);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseWithoutPaginationModel> SsoProfileDt(SsoProfileDtRequestModel objModel)
        {
            try
            {
                var data = _IUnitOfWorkRepository.UserLogins.SsoProfileDt(objModel);
                return await data;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
