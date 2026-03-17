using Case.Dto.ComplaintRegister;
using Case.Dto.DierRegistrations;
using Case.Dto.Shared;
using Common.Dapper;
using Common.Repository;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Case.Repository.ComplaintRegister
{
    public class ComplaintRegisterRepository : SqlRepository, IComplaintRegisterRepository
    {
        private readonly LogsService _logsService;
        private readonly System.Data.IDbConnection Con;
        public ComplaintRegisterRepository(IConfiguration Configuration, LogsService logsService) : base(Configuration)
        {
            _logsService = logsService;
        }

        public async Task<ResponseModel> GetComplaintList(ComplaintListFilterModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetComplaintList");
                    parmeters.Add("@SortBy", objModel.SortBy ?? "");
                    parmeters.Add("@IsSortByDesc", objModel.IsSortByDesc == true ? 1 : 0);
                    parmeters.Add("@PageNo", objModel.PageNo);
                    parmeters.Add("@Pagesize", objModel.PageSize);
                    var objResult = await Con.QueryMultipleAsync("spTrn_ComplaintRegister", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);

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
                _logsService.Logs("Error", "GetComplaintList", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/ComplaintRegisterRepository/GetComplaintList");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ComplaintRegisterResponseModel> AddEditComplaintRegister(ComplaintRegisterModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parameters = new DynamicParameters();

                    if (objModel.ComplaintRegId > 0)
                    {
                        parameters.Add("@Action", "ComplaintEdit");
                        parameters.Add("@UpdatedBy", UserId);
                        parameters.Add("@ComplaintRegId", objModel.ComplaintRegId);
                    }
                    else
                    {
                        parameters.Add("@Action", "ComplaintAdd");
                        parameters.Add("@CreatedBy", UserId);
                    }

                    parameters.Add("@ComplaintRegNo", objModel.ComplaintRegNo ?? "");
                    parameters.Add("@ComplaintNo", objModel.ComplaintNo ?? "");
                    parameters.Add("@ComplaintDate", objModel.ComplaintDate);
                    parameters.Add("@ComplaintTypeID", objModel.ComplaintTypeID);
                    parameters.Add("@DepartmentId", objModel.DepartmentId);
                    parameters.Add("@DeptOfficerNameDesignation", objModel.DeptOfficerNameDesignation ?? "");
                    parameters.Add("@OffenceBrief", objModel.OffenceBrief ?? "");
                    parameters.Add("@DateFiledInCourt", objModel.DateFiledInCourt);
                    parameters.Add("@ComplaintFirstPageDocs", objModel.ComplaintFirstPageDocs ?? "");
                    parameters.Add("@FullComplaintDocs", objModel.FullComplaintDocs ?? "");
                    parameters.Add("@OtherDocs", objModel.OtherDocs ?? "");
                    parameters.Add("@IsDeclaration", objModel.IsDeclaration);
                    parameters.Add("@CaseStatus", objModel.CaseStatus);
                    parameters.Add("@PersonAgainstId", objModel.PersonAgainstId);
                    parameters.Add("@OffenceClassifId", objModel.OffenceClassifId);
                    parameters.Add("@ClassificationID", objModel.ClassificationID);
                    parameters.Add("@IsCognizance", objModel.IsCognizance);

                    var objData = await Con.QueryAsync<ComplaintRegisterResponseModel>("spTrn_ComplaintRegister",parameters,commandTimeout: 300,commandType: CommandType.StoredProcedure);
                    var objResult = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResult ?? new ComplaintRegisterResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error","AddEditComplaintRegister",ex.Message,ex.StackTrace,ex.Source,"CaseService/Case.Repository/ComplaintRegisterRepository/AddEditComplaintRegister");
                return new ComplaintRegisterResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }


        public async Task<ResponseWithoutPaginationModel> GetPersonAgainstDetails(long ComplaintRegId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetPersonAgainst");
                    parmeters.Add("@ComplaintRegId", ComplaintRegId);
                    var objResult = await Con.QueryAsync("spTrn_PersonAgainstDetails", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);

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
                _logsService.Logs("Error", "GetPersonAgainstDetails", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/ComplaintRegisterRepository/GetPersonAgainstDetails");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ComplaintRegisterResponseModel> AddEditPersonAgainstDetails(PersonAgainstDetailsModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parameters = new DynamicParameters();

                    if (objModel.PersonAgainstId > 0)
                    {
                        parameters.Add("@Action", "PersonAgainstEdit");
                        parameters.Add("@PersonAgainstId", objModel.PersonAgainstId);
                        parameters.Add("@UpdatedBy", UserId);
                    }
                    else
                    {
                        parameters.Add("@Action", "PersonAgainstAdd");
                        parameters.Add("@CreatedBy", UserId);
                    }
                    parameters.Add("@ComplaintRegId", objModel.ComplaintRegId);
                    parameters.Add("@Name", objModel.Name);
                    parameters.Add("@Address", objModel.Address);
                    parameters.Add("@Designation", objModel.Designation);
                    parameters.Add("@Institution", objModel.Institution);

                    var objData = await Con.QueryAsync<ComplaintRegisterResponseModel>("spTrn_PersonAgainstDetails", parameters,commandTimeout: 300,commandType: CommandType.StoredProcedure);
                    var result = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();

                    return result ?? new ComplaintRegisterResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error","AddEditPersonAgainstDetails",ex.Message,ex.StackTrace,ex.Source,"CaseService/Case.Repository/ComplaintRegisterRepository/AddEditPersonAgainstDetails");

                return new ComplaintRegisterResponseModel()
                {
                    Status = false,
                    Message = ex.Message
                };
            }
        }


        public async Task<ComplaintRegisterResponseModel> DeletePersonAgainstDetails(long PersonAgainstId, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parameters = new DynamicParameters();

                    parameters.Add("@Action", "DeletePersonAgainst");
                    parameters.Add("@DeletedBy", UserId);
                    parameters.Add("@PersonAgainstId", PersonAgainstId);

                    var objData = await Con.QueryAsync<ComplaintRegisterResponseModel>("spTrn_PersonAgainstDetails",parameters,commandTimeout: 300,commandType: CommandType.StoredProcedure);
                    var objResult = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResult ?? new ComplaintRegisterResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error","DeletePersonAgainstDetails",ex.Message,ex.StackTrace,ex.Source,"CaseService/Case.Repository/ComplaintRegisterRepository/DeletePersonAgainstDetails");
                return new ComplaintRegisterResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

    }
}
