namespace Master.Dto.Masters
{
    public class CourtPlacesFilterModel
    {
        public int? StateId { get; set; }
        public int? DistrictId { get; set; }
        public int? TehsilId { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public bool? IsSortByDesc { get; set; }

    }
    public class CourtPlacesModel
    {
        public int? RowID { get; set; }
        public int PlaceId { get; set; }
        public string PlaceName { get; set; }
        public int? TehsilId { get; set; }
        public string? TehsilName { get; set; }
        public int? DistrictId { get; set; }
        public string? DistrictName { get; set; }
        public int? StateId { get; set; }
        public bool Active { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public long DeleteBy { get; set; }
        public DateTime DeleteOn { get; set; }
        public int CourtTypeId { get; set; }
    }

    public class CourtPlacesRequestModel
    {
        //public required string Tocken { get; set; }
        public required CourtPlacesModel Data { get; set; }
    }

    public class CourtPlacesActiveDeactiveModel
    {
        //public required string Tocken { get; set; }
        public int PlaceId { get; set; }
        public bool Active { get; set; }
        public long UpdatedBy { get; set; }
    }
}
