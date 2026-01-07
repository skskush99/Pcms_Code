using Common.Dapper;
using Common.Repository;
using Dapper;
using Master.Dto.Masters;
using Master.Dto.Shared;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Master.Repository.State
{
    public class StateRepository : SqlRepository, IState
    {
        private readonly System.Data.IDbConnection Con;
        private readonly LogsService _logsService;
        public StateRepository(IConfiguration configuration, LogsService logsService) : base(configuration)
        {
            _logsService = logsService;
        }
        public async Task<ResponseModel> StateList(StateFilterModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "StatesList");
                    parmeters.Add("@PageNo", objModel.PageNo);
                    parmeters.Add("@Pagesize", objModel.PageSize);
                    parmeters.Add("@SortBy", objModel.SortBy == null ? "" : objModel.SortBy);
                    parmeters.Add("@IsSortByDesc", objModel.IsSortByDesc == true ? 1 : 0);
                    var objResult = await Con.QueryMultipleAsync("spMstState", parmeters, commandType: CommandType.StoredProcedure);

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
                _logsService.Logs("Error", "StateList", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/StateRepository/StateList");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseWithoutPaginationModel> GetStateList()
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetStatesList");
                    var objData = await Con.QueryAsync<DropdownlistModel>("spMstState", parmeters, commandType: CommandType.StoredProcedure);

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
                _logsService.Logs("Error", "GetStateList", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/StateRepository/GetStateList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseModel> DivisionsList(StateFilterModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "DivisionsList");
                    parmeters.Add("@PageNo", objModel.PageNo);
                    parmeters.Add("@Pagesize", objModel.PageSize);
                    parmeters.Add("@SortBy", objModel.SortBy == null ? "" : objModel.SortBy);
                    parmeters.Add("@IsSortByDesc", objModel.IsSortByDesc == true ? 1 : 0);
                    var objResult = await Con.QueryMultipleAsync("spMstState", parmeters, commandType: CommandType.StoredProcedure);

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
                _logsService.Logs("Error", "DivisionsList", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/StateRepository/DivisionsList");
                return new ResponseModel()
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
                _logsService.Logs("Error", "GetDivisionsList", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/StateRepository/GetDivisionsList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<ResponseModel> DistrictsList(DistrictsFilterModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "DistrictsList");
                    parmeters.Add("@DivisionId", objModel.DivisionId);
                    parmeters.Add("@StateId", objModel.StateId);
                    parmeters.Add("@PageNo", objModel.PageNo);
                    parmeters.Add("@Pagesize", objModel.PageSize);
                    parmeters.Add("@SortBy", objModel.SortBy == null ? "" : objModel.SortBy);
                    parmeters.Add("@IsSortByDesc", objModel.IsSortByDesc == true ? 1 : 0);
                    var objResult = await Con.QueryMultipleAsync("spMstState", parmeters, commandType: CommandType.StoredProcedure);

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
                _logsService.Logs("Error", "DistrictsList", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/StateRepository/DistrictsList");
                return new ResponseModel()
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
                    };
                    DisposeCurrentSqlConnection();
                    return objResut;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetDistrictsList", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/StateRepository/GetDistrictsList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        
        
        //public async Task<ResponseModel> CityList(CityFilterModel objModel)
        //{
        //    try
        //    {
        //        using (var Con = GetOpenConnection())
        //        {
        //            var parmeters = new DynamicParameters();
        //            parmeters.Add("@Action", "CityList");
        //            parmeters.Add("@DistrictId", objModel.DistrictId);
        //            parmeters.Add("@PageNo", objModel.PageNo);
        //            parmeters.Add("@Pagesize", objModel.PageSize);
        //            parmeters.Add("@SortBy", objModel.SortBy == null ? "" : objModel.SortBy);
        //            parmeters.Add("@IsSortByDesc", objModel.IsSortByDesc == true ? 1 : 0);
        //            var objResult = await Con.QueryMultipleAsync("spMstState", parmeters, commandType: CommandType.StoredProcedure);

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
        //        _logsService.Logs("Error", "CityList", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/StateRepository/CityList");
        //        return new ResponseModel()
        //        {
        //            Status = false,
        //            Message = ex.Message,
        //        };
        //    }
        //}
        //public async Task<ResponseModel> GetCityList(int DistrictId)
        //{
        //    try
        //    {
        //        using (var Con = GetOpenConnection())
        //        {
        //            var parmeters = new DynamicParameters();
        //            parmeters.Add("@Action", "GetCityList");
        //            parmeters.Add("@DistrictId", DistrictId);
        //            var objData = await Con.QueryAsync<DropdownlistModel>("spMstState", parmeters, commandType: CommandType.StoredProcedure);

        //            ResponseModel objResut = new ResponseModel();
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
        //        _logsService.Logs("Error", "GetCityList", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/StateRepository/GetCityList");
        //        return new ResponseModel()
        //        {
        //            Status = false,
        //            Message = ex.Message,
        //        };
        //    }
        //}
        //public async Task<ResponseModel> TehsilsList(CityFilterModel objModel)
        //{
        //    try
        //    {
        //        using (var Con = GetOpenConnection())
        //        {
        //            var parmeters = new DynamicParameters();
        //            parmeters.Add("@Action", "TehsilsList");
        //            parmeters.Add("@DistrictId", objModel.DistrictId);
        //            parmeters.Add("@PageNo", objModel.PageNo);
        //            parmeters.Add("@Pagesize", objModel.PageSize);
        //            parmeters.Add("@SortBy", objModel.SortBy == null ? "" : objModel.SortBy);
        //            parmeters.Add("@IsSortByDesc", objModel.IsSortByDesc == true ? 1 : 0);
        //            var objResult = await Con.QueryMultipleAsync("spMstState", parmeters, commandType: CommandType.StoredProcedure);

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
        //        _logsService.Logs("Error", "TehsilsList", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/StateRepository/TehsilsList");
        //        return new ResponseModel()
        //        {
        //            Status = false,
        //            Message = ex.Message,
        //        };
        //    }
        //}
        //public async Task<ResponseModel> GetTehsilsList(int DistrictId)
        //{
        //    try
        //    {
        //        using (var Con = GetOpenConnection())
        //        {
        //            var parmeters = new DynamicParameters();
        //            parmeters.Add("@Action", "GetTehsilsList");
        //            parmeters.Add("@DistrictId", DistrictId);
        //            var objData = await Con.QueryAsync<DropdownlistModel>("spMstState", parmeters, commandType: CommandType.StoredProcedure);

        //            ResponseModel objResut = new ResponseModel();
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
        //        _logsService.Logs("Error", "GetTehsilsList", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/StateRepository/GetTehsilsList");
        //        return new ResponseModel()
        //        {
        //            Status = false,
        //            Message = ex.Message,
        //        };
        //    }
        //}
        //public async Task<ResponseModel> SubDivisionsList(SubDivisionsFilterModel objModel)
        //{
        //    try
        //    {
        //        using (var Con = GetOpenConnection())
        //        {
        //            var parmeters = new DynamicParameters();
        //            parmeters.Add("@Action", "SubDivisionsList");
        //            parmeters.Add("@DivisionId", objModel.DivisionId);
        //            parmeters.Add("@DistrictId", objModel.DistrictId);
        //            parmeters.Add("@PageNo", objModel.PageNo);
        //            parmeters.Add("@Pagesize", objModel.PageSize);
        //            parmeters.Add("@SortBy", objModel.SortBy == null ? "" : objModel.SortBy);
        //            parmeters.Add("@IsSortByDesc", objModel.IsSortByDesc == true ? 1 : 0);
        //            var objResult = await Con.QueryMultipleAsync("spMstState", parmeters, commandType: CommandType.StoredProcedure);

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
        //        _logsService.Logs("Error", "SubDivisionsList", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/StateRepository/SubDivisionsList");
        //        return new ResponseModel()
        //        {
        //            Status = false,
        //            Message = ex.Message,
        //        };
        //    }
        //}
        //public async Task<ResponseWithoutPaginationModel> GetSubDivisionsList(int DivisionId, int DistrictId)
        //{
        //    try
        //    {
        //        using (var Con = GetOpenConnection())
        //        {
        //            var parmeters = new DynamicParameters();
        //            parmeters.Add("@Action", "GetSubDivisionsList");
        //            parmeters.Add("@DivisionId", DivisionId);
        //            parmeters.Add("@DistrictId", DistrictId);
        //            var objData = await Con.QueryAsync<DropdownlistModel>("spMstState", parmeters, commandType: CommandType.StoredProcedure);

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
        //        _logsService.Logs("Error", "GetSubDivisionsList", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/StateRepository/GetSubDivisionsList");
        //        return new ResponseWithoutPaginationModel()
        //        {
        //            Status = false,
        //            Message = ex.Message,
        //        };
        //    }
        //}



    }
}
