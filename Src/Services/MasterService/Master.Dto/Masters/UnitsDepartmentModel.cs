namespace Master.Dto.Masters
{
    public class UnitsDepartmentFilterModel
    {
        public int? ActiveFilter { get; set; }
        public int? AdmDeptId { get; set; }
        public int DistrictId { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public bool? IsSortByDesc { get; set; }
    }
        public class UnitsDepartmentModel
    {
        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public string? UnitShortName { get; set; }
        public int? AdmDeptId { get; set; }
        public int NicUnitId { get; set; }
        public int DistrictId { get; set; }
        public bool Active { get; set; }
        public int? ActiveFilter { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public long DeleteBy { get; set; }
        public DateTime DeleteOn { get; set; }
        public string? AdmDeptName { get; set; }
    }

    public class UnitsDepartmentRequestModel
    {
        public required UnitsDepartmentModel Data { get; set; }
    }

    public class UnitsDepartmentActiveDeactiveModel
    {
        public int UnitId { get; set; }
        public bool Active { get; set; }
        public long UpdatedBy { get; set; }
    }

}
