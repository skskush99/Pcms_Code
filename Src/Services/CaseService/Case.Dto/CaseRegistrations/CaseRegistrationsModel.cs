namespace Case.Dto.CaseRegistrations
{
    public class CaseRegistrationsModel
    {
        public long CaseId { get; set; }
        public string? CRNNumber { get; set; }
        public int AdmDepttId { get; set; }
        public int UnitId { get; set; }
        public int OfficeId { get; set; }
        public int PlaceId { get; set; }
        public int CourtId { get; set; }
        public int AbbreviationId { get; set; }
        public int CaseYear { get; set; }
        public int CaseNo { get; set; }
        public string? FileNo { get; set; }
        public int SubjectSubCategoryId { get; set; }
        public int SubjectSubMatterId { get; set; }
        public required string AppellantOrResponded { get; set; }
        public string? R_E_Implication { get; set; }
        public bool Does_P_O_A { get; set; }
        public bool Does_P_A_PD { get; set; }
        public string? PriorityCode { get; set; }
        public int PriorityId { get; set; }
        public int SubPriorityId { get; set; }
        public string? CaseRegistrationDate { get; set; }
        public string? Bench { get; set; }
        public string? WACPNo { get; set; }
        public required string PrimarySecondary { get; set; }
        public int GroupingId { get; set; }
        public string? DateCaseFillingDeptToAG_AAG { get; set; }
        public string? DateFillingCaseCourtByAG_AAG { get; set; }
        public bool ApplicationUnderSec5FiledYN { get; set; }
        public string? Remark { get; set; }
        public int LinkCaseId { get; set; }
        public bool IsEmployee { get; set; }
        public string? EmployeeCode { get; set; }
        public bool ImportantCase { get; set; }
        public string? EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public string? EmployeeDesignation { get; set; }
        public string? EmployeeSSOID { get; set; }
    }

    public class CaseRegistrationsResponseModel
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public long ReturnID { get; set; }
        public object? Data { get; set; }
    }

    public class CaseListFilterModel
    {
        public int AdmDepttId { get; set; } = 0;
        public int UnitId { get; set; } = 0;
        public int OfficeId { get; set; }
        public int CourtTypeId { get; set; }
        public int AbbreviationId { get; set; }
        public int CaseYear { get; set; }
        public int GroupingId { get; set; }
        public int CaseStatus { get; set; }
        public string? PrimarySecondary { get; set; }
        public string? CRNNumber { get; set; }
        public int? CaseNo { get; set; }
        public int RoleId { get; set; } = 1;
        public int OICId { get; set; } = 0;
        public int LawyerId { get; set; } = 0;
        public int DistrictId { get; set; } = 0;
        public string? SortBy { get; set; }
        public bool? IsSortByDesc { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int CaseType { get; set; } = 1;
    }

    public class AddCaseGroupModel
    {
        public long CaseId { get; set; }
        public long GroupingId { get; set; }
    }

    public class AddCaseLinkingModel
    {
        public long CaseId { get; set; }
        public long LinkCaseId { get; set; }
    }

    public class AddCaseRemandModel
    {
        public long CaseId { get; set; }
        public long RemandId { get; set; }
    }

    public class CaseAppellantsModel
    {
        public long CaseAppellantId { get; set; }
        public long CaseId { get; set; }
        public string? Name { get; set; }
        public string? Designation { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? ContactNo { get; set; }
        public string? MobileNo { get; set; }
        public string? EmailId { get; set; }
        public int Appellant_SrNo { get; set; }
    }

    public class CaseRespondentsModel
    {
        public long RespondentId { get; set; }
        public long CaseId { get; set; }
        public string? Name { get; set; }
        public string? Designation { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? ContactNo { get; set; }
        public string? MobileNo { get; set; }
        public string? EmailId { get; set; }
        public int Respondant_SrNo { get; set; }
    }

    public class CaseDocumentsModel
    {
        public long CaseId { get; set; }
        public int DocType { get; set; }
        public string? DocumentName { get; set; }
    }

    public class CaseAddDocumentModel
    {
        public long CaseId { get; set; }
        public int DocType { get; set; }
        public string? DocumentName { get; set; }
        public string? DocumentFile { get; set; }
    }

    public class CaseWithoutCaseNoModel
    {
        public long CaseId { get; set; }
        public int AdmDepttId { get; set; }
        public int UnitId { get; set; }
        public int OfficeId { get; set; }
        public int PlaceId { get; set; }
        public int CourtId { get; set; }
        public int AbbreviationId { get; set; }
        public int CaseYear { get; set; }
        public int PreCaseNo { get; set; }
        public string? FileNo { get; set; }
        public int SubjectSubCategoryId { get; set; }
        public int SubjectSubMatterId { get; set; }
        public required string AppellantOrResponded { get; set; }
        public string? R_E_Implication { get; set; }
        public bool Does_P_O_A { get; set; }
        public bool Does_P_A_PD { get; set; }
        public string? PriorityCode { get; set; }
        public int PriorityId { get; set; }
        public int SubPriorityId { get; set; }
        public string? CaseRegistrationDate { get; set; }
        public string? Bench { get; set; }
        public string? WACPNo { get; set; }
        public required string PrimarySecondary { get; set; }
        public int GroupingId { get; set; }
        public string? DateCaseFillingDeptToAG_AAG { get; set; }
        public string? DateFillingCaseCourtByAG_AAG { get; set; }
        public bool ApplicationUnderSec5FiledYN { get; set; }
        public string? Remark { get; set; }
        public int LinkCaseId { get; set; } = 0;
    }

    public class CheckCaseEntryModel
    {
        public string? CRNNumber { get; set; }
        public required string PrimarySecondary { get; set; }
        public int CaseNo { get; set; }
        public int CaseYear { get; set; }
        public int AbbreviationId { get; set; }
        public int CourtId { get; set; }
    }

    // Add sandeep 25/07/2025
    public class CaseRegistrationGovtEmpModel
    {
        public long? CRGEId { get; set; }
        public long? CaseId { get; set; }
        public string? CRNNumber { get; set; }
        public string? EmployeeSSOID { get; set; }
        public string? EmployeeName { get; set; }
        public string? EmployeeDesignation { get; set; }
        public string? EmployeeId { get; set; }

    }

    public class CaseRegistrationGovtEmpListFilterModel
    {
        public long? CaseId { get; set; }
        public string? CRNNumber { get; set; }
        public string? SortBy { get; set; }
        public bool? IsSortByDesc { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
    }
    // Add sandeep 25/07/2025
}