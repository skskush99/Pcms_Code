using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Report.Repository.UnitOfwork;
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

namespace Report.Repository
{
    public static class ServiceRegistration
    {
        public static void AddRepositoryInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region Dashboard
            services.AddTransient<IDashboardRepository, DashboardRepository>();
            #endregion

            #region Details Reports
            services.AddTransient<IDistrictWiseReport, DistrictWiseReportRepository>();
            services.AddTransient<IMahilaAtayacharReport, MahilaAtayacharReportRepository>();

            #endregion

            #region MIS Report
            services.AddTransient<IUnitOfWorkRepository, UnitOfWorkRepository>();
            services.AddTransient<IFormatWiseReport, FormatWiseReportRepository>();
            services.AddTransient<ILoginReportRepository, LoginReportRepository>(); 
            services.AddTransient<INextHearingUpdateRepository, NextHearingUpdateRepository>();
            #endregion

            #region Reports            
            services.AddTransient<ICaseFileRegReports, CaseFileRegReportsRepository>();
            #endregion

            #region Summary Report
            services.AddTransient<IActionTakenRepository, ActionTakenRepository>(); 
            services.AddTransient<ICNRReportRepository, CNRReportRepository>();
            services.AddTransient<IDistrictWiseMonitoringRepository, DistrictWiseMonitoringRepository>();            
            services.AddTransient<IMonthlyEntryRepository, MonthlyEntryRepository>();
            services.AddTransient<IPravivaranWiseReport, PravivaranWiseReportRepository>();
            services.AddTransient<IUserRegistrationRepository, UserRegistrationRepository>();
            #endregion







        }
}
}
