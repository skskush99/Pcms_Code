namespace Master.Dto.Masters
{
    public class StateModel
    {
        public int Id { get; set; }
        public int? StateId { get; set; }
        public int? DistrictId { get; set; }
        public int? DivisionId { get; set; }

    }
    public class StateModelRequestModel
    {
        //public required string Tocken { get; set; }
        public required StateModel Data { get; set; }
    }

    public class StateFilterModel
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public bool? IsSortByDesc { get; set; }

    }

    public class DistrictsFilterModel
    {
        public int? DivisionId { get; set; }
        public int? StateId { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public bool? IsSortByDesc { get; set; }

    }

    public class CityFilterModel
    {
        public int? DistrictId { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public bool? IsSortByDesc { get; set; }

    }
    public class SubDivisionsFilterModel
    {
        public int? DivisionId { get; set; }
        public int? DistrictId { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public bool? IsSortByDesc { get; set; }

    }

}
