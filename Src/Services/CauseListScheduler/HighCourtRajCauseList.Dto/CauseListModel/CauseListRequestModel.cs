using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighCourtRajCauseList.Dto.CauseListModel
{
    public class NewCauseListRequestModel
    {
        public long CauseId { get; set; }
        public string Estt { get; set; }
        public Nullable<System.DateTime> cdate { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<long> sno { get; set; }
        public string courtno { get; set; }
        public string causelistdate { get; set; }
        public string causelisttype { get; set; }
        public string ctype { get; set; }
        public string cno { get; set; }
        public Nullable<int> cyear { get; set; }
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
        public Nullable<int> croom { get; set; }
        public string cino { get; set; }
        public string croom_ju { get; set; }
    }
    public class CauseListRequestModel
    {
        public long LitesCauselistId { get; set; }
        public string CourtJuJp { get; set; }
        public string CauseListDate { get; set; }
        public string CauseListType { get; set; }
        public long BenchSBDB { get; set; }
        public string CourtNoCourtName { get; set; }
        public string CaseRegTypeName { get; set; }
        public long CaseRegNo { get; set; }
        public long CaseRegyear { get; set; }
        public string CaseAbbreviation { get; set; }
        public string JudgeName { get; set; }
        public string JudgeName2 { get; set; }
        public string PetitionerLawyerName { get; set; }
        public string RespondentLawyerName { get; set; }
        public string PetitionerName { get; set; }
        public string RespondentName { get; set; }
        public string ForOrders { get; set; }
        public string MainConnected { get; set; }
        public long CaseSerialNo { get; set; }
        public string TimeCauseList { get; set; }
        public string TimeCauseList2 { get; set; }
        public string Roster { get; set; }
        public string Note { get; set; }
        public string ApplicationDetail { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Uniquecasenumberformaincase { get; set; }
        public string Uniquecasenumberforconnectedcase { get; set; }
        public string isfinalized { get; set; }
        public bool Active { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
    }
}
