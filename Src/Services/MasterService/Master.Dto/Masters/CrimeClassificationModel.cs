namespace Master.Dto.Masters
{
    public class CrimeClassificationFilterModel
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public bool? IsSortByDesc { get; set; }
    }

    public class AddEditCrimeClassificationModel
    {
        public int? CrimeClsId { get; set; }
        public string CrimeClsNameEnglish { get; set; }
        public string? CrimeClsNameHindi { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
    public class ActiveDeactiveCrimeClassificationModel
    {
        public int CrimeClsId { get; set; }
        public bool IsActive { get; set; }
        public long UpdatedBy { get; set; }
    }





}
