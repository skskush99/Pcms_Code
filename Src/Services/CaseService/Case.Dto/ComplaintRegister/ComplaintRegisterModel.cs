namespace Case.Dto.ComplaintRegister
{
    public class ComplaintRegisterResponseModel
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public long ReturnID { get; set; }
        public object? Data { get; set; }
    }
    public class ComplaintListFilterModel
    {        
        public string? SortBy { get; set; }
        public bool? IsSortByDesc { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
    }
    //public class ComplaintRegisterModel_Old
    //{
    //    public long? ComplaintRegId { get; set; }
    //    public string? ComplaintRegNo { get; set; }
    //    public string? ComplaintNo { get; set; }
    //    public DateTime ComplaintDate { get; set; }
    //    public long ComplaintTypeID { get; set; }
    //    public long? DepartmentId { get; set; }
    //    public string? DeptOfficerName { get; set; }
    //    public string? DeptOfficerDesignation { get; set; }
    //    public int CClassificationId { get; set; }
    //    public int CrimeActId { get; set; }
    //    public int CrimeActSubId { get; set; }
    //    public string? OffenceBrief { get; set; }
    //    public int AccusedGroupNo { get; set; }
    //    public DateTime? DateFiledInCourt { get; set; }
    //    public string? ComplaintFirstPageDocs { get; set; }
    //    public string? FullComplaintDocs { get; set; }
    //    public string? OtherDocs { get; set; }
    //    public bool IsCognizance { get; set; }
    //    public DateTime? CognizanceDate { get; set; }
    //    public int? ConvertedDiarRegId { get; set; }
    //    public int CaseStatus { get; set; }
      
    //}
    public class ComplaintRegisterModel
    {
        public long? ComplaintRegId { get; set; }
        public string? ComplaintRegNo { get; set; }
        public string? ComplaintNo { get; set; }
        public DateTime ComplaintDate { get; set; }
        public long ComplaintTypeID { get; set; }
        public long? DepartmentId { get; set; }
        public string? DeptOfficerNameDesignation { get; set; }
        public string? OffenceBrief { get; set; }
        public DateTime? DateFiledInCourt { get; set; }
        public string? ComplaintFirstPageDocs { get; set; }
        public string? FullComplaintDocs { get; set; }
        public string? OtherDocs { get; set; }
        public bool IsDeclaration { get; set; }
        public int? CaseStatus { get; set; }
        public string? PersonAgainstId { get; set; }
        public string? OffenceClassifId { get; set; }
        public string? ClassificationID { get; set; }
        public bool IsCognizance { get; set; }
    }

    public class PersonAgainstDetailsModel
    {
        public long? PersonAgainstId { get; set; }
        public long? ComplaintRegId { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Designation { get; set; }
        public string? Institution { get; set; }
    }





}
