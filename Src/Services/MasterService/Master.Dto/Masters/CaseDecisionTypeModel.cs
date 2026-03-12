namespace Master.Dto.Masters
{
    public class CaseDecisionTypeFilterModel
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public bool? IsSortByDesc { get; set; }
    }

    public class AddEditCaseDecisionTypeModel
    {
        public int? DecisionTypeId { get; set; }
        public string DecisionTypeEnglish { get; set; }
        public string? DecisionTypeHindi { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
    public class ActiveDeactiveCaseDecisionTypeModel
    {
        public int DecisionTypeId { get; set; }
        public bool IsActive { get; set; }
        public long UpdatedBy { get; set; }
    }



}
