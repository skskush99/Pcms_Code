namespace Master.Dto.Masters
{
    public class CircularOrderFilterModel
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public bool? IsSortByDesc { get; set; }
    }
    public class CircularOrderModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FilePath { get; set; }
        //public long UploadedBy { get; set; }
        //public long UpdatedBy { get; set; }

    }

    public class CircularOrderAddModel
    {
        public int Id { get; set; }
        public required string Title { get; set; }
    }
    public class CircularOrderRequestModel
    {
        //public required string Tocken { get; set; }
        public required CircularOrderModel Data { get; set; }
    }

    public class CircularOrderActiveDeactiveModel
    {
       // public required string Tocken { get; set; }
        public int Id { get; set; }
        public bool Active { get; set; }
        public long UpdatedBy { get; set; }
    }






}
