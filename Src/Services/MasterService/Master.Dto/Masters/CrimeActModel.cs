namespace Master.Dto.Masters
{

    public class CrimeActFilterModel
    {
        public int CrimeClsId { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public bool? IsSortByDesc { get; set; }
    }

    public class AddEditCrimeActModel
    {
        public int? CrimeActId { get; set; }
        public int? CrimeClsId { get; set; }
        public string CrimeActNameEnglish { get; set; }
        public string? CrimeActNameHindi { get; set; }
        public string? CrimeActShortName { get; set; }
        public string? CrimeActDescription { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
    public class ActiveDeactiveCrimeActModel
    {
        public int CrimeActId { get; set; }
        public bool IsActive { get; set; }
        public long UpdatedBy { get; set; }
    }




}
