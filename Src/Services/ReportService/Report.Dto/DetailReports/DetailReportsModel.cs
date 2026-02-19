using Report.Dto.Global;

namespace Report.Dto.DetailReports
{
    public class DetailReportsResponseModel
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public IEnumerable<object>? Data { get; set; }
        public IEnumerable<PaginationModel>? Pagination { get; set; }
    }
    
    public class DistrictWiseModel
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? DepartmentId { get; set; }
        public int? DistrictId { get; set; }
        public int? Status { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

    }

    public class MahilaAtayacharIPCModel
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? DepartmentId { get; set; }
        public int? DistrictId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

    }

    public class MahilaAtayacharBNSModel
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? DepartmentId { get; set; }
        public int? DistrictId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

    }








}
