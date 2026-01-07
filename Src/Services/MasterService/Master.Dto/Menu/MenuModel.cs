namespace Master.Dto.Menu
{
    public class MenuModel
    {
        public int Id { get; set; }
        public required int ParentId { get; set; }
        public string? EnglishName { get; set; }
        public string? HindiName { get; set; }
        public string? LinkPage { get; set; }
        public string? Icon { get; set; }
        public bool IsDisplay { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
    }

    public class MenuRequestModel
    {
        public required string Tocken { get; set; }
        public required MenuModel Data { get; set; }
    }

    public class MenuActiveDeactiveModel
    {
        public required string Tocken { get; set; }
        public int Id { get; set; }
        public bool Active { get; set; }
    }

    public class MenuPageLinkFilterModel
    {
        public int RoleId { get; set; }
        public int MenuId { get; set; }
    }

    public class SubMenuModel
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string? EnglishName { get; set; }
        public string? LinkPage { get; set; }
        public string? Icon { get; set; }
        public bool IsDisplay { get; set; }
        public bool IsSelected { get; set; }
        public bool IsAddPermission { get; set; }
        public bool IsEditPermission { get; set; }
        public bool IsDeletePermission { get; set; }
    }

    public class MenuMappingModel
    {
        public int Id { get; set; }
        public string? EnglishName { get; set; }
        public string? LinkPage { get; set; }
        public string? Icon { get; set; }
        public bool IsDisplay { get; set; }
        public bool IsSelected { get; set; }
        public bool IsAddPermission { get; set; }
        public bool IsEditPermission { get; set; }
        public bool IsDeletePermission { get; set; }
        public IEnumerable<SubMenuModel>? SubMenus { get; set; }
    }

    public class MenuMappingRequestModel
    {
        public int RoleId { get; set; }
        public required IEnumerable<MenuMappingModel> Data { get; set; }
    }

    public class MenuMappingRequestUserModel
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public required IEnumerable<MenuMappingModel> Data { get; set; }
    }
}
