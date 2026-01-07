namespace Authentication.Dto.Shared
{
    public class ResponseModel
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public IEnumerable<object>? Data { get; set; }
        public IEnumerable<PaginationModel>? Pagination { get; set; }
    }

    public class ResponseWithoutPaginationModel
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public IEnumerable<object>? Data { get; set; }
    }

    public class ResponseUserMappingModel
    {
        public bool Status { get; set; }
        public bool UserMappingReq { get; set; }
        public string? Message { get; set; }
        public IEnumerable<object>? Data { get; set; }
    }

    public class PaginationModel
    {
        public string? SortBy { get; set; }
        public bool? IsSortByDesc { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public long TotalRecords { get; set; }
    }

    public class RequestModel
    {
        public required string Tocken { get; set; }
        public required object Data { get; set; }
    }

    public class TokenAuthModel
    {
        public string? Token { get; set; }
        public bool Status { get; set; }
        public string? Message { get; set; }
        public long UserId { get; set; }
        public long RoleId { get; set; }
        public string? LoginOn { get; set; }
        public string? IPAddress { get; set; }
    }

    public class ActiveDeactiveModel
    {
        public required string Tocken { get; set; }
        public int Id { get; set; }
        public bool Status { get; set; }
        public long ActionBy { get; set; }
    }

    public class DropdownlistModel
    {
        public required string Text { get; set; }
        public required string Value { get; set; }
    }

    public class SSOServiceResponse
    {
        public string sAMAccountName { get; set; }
        public List<string> Roles { get; set; }
    }

    public class LoginDetails1Model
    {
        public string? SSOID { get; set; }
    }
    public class LoginDetailsModel
    {
        public required string Token { get; set; }
        public long UserId { get; set; }
        public string? UserName { get; set; }
        public string? Name { get; set; }
        public long RoleId { get; set; }
        public string? RoleName { get; set; }
        public long DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public long UnitId { get; set; }
        public string? UnitName { get; set; }
        public long OfficeId { get; set; }
        public string? OfficeName { get; set; }
        public long DistrictId { get; set; }
        public string? DistrictName { get; set; }
        public string? SSOID { get; set; }
    }
    public class LoginDetails2Model
    {
        public int RId { get; set; }
        public string? RSSOID { get; set; }
        public string? RUserName { get; set; }
        public string? RDOB { get; set; }
        public long RDesignationId { get; set; }
        public string? RDesignationName { get; set; }
        public string? RDepartmentName { get; set; }
        public string? RMobile { get; set; }
        public string? ROfficialMail { get; set; }
        public string? RAadhaarId { get; set; }
        public long RoleId { get; set; }
        public string? RoleName { get; set; }
        public long DivisionId { get; set; }
        public string? DivisionName { get; set; }
        public long DistrictId { get; set; }
        public string? DistrictName { get; set; }
        public long OfficeId { get; set; }
        public string? OfficeName { get; set; }
        public long CourtId { get; set; }
        public string? CourtName { get; set; }
        public long? UserMapped { get; set; }
        public long? IsActive { get; set; }
    }

    public class LoginModel
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? SSOID { get; set; }
        public string? IPAddress { get; set; }
        public string? SSOToken { get; set; }
        public bool IsSSOLogin { get; set; }
    }

    public class SSOIDMappedModel
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string SSOID { get; set; }
    }

    public class ResponseWithoutPaginationModel_New
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public IEnumerable<object>? Data { get; set; }
        public string SSOID { get; set; }
    }
    public class LoginModel_New
    {
        public string? SSOID { get; set; }
        public string? IPAddress { get; set; }
        public string? SSOToken { get; set; }
        public bool IsSSOLogin { get; set; }
    }

    public class SsoProfileDtModel
    {
        public required string SSOID { get; set; }
    }
    public class SsoProfileDtRequestModel
    {
        public required string SSOID { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? SsoBaseUrl { get; set; }
        public string? EncryptedPassword { get; set; }

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
}
