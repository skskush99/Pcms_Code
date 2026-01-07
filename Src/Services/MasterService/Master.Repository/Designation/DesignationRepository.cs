using Common.Dapper;
using Common.Repository;
using Dapper;
using Master.Dto.Masters;
using Master.Dto.Shared;
using Microsoft.Extensions.Configuration;
using System.Data;


namespace Master.Repository.Designation
{
    public class DesignationRepository : SqlRepository, IDesignation
    {
        private readonly LogsService _logsService;
        private readonly System.Data.IDbConnection Con;
        public DesignationRepository(IConfiguration configuration, LogsService logsService) : base(configuration)
        {
            _logsService = logsService;
        }

        public async Task<ResponseModel> GetDesignation(DesignationFilterModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetDesignation");
                    parmeters.Add("@PageNo", objModel.PageNo);
                    parmeters.Add("@Pagesize", objModel.PageSize);
                    parmeters.Add("@SortBy", objModel.SortBy == null ? "" : objModel.SortBy);
                    parmeters.Add("@IsSortByDesc", objModel.IsSortByDesc == true ? 1 : 0);
                    var objResult = await Con.QueryMultipleAsync("spMstDesignation", parmeters, commandType: CommandType.StoredProcedure);

                    ResponseModel objResut = new()
                    {
                        Status = true,
                        Message = "",
                        Data = objResult.Read<DesignationModel>(),
                        Pagination = objResult.Read<PaginationModel>()
                    };
                    DisposeCurrentSqlConnection();
                    return objResut;

                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetDesignation", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/DesignationRepository/GetDesignation");
                return new ResponseModel()
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
                _logsService.Logs("Error", "GetDesignationDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/DesignationRepository/GetDesignationDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseModel> AddEditDesignation(DesignationModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "AddEditDesignation");
                    parmeters.Add("@DesignationId", objModel.DesignationId);
                    parmeters.Add("@DesignationEng", objModel.DesignationEng);
                    parmeters.Add("@DesignationHindi", objModel.DesignationHindi);
                    parmeters.Add("@LevelId", objModel.LevelId);
                    parmeters.Add("@IsActive", objModel.IsActive == true ? 1 : 0);
                    parmeters.Add("@CreatedBy", UserId);
                    parmeters.Add("@UpdatedBy", UserId);
                    var objData = await Con.QueryAsync<ResponseModel>("spMstDesignation", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditDesignation", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/DesignationRepository/AddEditDesignation");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseModel> ActiveDeactiveDesignation(DesignationActiveDeactiveModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "ActiveDeactiveDesignation");
                    parmeters.Add("@DesignationId", objModel.DesignationId);
                    parmeters.Add("@IsActive", objModel.IsActive == true ? 1 : 0);
                    parmeters.Add("@UpdatedBy", UserId);
                    var objData = await Con.QueryAsync<ResponseModel>("spMstDesignation", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ActiveDeactiveDesignation", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/DesignationRepository/ActiveDeactiveDesignation");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        //public async Task<ResponseModel> GetDesignationRajmaster(DesignationFilterModel objModel)
        //{
        //    try
        //    {
        //        using (var Con = GetOpenConnection())
        //        {
        //            var parmeters = new DynamicParameters();
        //            parmeters.Add("@Action", "GetDesignationRaj");
        //            parmeters.Add("@PageNo", objModel.PageNo);
        //            parmeters.Add("@Pagesize", objModel.PageSize);
        //            parmeters.Add("@SortBy", objModel.SortBy == null ? "" : objModel.SortBy);
        //            parmeters.Add("@IsSortByDesc", objModel.IsSortByDesc == true ? 1 : 0);
        //            var objResult = await Con.QueryMultipleAsync("spMstDesignation", parmeters, commandType: CommandType.StoredProcedure);

        //            ResponseModel objResut = new()
        //            {
        //                Status = true,
        //                Message = "",
        //                Data = objResult.Read<object>(),
        //                Pagination = objResult.Read<PaginationModel>()
        //            };
        //            DisposeCurrentSqlConnection();
        //            return objResut;

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logsService.Logs("Error", "GetDesignationRajmaster", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/DesignationRepository/GetDesignationRajmaster");
        //        return new ResponseModel()
        //        {
        //            Status = false,
        //            Message = ex.Message,
        //        };
        //    }
        //}
        //public async Task<ResponseWithoutPaginationModel> GetDesignationRajmasterDropdownList()
        //{
        //    try
        //    {
        //        using (var Con = GetOpenConnection())
        //        {
        //            var parmeters = new DynamicParameters();
        //            parmeters.Add("@Action", "GetDesignationRajDropdownList");
        //            var objData = await Con.QueryAsync<DropdownlistModel>("spMstDesignation", parmeters, commandType: CommandType.StoredProcedure);

        //            ResponseWithoutPaginationModel objResut = new ResponseWithoutPaginationModel();
        //            {
        //                objResut.Status = true;
        //                objResut.Message = "";
        //                objResut.Data = objData;
        //            };
        //            DisposeCurrentSqlConnection();
        //            return objResut;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logsService.Logs("Error", "GetDesignationRajmasterDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/DesignationRepository/GetDesignationRajmasterDropdownList");
        //        return new ResponseWithoutPaginationModel()
        //        {
        //            Status = false,
        //            Message = ex.Message,
        //        };
        //    }
        //}


        /////////// OISc Designation Mapping Start
        //public async Task<ResponseModel> GetOICSDesigMapping(OICSDesigMappingFilterModel objModel)
        //{
        //    try
        //    {
        //        using (var Con = GetOpenConnection())
        //        {
        //            var parmeters = new DynamicParameters();
        //            parmeters.Add("@Action", "GetOICSDesigMapping");
        //            parmeters.Add("@AdminDeptId", objModel.AdminDeptId);
        //            parmeters.Add("@UnitId", objModel.UnitId);
        //            parmeters.Add("@ActiveFilter", objModel.ActiveFilter);
        //            parmeters.Add("@PageNo", objModel.PageNo);
        //            parmeters.Add("@Pagesize", objModel.PageSize);
        //            parmeters.Add("@SortBy", objModel.SortBy == null ? "" : objModel.SortBy);
        //            parmeters.Add("@IsSortByDesc", objModel.IsSortByDesc == true ? 1 : 0);
        //            var objResult = await Con.QueryMultipleAsync("spMstDesignation", parmeters, commandType: CommandType.StoredProcedure);

        //            ResponseModel objResut = new()
        //            {
        //                Status = true,
        //                Message = "",
        //                Data = objResult.Read<object>(),
        //                Pagination = objResult.Read<PaginationModel>()
        //            };
        //            DisposeCurrentSqlConnection();
        //            return objResut;

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logsService.Logs("Error", "GetOICSDesigMapping", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/DesignationRepository/GetOICSDesigMapping");
        //        return new ResponseModel()
        //        {
        //            Status = false,
        //            Message = ex.Message,
        //        };
        //    }
        //}
        //public async Task<ResponseWithoutPaginationModel> GetOICSDesigMappingDropdownList(int AdminDeptId, int UnitId)
        //{
        //    try
        //    {
        //        using (var Con = GetOpenConnection())
        //        {
        //            var parmeters = new DynamicParameters();
        //            parmeters.Add("@Action", "GetOICSDesigMappingDropdownList");
        //            parmeters.Add("@AdminDeptId", AdminDeptId);
        //            parmeters.Add("@UnitId", UnitId);
        //            var objData = await Con.QueryAsync<DropdownlistModel>("spMstDesignation", parmeters, commandType: CommandType.StoredProcedure);

        //            ResponseWithoutPaginationModel objResut = new ResponseWithoutPaginationModel();
        //            {
        //                objResut.Status = true;
        //                objResut.Message = "";
        //                objResut.Data = objData;
        //            };
        //            DisposeCurrentSqlConnection();
        //            return objResut;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logsService.Logs("Error", "GetOICSDesigMappingDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/DesignationRepository/GetOICSDesigMappingDropdownList");
        //        return new ResponseWithoutPaginationModel()
        //        {
        //            Status = false,
        //            Message = ex.Message,
        //        };
        //    }
        //}
        //public async Task<ResponseModel> AddEditOICSDesigMapping(OICSDesigMappingModel objModel, int UserId)
        //{
        //    try
        //    {
        //        using (var Con = GetOpenConnection())
        //        {
        //            var parmeters = new DynamicParameters();
        //            parmeters.Add("@Action", "AddEditOICSDesigMapping");
        //            parmeters.Add("@DesignationId", objModel.DesignationId);
        //            parmeters.Add("@AdminDeptId", objModel.AdminDeptId);
        //            parmeters.Add("@UnitId", objModel.UnitId);
        //            parmeters.Add("@ExistMstDesignationId", objModel.ExistMstDesignationId);
        //            parmeters.Add("@SectionName", objModel.SectionName);
        //            parmeters.Add("@RajMasterDesignationId", objModel.RajMasterDesignationId);
        //            parmeters.Add("@RajMasterDesignationName", objModel.RajMasterDesignationName);
        //            parmeters.Add("@CreatedBy", UserId);
        //            parmeters.Add("@UpdatedBy", UserId);
        //            parmeters.Add("@IfBracket", objModel.IfBracket);

        //            var objData = await Con.QueryAsync<ResponseModel>("spMstDesignation", parmeters, commandType: CommandType.StoredProcedure);
        //            var objResut = objData.FirstOrDefault();
        //            DisposeCurrentSqlConnection();
        //            return objResut != null ? objResut : new ResponseModel();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logsService.Logs("Error", "AddEditOICSDesigMapping", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/DesignationRepository/AddEditOICSDesigMapping");
        //        return new ResponseModel()
        //        {
        //            Status = false,
        //            Message = ex.Message,
        //        };
        //    }
        //}
        //public async Task<ResponseModel> ActiveDeactiveOICSDesigMapping(OICsDesigActiveDeactiveModel objModel, int UserId)
        //{
        //    try
        //    {
        //        using (var Con = GetOpenConnection())
        //        {
        //            var parmeters = new DynamicParameters();
        //            parmeters.Add("@Action", "ActiveDeactiveOICSDesigMapping");
        //            parmeters.Add("@DesignationId", objModel.DesignationId);
        //            parmeters.Add("@Active", objModel.Active == true ? 1 : 0);
        //            parmeters.Add("@UpdatedBy", UserId);
        //            parmeters.Add("@DeleteBy", UserId);
        //            var objData = await Con.QueryAsync<ResponseModel>("spMstDesignation", parmeters, commandType: CommandType.StoredProcedure);
        //            var objResut = objData.FirstOrDefault();
        //            DisposeCurrentSqlConnection();
        //            return objResut != null ? objResut : new ResponseModel();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logsService.Logs("Error", "ActiveDeactiveDesignation", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/DesignationRepository/ActiveDeactiveDesignation");
        //        return new ResponseModel()
        //        {
        //            Status = false,
        //            Message = ex.Message,
        //        };
        //    }
        //}

        /////////// OISc Designation Mapping End

        /////////// OISc Designation Section Start
        //public async Task<ResponseModel> GetSection(SectionFilterModel objModel)
        //{
        //    try
        //    {
        //        using (var Con = GetOpenConnection())
        //        {
        //            var parmeters = new DynamicParameters();
        //            parmeters.Add("@Action", "GetSection");
        //            parmeters.Add("@PageNo", objModel.PageNo);
        //            parmeters.Add("@Pagesize", objModel.PageSize);
        //            parmeters.Add("@SortBy", objModel.SortBy == null ? "" : objModel.SortBy);
        //            parmeters.Add("@IsSortByDesc", objModel.IsSortByDesc == true ? 1 : 0);
        //            var objResult = await Con.QueryMultipleAsync("spMstSection", parmeters, commandType: CommandType.StoredProcedure);

        //            ResponseModel objResut = new()
        //            {
        //                Status = true,
        //                Message = "",
        //                Data = objResult.Read<object>(),
        //                Pagination = objResult.Read<PaginationModel>()
        //            };
        //            DisposeCurrentSqlConnection();
        //            return objResut;

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logsService.Logs("Error", "GetSection", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/DesignationRepository/GetSection");
        //        return new ResponseModel()
        //        {
        //            Status = false,
        //            Message = ex.Message,
        //        };
        //    }
        //}
        //public async Task<ResponseWithoutPaginationModel> GetSectionDropdownList(int AdmDeptId, int UnitId)
        //{
        //    try
        //    {
        //        using (var Con = GetOpenConnection())
        //        {
        //            var parmeters = new DynamicParameters();
        //            parmeters.Add("@Action", "GetSectionDropdownList");
        //            parmeters.Add("@AdmDeptId", AdmDeptId);
        //            parmeters.Add("@UnitId", UnitId);
        //            var objData = await Con.QueryAsync<DropdownlistModel>("spMstSection", parmeters, commandType: CommandType.StoredProcedure);

        //            ResponseWithoutPaginationModel objResut = new ResponseWithoutPaginationModel();
        //            {
        //                objResut.Status = true;
        //                objResut.Message = "";
        //                objResut.Data = objData;
        //            };
        //            DisposeCurrentSqlConnection();
        //            return objResut;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logsService.Logs("Error", "GetSectionDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/DesignationRepository/GetSectionDropdownList");
        //        return new ResponseWithoutPaginationModel()
        //        {
        //            Status = false,
        //            Message = ex.Message,
        //        };
        //    }
        //}

        //public async Task<ResponseModel> AddEditSection(SectionModel objModel, int UserId)
        //{
        //    try
        //    {
        //        using (var Con = GetOpenConnection())
        //        {
        //            var parmeters = new DynamicParameters();
        //            parmeters.Add("@Action", "AddEditSection");
        //            parmeters.Add("@SectionId", objModel.SectionId);
        //            parmeters.Add("@SectionName", objModel.SectionName);
        //            parmeters.Add("@CreatedBy", UserId);
        //            parmeters.Add("@UpdatedBy", UserId);
        //            var objData = await Con.QueryAsync<ResponseModel>("spMstSection", parmeters, commandType: CommandType.StoredProcedure);
        //            var objResut = objData.FirstOrDefault();
        //            DisposeCurrentSqlConnection();
        //            return objResut != null ? objResut : new ResponseModel();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logsService.Logs("Error", "AddEditSection", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/DesignationRepository/AddEditSection");
        //        return new ResponseModel()
        //        {
        //            Status = false,
        //            Message = ex.Message,
        //        };
        //    }
        //}
        //public async Task<ResponseModel> ActiveDeactiveSection(SectionActiveDeactiveModel objModel, int UserId)
        //{
        //    try
        //    {
        //        using (var Con = GetOpenConnection())
        //        {
        //            var parmeters = new DynamicParameters();
        //            parmeters.Add("@Action", "ActiveDeactiveOICSDesigMapping");
        //            parmeters.Add("@SectionId", objModel.SectionId);
        //            parmeters.Add("@Active", objModel.Active == true ? 1 : 0);
        //            parmeters.Add("@UpdatedBy", UserId);
        //            parmeters.Add("@DeleteBy", UserId);
        //            var objData = await Con.QueryAsync<ResponseModel>("spMstSection", parmeters, commandType: CommandType.StoredProcedure);
        //            var objResut = objData.FirstOrDefault();
        //            DisposeCurrentSqlConnection();
        //            return objResut != null ? objResut : new ResponseModel();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logsService.Logs("Error", "ActiveDeactiveSection", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/DesignationRepository/ActiveDeactiveSection");
        //        return new ResponseModel()
        //        {
        //            Status = false,
        //            Message = ex.Message,
        //        };
        //    }
        //}

        /////////// OISc Designation Section End

    }
}
