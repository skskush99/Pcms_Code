namespace Case.Dto.CasesDecidedOnIstHearing
{
    public class CasesDecidedOnIstHearingModel
    {
        public string? CRNNumber { get; set; }
        public int AdmDepttId { get; set; }
        public int UnitId { get; set; }
        public int OfficeId { get; set; }
        public int PlaceId { get; set; }
        public int CourtId { get; set; }
        public int AbbreviationId { get; set; }
        public int CaseYear { get; set; }
        public int CaseNo { get; set; }
        public int SubjectSubCategoryId { get; set; }
        public int SubjectSubMatterId { get; set; }
        public required string PrimarySecondary { get; set; }
        public int PriorityId { get; set; }
        public int SubPriorityId { get; set; }
        public string? FileNo { get; set; }
        public required string AppellantOrResponded { get; set; }
        public string? R_E_Implication { get; set; }
        public bool Does_P_O_A { get; set; }
        public bool Does_P_A_PD { get; set; }
        public string? CaseRegistrationDate { get; set; }
        public string? Bench { get; set; }
        public string? WACPNo { get; set; }
        public string? Remark { get; set; }
    }

    public class CasesDecidedOnIstHearingFilterModel
    {
        public int AdmDepttId { get; set; }
        public int UnitId { get; set; }
        public int OfficeId { get; set; }
        public int CourtTypeId { get; set; }
        public int AbbreviationId { get; set; }
        public int CaseYear { get; set; }
        public string? CRNNumber { get; set; }
        public int? CaseNo { get; set; }
        public string? SortBy { get; set; }
        public bool? IsSortByDesc { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
    }
}
