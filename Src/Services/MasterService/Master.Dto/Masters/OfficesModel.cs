namespace Master.Dto.Masters
{
    public class OfficesFilterModel
    {
        public int? OfficeId { get; set; }
        public int? DistrictId { get; set; }
        public int? IsActive { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public bool? IsSortByDesc { get; set; }

    }
    public class OfficesModel
    {
        public int? OfficeId { get; set; }
        public string OfficeEng { get; set; }
        public string? OfficeHindi { get; set; }
        public int? DistrictId { get; set; }
        public int? IsActive { get; set; }
        public long CreatedBy { get; set; }
        public long UpdatedBy { get; set; }
    }
    public class OfficesRequestModel
    {
        public required OfficesModel Data { get; set; }
    }
    public class OfficesActiveDeactiveModel
    {
        public int OfficeId { get; set; }
        public bool IsActive { get; set; }
        public long UpdatedBy { get; set; }
    }
}
