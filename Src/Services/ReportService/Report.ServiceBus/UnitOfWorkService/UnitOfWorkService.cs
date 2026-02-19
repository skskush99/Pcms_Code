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
    public class UnitOfWorkService(IDashboardServiceBus Dashboard, IDistrictWiseServiceBus DistrictWiseService, IMahilaAtayacharServiceBus MahilaAtayacharService
        , IFormatWiseServiceBus FormatWiseService, ILoginReportServiceBus LoginReport, INextHearingUpdateServiceBus NextHearingUpdate, ICaseFileRegServiceBus CaseFileRegService, IActionTakenServiceBus ActionTaken
        , ICNRReportServiceBus CNRReport, IDistrictWiseMonitoringServiceBus DistrictWiseMonitoring, IMonthlyEntryServiceBus MonthlyEntry, IPravivaranWiseServiceBus PravivaranWiseService
        , IUserRegistrationServiceBus UserRegistration) : IUnitOfWorkService

    {
        #region Dashboard
        public IDashboardServiceBus Dashboard { get; set; } = Dashboard;
        #endregion


        #region Details Reports
        public IDistrictWiseServiceBus DistrictWiseService { get; set; } = DistrictWiseService;
        public IMahilaAtayacharServiceBus MahilaAtayacharService { get; set; } = MahilaAtayacharService;
        #endregion

        #region MIS Report
        public IFormatWiseServiceBus FormatWiseService { get; set; } = FormatWiseService;
        public ILoginReportServiceBus LoginReport { get; set; } = LoginReport; 
        public INextHearingUpdateServiceBus NextHearingUpdate { get; set; } = NextHearingUpdate;
        #endregion

        #region Report
        public ICaseFileRegServiceBus CaseFileRegService { get; set; } = CaseFileRegService;
        #endregion

        #region Summary Report
        public IActionTakenServiceBus ActionTaken { get; set; } = ActionTaken; 
        public ICNRReportServiceBus CNRReport { get; set; } = CNRReport;
        public IDistrictWiseMonitoringServiceBus DistrictWiseMonitoring { get; set; } = DistrictWiseMonitoring;        
        public IMonthlyEntryServiceBus MonthlyEntry { get; set; } = MonthlyEntry;
        public IPravivaranWiseServiceBus PravivaranWiseService { get; set; }= PravivaranWiseService;
        public IUserRegistrationServiceBus UserRegistration { get; set; } = UserRegistration;

        #endregion





    }
}
