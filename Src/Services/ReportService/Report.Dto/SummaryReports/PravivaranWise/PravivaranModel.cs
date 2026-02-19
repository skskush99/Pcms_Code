using Report.Dto.Global;

namespace Report.Dto.SummaryReports.PravivaranWise

{
    public class PravivaranResponseModel
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public IEnumerable<object>? Data { get; set; }
        public IEnumerable<PaginationModel>? Pagination { get; set; }
    }

    public class Pravivaran_2Model
    {
        public int? DistrictId { get; set; }
        public int? DepartmentId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

    }







}
