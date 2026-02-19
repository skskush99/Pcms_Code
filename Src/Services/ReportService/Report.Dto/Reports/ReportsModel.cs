using Report.Dto.Global;

namespace Report.Dto.Reports
{
    public class ReportsResponseModel
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public IEnumerable<object>? Data { get; set; }
        public IEnumerable<PaginationModel>? Pagination { get; set; }
    }
   public class CaseFileRegModel
    {
        public int? AdmDepttId { get; set; }
        public int? CellId { get; set; }
        public int? HeadId { get; set; }
        public int? CourtId { get; set; }
        public string? CaseNo { get; set; }
        public int? CaseRegistorYear { get; set; }
        public int? AbbreviationId { get; set; }
        public string? Banch { get; set; }  
        public int? CsIsParty { get; set; }
        public int? CourtType { get; set; }
        public int? PageSize { get; set; }
        public int? PageNo { get; set; }

    }

    


}
