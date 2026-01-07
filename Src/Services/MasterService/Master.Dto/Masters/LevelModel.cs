namespace Master.Dto.Masters
{
    public class LevelModelFilterModel
    {
        //public int? LevelIdId { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public bool? IsSortByDesc { get; set; }

    }
    public class LevelModel
    {
        public int LevelId { get; set; }
        public string LevelName { get; set; }
        public long CreatedBy { get; set; }
        public long UpdatedBy { get; set; }
    }

    public class LevelRequestModel
    {
        public required LevelModel Data { get; set; }
    }

    public class LevelActiveDeactiveModel
    {
        public int LevelId { get; set; }
        public bool IsActive { get; set; }
        public long UpdatedBy { get; set; }
    }
}
