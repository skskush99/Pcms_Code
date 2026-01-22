namespace Master.Dto.Masters
{
    public class FIRStatusFilterModel
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public bool? IsSortByDesc { get; set; }
    }

    public class AddEditFIRStatusModel
    {
        public int? FirStatusId { get; set; }
        public string FirStatusNameEnglish { get; set; }
        public string? FirStatusNameHindi { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
    public class ActiveDeactiveFIRStatusModel
    {
        public int FirStatusId { get; set; }
        public bool IsActive { get; set; }
        public long UpdatedBy { get; set; }
    }
}
