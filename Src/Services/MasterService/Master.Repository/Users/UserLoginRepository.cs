using System.Data;
using Master.Dto.Users;
using Master.Dto.Shared;
using Dapper;
using Common.Dapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Core.Models.User;
using Core.Utils;
using System.Text.Json;
using Common.Repository;

namespace Master.Repository.Users
{
    public class UserLoginRepository : SqlRepository, IUserLogin
    {
        private readonly LogsService _logsService;
        private readonly System.Data.IDbConnection Con;
        public UserLoginRepository(IConfiguration Configuration, LogsService logsService) : base(Configuration)
        {
            _logsService = logsService;
        }
        public async Task<ResponseWithoutPaginationModel> Login(LoginModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "Login");
                    parmeters.Add("@UserName", objModel.UserName);
                    parmeters.Add("@Password", objModel.Password);
                    parmeters.Add("@SSOID", objModel.SSOID);
                    parmeters.Add("@IPAddress", objModel.IPAddress);
                    parmeters.Add("@IsSSOLogin", objModel.IsSSOLogin == true ? 1 : 0);
                    var objData = await Con.QueryAsync<LoginDetailsModel>("spUsr_UserLogin", parmeters, commandType: CommandType.StoredProcedure);

                    ResponseWithoutPaginationModel objResult = new ResponseWithoutPaginationModel();
                    if (objData.Any())
                    {
                        foreach (var item in objData)
                        {
                            item.Token = Core.Common.Encrypt(item.Token);
                        }

                        objResult.Status = true;
                        objResult.Message = "";
                        objResult.Data = objData;
                    }
                    else
                    {
                        objResult.Status = false;
                        objResult.Message = "Please provide valid login details.";
                    }
                    DisposeCurrentSqlConnection();
                    return objResult;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "Login", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/UserLoginRepository/Login");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseModel> GetUserMapReqList(UsersMapReqFilterModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetUserMapReqList");
                    parmeters.Add("@LevelId", objModel.LevelId);
                    parmeters.Add("@RoleId", objModel.RoleId);
                    parmeters.Add("@DepartmentId", objModel.DepartmentId);
                    parmeters.Add("@OfficeId", objModel.OfficeId);
                    parmeters.Add("@SSOID", objModel.SSOID);
                    parmeters.Add("@DistrictId", objModel.DistrictId);
                    parmeters.Add("@UserName", objModel.UserName);
                    parmeters.Add("@IsActive", objModel.IsActive);
                    parmeters.Add("@SortBy", objModel.SortBy == null ? "" : objModel.SortBy);
                    parmeters.Add("@IsSortByDesc", objModel.IsSortByDesc == true ? 1 : 0);
                    parmeters.Add("@PageNo", objModel.PageNo);
                    parmeters.Add("@Pagesize", objModel.PageSize);
                    var objResult = await Con.QueryMultipleAsync("spUsr_UserMappingRequest", parmeters, commandType: CommandType.StoredProcedure);

