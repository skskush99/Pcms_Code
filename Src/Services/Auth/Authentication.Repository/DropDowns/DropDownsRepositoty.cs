using Authentication.Dto.Shared;
using Common.Dapper;
using Common.Repository;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;




namespace Authentication.Repository.DropDowns
{
    public class DropDownsRepositoty : SqlRepository, IDropDowns
    {
        private readonly LogsService _logsService;
        private readonly System.Data.IDbConnection Con;
        public DropDownsRepositoty(IConfiguration configuration, LogsService logsService) : base(configuration)
        {
            _logsService = logsService;
        }
        public async Task<ResponseWithoutPaginationModel> GetLevelDropdownList()
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetLevelDropdownList");
                    var objData = await Con.QueryAsync<DropdownlistModel>("spMstLevel", parmeters, commandType: CommandType.StoredProcedure);

                    ResponseWithoutPaginationModel objResut = new ResponseWithoutPaginationModel();
                    {
                        objResut.Status = true;
                        objResut.Message = "";
                        objResut.Data = objData;
                    }
                    ;
                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetLevelDropdownList", ex.Message, ex.StackTrace, ex.Source, "Auth/Authentication.Repository/DropDowns/DropDownsRepositoty/GetLevelDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseWithoutPaginationModel> GetRolesDropdownList()
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetRolesDropdownList");
                    var objResult = await Con.QueryAsync<DropdownlistModel>("spUsr_Roles", parmeters, commandType: CommandType.StoredProcedure);

                    ResponseWithoutPaginationModel objResut = new ResponseWithoutPaginationModel();
                    objResut.Status = true;
                    objResut.Message = "";
                    objResut.Data = objResult;
                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetRolesDropdownList", ex.Message, ex.StackTrace, ex.Source, "Auth/Authentication.Repository/DropDowns/DropDownsRepositoty/GetRolesDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseWithoutPaginationModel> GetDivisionsList()
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetDivisionsList");
                    var objData = await Con.QueryAsync<DropdownlistModel>("spMstState", parmeters, commandType: CommandType.StoredProcedure);

