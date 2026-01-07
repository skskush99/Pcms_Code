namespace Master.Dto.Masters
{
    public class NodalOfficerFilterModel
    {
        public int? AdmDeptId { get; set; }
        public int? UnitId { get; set; }
        public int? Role { get; set; }
        public int DistrictId { get; set; } = 0;
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public bool? IsSortByDesc { get; set; }
    }
    public class NodalOfficerModel
    {
        public int LicID { get; set; }
        public int? AdmDeptId { get; set; }
        public string? AdmDepttName { get; set; }
        public int? UnitId { get; set; }
        public string? UnitName { get; set; }
        public int Level { get; set; }
        public string? Role { get; set; }
        public string? Name { get; set; }
        public string? Designation { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public int? DistrictId { get; set; }
        public string? DistrictName { get; set; }
        public string? Mobile { get; set; }
        public string? Fax { get; set; }
        public string? Email { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public bool Active { get; set; }
        public long CreatedBy { get; set; }
        public long UpdatedBy { get; set; }
    }

    public class NodalOfficerRequestModel
    {
        //public required string Tocken { get; set; }
        public required NodalOfficerModel Data { get; set; }
    }

    public class NodalOfficerActiveDeactiveModel
    {
        //public required string Tocken { get; set; }
        public int LicID { get; set; }
        public bool Active { get; set; }
        public long UpdatedBy { get; set; }
        public long DeleteBy { get; set; }
    }

}
