namespace Master.Dto.Masters
{
    public class CourtNamesFilterModel
    {
        public int? JCourtId { get; set; }
        public int? DistrictId { get; set; }
        public int? DivisionId { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public bool? IsSortByDesc { get; set; }

    }
    public class CourtNamesListModel
    {
        public int? RowID { get; set; }
        public int CourtId { get; set; }
        public int CourtTypeId { get; set; }
        public string? CourtTypeName { get; set; }
        public int PlaceId { get; set; }
        public string CourtName { get; set; }
        public string PlaceName { get; set; }
        public int TehsilId { get; set; }
        public int DistrictId { get; set; }
        public int StateId { get; set; }
        public bool Active { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDt { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDt { get; set; }
        public long DeleteBy { get; set; }
        public DateTime DeleteDt { get; set; }
    }
    public class CourtNamesModel
    {
        public int JCourtId { get; set; }
        public int JCourtCode { get; set; }
        public string JCourtEng { get; set; }
        public string JCourtHindi { get; set; }
        public int DivisionId { get; set; }
        public int DistrictId { get; set; }
        public int OfficeId { get; set; }
        public bool IsActive { get; set; }
        public long CreatedBy { get; set; }
        public long UpdatedBy { get; set; }
    }



    public class CourtNamesRequestModel
    {
        public required CourtNamesModel Data { get; set; }
    }

    public class CourtNamesActiveDeactiveModel
    {
        public int JCourtId { get; set; }
        public bool IsActive { get; set; }
        public long UpdatedBy { get; set; }
    }
}
