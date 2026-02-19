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


namespace Report.Repository.UnitOfwork
{
    public interface IUnitOfWorkRepository
    {
        #region
        IDashboardRepository Dashboard { get; set; }
        #endregion

        #region Details Reports
        IDistrictWiseReport DistrictWiseReport { get; set; }
        IMahilaAtayacharReport MahilaAtayacharReport { get; set; }
        #endregion

        #region MIS Report
        IFormatWiseReport FormatWiseReport { get; set; }
        ILoginReportRepository LoginReport { get; set; }
        INextHearingUpdateRepository NextHearingUpdate { get; set; }
        #endregion

        #region Reports
        ICaseFileRegReports CaseFileRegReports { get; set; }
        #endregion

        #region Summary Report
        IActionTakenRepository ActionTaken { get; set; }
        ICNRReportRepository CNRReport { get; set; }
        IDistrictWiseMonitoringRepository DistrictWiseMonitoring { get; set; }
        IMonthlyEntryRepository MonthlyEntry { get; set; }
        IPravivaranWiseReport PravivaranWiseReport { get; set; }
        IUserRegistrationRepository UserRegistration { get; set; }        
        #endregion







        
    }
}
