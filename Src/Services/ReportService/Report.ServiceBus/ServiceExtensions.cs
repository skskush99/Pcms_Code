using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Report.ServiceBus.UnitOfWork;
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

namespace Report.ServiceBus
{
    public static class ServiceExtensions
    {
        public static void AddServiceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region Dashboard
            services.AddTransient<IDashboardServiceBus, DashboardServiceBus>();
            #endregion

            #region Details Reports
            services.AddTransient<IDistrictWiseServiceBus, DistrictWiseServiceBus>();
            services.AddTransient<IMahilaAtayacharServiceBus, MahilaAtayacharServiceBus>();
            #endregion

            #region MIS Report
            services.AddTransient<IUnitOfWorkService, UnitOfWorkService>();
            services.AddTransient<IFormatWiseServiceBus, FormatWiseServiceBus>();
            services.AddTransient<ILoginReportServiceBus, LoginReportServiceBus>(); 
            services.AddTransient<INextHearingUpdateServiceBus, NextHearingUpdateServiceBus>();            
            #endregion

            #region Report
            services.AddTransient<ICaseFileRegServiceBus, CaseFileRegServiceBus>();
            #endregion

            #region Summary Report
            services.AddTransient<IActionTakenServiceBus, ActionTakenServiceBus>(); 
            services.AddTransient<ICNRReportServiceBus, CNRReportServiceBus>();
            services.AddTransient<IDistrictWiseMonitoringServiceBus, DistrictWiseMonitoringServiceBus>();            
            services.AddTransient<IMonthlyEntryServiceBus, MonthlyEntryServiceBus>();
            services.AddTransient<IPravivaranWiseServiceBus, PravivaranWiseServiceBus>();
            services.AddTransient<IUserRegistrationServiceBus, UserRegistrationServiceBus>();
            #endregion


        }
    }
}
