using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Case.Dto.DierRegistrations_New
{
    public class DierRegistrations_NewResponseModel
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public long ReturnID { get; set; }
        public string? DierNoGenrated { get; set; }
        public int RegisterType { get; set; }
        public object? Data { get; set; }
    }
    public class Dier_NewListFilterModel
    {
        public string? CNRNo { get; set; }
        public string? FIRNo { get; set; }
        public int DistrictId { get; set; } = 0;
        public int OfficeId { get; set; } = 0;
        public int JCourtId { get; set; } = 0;
        public int RegisterType { get; set; }
        public string? SortBy { get; set; }
        public bool? IsSortByDesc { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
    }
    public class DierRegistrations_NewSteps1Model
    {
        public long? DirRegId { get; set; }
        public int Steps { get; set; }
        public int? DistrictId { get; set; }
        public int? OfficeId { get; set; }
        public int? JCourtId { get; set; }
        public int? RegisterType { get; set; }
        public int? SearchCaseVia { get; set; }
        public string? DierNo { get; set; }
        public long? PoliceStationId { get; set; }
        public string? CNRNo { get; set; }
        public string? FIRNo { get; set; }
        public long? FIRYear { get; set; }
    }

    public class DierRegistrations_NewSteps2Model
    {
        public long? DirRegId { get; set; }
        public int Steps { get; set; }
        public string? FIRNo { get; set; }
        public string? FIRDt { get; set; }
        public string? PSName { get; set; }
        public string? PSCode { get; set; }
        public long? InvestGroupNo { get; set; }
        public string? ChargeSheetNo { get; set; }
        public string? ChargeSheetDate { get; set; }
        public string? DateBeforeFillingCourt { get; set; }
        public string? InvestigatingNameRank { get; set; }
        public string? TitleOfCase { get; set; }
        public int? CClassificationId { get; set; }
        public int? CrimeActId { get; set; }
        public int? CrimeActSubId { get; set; }
        public string? FRNo { get; set; }
        public string? FRDate { get; set; }
        public string? CourtSubmissionDate { get; set; }
        public long? FRStatusID { get; set; }
        public string? FRStatusName { get; set; }
        public List<Dier_NewInvestigationModel>? DierInvestigationDetails { get; set; }
        public List<OffenceClassification_NewModel>? OffenceClassificationDetails { get; set; }
    }

    public class DierRegistrations_NewSteps3Model
    {
        public long? DirRegId { get; set; }
        public int Steps { get; set; }
        public int? IsAccusedType { get; set; }
        public long? AccusedGroupNo { get; set; }
        public long? VictimWitnessGroupNo { get; set; }
        public List<Dier_NewAccusedModel>? DierAccusedModel { get; set; }
        public List<Dier_NewVictimWitnessModel>? DierVictimWitnessDetails { get; set; }
    }

    public class DierRegistrations_NewSteps4Model
    {
        public long? DirRegId { get; set; }
        public int Steps { get; set; }
        public string? Remarks { get; set; }
        public string? ChargeSheetDocs { get; set; }
        public string? FullChargeSheetDocs { get; set; }
        public string? OtherDocs { get; set; }
        public string? CaseStatus { get; set; }

    }

    public class DierRegistrations_NewModel
    {
        public long? DirRegId { get; set; }
        public int Steps { get; set; }
        public int? DistrictId { get; set; }
        public int? OfficeId { get; set; }
        public int? JCourtId { get; set; }
        public int? RegisterType { get; set; }
        public int? SearchCaseVia { get; set; }
        public string? DierNo { get; set; }
        public long? PoliceStationId { get; set; }
        public string? CNRNo { get; set; }
        public string? FIRNo { get; set; }
        public long? FIRYear { get; set; }
        public string? FIRDt { get; set; }
        public int FirStatusId { get; set; }
        public string? PSName { get; set; }
        public string? PSCode { get; set; }
        public long? InvestGroupNo { get; set; }
        public string? ChargeSheetNo { get; set; }
        public string? ChargeSheetDate { get; set; }
        public string? DateBeforeFillingCourt { get; set; }
        public string? InvestigatingNameRank { get; set; }
        public string? TitleOfCase { get; set; }
        public int? CClassificationId { get; set; }
        public int? CrimeActId { get; set; }
        public int? CrimeActSubId { get; set; }
        public int? IsAccusedType { get; set; }
        public long? AccusedGroupNo { get; set; }
        public long? VictimWitnessGroupNo { get; set; }
        public string? Remarks { get; set; }
        public string? ChargeSheetDocs { get; set; }
        public string? FullChargeSheetDocs { get; set; }
        public string? OtherDocs { get; set; }
        public string? CaseStatus { get; set; }

        public string? FRNo { get; set; }
        public string? FRDate { get; set; }
        public string? CourtSubmissionDate { get; set; }
        public long? FRStatusID { get; set; }
        public string? FRStatusName { get; set; }

        public List<Dier_NewInvestigationModel>? DierInvestigationDetails { get; set; }
        public List<OffenceClassification_NewModel>? OffenceClassificationDetails { get; set; }
        public List<Dier_NewAccusedModel>? DierAccusedModel { get; set; }
        public List<Dier_NewVictimWitnessModel>? DierVictimWitnessDetails { get; set; }
    }

    public class DierRegistrations_New_OldModel
    {
        public long? DirRegId { get; set; }
        public string? TitleOfCase { get; set; }
        public string? DierNo { get; set; }
        public string? CNRNo { get; set; }
        public string? FIRNo { get; set; }
        public long? FIRYear { get; set; }
        public int? PoliceStationId { get; set; }
        public int? CClassificationId { get; set; }
        public int? CrimeActId { get; set; }
        public int? CrimeActSubId { get; set; }
        public int FirStatusId { get; set; }
        public long? AccusedGroupNo { get; set; }
        public long? VictimGroupNo { get; set; }
        public long? WitnessGroupNo { get; set; }
        public long? InvestigationDtId { get; set; }
        public string? ChargeSheetNo { get; set; }
        public string? ChargeSheetDate { get; set; }
        public string? DateBeforeFillingCourt { get; set; }
        public int? DistrictId { get; set; }
        public int? OfficeId { get; set; }
        public int? JCourtId { get; set; }
        public bool? IsGovtAccused { get; set; }
        public long? GovtGroupId { get; set; }
        public bool? IsConstitutionPost { get; set; }
        public long? ConsGroupId { get; set; }
        public string? Remarks { get; set; }
        public string? ChargeSheetDocs { get; set; }
        public string? FullChargeSheetDocs { get; set; }
        public string? OtherDocs { get; set; }
        public string? CaseStatus { get; set; }
        public int Steps { get; set; }
    }

    public class Dier_NewAccusedModel
    {
        public long? AccusedId { get; set; }
        public int IsAccusedType { get; set; }
        public long AccusedGroupNo { get; set; }
        public string? AccuseName { get; set; }
        public string? FatherName { get; set; }
        public string? Address { get; set; }
        public int? Age { get; set; }
        public int? Gender { get; set; }
        public int? FIRStatusId { get; set; }
        public string? Remark { get; set; }
        public int? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public int? DesignationId { get; set; }
        public string? DesignationName { get; set; }
        public string? EmpID { get; set; }
        public int? JanPratinidhiPostID { get; set; }
        public string? JanPratinidhiPostName { get; set; }
        public string? ConstitutionDT { get; set; }
        public bool? IsSanction { get; set; }
        public string? SanctionDocs { get; set; }
        public string? MobileNo { get; set; }
        public string? UIDNo { get; set; }
        public long? DistrictId { get; set; }
        public long? PsId { get; set; }
        // ✅ File comes here
        [NotMapped]
        public IFormFile? SanctionFile { get; set; }
    }

    public class Dier_NewVictimWitnessModel
    {
        public long Id { get; set; }
        public int IsVictimWitness { get; set; }
        public long GroupNo { get; set; }
        public string? Name { get; set; }
        public string? FatherName { get; set; }
        public int? Gender { get; set; }
        public string? Address { get; set; }
        public string? MobileNo { get; set; }
        public string? UIDNo { get; set; }
        public long? DistrictId { get; set; }
        public long? ThanaId { get; set; }
        public int? Status { get; set; }
    }
    public class Dier_NewInvestigationModel
    {
        public long InvestId { get; set; }
        public long InvestGroupNo { get; set; }
        public string? InvestName { get; set; }
        public string? FatherName { get; set; }
        public string? RankName { get; set; }
        public string? PostingPlace { get; set; }
        public int? Gender { get; set; }
        public string? MobileNo { get; set; }
        public int? DistrictId { get; set; }
        public int? ThanaId { get; set; }
        public int? InvestStatus { get; set; }
    }

    public class Dier_NewComplaintAgainstPersonModel
    {
        public long ComplaintPerId { get; set; }
        public long ComplaintPerGroupNo { get; set; }
        public string? ComplaintPerName { get; set; }
        public string? Address { get; set; }
        public string? MobileNo { get; set; }
        public string? UIDNo { get; set; }
        public string? EmpID { get; set; }
        public string? Designation { get; set; }
        public string? Institution { get; set; }
    }

    public class OffenceClassification_NewModel
    {
        public long? OffenceClassifId { get; set; }
        public long? OffenceClassifGroupNo { get; set; }
        public int IsCaseComplaintReg { get; set; }
        public long ClassificationID { get; set; }
        public string ClassificationName { get; set; }
        public long ActsID { get; set; }
        public string ActsName { get; set; }
        public long SectionsID { get; set; }
        public string SectionsName { get; set; }
    }
}
