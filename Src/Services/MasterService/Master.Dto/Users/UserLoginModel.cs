namespace Master.Dto.Users
{
    public class UserLoginModel
    {
        public long UserId { get; set; }
        public long RoleId { get; set; }
        public long DepartmentId { get; set; }
        public long UnitId { get; set; }
        public long OfficeId { get; set; }
        public long OICId { get; set; }
        public long DistrictId { get; set; }
        public long LawyerId { get; set; }
        public string? SSOID { get; set; }
        public string? Name { get; set; }
        public string? DOB { get; set; }
        public string? Gender { get; set; }
        public string? Designation { get; set; }
        public string? Mobile { get; set; }
        public string? Contact { get; set; }
        public string? OfficialMail { get; set; }
        public string? PersonalMail { get; set; }
        public string? PostalAddress { get; set; }
        public string? PostalCode { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public bool Active { get; set; }
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


    public class SsoProfileModel
    {
        public required string SSOID { get; set; }
    }

    public class SsoProfileRequestModel
    {
        public required string SSOID { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? SsoBaseUrl { get; set; }
        public string? EncryptedPassword { get; set; }

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
        public long LawyerId { get; set; }
        public string? LawyerName { get; set; }
        public string? SSOID { get; set; }
    }

    public class UserSubMenuModel
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string? EnglishName { get; set; }
        public string? LinkPage { get; set; }
        public string? Icon { get; set; }
        public bool IsDisplay { get; set; }
        public bool IsAddPermission { get; set; }
        public bool IsEditPermission { get; set; }
        public bool IsDeletePermission { get; set; }
    }

    public class UserMenuModel
    {
        public int Id { get; set; }
        public string? EnglishName { get; set; }
        public string? LinkPage { get; set; }
        public string? Icon { get; set; }
        public bool IsDisplay { get; set; }
        public bool IsAddPermission { get; set; }
        public bool IsEditPermission { get; set; }
        public bool IsDeletePermission { get; set; }
        public IEnumerable<UserSubMenuModel>? SubMenus { get; set; }
    }

    public class UsersFilterModel
    {
        public long RoleId { get; set; }
        public long DepartmentId { get; set; }
        public long UnitId { get; set; }
        public long OfficeId { get; set; }
        public string? SSOID { get; set; }
        public long DistrictId { get; set; }
        public string? UserName { get; set; }
        public int Active { get; set; }
        public string? SortBy { get; set; }
        public bool? IsSortByDesc { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
    }

    public class UsersListReqestModel
    {
        public required string Tocken { get; set; }
        public required UsersFilterModel Data { get; set; }
    }

    public class UserAddEditModel
    {
        public required string Tocken { get; set; }
        public required UserLoginModel Data { get; set; }
    }

    public class MappedUserModel
    {
        public required string Tocken { get; set; }
        public long RoleId { get; set; }
        public required string SSOID { get; set; }
        public required string UserName { get; set; }
    }

    public class DemapUserModel
    {
        public required string Tocken { get; set; }
        public long UserId { get; set; }
    }
}
