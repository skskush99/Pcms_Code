using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Master.Dto.Masters
{
    public class CaseDecisionReasonFilterModel
    {
        public int? DecisionTypeId { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public bool? IsSortByDesc { get; set; }
    }

    public class AddEditCaseDecisionReasonModel
    {
        public int? DecisionReasonId { get; set; }
        public string DecisionReasonEnglish { get; set; }
        public string? DecisionReasonHindi { get; set; }
        public int? DecisionTypeId { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
    public class ActiveDeactiveCaseDecisionReasonModel
    {
        public int DecisionReasonId { get; set; }
        public bool IsActive { get; set; }
        public long UpdatedBy { get; set; }
    }



}
