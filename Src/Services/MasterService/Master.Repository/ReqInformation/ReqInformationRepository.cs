using Common.Dapper;
using Common.Repository;
using Dapper;
using Master.Dto.Masters;
using Master.Dto.Shared;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Master.Repository.ReqInformation
{
    public class ReqInformationRepository : SqlRepository, IReqInformation
    {
        private readonly LogsService _logsService;
        private readonly System.Data.IDbConnection Con;
        public ReqInformationRepository(IConfiguration configuration, LogsService logsService) : base(configuration)
        {
            _logsService = logsService;
        }

        public async Task<ResponseModel> GetReqInformation(ReqInformationFilterModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetReqInformation");
                    //parmeters.Add("@DeptDistrictType", objModel.DeptDistrictType);
                    parmeters.Add("@DistDeptType", objModel.DistDeptType);
                    parmeters.Add("@DistDept", objModel.DistDept);
                    parmeters.Add("@DistrictId", objModel.DistrictId);
                    parmeters.Add("@IsinfoReceived", objModel.IsinfoReceived);
                    parmeters.Add("@PageNo", objModel.PageNo);
                    parmeters.Add("@Pagesize", objModel.PageSize);
                    parmeters.Add("@SortBy", objModel.SortBy == null ? "" : objModel.SortBy);
                    parmeters.Add("@IsSortByDesc", objModel.IsSortByDesc == true ? 1 : 0);
                    var objResult = await Con.QueryMultipleAsync("spMstReqInformation", parmeters, commandType: CommandType.StoredProcedure);

                    ResponseModel objResut = new()
                    {
                        Status = true,
                        Message = "",
                        Data = objResult.Read<ReqInformationModel>(),
                        Pagination = objResult.Read<PaginationModel>()
                    };
                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetReqInformation", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/ReqInformationRepository/GetReqInformation");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseModel> GetReqInformationPopUp(GetReqInformationPopUpFilterModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetReqInformationPopUp");
                    parmeters.Add("@DistDept", objModel.DistDept);
                    parmeters.Add("@DPDT", objModel.DPDT);
                    var objResult = await Con.QueryMultipleAsync("spMstReqInformation", parmeters, commandType: CommandType.StoredProcedure);

                    ResponseModel objResut = new()
                    {
                        Status = true,
                        Message = "",
                        Data = objResult.Read<object>()
                    };
                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetReqInformation", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/ReqInformationRepository/GetReqInformation");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> GetReqInformationDropdownList()
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetReqInformationDropdownList");
                    var objData = await Con.QueryAsync<DropdownlistModel>("spMstReqInformation", parmeters, commandType: CommandType.StoredProcedure);

                    ResponseWithoutPaginationModel objResut = new ResponseWithoutPaginationModel();
                    {
                        objResut.Status = true;
                        objResut.Message = "";
                        objResut.Data = objData;
                    };
                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetReqInformationDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/ReqInformationRepository/GetReqInformationDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseModel> AddEditReqInformation(ReqInformationModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "AddEditReqInformation");
                    parmeters.Add("@InfoID", objModel.InfoID);
                    parmeters.Add("@ReqInforamtion", objModel.ReqInforamtion);
                    parmeters.Add("@ShortReqInforamtion", objModel.ShortReqInforamtion);
                    parmeters.Add("@DesReqInforamtion", objModel.DesReqInforamtion);
                    parmeters.Add("@SubjectID", objModel.SubjectID);
                    parmeters.Add("@DistDeptType", objModel.DistDeptType);
                    parmeters.Add("@DistDept", objModel.DistDept);
                    parmeters.Add("@IsInfoReceived", objModel.IsInfoReceived);
                    parmeters.Add("@DPDT", objModel.DPDT);
                    parmeters.Add("@Active", objModel.Active == true ? 1 : 0);
                    parmeters.Add("@StartDate", objModel.StartDate);
                    parmeters.Add("@EndDate", objModel.EndDate);
                    parmeters.Add("@SubmitLastDate", objModel.SubmitLastDate);
                    parmeters.Add("@CreatedBy", UserId);
                    parmeters.Add("@UpdatedBy", UserId);
                    var objData = await Con.QueryAsync<ResponseModel>("spMstReqInformation", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditReqInformation", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/ReqInformationRepository/AddEditReqInformation");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseModel> ActiveDeactiveReqInformation(ReqInformationActiveDeactiveModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "ActiveDeactiveReqInformation");
                    parmeters.Add("@InfoID", objModel.InfoID);
                    parmeters.Add("@Active", objModel.Active);
                    parmeters.Add("@UpdatedBy", UserId);
                    var objData = await Con.QueryAsync<ResponseModel>("spMstReqInformation", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ActiveDeactiveReqInformation", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/ReqInformationRepository/ActiveDeactiveReqInformation");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseModel> ReqInformationUpdate(ReqInformationUpdateModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "ReqInformationUpdate");
                    parmeters.Add("@InfoID", objModel.InfoID);
                    parmeters.Add("@IsInfoReceived", objModel.IsInfoReceived);
                    parmeters.Add("@UpdatedBy", UserId);
                    var objData = await Con.QueryAsync<ResponseModel>("spMstReqInformation", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ReqInformationUpdate", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/ReqInformationRepository/ReqInformationUpdate");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseModel> ReqInformationReset(ReqInformationUpdateModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "ReqInformationReset");
                    parmeters.Add("@InfoID", objModel.InfoID);
                    parmeters.Add("@IsInfoReceived", objModel.IsInfoReceived);
                    parmeters.Add("@UpdatedBy", UserId);
                    var objData = await Con.QueryAsync<ResponseModel>("spMstReqInformation", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ReqInformationReset", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/ReqInformationRepository/ReqInformationReset");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }


    }
}
