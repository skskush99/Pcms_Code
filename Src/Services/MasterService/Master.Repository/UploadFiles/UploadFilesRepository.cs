using Master.Dto.UploadFiles;
using Master.Dto.Shared;
using Common.Dapper;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using Common.Repository;

namespace Master.Repository.UploadFiles
{
    public class UploadFilesRepository : SqlRepository, IUploadFilesRepository
    {
        private readonly LogsService _logsService;
        private readonly System.Data.IDbConnection Con;
        public UploadFilesRepository(IConfiguration Configuration, LogsService logsService) : base(Configuration)
        {
            _logsService = logsService;
        }

        public async Task<ResponseWithoutPaginationModel> GetUploadFileCategoryList()
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetUploadFileCategoryList");
                    var objResult = await Con.QueryAsync("spMst_UploadFiles", parmeters, commandType: CommandType.StoredProcedure);

                    ResponseWithoutPaginationModel objResut = new()
                    {
                        Status = true,
                        Message = "",
                        Data = objResult,
                    };
                    DisposeCurrentSqlConnection();

                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetUploadFileCategoryList", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/UploadFilesRepository/GetUploadFileCategoryList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseModel> GetUploadFilesList(UploadFilesFIlterModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetUploadFilesList");
                    parmeters.Add("@CategoryId", objModel.CategoryId);
                    parmeters.Add("@FilesName", objModel.FilesName);
                    parmeters.Add("@SortBy", objModel.SortBy == null ? "" : objModel.SortBy);
                    parmeters.Add("@IsSortByDesc", objModel.IsSortByDesc == true ? 1 : 0);
                    parmeters.Add("@PageNo", objModel.PageNo);
                    parmeters.Add("@Pagesize", objModel.PageSize);
                    var objResult = await Con.QueryMultipleAsync("spMst_UploadFiles", parmeters, commandType: CommandType.StoredProcedure);

                    ResponseModel objResut = new()
                    {
                        Status = true,
                        Message = "",
                        Data = objResult.Read<object>(),
                        Pagination = objResult.Read<PaginationModel>()
                    };
                    DisposeCurrentSqlConnection();

                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetUploadFilesList", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/UploadFilesRepository/GetUploadFilesList");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseWithoutPaginationModel> UploadFile(UploadFilesModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "UploadFile");
                    parmeters.Add("@ActionBy", UserId);
                    parmeters.Add("@CategoryId", objModel.CategoryId);
                    parmeters.Add("@FilesName", objModel.FilesName);
                    parmeters.Add("@FilesPath", objModel.FilesPath);
                    parmeters.Add("@DisplayOrder", objModel.DisplayOrder);
                    parmeters.Add("@StartDate", objModel.StartDate);
                    parmeters.Add("@EndDate", objModel.EndDate);
                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spMst_UploadFiles", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "UploadFile", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/UploadFilesRepository/UploadFile");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseWithoutPaginationModel> DeleteFile(int FileId, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "DeleteFile");
                    parmeters.Add("@ActionBy", UserId);
                    parmeters.Add("@Id", FileId);
                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spMst_UploadFiles", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DeleteFile", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/UploadFilesRepository/DeleteFile");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseModel> GetUserManualList(UserManualFIlterModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetUserManualList");
                    parmeters.Add("@RoleId", objModel.RoleId);
                    parmeters.Add("@SortBy", objModel.SortBy == null ? "" : objModel.SortBy);
                    parmeters.Add("@IsSortByDesc", objModel.IsSortByDesc == true ? 1 : 0);
                    parmeters.Add("@PageNo", objModel.PageNo);
                    parmeters.Add("@Pagesize", objModel.PageSize);
                    var objResult = await Con.QueryMultipleAsync("spMst_UploadFiles", parmeters, commandType: CommandType.StoredProcedure);

                    ResponseModel objResut = new()
                    {
                        Status = true,
                        Message = "",
                        Data = objResult.Read<object>(),
                        Pagination = objResult.Read<PaginationModel>()
                    };
                    DisposeCurrentSqlConnection();

                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetUserManualList", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/UploadFilesRepository/GetUserManualList");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> UploadUserManual(UserManualAddEditModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    if (objModel.Id > 0)
                    {
                        parmeters.Add("@Action", "EditUserManual");
                        parmeters.Add("@UpdatedBy", UserId);
                        parmeters.Add("@Id", objModel.Id);
                    }
                    else
                    {
                        parmeters.Add("@Action", "AddUserManual");
                        parmeters.Add("@CreatedBy", UserId);
                    }                    
                    parmeters.Add("@RoleId", objModel.RoleId);
                    parmeters.Add("@FilesName", objModel.FilesName);
                    parmeters.Add("@FilesPath", objModel.FilesPath);
                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spMst_UploadFiles", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "UploadUserManual", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/UploadFilesRepository/UploadUserManual");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseWithoutPaginationModel> DeleteUserManual(int Id, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "DeleteUserManual");
                    parmeters.Add("@DeletedBy", UserId);
                    parmeters.Add("@Id", Id);
                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spMst_UploadFiles", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DeleteUserManual", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/UploadFilesRepository/DeleteUserManual");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
