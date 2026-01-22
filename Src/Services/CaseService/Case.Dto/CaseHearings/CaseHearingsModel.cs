namespace Case.Dto.CaseHearings
{
    public class CaseHearingsModel
    {
        public long CaseHearingId { get; set; }
        public long CaseId { get; set; }
        public long LawyerId { get; set; }
        public string? HearingDate { get; set; }
        public long OICId { get; set; }
        public string? Judgment_PR { get; set; }
        public bool ArgumentOver_YN { get; set; }
        public int HC_Admitted_YNA { get; set; }
        public bool HC_StayGranted_YN { get; set; }
        public bool HC_AnyMiscAppfiled_YN { get; set; }
        public string? HC_Sup_InwordNo { get; set; }
        public string? HC_Sup_InwordRegion { get; set; }
        public string? HC_Sup_InwordDate { get; set; }
        public int HC_Replyfiled_YN { get; set; }
        public bool StayOrder_FA { get; set; }
        public string? StayFinishDate { get; set; }
        public bool InterimOrder_YN { get; set; }
        public string? Interim_Order_Date { get; set; }
        public string? Interim_Order_No { get; set; }
        public bool SupplementaryFactul_YN { get; set; }
        public string? SupplementaryInwordNo { get; set; }
        public string? SupplementaryInwordDate { get; set; }
        public bool ApplVactingStay_YN { get; set; }
        public string? ApplVactingInwordNo { get; set; }
        public string? ApplVactingInwordDate { get; set; }
        public string? ReplayFildInwordNo { get; set; }
        public string? ReplayFildInwordDate { get; set; }
        public bool Adjourned_YN { get; set; }
        public string? ReplyFileDate { get; set; }
        public bool AdjournmentByCourt_YN { get; set; }
        public bool AdjournmentByPertitnor_YN { get; set; }
        public bool AdjournmentByResponent_YN { get; set; }
        public string? AdjournmentDate { get; set; }
        public string? AdjournmentRegion { get; set; }
        public string? SpecialAppearance { get; set; }
        public string? FactualReportDate { get; set; }
        public bool Next_HearingYN { get; set; }
        public string? NextHearing_Date { get; set; }
        public bool IsExPartyStay { get; set; }
        public string? ExPartyStayDate { get; set; }
        public string? Remark { get; set; }
        public string? DueCourse { get; set; }
        public bool Decided { get; set; }
        public string? DateCaseFillingDeptToAG_AAG { get; set; }
    }

    public class CaseHearingDetailModel
    {
        public long HearingDetailId { get; set; }
        public long CaseId { get; set; }
        public long CaseHearingId { get; set; }
        public string? ReplyStatus { get; set; }
        public string? OrderDetail { get; set; }
        public bool ComplianceFiled { get; set; }
        public string? ComplianceFiledDate { get; set; }
        public string? ComplianceDetail { get; set; }
    }
}