                    ResponseWithoutPaginationModel objResut = new ResponseWithoutPaginationModel();
                    {
                        objResut.Status = true;
                        objResut.Message = "";
                        objResut.Data = objData;
                    }
                    ;
                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetDivisionsList", ex.Message, ex.StackTrace, ex.Source, "Auth/Authentication.Repository/DropDowns/DropDownsRepositoty/GetDivisionsList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        
        public async Task<ResponseWithoutPaginationModel> GetDistrictsList(int DivisionId, int StateId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetDistrictsList");
                    parmeters.Add("@DivisionId", DivisionId);
                    parmeters.Add("@StateId", StateId);
                    var objData = await Con.QueryAsync<DropdownlistModel>("spMstState", parmeters, commandType: CommandType.StoredProcedure);

                    ResponseWithoutPaginationModel objResut = new ResponseWithoutPaginationModel();
                    {
                        objResut.Status = true;
                        objResut.Message = "";
                        objResut.Data = objData;
                    }
                    ;
                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetDistrictsList", ex.Message, ex.StackTrace, ex.Source, "Auth/Authentication.Repository/DropDowns/DropDownsRepositoty/GetDistrictsList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> GetOfficesDropdownList(int OfficeId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetOfficesDropdownList");
                    parmeters.Add("@OfficeId", OfficeId);
                    var objData = await Con.QueryAsync<DropdownlistModel>("spMstOffices", parmeters, commandType: CommandType.StoredProcedure);

                    ResponseWithoutPaginationModel objResut = new ResponseWithoutPaginationModel();
                    {
                        objResut.Status = true;
                        objResut.Message = "";
                        objResut.Data = objData;
                    }
                    ;
                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetOfficesDropdownList", ex.Message, ex.StackTrace, ex.Source, "Auth/Authentication.Repository/DropDowns/DropDownsRepositoty/GetOfficesDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> GetOfficesByDistrictIdDropdownList(int DistrictId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetOfficesByDistrictIdDropdownList");
                    parmeters.Add("@DistrictId", DistrictId);
                    var objData = await Con.QueryAsync<DropdownlistModel>("spMstOffices", parmeters, commandType: CommandType.StoredProcedure);

                    ResponseWithoutPaginationModel objResut = new ResponseWithoutPaginationModel();
                    {
                        objResut.Status = true;
                        objResut.Message = "";
                        objResut.Data = objData;
                    }
                    ;
                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetOfficesByDistrictIdDropdownList", ex.Message, ex.StackTrace, ex.Source, "Auth/Authentication.Repository/DropDowns/DropDownsRepositoty/GetOfficesByDistrictIdDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> GetDesignationDropdownList()
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetDesignationDropdownList");
                    var objData = await Con.QueryAsync<DropdownlistModel>("spMstDesignation", parmeters, commandType: CommandType.StoredProcedure);

                    ResponseWithoutPaginationModel objResut = new ResponseWithoutPaginationModel();
                    {
                        objResut.Status = true;
                        objResut.Message = "";
                        objResut.Data = objData;
                    }
                    ;
                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetDesignationDropdownList", ex.Message, ex.StackTrace, ex.Source, "Auth/Authentication.Repository/DropDowns/DropDownsRepositoty/GetDesignationDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseWithoutPaginationModel> GetDesignationByRoleIdDropdownList(int RoleId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetDesignationByRoleIdDropdownList");
                    parmeters.Add("@LevelId", RoleId);
                    var objData = await Con.QueryAsync<DropdownlistModel>("spMstDesignation", parmeters, commandType: CommandType.StoredProcedure);

                    ResponseWithoutPaginationModel objResut = new ResponseWithoutPaginationModel();
                    {
                        objResut.Status = true;
                        objResut.Message = "";
                        objResut.Data = objData;
                    }
                    ;
                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetDesignationByRoleIdDropdownList", ex.Message, ex.StackTrace, ex.Source, "Auth/Authentication.Repository/DropDowns/DropDownsRepositoty/GetDesignationByRoleIdDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> GetCourtNamesDropdownList(int JCourtId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetCourtNamesDropdownList");
                    parmeters.Add("@JCourtId", JCourtId);
                    var objData = await Con.QueryAsync<DropdownlistModel>("spMstCourtNames", parmeters, commandType: CommandType.StoredProcedure);

                    ResponseWithoutPaginationModel objResut = new ResponseWithoutPaginationModel();
                    {
                        objResut.Status = true;
                        objResut.Message = "";
                        objResut.Data = objData;
                    }
                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetCourtNamesDropdownList", ex.Message, ex.StackTrace, ex.Source, "Auth/Authentication.Repository/DropDowns/DropDownsRepositoty/GetCourtNamesDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> GetCourtNamesByOfficeIdDropdownList(int OfficeId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetCourtNamesByOfficeIdDropdownList");
                    parmeters.Add("@OfficeId", OfficeId);
                    var objData = await Con.QueryAsync<DropdownlistModel>("spMstCourtNames", parmeters, commandType: CommandType.StoredProcedure);

                    ResponseWithoutPaginationModel objResut = new ResponseWithoutPaginationModel();
                    {
                        objResut.Status = true;
                        objResut.Message = "";
                        objResut.Data = objData;
                    }
                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetCourtNamesByOfficeIdDropdownList", ex.Message, ex.StackTrace, ex.Source, "Auth/Authentication.Repository/DropDowns/DropDownsRepositoty/GetCourtNamesByOfficeIdDropdownList");
                return new ResponseWithoutPaginationModel()
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
                    parmeters.Add("@CreatedBy", objModel.RId);
                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spUsr_UserMappingRequest", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();

                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditUserMapReq", ex.Message, ex.StackTrace, ex.Source, "Auth/Authentication.Repository/DropDowns/DropDownsRepositoty/AddEditUserMapReq");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

    }
}
