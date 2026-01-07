namespace NextHearing.Dto.NextHearingModel
{
    public class HearingModel
    {
    }

    public class NextHearingData
    {
        public string CRNNumber { get; set; }
        public Int64 CaseId { get; set; }
        public int Hearing_SNo { get; set; }
    }
    public class NextHearingResponseData
    {
        public string CRNNumber { get; set; }
        public Int64 CaseId { get; set; }
    }

    public class NextHearingResponseData1
    {
        public string CRNNumber { get; set; }
        public Int64 CaseId { get; set; }
        public int Hearing_SNo { get; set; }
    }
}
