using Common.Dapper;
using Microsoft.Extensions.Configuration;
using HighCourtRajCauseList.Dto.shared;
using Dapper;
using System.Data;
using HighCourtRajCauseList.Dto.CauseListModel;

namespace HighCourtRajCauseList.ServiceBus.HighCourtRajCauseList
{
    public class HighCourtRajCauseListService : SqlRepository, IHighCourtRajCauseListService
    {
        private readonly System.Data.IDbConnection Con;
        public HighCourtRajCauseListService(IConfiguration Configuration) : base(Configuration)
        {
            
        }


        public async Task<ResponseWithoutPaginationModel> AddHighCourtRajCauseList(CauseListRequestModel data)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "AddHighCourtCauselist");
                    parmeters.Add("@LitesCauselistId", data.LitesCauselistId);
                    parmeters.Add("@CourtJuJp", data.CourtJuJp);
                    parmeters.Add("@CauseListDate", data.CauseListDate);
                    parmeters.Add("@CauseListType", data.CauseListType);
                    parmeters.Add("@BenchSBDB", data.BenchSBDB);
                    parmeters.Add("@CourtNoCourtName", data.CourtNoCourtName);
                    parmeters.Add("@CaseRegTypeName", data.CaseRegTypeName);
                    parmeters.Add("@CaseRegNo", data.CaseRegNo);
                    parmeters.Add("@CaseRegyear", data.CaseRegyear);
                    parmeters.Add("@CaseAbbreviation", data.CaseAbbreviation);
                    parmeters.Add("@JudgeName", data.JudgeName);
                    parmeters.Add("@JudgeName2", data.JudgeName2);
                    parmeters.Add("@PetitionerLawyerName", data.PetitionerLawyerName);
                    parmeters.Add("@RespondentLawyerName", data.RespondentLawyerName);
                    parmeters.Add("@PetitionerName", data.PetitionerName);
                    parmeters.Add("@RespondentName", data.RespondentName);
                    parmeters.Add("@ForOrders", data.ForOrders);
                    parmeters.Add("@MainConnected", data.MainConnected);
                    parmeters.Add("@CaseSerialNo", data.CaseSerialNo);
                    parmeters.Add("@TimeCauseList", data.TimeCauseList);
                    parmeters.Add("@TimeCauseList2", data.TimeCauseList2);
                    parmeters.Add("@Roster", data.Roster);
                    parmeters.Add("@Note", data.Note);
                    parmeters.Add("@ApplicationDetail", data.ApplicationDetail);
                    parmeters.Add("@UpdateDate", data.UpdateDate);
                    parmeters.Add("@Uniquecasenumberformaincase", data.Uniquecasenumberformaincase);
                    parmeters.Add("@Uniquecasenumberforconnectedcase", data.Uniquecasenumberforconnectedcase);
                    parmeters.Add("@isfinalized", data.isfinalized);
                    parmeters.Add("@active", data.Active);
                    
                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spTrn_CaseRegistrationHighCourtCauselist", parmeters, commandType: CommandType.StoredProcedure);
                    var objResult = objData.FirstOrDefault();
                    Console.WriteLine(objResult.Message);
                    DisposeCurrentSqlConnection();
                    return objResult != null ? objResult : new ResponseWithoutPaginationModel();
                }
            }


            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ResponseWithoutPaginationModel> AddNewHighCourtRajCauseList(NewCauseListRequestModel data)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "AddHighCourtCauselist");
                    parmeters.Add("@Estt", data.Estt);
                    parmeters.Add("@Cdate", data.cdate);
                    parmeters.Add("@AddedOn",data.AddedOn );
                    parmeters.Add("@Sno", data.sno);
                    parmeters.Add("@Courtno", data.courtno);
                    parmeters.Add("@CauseListDate",data.causelistdate);
                    parmeters.Add("@CauseListType", data.causelisttype);
                    parmeters.Add("@Ctype", data.ctype);
                    parmeters.Add("@Cno", data.cno);
                    parmeters.Add("@Cyear", data.cyear);
                    parmeters.Add("@Pet", data.pet);
                    parmeters.Add("@Res", data.res);
                    parmeters.Add("@Law1", data.law1);
                    parmeters.Add("@Law2", data.law2);
                    parmeters.Add("@Stg", data.stg);
                    parmeters.Add("@Judname", data.judname);
                    parmeters.Add("@Judname2", data.judname2);
                    parmeters.Add("@Padv", data.padv);
                    parmeters.Add("@Radv", data.radv);
                    parmeters.Add("@Case_no", data.case_no);
                    parmeters.Add("@Pet_org_name", data.pet_org_name);
                    parmeters.Add("@Res_org_name", data.res_org_name);
                    parmeters.Add("@Div_ben", data.div_ben);
                    parmeters.Add("@Croom", data.div_ben);
                    parmeters.Add("@Cino", data.cino);
                    parmeters.Add("@Croom_ju", data.croom_ju);

                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spAPI_CauseListData", parmeters, commandType: CommandType.StoredProcedure);
                    var objResult = objData.FirstOrDefault();
                    //Console.WriteLine(objResult.Message);
                    DisposeCurrentSqlConnection();
                    return objResult != null ? objResult : new ResponseWithoutPaginationModel();
                }
            }


            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ResponseWithoutPaginationModel> JustDeptScheduler(string JsonData, string CourtType)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "JustDeptScheduler");
                    parmeters.Add("@JsonData", JsonData);
                    parmeters.Add("@CourtType", CourtType);

                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spAPI_HCScheduler", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                    var objResult = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResult != null ? objResult : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ResponseWithoutPaginationModel> CaseRegistrationHighCourtScheduler(string JsonData, string CourtType)
        {
            try
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "CaseRegistrationHighCourtScheduler");
                    parmeters.Add("@JsonData", JsonData);
                    parmeters.Add("@CourtType", CourtType);

                    var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spAPI_HCScheduler", parmeters, commandTimeout: 300, commandType: CommandType.StoredProcedure);
                    var objResult = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResult != null ? objResult : new ResponseWithoutPaginationModel();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
