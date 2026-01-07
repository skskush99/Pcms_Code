namespace Master.Dto.Masters
{
    public class ReqInformationFilterModel
    {
        //public int? DeptDistrictType { get; set; }
        public int? DistDeptType { get; set; }
        public int? DistDept { get; set; }
        public int? DistrictId { get; set; }
        public int? IsinfoReceived { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public bool? IsSortByDesc { get; set; }
    }
    public class ReqInformationModel
    {
        public int? RowID { get; set; }
        public int InfoID { get; set; }
        public int? DeptDistrictType { get; set; }
        public int? DistDeptType { get; set; }
        public int? DistrictId { get; set; }
        public string? AdmDeptName { get; set; }
        public string? ReqInforamtion { get; set; }
        public string? DesReqInforamtion { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? SubmitLastDate { get; set; }
        public string? StartDate_string { get; set; }
        public string? EndDate_string { get; set; }
        public string? SubmitLastDate_string { get; set; }
        public string? IsInfoReceived_string { get; set; }
        public int IsInfoReceived { get; set; }
        public bool Active { get; set; }
        public string? ShortReqInforamtion { get; set; }
        public int SubjectID { get; set; }
        public long DistDept { get; set; }
        public string? DPDT { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public long DeleteBy { get; set; }
        public DateTime DeleteOn { get; set; }
    }
    public class ReqInformationRequestModel
    {
        //public required string Tocken { get; set; }
        public required ReqInformationModel Data { get; set; }
    }
    public class ReqInformationActiveDeactiveModel
    {
        //public required string Tocken { get; set; }
        public int InfoID { get; set; }
        public bool Active { get; set; }
        public long UpdatedBy { get; set; }
    }
    public class ReqInformationUpdateModel
    {
        public int InfoID { get; set; }
        public int IsInfoReceived { get; set; }
        public long UpdatedBy { get; set; }
    }
    public class GetReqInformationPopUpFilterModel
    {
        public int DistDept { get; set; }
        public string DPDT { get; set; }
    }
}
