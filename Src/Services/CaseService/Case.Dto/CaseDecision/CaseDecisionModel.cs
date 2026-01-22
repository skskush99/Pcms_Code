namespace Case.Dto.CaseDecision
{
    public class CaseDecisionModel
    {
        public long DecisionId { get; set; }
        public long CaseId { get; set; }
        public string? DecisionDate { get; set; }
        public string? Decision_Comp_Date { get; set; }
        public string? Decision_Detail { get; set; }
        public bool Decision_FA { get; set; }
        public bool Web_copy_order_obtained { get; set; }
        public string? Web_obtained_date { get; set; }
        public string? DocumentName { get; set; }
        public bool Implementation_required { get; set; }
        public int Implementation_required_OrNo { get; set; }
        public string? Implementation_required_date { get; set; }
        public bool AppliedForCertifiedCopy_YN { get; set; }
        public int AppliedForCertifiedCopyInwordNo { get; set; }
        public string? AppliedForCertifiedCopyDate { get; set; }
        public bool CopyReceived_YN { get; set; }
        public bool CopyForwordedOfOic_Hod_YN { get; set; }
        public bool OpinionProvidedToOic_Hod_YN { get; set; }
        public bool PD_DecisionCopyRecYN { get; set; }
        public string? PD_DecisionCopyRecDate { get; set; }
        public bool PD_DecisionSenttoHOOYN { get; set; }
        public string? PD_DecisionSenttoHOODate { get; set; }
        public bool PD_DecisionSenttoGovtYN { get; set; }
        public string? PD_DecisionSenttoGovtDate { get; set; }
        public bool PD_StayGrantedYN { get; set; }
        public string? PD_StayGrantedDate { get; set; }
        public bool PD_LawyerOpenionYN { get; set; }
        public string? PD_DateoffilingAppeal { get; set; }
        public string? Remark { get; set; }
        public bool DateoSendingCertifiedCopyYN { get; set; }
        public string? DateoSendingCertifiedCopy { get; set; }
        public string? PD_AppealFilingDate { get; set; }
        public bool PD_DecisionSenttoHODYN { get; set; }
        public string? PD_DecisionSenttoHODDate { get; set; }
        public bool PD_FinalDecisionofGovtYN { get; set; }
        public string? PD_FinalDecisionofGovtDate { get; set; }
        public bool PD_DecisionCompliedYN { get; set; }
        public string? PD_DecisionCompliedDate { get; set; }
        public bool PD_DepttOpenionYN { get; set; }
        public string? PD_AppealNo { get; set; }
        public bool IsExParty { get; set; }
        public string? ExPartyDate { get; set; }
        public bool DataSendCommYN { get; set; }
        public string? Date_Sending_Comment { get; set; }
        public string? DateoSendingCertifiedCopyFileType { get; set; }
        public string? PLC_Date { get; set; }
        public string? PLC_Document { get; set; }
        public string? CopyOfDecisionReceivedDocs { get; set; }
        public bool OpinionOfOic_YN { get; set; }
        public string? OpinionOfOicDocs { get; set; }
        public string? PD_DecisionNonCompliedReason { get; set; }
    }

    public class CaseDecisionResponseModel
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public long ReturnID { get; set; }
    }

    public class CaseDecisionPamcAddModel
    {
        public long PamcId { get; set; }
        public long CaseId { get; set; }
        public long? DecisionId { get; set; }
        public string PamcDate { get; set; }
        public string? PamcDocs { get; set; }
        public string? CopyOfPamcDecision { get; set; }
        public required string MeetingConducted { get; set; }
        public string? MeetingStatus { get; set; }

    }

}
