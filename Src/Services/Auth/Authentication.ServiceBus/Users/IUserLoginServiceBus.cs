using Authentication.Dto.Shared;

namespace Authentication.ServiceBus.Users
{
    public interface IUserLoginServiceBus
    {
        Task<ResponseUserMappingModel> SSOLogin(LoginModel objModel);
        Task<ResponseWithoutPaginationModel> SSOLoginBypass(LoginModel objModel);
        Task<ResponseWithoutPaginationModel> SSOLoginForMobleApp(LoginModel objModel);
        Task<ResponseWithoutPaginationModel> SSOIDMapped(SSOIDMappedModel objModel);
        Task<ResponseWithoutPaginationModel_New> AuthenticateMapping(LoginModel_New objModel);
        Task<ResponseWithoutPaginationModel> SsoProfileDt(SsoProfileDtRequestModel objModel);
    }
}
