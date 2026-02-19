using Report.Dto.Global;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Dto.Dashboard
{
    public partial class Trn_CaseRegistrations
    {
        public int CaseDecide { get; set; }
        public string Token { get; set; }
        public string CNR { get; set; }
        public int SubjectMatterId { get; set; }

        public int SubjectCategoryId { get; set; }

        public int CourtTypeId { get; set; }

        public string AdmDeptName { get; set; }

        public string UnitName { get; set; }

        public string OfficeName { get; set; }

        public string CourtName { get; set; }

        public string CourtTypeName { get; set; }

        public string SubjectCategoryName { get; set; }

        public string SubjectSubCategoryName { get; set; }

        public string SubjectMatterName { get; set; }

        public string SubjectSubMatterName { get; set; }

        public string OICName { get; set; }

        public string LawyerName { get; set; }

        public string AbbrevationName { get; set; }

        public string AbbrevationShort { get; set; }

        public string CaseRegDate { get; set; }

        public string LICName { get; set; }

        public string LICDesignation { get; set; }
        public string Designation { get; set; }

        public string ExPartyDateStr { get; set; }

        public string RowNum { get; set; }

        public int CourtPlace { get; set; }

        public string PlaceName { get; set; }
        public string Name { get; set; }

        public string DateDeptttoAG_AAG { get; set; }

        public string DateCaseinCourtbyAG_AAG { get; set; }

        public string DistrictName { get; set; }

        public string PriorityName { get; set; }

        public string AppellantName { get; set; }

        public string RespondentName { get; set; }

        public DateTime? NextHearing_Date { get; set; }

        public DateTime? NextHearingHC { get; set; }

        public DateTime? DecisionDate { get; set; }

        public DateTime? GenerateSectionDate { get; set; }

        public bool Decision_FA { get; set; }

        public string OICAppointed { get; set; }

        public string ReplyFiledYesNo { get; set; }

        public string GroupingName { get; set; }

        public string HC_StayGranted_YN { get; set; }

        public string OICMobileNo { get; set; }

        public int Court { get; set; }

        public int CourtType { get; set; }

        public Nullable<System.DateTime> DecisionCreatedDate { get; set; }

        public Trn_CaseRegistrations Remand { get; set; }

        public Trn_CaseRegistrations_Main_PartyData Main_Party { get; set; }

        public ApplicationTransactionMessage TransactionMessage { get; set; }

        public string chart { get; set; }
        public string PetitionerName { get; set; }

        public class CaseRegis
        {
            public string R_E_Implication { get; set; }

            public string CaseNo { get; set; }
            //public string CRNNumber { get; set; }
            public int AdmDepttId { get; set; }

            public int UnitId { get; set; }

            public int OfficeId { get; set; }

            public int CourtId { get; set; }

            public int CourtPlace { get; set; }

            public int AbbreviationId { get; set; }

            public string CaseRegDate { get; set; }

            public int CaseYear { get; set; }

            public int CourtTypeId { get; set; }

            public int FileNo { get; set; }

            public int SubjectCategoryId { get; set; }

            public int SubjectMatterId { get; set; }

            public int SubjectSubCategoryId { get; set; }

            public string SubjectSubMatterId { get; set; }

        }
    }

    public partial class Trn_CaseRegistrations_Main_PartyData
    {
        public string Main_AdmDepttName { get; set; }
        public string Main_UnitName { get; set; }
        public string Main_OfficeName { get; set; }
        public string Main_Court_Plc { get; set; }
        public string Main_Abb_CaseNo_Year { get; set; }
        public string Main_Appellant { get; set; }
        public string Main_Respondent { get; set; }
        public string States { get; set; }
        public int Main_CaseId { get; set; }
        public string Main_Massage { get; set; }
        public string Main_OIC { get; set; }
    }

    public partial class Trn_CaseRegistrations
    {
        public int CaseId { get; set; }
        public Nullable<int> CaseId_Pcms { get; set; }
        public Nullable<int> AdmDepttId { get; set; }
        public Nullable<int> UnitId { get; set; }
        public Nullable<int> CourtId { get; set; }
        public Nullable<int> OfficeId { get; set; }
        public Nullable<int> PlaceId { get; set; }
        public Nullable<int> ParentId { get; set; }
        public Nullable<int> RemandId { get; set; }
        public string PriorityCode { get; set; }
        public string AppellantOrResponded { get; set; }
        public Nullable<int> AbbreviationId { get; set; }
        public Nullable<int> SubjectSubCategoryId { get; set; }
        public Nullable<int> SubjectSubMatterId { get; set; }
        public Nullable<int> CaseType { get; set; }
        public Nullable<int> DivisionId { get; set; }
        public Nullable<int> DistrictId { get; set; }
        public Nullable<int> CaseStatus { get; set; }
        public Nullable<int> CaseNo { get; set; }
        public Nullable<int> CaseYear { get; set; }
        public Nullable<int> OICId { get; set; }
        public Nullable<int> LawyerId { get; set; }
        public string WACPNo { get; set; }
        public Nullable<int> PriorityId { get; set; }
        public string Bench { get; set; }
        public Nullable<System.DateTime> CaseRegistrationDate { get; set; }
        public string FileNo { get; set; }
        public string R_E_Implication { get; set; }
        public bool Does_P_O_A { get; set; }
        public bool Does_P_A_PD { get; set; }
        public string DPAPS { get; set; }
        public Nullable<bool> IsExParty { get; set; }
        public Nullable<System.DateTime> ExPartyDate { get; set; }
        public string Reason { get; set; }
        public string Remark { get; set; }
        public string DeleteMobileNo { get; set; }
        public string PrimarySecondary { get; set; }
        public Nullable<bool> IsTransfered { get; set; }
        public Nullable<int> AppellantCount { get; set; }
        public Nullable<int> RespondantCount { get; set; }
        public Nullable<int> LawyerCount { get; set; }
        public Nullable<int> OICCount { get; set; }
        public Nullable<int> HearingCount { get; set; }
        public Nullable<int> DecisionCount { get; set; }
        public Nullable<int> ContemptCount { get; set; }
        public string SubjectMatter_Detail { get; set; }
        public string DataStatus { get; set; }
        public string PenDec { get; set; }
        public string Cancel_Flag { get; set; }
        public string Updation_Mode { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> LastUpdatedBy { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public Nullable<int> DeleteConfirmedBy { get; set; }
        public Nullable<System.DateTime> DeleteConfirmedDate { get; set; }
        public Nullable<int> DeleteBy { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<int> SubPriorityId { get; set; }
        public Nullable<System.DateTime> DateCaseFillingDeptToAG_AAG { get; set; }
        public Nullable<System.DateTime> DateFillingCaseCourtByAG_AAG { get; set; }
        public Nullable<bool> ApplicationUnderSec5FiledYN { get; set; }
        public Nullable<bool> IsEmployee { get; set; }
        public string CRNNumber { get; set; }
        public Nullable<int> GroupingId { get; set; }
        public Nullable<int> LinkCaseId { get; set; }
        public Nullable<int> AORId { get; set; }
        public Nullable<int> SrAdv { get; set; }
        public Nullable<int> PreCaseNo { get; set; }
        public List<CaseHearing> CaseHearings { get; set; }
        public List<CaseDecision> CaseDecisions { get; set; }
        public List<CaseContempt> CaseContempts { get; set; }
    }

    public class CaseHearing
    {
        public int RowID { get; set; }
        public string HearingDate { get; set; }
        public string Admitted { get; set; }
        public string HC_WritAppealSplAppeal { get; set; }
        public string StayGranted { get; set; }
        public string AnyMiscAppfiled { get; set; }
        public string Reply_filed { get; set; }
        public string ArgumentOver { get; set; }
        public string Judgment_PR { get; set; }
        public string Appliedforcopy { get; set; }
        public string ReceivedCopy { get; set; }
        public string NextHearing_Date { get; set; }
    }

    public class CaseDecision
    {
        public int RowID { get; set; }
        public string DecisionDate { get; set; }
        public string StayGranted { get; set; }
        public string PD_DecisionCompliedDate { get; set; }
    }

    public class CaseContempt
    {
        public int RowID { get; set; }
        public string Contempt_No { get; set; }
        public string Contempt_Date { get; set; }
        public string Contempt_GovtParty { get; set; }
        public string Contempt_NonGovtParty { get; set; }
        public string AdvocateAppointed { get; set; }
        public string ContempReplyFiledDate { get; set; }
        public string ContempHearingDate { get; set; }
    }

}
