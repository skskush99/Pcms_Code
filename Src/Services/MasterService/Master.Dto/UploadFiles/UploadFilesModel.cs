namespace Master.Dto.UploadFiles
{
    public class UploadFilesModel
    {
        //public int Id { get; set; }
        public int CategoryId { get; set; }
        public string? FilesName { get; set; }
        public string? FilesPath { get; set; }
        public int DisplayOrder { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
    }

    public class UploadFilesFIlterModel
    {
        public int CategoryId { get; set; }
        public string? FilesName { get; set; }
        public string? SortBy { get; set; }
        public bool? IsSortByDesc { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
    }

    public class UploadFilesAddModel
    {
        public required int CategoryId { get; set; }
        public required string CategoryName { get; set; }
        public required string FilesName { get; set; }
        public int DisplayOrder { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
    }

    public class UserManualFIlterModel
    {
        public int RoleId { get; set; }
        public string? SortBy { get; set; }
        public bool? IsSortByDesc { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
    }
    public class UserManualModel
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string? FilesName { get; set; }
        public string? FilesPath { get; set; }
    }
    public class UserManualAddEditModel
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string? FilesName { get; set; }
        public string? FilesPath { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }

}
