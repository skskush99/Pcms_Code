namespace Master.Dto.Masters
{
    public class CourtTypesFilterModel
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public bool? IsSortByDesc { get; set; }

    }
    public class CourtTypesModel
    {
        public int? RowID { get; set; }
        public int CourtTypeId { get; set; }
        public string CourtTypeName { get; set; }
        public string? CourtTypeShortName { get; set; }
        public int OrderNo { get; set; }
        public bool Active { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public long DeleteBy { get; set; }
        public DateTime DeleteOn { get; set; }

    }

    public class CourtTypesRequestModel
    {
        //public required string Tocken { get; set; }
        public required CourtTypesModel Data { get; set; }
    }

    public class CourtTypesActiveDeactiveModel
    {
        // public required string Tocken { get; set; }
        public int CourtTypeId { get; set; }
        public bool Active { get; set; }
        public long UpdatedBy { get; set; }
    }
}
