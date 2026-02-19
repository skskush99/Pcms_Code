using Report.Dto.Global;

namespace Report.Dto.Dashboard
{
    public class DashboardModel
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public IEnumerable<object>? Data { get; set; }
    }

    public class DashboardFilterModel
    {
        public int AdmDepttId { get; set; }
        public int UnitId { get; set; }
        public int OfficeId { get; set; }
        public int DistrictId { get; set; }
        public int OICId { get; set; }
        public int LawyerId { get; set; } = 0;
        public int Status { get; set; }
        public string? PrimarySecondary { get; set; }
        public int RoleId { get; set; } = 1;
    }


    public class DashboardResponseModel
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public IEnumerable<object>? Data { get; set; }
    }

    public class DashboardDataResponseModel
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public object? CaseData { get; set; }
        public object? CaseEntryStatusData { get; set; }
        public object? CasePriorityWiseData { get; set; }
        public object? CaseCourtWiseData { get; set; }
    }

    public class PendingDetailReportFilterModel
    {
        public int AdmDepttId { get; set; }
        public int UnitId { get; set; }
        public int OfficeId { get; set; }
        public int DistrictId { get; set; }
        public int CourtId { get; set; }
        public int Type { get; set; }
        public string? PrimarySecondary { get; set; }
        public int OICId { get; set; } = 0;
        public int LawyerId { get; set; } = 0;
        public int RoleId { get; set; } = 1;
    }

    public class DashboardPendencyReportFilterModel
    {
        public int AdmDepttId { get; set; }
        public int UnitId { get; set; }
        public int OfficeId { get; set; }
        public int DistrictId { get; set; }
        public int OICId { get; set; }
        public int LawyerId { get; set; } = 0;
        public int Status { get; set; }
        //public string? PrimarySecondary { get; set; }
        public int RoleId { get; set; }
        public int Level { get; set; }
        public int? CourtTypeId { get; set; }
        public int? PlaceId { get; set; }
        public string? Bench { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
    }

    public class CaseEntryStatusDetailsReportFilterModel
    {
        public int AdmDepttId { get; set; }
        public int UnitId { get; set; }
        public int OfficeId { get; set; }
        public int DistrictId { get; set; }
        public int OICId { get; set; }
        public int LawyerId { get; set; } = 0;
        public int Status { get; set; }
        public string? PrimarySecondary { get; set; }
        public int Duration { get; set; }
        public int Type { get; set; }
        public string? SortBy { get; set; }
        public bool? IsSortByDesc { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int RoleId { get; set; } = 1;
    }

    public class CaseCountDetailsReportFilterModel
    {
        public int AdmDepttId { get; set; }
        public int UnitId { get; set; }
        public int OfficeId { get; set; }
        public int DistrictId { get; set; }
        public int OICId { get; set; }
        public int LawyerId { get; set; } = 0;
        public string? PrimarySecondary { get; set; }
        public int Type { get; set; }
        public int RoleId { get; set; } = 1;
        public string? SortBy { get; set; }
        public bool? IsSortByDesc { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
    }

    public class DashboardResponseWithPaginationModel
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public IEnumerable<object>? Data { get; set; }
        public IEnumerable<object>? Pagination { get; set; }
    }

    public class DashboardHearingDetailsFilterModel
    {
        public int AdmDepttId { get; set; }
        public int UnitId { get; set; }
        public int OfficeId { get; set; }
        public int OICId { get; set; }
        public int DistrictId { get; set; }
        public string? PrimarySecondary { get; set; }
        public int RoleId { get; set; }
    }

    public class DashboardPendencyDetailsReportFilterModel
    {
        public int AdmDepttId { get; set; }
        public int UnitId { get; set; }
        public int OfficeId { get; set; }
        public int DistrictId { get; set; }
        public int OICId { get; set; }
        public int LawyerId { get; set; } = 0;
        public int Status { get; set; }
        public int RoleId { get; set; }
        public int Level { get; set; }
        public int Type { get; set; }
        public int? CourtTypeId { get; set; }
        public int? PlaceId { get; set; }
        public string? Bench { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
    }
}
