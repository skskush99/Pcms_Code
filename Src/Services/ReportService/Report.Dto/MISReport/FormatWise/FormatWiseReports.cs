using Report.Dto.Global;

namespace Report.Dto.MISReport.FormatWise
{
    public class FormatWiseReportsModel
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public IEnumerable<object>? Data { get; set; }
        public IEnumerable<PaginationModel>? Pagination { get; set; }

    }

    public class Format_AReportModel
    {
        public int CaseId { get; set; }
        public string CNRNumber { get; set; }
        public int? DistrictId { get; set; }
        public int? DepartmentId { get; set; }        
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class Format_BReportModel
    {
        public int CaseId { get; set; }
        public string CNRNumber { get; set; }
        public int? DistrictId { get; set; }
        public int? DepartmentId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }



}
