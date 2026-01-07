namespace Master.Dto.Masters
{
    public class AdminRequestFilterModel
    {
        public string? MajorMinor { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public bool? IsSortByDesc { get; set; }

    }
    public class AdminDepartmentModel
    {
        //public int? RowID { get; set; }
        public int AdmDeptId { get; set; }
        public string AdmDeptName { get; set; }
        public string? AdmDeptShortName { get; set; }
        public string? MajorMinor { get; set; }
        public bool IsActive { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDt { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDt { get; set; }
        public long DeleteBy { get; set; }
        public DateTime DeleteDt { get; set; }
    }

    public class AdminRequestModel
    {
        public required AdminDepartmentModel Data { get; set; }
    }

    public class AdminActiveDeactiveModel
    {
        public int AdmDeptId { get; set; }
        public bool Active { get; set; }
        public long UpdatedBy { get; set; }
    }
}
