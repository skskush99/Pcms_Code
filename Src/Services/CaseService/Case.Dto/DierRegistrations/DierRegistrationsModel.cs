namespace Case.Dto.DierRegistrations
{
    public class DierRegistrationsResponseModel
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public long ReturnID { get; set; }
        public object? Data { get; set; }
    }
    public class DierListFilterModel
    {
        public string? CNRNo { get; set; }
        public string? FIRNo { get; set; }
        public int DistrictId { get; set; } = 0;
        public int OfficeId { get; set; } = 0;
        public int JCourtId { get; set; } = 0;
        public string? SortBy { get; set; }
        public bool? IsSortByDesc { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
    }
    public class DierRegistrationsSteps1Model
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

    public class DierRegistrationsSteps2Model
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
    }

    public class DierRegistrationsSteps3Model
    {
        public long? DirRegId { get; set; }
        public int Steps { get; set; }
        public int? IsAccusedType { get; set; }
        public long? AccusedGroupNo { get; set; }
        public long? VictimWitnessGroupNo { get; set; }
    }

    public class DierRegistrationsSteps4Model
    {
        public long? DirRegId { get; set; }
        public int Steps { get; set; }
        public string? Remarks { get; set; }
        public string? ChargeSheetDocs { get; set; }
        public string? FullChargeSheetDocs { get; set; }
        public string? OtherDocs { get; set; }
        public string? CaseStatus { get; set; }

    }

    public class DierRegistrationsModel
    {
        public long? DirRegId { get; set; }
        public int Steps { get; set; }
        public int? DistrictId { get; set; }
        public int? OfficeId { get; set; }
        public int? JCourtId { get; set; }
        public int? RegisterType { get; set; }
        public int? SearchCaseVia { get; set; }
        public string? DierNo { get; set; }
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
    }  

    public class DierRegistrations_OldModel
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

    public class DierAccusedModel
    {
        public long AccusedId { get; set; }
        public long AccusedGroupNo { get; set; }
        public string? AccuseName { get; set; }
        public string? FatherName { get; set; }
        public int? Gender { get; set; }
        public string? Address { get; set; }
        public string? MobileNo { get; set; }
        public string? UIDNo { get; set; }
        public long? DistrictId { get; set; }
        public long? ThanaId { get; set; }
        public int? FIRStatusId { get; set; }
    }

    public class DierVictimWitnessModel
    {
        public long Id { get; set; }
        public int IsVictimWitness { get; set; }
        public long GroupNo { get; set; }
        public string?  Name { get; set; }
        public string? FatherName { get; set; }
        public int? Gender { get; set; }
        public string? Address { get; set; }
        public string? MobileNo { get; set; }
        public string? UIDNo { get; set; }
        public long? DistrictId { get; set; }
        public long? ThanaId { get; set; }
        public int? Status { get; set; }
    }

    //public class DierVictimModel
    //{
    //    public long VictimId { get; set; }
    //    public long VictimGroupNo { get; set; }
    //    public string? VictimName { get; set; }
    //    public string? FatherName { get; set; }
    //    public int? Gender { get; set; }
    //    public string? Address { get; set; }
    //    public string? MobileNo { get; set; }
    //    public string? UIDNo { get; set; }
    //    public long? DistrictId { get; set; }
    //    public long? ThanaId { get; set; }
    //    public int? VictimStatus { get; set; }
    //}

    //public class DierWitnessModel
    //{
    //    public long WitnessId { get; set; }
    //    public long WitnessGroupNo { get; set; }
    //    public string? WitnessName { get; set; }
    //    public string? FatherName { get; set; }
    //    public int? Gender { get; set; }
    //    public string? Address { get; set; }
    //    public string? MobileNo { get; set; }
    //    public string? UIDNo { get; set; }
    //    public long? DistrictId { get; set; }
    //    public long? ThanaId { get; set; }
    //    public int? WitnessStatus { get; set; }
    //}

    public class DierInvestigationModel
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

    public class DierComplaintAgainstPersonModel
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

    public class OffenceClassificationModel
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
