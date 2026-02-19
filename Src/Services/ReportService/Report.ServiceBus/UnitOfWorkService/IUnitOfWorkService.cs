using Report.ServiceBus.Dashboard;
using Report.ServiceBus.DetailReportsService.DistrictWiseService;
using Report.ServiceBus.DetailReportsService.MahilaAtayacharService;
using Report.ServiceBus.MISReportService;
using Report.ServiceBus.MISReportService.FormatWiseService;
using Report.ServiceBus.MISReportService.Login;
using Report.ServiceBus.MISReportService.NextHearing;
using Report.ServiceBus.ReportsService.CaseFileRegService;
using Report.ServiceBus.SummaryReport;
using Report.ServiceBus.SummaryReport.Action;
using Report.ServiceBus.SummaryReport.CNR;
using Report.ServiceBus.SummaryReport.DistrictLevel;
using Report.ServiceBus.SummaryReport.MonthlyEntry;
using Report.ServiceBus.SummaryReport.Pravivaran;
using Report.ServiceBus.SummaryReport.User;


namespace Report.ServiceBus.UnitOfWork
{
    public interface IUnitOfWorkService
    {
        #region Dashboard
        IDashboardServiceBus Dashboard { get; }
        #endregion

        #region Details Reports
        IDistrictWiseServiceBus DistrictWiseService { get; }
        IMahilaAtayacharServiceBus MahilaAtayacharService { get; }
        #endregion

        #region MIS Report
        IFormatWiseServiceBus FormatWiseService { get; }
        ILoginReportServiceBus LoginReport { get; }
        INextHearingUpdateServiceBus NextHearingUpdate { get; }
        #endregion

        #region Report
        ICaseFileRegServiceBus CaseFileRegService { get; }
        #endregion

        #region Summary Report
        IActionTakenServiceBus ActionTaken { get; }
        ICNRReportServiceBus CNRReport { get; }
        IDistrictWiseMonitoringServiceBus DistrictWiseMonitoring { get; }
        IMonthlyEntryServiceBus MonthlyEntry { get; }
        IPravivaranWiseServiceBus PravivaranWiseService { get; }
        IUserRegistrationServiceBus UserRegistration { get; }        
        #endregion



        
    }
}