                    ResponseModel objResut = new ResponseModel();
                    objResut.Status = true;
                    objResut.Message = "";
                    objResut.Data = objResult.Read<object>();
                    objResut.Pagination = objResult.Read<PaginationModel>();
                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetUserMapReqList", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/UserLoginRepository/GetUserMapReqList");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> AddEditUserMapReq(UserMapReqModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "AddEditUserMapReq");
                    parmeters.Add("@RId", objModel.RId);
                    parmeters.Add("@RSSOID", objModel.RSSOID);
                    parmeters.Add("@RUserName", objModel.RUserName);
                    parmeters.Add("@RDesignationId", objModel.RDesignationId);
                    parmeters.Add("@RDesignationName", objModel.RDesignationName);
                    parmeters.Add("@RDepartmentId", objModel.RDepartmentId);
                    parmeters.Add("@RDepartmentName", objModel.RDepartmentName);
                    parmeters.Add("@RDOB", objModel.RDOB);
                    parmeters.Add("@RGender", objModel.RGender);
                    parmeters.Add("@ROfficialMail", objModel.ROfficialMail);
                    parmeters.Add("@RMobile", objModel.RMobile);
                    parmeters.Add("@RAadhaarId", objModel.RAadhaarId);
                    parmeters.Add("@RBhamashahId", objModel.RBhamashahId);
                    parmeters.Add("@RBhamashahMemberId", objModel.RBhamashahMemberId);
                    parmeters.Add("@LevelId", objModel.LevelId);
                    parmeters.Add("@RoleId", objModel.RoleId);
                    parmeters.Add("@DivisionId", objModel.DivisionId);
                    parmeters.Add("@DistrictId", objModel.DistrictId);
                    parmeters.Add("@OfficeId", objModel.OfficeId);
                    parmeters.Add("@DesignationId", objModel.DesignationId);
                    parmeters.Add("@CourtId", objModel.CourtId);
                    parmeters.Add("@IsActive", objModel.IsActive == true ? 1 : 0);
                    parmeters.Add("@CreatedBy", UserId);
                    parmeters.Add("@CreatedBy", UserId);
                    parmeters.Add("@UpdatedBy", UserId);
                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spUsr_UserMappingRequest", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();

                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditUserMapReq", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/UserLoginRepository/AddEditUserMapReq");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> MappedUserBySA(ApprovelUserModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "MappedUserBySA");
                    parmeters.Add("@RId", objModel.RId);
                    parmeters.Add("@DepartmentId", objModel.DepartmentId);
                    parmeters.Add("@CreatedBy", UserId);
                    parmeters.Add("@ApprovedBy", UserId);
                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spUsr_UserMappingRequest", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();

                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ApprovelMappReq", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/UserLoginRepository/ApprovelMappReq");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseModel> GetUserList(UsersFilterModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetUserList");
                    parmeters.Add("@RoleId", objModel.RoleId);
                    parmeters.Add("@DepartmentId", objModel.DepartmentId);
                    parmeters.Add("@UnitId", objModel.UnitId);
                    parmeters.Add("@OfficeId", objModel.OfficeId);
                    parmeters.Add("@SSOID", objModel.SSOID);
                    parmeters.Add("@DistrictId", objModel.DistrictId);
                    parmeters.Add("@UserName", objModel.UserName);
                    parmeters.Add("@Active", objModel.Active);
                    parmeters.Add("@SortBy", objModel.SortBy == null ? "" : objModel.SortBy);
                    parmeters.Add("@IsSortByDesc", objModel.IsSortByDesc == true ? 1 : 0);
                    parmeters.Add("@PageNo", objModel.PageNo);
                    parmeters.Add("@Pagesize", objModel.PageSize);
                    var objResult = await Con.QueryMultipleAsync("spUsr_UserLogin", parmeters, commandType: CommandType.StoredProcedure);

                    ResponseModel objResut = new ResponseModel();
                    objResut.Status = true;
                    objResut.Message = "";
                    objResut.Data = objResult.Read<object>();
                    objResut.Pagination = objResult.Read<PaginationModel>();
                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetUserList", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/UserLoginRepository/GetUserList");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> AddEditUser(UserLoginModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "AddEditUser");
                    parmeters.Add("@UserId", objModel.UserId);
                    parmeters.Add("@RoleId", objModel.RoleId);
                    parmeters.Add("@DepartmentId", objModel.DepartmentId);
                    parmeters.Add("@UnitId", objModel.UnitId);
                    parmeters.Add("@OfficeId", objModel.OfficeId);
                    parmeters.Add("@OICId", objModel.OICId);
                    parmeters.Add("@DistrictId", objModel.DistrictId);
                    parmeters.Add("@LawyerId", objModel.LawyerId);
                    parmeters.Add("@SSOID", objModel.SSOID);
                    parmeters.Add("@Name", objModel.Name);
                    parmeters.Add("@DOB", objModel.DOB);
                    parmeters.Add("@Gender", objModel.Gender);
                    parmeters.Add("@Designation", objModel.Designation);
                    parmeters.Add("@Mobile", objModel.Mobile);
                    parmeters.Add("@Contact", objModel.Contact);
                    parmeters.Add("@OfficialMail", objModel.OfficialMail);
                    parmeters.Add("@PersonalMail", objModel.PersonalMail);
                    parmeters.Add("@PostalAddress", objModel.PostalAddress);
                    parmeters.Add("@PostalCode", objModel.PostalCode);
                    parmeters.Add("@City", objModel.City);
                    parmeters.Add("@State", objModel.State);
                    parmeters.Add("@Active", objModel.Active == true ? 1 : 0);
                    parmeters.Add("@CreatedBy", UserId);
                    parmeters.Add("@UpdatedBy", UserId);
                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spUsr_UserLogin", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();

                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditUser", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/UserLoginRepository/AddEditUser");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> ActiveDeactiveUser(ActiveDeactiveModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "ActiveDeactiveUser");
                    parmeters.Add("@UserId", objModel.Id);
                    parmeters.Add("@Active", objModel.Status == true ? 1 : 0);
                    parmeters.Add("@UpdatedBy", UserId);
                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spUsr_UserLogin", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();

                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ActiveDeactiveUser", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/UserLoginRepository/ActiveDeactiveUser");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> MappedUser(MappedUserModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "MappedUser");
                    parmeters.Add("@RoleId", objModel.RoleId);
                    parmeters.Add("@SSOID", objModel.SSOID);
                    parmeters.Add("@UserName", objModel.UserName);
                    parmeters.Add("@UpdatedBy", UserId);
                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spUsr_UserLogin", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();

                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "MappedUser", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/UserLoginRepository/MappedUser");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> DemapUser(DemapUserModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "DemapUser");
                    parmeters.Add("@UserId", objModel.UserId);
                    parmeters.Add("@UpdatedBy", UserId);
                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spUsr_UserLogin", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();

                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DemapUser", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/UserLoginRepository/DemapUser");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> SSOLogin(LoginModel objModel)
        {
            try
            {
                var parmeters = new DynamicParameters();
                parmeters.Add("@Action", "Login");
                parmeters.Add("@UserName", objModel.UserName);
                parmeters.Add("@Password", objModel.Password);
                parmeters.Add("@IPAddress", objModel.IPAddress);
                parmeters.Add("@IsSSOLogin", objModel.IsSSOLogin == true ? 1 : 0);
                //parmeters.Add("@SSOToken", objModel.SSOToken); // Adding SSOToken parameter

                // Create a response object to return
                ResponseWithoutPaginationModel objResult = new ResponseWithoutPaginationModel();

                // Perform SSO lookup if IsSSOLogin is true
                if (objModel.IsSSOLogin)
                {
                    SSOServiceResponse ssoInfo = new SSOServiceResponse();
                    string SSOId = string.Empty;
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://ssotest.rajasthan.gov.in:4443"); // Add the appropriate base URL here
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        // Sending request to get token details
                        HttpResponseMessage Res = await client.GetAsync("SSOREST/GetTokenDetailJSON/" + objModel.SSOToken);

                        if (Res.IsSuccessStatusCode)
                        {
                            var ssoResponse = await Res.Content.ReadAsStringAsync();
                            ssoInfo = JsonConvert.DeserializeObject<SSOServiceResponse>(ssoResponse);
                            SSOId = ssoInfo?.sAMAccountName;
                            if (!string.IsNullOrEmpty(SSOId))
                            {
                                parmeters.Add("@SSOID", SSOId);

                                using (var Con = GetOpenConnection())
                                {

                                    var objData = await Con.QueryAsync<LoginDetailsModel>("spUsr_UserLogin", parmeters, commandType: CommandType.StoredProcedure);

                                    if (objData.Any())
                                    {
                                        // Encrypt token for each item in objData
                                        foreach (var item in objData)
                                        {
                                            item.Token = Core.Common.Encrypt(item.Token);
                                        }

                                        objResult.Status = true;
                                        objResult.Message = "";
                                        objResult.Data = objData;
                                    }
                                    else
                                    {
                                        objResult.Status = false;
                                        objResult.Message = "Please provide valid login details.";
                                    }

                                    DisposeCurrentSqlConnection();
                                }
                            }
                            else
                            {
                                objResult.Status = false;
                                objResult.Message = "Invalid SSO Token.";
                            }
                        }
                        else
                        {
                            objResult.Status = false;
                            objResult.Message = "Failed to retrieve SSO information.";
                        }
                    }
                }
                else
                {
                    objResult.Status = false;
                    objResult.Message = "SSO login not enabled.";
                }

                return objResult;
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "SSOLogin", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/UserLoginRepository/SSOLogin");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> SsoProfile(SsoProfileRequestModel objModel)
        {
            try
            {
                var parmeters = new DynamicParameters();
                ResponseWithoutPaginationModel objResult = new ResponseWithoutPaginationModel();

                if (!string.IsNullOrEmpty(objModel.SSOID))
                {
                    SSOServiceResponse ssoInfo = new SSOServiceResponse();
                    IEnumerable<SSOUserDetails> data = new List<SSOUserDetails>();
                    SSOUserDetails userdetails = new SSOUserDetails();

                    //string userProfileUrl = objModel.SsoBaseUrl;
                    //string wsUsername = objModel.UserName;
                    //string wsPassword = objModel.Password;
                    using (var client = new HttpClient())
                    {
                        using (var request = new HttpRequestMessage(HttpMethod.Post, objModel.SsoBaseUrl))
                        {
                            var collection = new List<KeyValuePair<string, string>>
                            {
                                new("SSOID", objModel.SSOID),
                                new("WSUSERNAME", objModel.UserName),
                                new("WSPASSWORD", objModel.EncryptedPassword)
                            };

                            var content = new FormUrlEncodedContent(collection);
                            request.Content = content;
                            using (var responseSsoUser = client.Send(request))
                            {
                                if (responseSsoUser.IsSuccessStatusCode)
                                {
                                    var optionsSso = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                                    var stateInfoSso = responseSsoUser.Content.ReadAsStringAsync().Result;
                                    dynamic resultSso = System.Text.Json.JsonSerializer.Deserialize<Object>(stateInfoSso, optionsSso);
                                    userdetails = ConversionUtility.ConvertFromDynamicObject<SSOUserDetails>(resultSso);
                                    data = data.Append(userdetails);
                                    if (userdetails != null)
                                    {

                                        objResult.Status = true;
                                        objResult.Message = "";
                                        objResult.Data = data;
                                    }
                                    else
                                    {
                                        objResult.Status = false;
                                        objResult.Message = "Invalid SSO Id.";
                                    }
                                }
                                else
                                {
                                    objResult.Status = false;
                                    objResult.Message = "Invalid SSO Id.";
                                }
                            }
                        }
                    }
                }
                else
                {
                    objResult.Status = false;
                    objResult.Message = "SSO Id not provided.";
                }

                return objResult;
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "SsoProfile", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/UserLoginRepository/SsoProfile");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> Loginlogs(TokenAuthModel authUser)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "LoginLogs");
                    parmeters.Add("@Token", authUser.Token);
                    parmeters.Add("@Status", authUser.Status);
                    parmeters.Add("@Message", authUser.Message);
                    parmeters.Add("@UserId", authUser.UserId);
                    parmeters.Add("@RoleId", authUser.RoleId);
                    parmeters.Add("@LoginOn", authUser.LoginOn);
                    parmeters.Add("@IPAddress", authUser.IPAddress);
                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spUsr_UserLogin", parmeters, commandType: CommandType.StoredProcedure);

                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "Loginlogs", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/UserLoginRepository/Loginlogs");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> GetUserMenulist(long RoleId, long UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetUserMenulist");
                    parmeters.Add("@RoleId", RoleId);
                    parmeters.Add("@UserId", UserId);
                    var objResult = await Con.QueryMultipleAsync("spUsr_UserLogin", parmeters, commandType: CommandType.StoredProcedure);

                    var mainMenu = objResult.Read<UserMenuModel>();
                    var subMenu = objResult.Read<UserSubMenuModel>();
                    foreach (var item in mainMenu)
                        item.SubMenus = subMenu.Where(x => x.ParentId == item.Id);

                    ResponseWithoutPaginationModel objResut = new ResponseWithoutPaginationModel();
                    objResut.Status = true;
                    objResut.Message = "";
                    objResut.Data = mainMenu;
                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetUserMenulist", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/UserLoginRepository/GetUserMenulist");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> Logout(TokenAuthModel authUser)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "LoginLogs");
                    parmeters.Add("@Token", authUser.Token);
                    parmeters.Add("@IPAddress", authUser.IPAddress);
                    parmeters.Add("@LogoutOn", System.DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss"));
                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spUsr_UserLogin", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();

                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "Logout", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/UserLoginRepository/Logout");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }


    }
}
