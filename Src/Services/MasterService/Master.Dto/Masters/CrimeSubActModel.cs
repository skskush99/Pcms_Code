namespace Master.Dto.Masters
{
    public class CrimeSubActFilterModel
    {
        public int? CrimeActId { get; set; }
        public int CrimeClsId { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public bool? IsSortByDesc { get; set; }
    }

    public class AddEditCrimeSubActModel
    {
        public int? CrimeSubActId { get; set; }
        public int? CrimeActId { get; set; }
        public int? CrimeClsId { get; set; }
        public string CrimeSubActNameEnglish { get; set; }
        public string? CrimeSubActNameHindi { get; set; }
        public string? CrimeSubActShortName { get; set; }
        public string? CrimeSubActDescription { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
    public class ActiveDeactiveCrimeSubActModel
    {
        public int CrimeSubActId { get; set; }
        public bool IsActive { get; set; }
        public long UpdatedBy { get; set; }
    }


}
