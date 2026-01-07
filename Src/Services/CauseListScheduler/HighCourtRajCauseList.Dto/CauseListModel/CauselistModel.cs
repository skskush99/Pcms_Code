using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighCourtRajCauseList.Dto.CauseListModel
{
    public class CauselistModel
    {
        public string status { get; set; }
        public string Message { get; set; }
        public CauselistResult[] result { get; set; }
    }

    public class NewCauselistModel
    {
        public string status { get; set; }
        public string Message { get; set; }
        public List<CauseModel> result { get; set; }
    }

    public class CauselistResult
    {
        public Causelist Causelist { get; set; }
    }
    public class CauseModel
    {
        public int sno { get; set; }
        public string courtno { get; set; }
        public string causelistdate { get; set; }
        public string causelisttype { get; set; }
        public string ctype { get; set; }
        public string cno { get; set; }
        public int cyear { get; set; }
        public string pet { get; set; }
        public string res { get; set; }
        public string law1 { get; set; }
        public string law2 { get; set; }
        public string stg { get; set; }
        public string judname { get; set; }
        public string judname2 { get; set; }
        public string padv { get; set; }
        public string radv { get; set; }
        public string case_no { get; set; }
        public string pet_org_name { get; set; }
        public string res_org_name { get; set; }
        public string div_ben { get; set; }
        public string croom { get; set; }
        public string croom_numeric { get; set; }
        public string cino { get; set; }
    }
    public class Causelist
    {
        public string CauseListDate { get; set; }
        public string CauseListType { get; set; }
        public string BenchSBDB { get; set; }
        public string CourtNoCourtName { get; set; }
        public string CaseRegTypeName { get; set; }
        public string CaseRegNo { get; set; }
        public int CaseRegyear { get; set; }
        public string CaseAbbreviation { get; set; }
        public string JudgeName { get; set; }
        public string JudgeName2 { get; set; }
        public string PetitionerLawyerName { get; set; }
        public string RespondentLawyerName { get; set; }
        public string PetitionerName { get; set; }
        public string RespondentName { get; set; }
        public string ForOrders { get; set; }
        public string MainConnected { get; set; }
        public int CaseSerialNo { get; set; }
        public string TimeCauseList { get; set; }
        public string TimeCauseList2 { get; set; }
        public string Roster { get; set; }
        public string Note { get; set; }
        public string ApplicationDetail { get; set; }
        public string UpdateDate { get; set; }
        public string Uniquecasenumberformaincase { get; set; }
        public string Uniquecasenumberforconnectedcase { get; set; }
        public string isfinalized { get; set; }
    }

    public class DeptHC
    {
        public string message { get; set; }
        public string Status { get; set; }
        public DeptResult[] result { get; set; }
    }

    public class DeptResult
    {
        public Department Department { get; set; }
    }
    public class Department
    {
        public int orgid { get; set; }
        public string orgname { get; set; }
        public int orgtype { get; set; }

    }

    public class CaseDataHC
    {
        public string status { get; set; }
        public string Message { get; set; }
        public CaseDataResult[] result { get; set; }
    }

    public class CaseDataResult
    {
        public Casedetail CaseDetail { get; set; }
        public CaseDetailDecided CaseDetailDecided { get; set; }
        public Petitionerparty[] PetitionerParty { get; set; }
        public Respondentparty[] RespondentParty { get; set; }
        public Orderdetail[] OrderDetail { get; set; }
        public PetitionerpartyD[] PetitionerPartyD { get; set; }
        public RespondentpartyD[] RespondentPartyD { get; set; }
        public OrderdetailD[] OrderDetailD { get; set; }
        public object[] CopyingTran { get; set; }
    }

    public class Casedetail
    {
        public int purpose_next { get; set; }
        public string case_no { get; set; }
        public int DepartmentId { get; set; }
        public string CreateModify { get; set; }
        public string DepartmentName { get; set; }
        public int? DepartmentType { get; set; }
        public int? CaseCategory { get; set; }
        public int? CaseRegTypeId { get; set; }
        public string CaseRegTypeName { get; set; }
        public int? RegNo { get; set; }
        public int? RegYear { get; set; }
        public int FilingNo { get; set; }
        public int FilingYear { get; set; }
        public string FilingDate { get; set; }
        public string RegistrationDate { get; set; }
        public string PetitionerAdvocateName { get; set; }
        public string PetitionerAdvocateMobile { get; set; }
        public string RespondentAdvocateName { get; set; }
        public string RespondentAdvocateMobile { get; set; }
        public string PetitionerName { get; set; }
        public string RespondentName { get; set; }
        public int? PreviousCourtCode { get; set; }
        public string PreviousCourtCaseCode { get; set; }
        public string PreviousCourtCaseType { get; set; }
        public string PreviousCourtCaseNo { get; set; }
        public string PreviousCourtCaseYear { get; set; }
        public string PreviousCourtName { get; set; }
        public int Status { get; set; }
        public string Stage { get; set; }
        public int? BencType { get; set; }
        public string CNR { get; set; }
        public string NextHearingDate { get; set; }
        public string CaseDecisionDate { get; set; }
        public string cino { get; set; }
    }
    public class CaseDetailDecided
    {
        public int purpose_next { get; set; }
        public string case_no { get; set; }
        public int DepartmentId { get; set; }
        public string CreateModify { get; set; }
        public string DepartmentName { get; set; }
        public int? DepartmentType { get; set; }
        public int? CaseCategory { get; set; }
        public int? CaseRegTypeId { get; set; }
        public string CaseRegTypeName { get; set; }
        public int? RegNo { get; set; }
        public int? RegYear { get; set; }
        public int FilingNo { get; set; }
        public int FilingYear { get; set; }
        public string FilingDate { get; set; }
        public string RegistrationDate { get; set; }
        public string PetitionerAdvocateName { get; set; }
        public string PetitionerAdvocateMobile { get; set; }
        public string RespondentAdvocateName { get; set; }
        public string RespondentAdvocateMobile { get; set; }
        public string PetitionerName { get; set; }
        public string RespondentName { get; set; }
        public int? PreviousCourtCode { get; set; }
        public string PreviousCourtCaseCode { get; set; }
        public string PreviousCourtCaseType { get; set; }
        public string PreviousCourtCaseNo { get; set; }
        public string PreviousCourtCaseYear { get; set; }
        public string PreviousCourtName { get; set; }
        public int Status { get; set; }
        public string Stage { get; set; }
        public int? BencType { get; set; }
        public string CNR { get; set; }
        public string NextHearingDate { get; set; }
        public string CaseDecisionDate { get; set; }
        public string cino { get; set; }
    }
    public class Petitionerparty
    {
        public string name { get; set; }
        public int party_no { get; set; }
        public string pet_mobile { get; set; }
        public string address { get; set; }
        public string cino { get; set; }
    }
    public class Respondentparty
    {
        public string name { get; set; }
        public int party_no { get; set; }
        public string pet_mobile { get; set; }
        public string address { get; set; }
        public string cino { get; set; }
    }
    public class Orderdetail
    {
        public int order_no { get; set; }
        public int doc_type { get; set; }
        public string order_dt { get; set; }
        public string cino { get; set; }
    }
    public class PetitionerpartyD
    {
        public string name { get; set; }
        public int party_no { get; set; }
        public string pet_mobile { get; set; }
        public string address { get; set; }
        public string cino { get; set; }
    }
    public class RespondentpartyD
    {
        public string name { get; set; }
        public int party_no { get; set; }
        public string pet_mobile { get; set; }
        public string address { get; set; }
        public string cino { get; set; }
    }
    public class OrderdetailD
    {
        public int order_no { get; set; }
        public int doc_type { get; set; }
        public string order_dt { get; set; }
        public string cino { get; set; }
    }
}
