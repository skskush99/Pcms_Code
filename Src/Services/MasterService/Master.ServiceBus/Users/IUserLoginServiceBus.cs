using Master.Dto.Users;
using Master.Dto.Shared;

namespace Master.ServiceBus.Users
{
    public interface IUserLoginServiceBus
    {
        Task<ResponseWithoutPaginationModel> Login(LoginModel objModel);
        Task<ResponseModel> GetUserMapReqList(UsersMapReqFilterModel objModel); 
        Task<ResponseWithoutPaginationModel> AddEditUserMapReq(UserMapReqModel objModel, int UserId);
        Task<ResponseWithoutPaginationModel> MappedUserBySA(ApprovelUserModel objModel, int UserId);
        Task<ResponseModel> GetUserList(UsersFilterModel objModel);
        Task<ResponseWithoutPaginationModel> AddEditUser(UserLoginModel objModel, int UserId);
        Task<ResponseWithoutPaginationModel> ActiveDeactiveUser(ActiveDeactiveModel objModel, int UserId);
        Task<ResponseWithoutPaginationModel> MappedUser(MappedUserModel objModel, int UserId);
        Task<ResponseWithoutPaginationModel> DemapUser(DemapUserModel objModel, int UserId);
        Task<ResponseWithoutPaginationModel> SSOLogin(LoginModel objModel);
        Task<ResponseWithoutPaginationModel> SsoProfile(SsoProfileRequestModel objModel);
        Task<ResponseWithoutPaginationModel> Loginlogs(TokenAuthModel authUser);
        Task<ResponseWithoutPaginationModel> GetUserMenulist(long RoleId, long UserId);
        Task<ResponseWithoutPaginationModel> Logout(TokenAuthModel authUser);
    }
}
