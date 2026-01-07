using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Email.Dto.Email
{
    public class EmailDataModel
    {
        public int EId { get; set; }
        public int SNo { get; set; }
        public string ReceiverName { get; set; }
        public string AdmDeptName { get; set; }
        public string UnitName { get; set; }
        public string NodalMobile { get; set; }
        public string NodalEmail { get; set; }
        public string OfficeName { get; set; }
        public string AbCaseNoYear { get; set; }
        public string CourtNamePlace { get; set; }
        public string AppealantDesg { get; set; }
        public string RespondentDesg { get; set; }
        public string LawyersMobileNo { get; set; }
        public string OICNameMobileNo { get; set; }
        public string PriorityName { get; set; }
        public string DecisionHearing { get; set; }
        public string Status { get; set; }
        public string ReplyFiled { get; set; }
        public string DecisionDate { get; set; }
        public string NextHearing_Date { get; set; }
        public string CaseRegistrationDate { get; set; }
        public string Subject { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Role { get; set; }
    }
}
