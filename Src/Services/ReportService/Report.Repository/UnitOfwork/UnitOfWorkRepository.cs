using Report.Repository.Dashboard;
using Report.Repository.DetailReports.DistrictWise;
using Report.Repository.DetailReports.MahilaAtayachar;
using Report.Repository.MISReport;
using Report.Repository.MISReport.FormatWise;
using Report.Repository.MISReport.Login;
using Report.Repository.MISReport.NextHearing;
using Report.Repository.Reports.CaseFileReg;
using Report.Repository.SummaryReports;
using Report.Repository.SummaryReports.Action;
using Report.Repository.SummaryReports.CNR;
using Report.Repository.SummaryReports.DistrictLevel;
using Report.Repository.SummaryReports.MonthlyEntry;
using Report.Repository.SummaryReports.Pravivaran;
using Report.Repository.SummaryReports.User;


namespace Report.Repository.UnitOfwork;
public class UnitOfWorkRepository(IDashboardRepository Dashboard, IDistrictWiseMonitoringRepository DistrictWiseMonitoring, IMahilaAtayacharReport MahilaAtayacharReport
    , IFormatWiseReport FormatWiseReport, ILoginReportRepository LoginReport, INextHearingUpdateRepository NextHearingUpdate, ICaseFileRegReports CaseFileRegReports
    , IActionTakenRepository ActionTaken, ICNRReportRepository CNRReport, IDistrictWiseReport DistrictWiseReport, IMonthlyEntryRepository MonthlyEntry
    , IPravivaranWiseReport PravivaranWiseReport, IUserRegistrationRepository UserRegistration) : IUnitOfWorkRepository

{
    #region Dashboard
    public IDashboardRepository Dashboard { get; set; } = Dashboard;
    #endregion


    #region Details Reports
    public IDistrictWiseReport DistrictWiseReport { get; set; } = DistrictWiseReport;
    public IMahilaAtayacharReport MahilaAtayacharReport { get; set; } = MahilaAtayacharReport;
    #endregion


    #region MIS Report
    public IFormatWiseReport FormatWiseReport { get; set; }=FormatWiseReport;
    public ILoginReportRepository LoginReport { get; set; } = LoginReport;
    public INextHearingUpdateRepository NextHearingUpdate { get; set; } = NextHearingUpdate;
    #endregion

    #region Reports
    public ICaseFileRegReports CaseFileRegReports { get; set; } = CaseFileRegReports;
    #endregion

    #region Summary Report
    public IActionTakenRepository ActionTaken { get; set; } = ActionTaken;
    public ICNRReportRepository CNRReport { get; set; } = CNRReport;
    public IDistrictWiseMonitoringRepository DistrictWiseMonitoring { get; set; } = DistrictWiseMonitoring;
    public IMonthlyEntryRepository MonthlyEntry { get; set; } = MonthlyEntry;
    public IPravivaranWiseReport PravivaranWiseReport { get; set; } = PravivaranWiseReport;
    public IUserRegistrationRepository UserRegistration { get; set; } = UserRegistration;
    #endregion








}
