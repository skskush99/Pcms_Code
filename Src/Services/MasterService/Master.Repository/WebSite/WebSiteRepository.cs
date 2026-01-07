using Master.Dto.WebSite;
using Master.Dto.Shared;
using Common.Dapper;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using Common.Repository;

namespace Master.Repository.WebSite
{
    public class WebSiteRepository : SqlRepository, IWebSiteRepository
    {
        private readonly LogsService _logsService;
        private readonly System.Data.IDbConnection Con;
        public WebSiteRepository(IConfiguration Configuration, LogsService logsService) : base(Configuration)
        {
            _logsService = logsService;
        }

        public async Task<ResponseModel> GetWebSiteUploadFilesList(WebSitesFIlterModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetWebSiteUploadFilesList");
                    parmeters.Add("@CategoryId", objModel.CategoryId);
                    parmeters.Add("@SortBy", objModel.SortBy == null ? "" : objModel.SortBy);
                    parmeters.Add("@IsSortByDesc", objModel.IsSortByDesc == true ? 1 : 0);
                    parmeters.Add("@PageNo", objModel.PageNo);
                    parmeters.Add("@Pagesize", objModel.PageSize);
                    var objResult = await Con.QueryMultipleAsync("spMst_UploadFilesWebSite", parmeters, commandType: CommandType.StoredProcedure);

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
                _logsService.Logs("Error", "GetWebSiteUploadFilesList", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/WebSiteRepository/GetWebSiteUploadFilesList");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> WebSiteUploadFile(WebSitesModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();

                    if (objModel.Id > 0)
                    {
                        parmeters.Add("@Action", "EditWebSiteUploadFile");
                        parmeters.Add("@UpdatedBy", UserId);
                        parmeters.Add("@Id", objModel.Id);
                    }
                    else
                    {
                        parmeters.Add("@Action", "AddWebSiteUploadFile");
                        parmeters.Add("@CreatedBy", UserId);
                    }
                    parmeters.Add("@CategoryId", objModel.CategoryId);
                    parmeters.Add("@Title", objModel.Title);
                    parmeters.Add("@Description", objModel.Description);
                    parmeters.Add("@ImagePath", objModel.ImagePath == null ? "" : objModel.ImagePath);
                    parmeters.Add("@LinkURL", objModel.LinkURL == null ? "" : objModel.LinkURL);
                    parmeters.Add("@DisplayOrder", objModel.DisplayOrder);
                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spMst_UploadFilesWebSite", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "WebSiteUploadFile", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/WebSiteRepository/WebSiteUploadFile");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> WebSiteContact(WebSitesContactAddModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();

                    if (objModel.Id > 0)
                    {
                        parmeters.Add("@Action", "EditWebSiteContact");
                        parmeters.Add("@UpdatedBy", UserId);
                        parmeters.Add("@Id", objModel.Id);
                    }
                    else
                    {
                        parmeters.Add("@Action", "AddWebSiteContact");
                        parmeters.Add("@CreatedBy", UserId);
                    }
                    parmeters.Add("@CategoryId", objModel.CategoryId);
                    parmeters.Add("@Title", objModel.Title);
                    parmeters.Add("@Description", objModel.Description);
                    parmeters.Add("@DisplayOrder", objModel.DisplayOrder);
                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spMst_UploadFilesWebSite", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "WebSiteUploadFile", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/WebSiteRepository/WebSiteUploadFile");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> WebSiteActiveDeActiveFile(int Id, int Active, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "ActiveDeActiveFile");
                    parmeters.Add("@Id", Id);
                    parmeters.Add("@Active", Active);
                    parmeters.Add("@UpdatedBy", UserId);
                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spMst_UploadFilesWebSite", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "WebSiteActiveDeActiveFile", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/WebSiteRepository/WebSiteActiveDeActiveFile");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }


    }
}
