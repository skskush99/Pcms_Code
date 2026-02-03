namespace Master.Dto.Masters
{
    public class JanPratinidhiFilterModel
    {
        public int? PostId { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public bool? IsSortByDesc { get; set; }
    }

    public class JanPratinidhiModel
    {
        public int PostId { get; set; }
        public string PostNameEnglish { get; set; }
        public string? PostNameHindi { get; set; }
        public string? PostShortForm { get; set; }
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
    public class JanPratinidhiRequestModel
    {
        public required JanPratinidhiModel Data { get; set; }
    }
    public class JanPratinidhiActiveDeactiveModel
    {
        public int PostId { get; set; }
        public bool IsActive { get; set; }
        public long? UpdatedBy { get; set; }
        public long? DeletedBy { get; set; }
    }
}
