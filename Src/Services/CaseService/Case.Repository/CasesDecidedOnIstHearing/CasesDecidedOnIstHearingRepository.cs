using Case.Dto.CaseRegistrations;
using Case.Dto.CasesDecidedOnIstHearing;
using Case.Dto.Shared;
using Common.Dapper;
using Common.Repository;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Case.Repository.CasesDecidedOnIstHearing
{
    public class CasesDecidedOnIstHearingRepository : SqlRepository, ICasesDecidedOnIstHearingRepository
    {
        private readonly LogsService _logsService;
        private readonly System.Data.IDbConnection Con;
        public CasesDecidedOnIstHearingRepository(IConfiguration Configuration, LogsService logsService) : base(Configuration)
        {
            _logsService = logsService;
        }

        public async Task<ResponseModel> GetCaseList(CasesDecidedOnIstHearingFilterModel objModel)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "GetCaseList");
                    parmeters.Add("@AdmDepttId", objModel.AdmDepttId);
                    parmeters.Add("@UnitId", objModel.UnitId);
                    parmeters.Add("@OfficeId", objModel.OfficeId);
                    parmeters.Add("@CourtTypeId", objModel.CourtTypeId);
                    parmeters.Add("@AbbreviationId", objModel.AbbreviationId);
                    parmeters.Add("@CaseYear", objModel.CaseYear);
                    parmeters.Add("@PageNo", objModel.PageNo);
                    parmeters.Add("@CRNNumber", objModel.CRNNumber == null ? "" : objModel.CRNNumber);
                    parmeters.Add("@CaseNo", objModel.CaseNo == null ? 0 : objModel.CaseNo);
                    parmeters.Add("@SortBy", objModel.SortBy == null ? "" : objModel.SortBy);
                    parmeters.Add("@IsSortByDesc", objModel.IsSortByDesc == true ? 1 : 0);
                    parmeters.Add("@Pagesize", objModel.PageSize);
                    parmeters.Add("@CaseType", Core.CaseType.CasesDecided1stHearing);
                    var objResult = await Con.QueryMultipleAsync("spCasesDecidedOnIstHearing", parmeters, commandType: CommandType.StoredProcedure);

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
                _logsService.Logs("Error", "GetCaseList", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/CasesDecidedOnIstHearingRepository/GetCaseList");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<CaseRegistrationsResponseModel> AddCase(CasesDecidedOnIstHearingModel objModel, int UserId)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "AddCase");
                    parmeters.Add("@CreatedBy", UserId);
                    parmeters.Add("@CRNNumber", objModel.CRNNumber);
                    parmeters.Add("@AdmDepttId", objModel.AdmDepttId);
                    parmeters.Add("@UnitId", objModel.UnitId);
                    parmeters.Add("@OfficeId", objModel.OfficeId);
                    parmeters.Add("@PlaceId", objModel.PlaceId);
                    parmeters.Add("@CourtId", objModel.CourtId);
                    parmeters.Add("@AbbreviationId", objModel.AbbreviationId);
                    parmeters.Add("@CaseYear", objModel.CaseYear);
                    parmeters.Add("@CaseNo", objModel.CaseNo);
                    parmeters.Add("@SubjectSubCategoryId", objModel.SubjectSubCategoryId);
                    parmeters.Add("@SubjectSubMatterId", objModel.SubjectSubMatterId);
                    parmeters.Add("@PrimarySecondary", objModel.PrimarySecondary);
                    parmeters.Add("@PriorityId", objModel.PriorityId);
                    parmeters.Add("@SubPriorityId", objModel.SubPriorityId);
                    parmeters.Add("@FileNo", objModel.FileNo);
                    parmeters.Add("@AppellantOrResponded", objModel.AppellantOrResponded);
                    parmeters.Add("@R_E_Implication", objModel.R_E_Implication);
                    parmeters.Add("@Does_P_O_A", objModel.Does_P_O_A == true ? 1 : 0);
                    parmeters.Add("@Does_P_A_PD", objModel.Does_P_A_PD == true ? 1 : 0);
                    parmeters.Add("@CaseRegistrationDate", objModel.CaseRegistrationDate);
                    parmeters.Add("@Bench", objModel.Bench);
                    parmeters.Add("@WACPNo", objModel.WACPNo);
                    parmeters.Add("@Remark", objModel.Remark);
                    parmeters.Add("@CaseType", Core.CaseType.CasesDecided1stHearing);

                    var objData = await Con.QueryAsync<CaseRegistrationsResponseModel>("spCasesDecidedOnIstHearing", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new CaseRegistrationsResponseModel();
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddCase", ex.Message, ex.StackTrace, ex.Source, "CaseService/Case.Repository/CasesDecidedOnIstHearingRepository/AddCase");
                return new CaseRegistrationsResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
