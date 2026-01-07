namespace Master.Dto.Users
{

    public class UsersMapReqFilterModel
    {
        public long LevelId { get; set; }
        public long RoleId { get; set; }
        public long DepartmentId { get; set; }
        public long OfficeId { get; set; }
        public string? SSOID { get; set; }
        public long DistrictId { get; set; }
        public string? UserName { get; set; }
        public int IsActive { get; set; }
        public string? SortBy { get; set; }
        public bool? IsSortByDesc { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
    }
    public class UserMapReqModel
    {
        public long RId { get; set; }
        public string? RSSOID { get; set; }
        public string? RUserName { get; set; }
        public long RDesignationId { get; set; }
        public string? RDesignationName { get; set; }
        public long RDepartmentId { get; set; }
        public string? RDepartmentName { get; set; }
        public string? RDOB { get; set; }
        public string? RGender { get; set; }
        public string? ROfficialMail { get; set; }
        public string? RMobile { get; set; }
        public string? Contact { get; set; }
        public string? RAadhaarId { get; set; }
        public string? RBhamashahId { get; set; }
        public string? RBhamashahMemberId { get; set; }
        public string? RImage { get; set; }
        public long LevelId { get; set; }
        public long RoleId { get; set; }
        public long DivisionId { get; set; }
        public long DistrictId { get; set; }
        public long OfficeId { get; set; }
        public long DesignationId { get; set; }
        public long CourtId { get; set; }
        public bool IsActive { get; set; }
        public long CreatedBy { get; set; }
        public long ApprovedBy { get; set; }
        public long UpdatedBy { get; set; }
        public long DeletedBy { get; set; }
    }

    public class UserMapReqAddEditModel
    {
        public required string Tocken { get; set; }
        public required UserMapReqModel Data { get; set; }
    }

    public class ApprovelUserModel
    {
        public required string Tocken { get; set; }
        public long RId { get; set; }
        public long DepartmentId { get; set; }
        //public long CreatedBy { get; set; }
        //public long ApprovedBy { get; set; }

        //public long RoleId { get; set; }
        //public required string SSOID { get; set; }
        //public required string UserName { get; set; }
    }



}
