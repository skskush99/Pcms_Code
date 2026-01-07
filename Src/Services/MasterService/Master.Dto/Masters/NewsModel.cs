namespace Master.Dto.Masters
{
    public class NewsFilterModel
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public bool? IsSortByDesc { get; set; }

    }
        public class NewsModel
    {
        public int NewsId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool Active { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public long DeleteBy { get; set; }
        public DateTime DeleteOn { get; set; }
    }
    public class NewsRequestModel
    {
        //public required string Tocken { get; set; }
        public required NewsModel Data { get; set; }
    }

    public class NewsActiveDeactiveModel
    {
        //public required string Tocken { get; set; }
        public int NewsId { get; set; }
        public bool Active { get; set; }
        public long UpdatedBy { get; set; }
    }






}
