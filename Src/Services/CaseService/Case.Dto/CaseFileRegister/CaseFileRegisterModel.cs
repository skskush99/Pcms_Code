using System;

namespace Case.Dto.CaseFileRegister
{
    public class CaseFileRegisterCountFilterModel
    {
        public int Cell { get; set; }
        public int HeadId { get; set; }
        public int CourtType { get; set; }
        public int Court { get; set; }
    }
    public class CaseFileRegisterModel
    {
        public int CaseFileRegistorId { get; set; }
        public int Cell { get; set; }
        public int HeadId { get; set; }
        public int Court { get; set; }
        public int CourtType { get; set; }
        public string CaseNo { get; set; }
        public string Title { get; set; }
        public string Respondents { get; set; }
        public int CaseRegistorYear { get; set; }
        public int AbbrevationId { get; set; }
        public string Banch { get; set; }
        public int AdmDepttId { get; set; }
        public string AdmDepttFileNo { get; set; }
        public string AdmDepttPartFileNo { get; set; }
        public string? ConnectedCaseNo { get; set; }
        public string? CnnectedTitle { get; set; }
        public string? ConnectedRespondents { get; set; }
        public int? ConnectedYear { get; set; }
        public int? ConnectedAbbrevationId { get; set; }
        public string? ConnectedBanch { get; set; }
        public string LawDeptFileNo { get; set; }
        public string LawDeptPartFileNo { get; set; }
        public int CsIsParty { get; set; }
        public int ConnectedCaseStatus { get; set; }
        public int? ConnectedCaseFileRegId { get; set; }
        public int LawCCsId { get; set; }
        public int LawOtherSignatureAuthorityId { get; set; }
        public int LawOtherSignatureAuthoritysId { get; set; }
        public string? maintext { get; set; }
        public string? maintextnext { get; set; }
        public string? textname1 { get; set; }
        public string? textname2 { get; set; }
        public string? textname3 { get; set; }
        public long CreatedBy { get; set; }
        public long LastUpdatedBy { get; set; }
        public long DeleteBy { get; set; }
        public bool? Active { get; set; }
        public string? AddressGenLetter { get; set; }
        public int LawCCsIdGenLetter { get; set; }
        public string? Address1GenLetter { get; set; }
        public string? Address2GenLetter { get; set; }
        public string? Address3GenLetter { get; set; }
        public string? MiddleContGenLetter { get; set; }
        public int LawOtherSignatureAuthorityIdGenLetter { get; set; }
        public int LawOtherSignatureAuthoritysIdGenLetter { get; set; }
        public string? ShowCC { get; set; }
        public int HasConnectedCase { get; set; }

    }
    public class CaseFileRegisterFilterModel
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int? AdmDepttId { get; set; }
        public int? Cell { get; set; }
        public int? HeadId { get; set; }
        public int? Court { get; set; }
        public string? CaseNo { get; set; }
        public int? CaseRegistorYear { get; set; }
        public int? AbbrevationId { get; set; }
        public string? Banch { get; set; }
        public int? CsIsParty { get; set; }         
        public int? CourtType { get; set; }         
        public int? HasConnectedCase { get; set; }
    }
    public class ConnectedCaseFilterModel
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int? AdmDepttId { get; set; }
        public int? Cell { get; set; }
        public int? HeadId { get; set; }
        public int? Court { get; set; }
        public string? CaseNo { get; set; }
        public int? CaseRegistorYear { get; set; }
        public int? AbbrevationId { get; set; }
        public string? Banch { get; set; }
        public int? CsIsParty { get; set; }
        public int? CourtType { get; set; }
    }
    public class AddCaseFileRegisterUploadDocumentModel
    {
        public string DocumentName { get; set; }
        public string? DocumentFile { get; set; }
        public string CaseNo { get; set; }
        public int CaseFileRegistorId { get; set; }

    }
    public class DeactiveCaseFileRegisterUploadDocumentModel
    {
        public int Id { get; set; }
        public bool Active { get; set; }

    }

}
