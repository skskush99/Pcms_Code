using Common.Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Master.Repository.Menu;
using Master.Repository.Roles;
using Master.Repository.UnitOfwork;
using Master.Repository.Users;
using Master.Repository.AdminDepartment;
using Master.Repository.CaseDecisionReason;
using Master.Repository.CaseDecisionType;
using Master.Repository.CircularOrder;
using Master.Repository.CourtName;
using Master.Repository.CourtPlaces;
using Master.Repository.CourtTypes;
using Master.Repository.CrimeAct;
using Master.Repository.CrimeClassification;
using Master.Repository.CrimeSubAct;
using Master.Repository.Designation;
using Master.Repository.FirStatus;
using Master.Repository.JanPratinidhi;
using Master.Repository.Level;
using Master.Repository.News;
using Master.Repository.NodalOfficer;
using Master.Repository.Offices;
using Master.Repository.PoliceThana;
using PcmsMasterMicroServices.Repository;
using Master.Repository.RajMaster;
using Master.Repository.ReqInformation;
using Master.Repository.State;
using Master.Repository.UnitsDepartment;
using Master.Repository.Documents;
using Master.Repository.UploadFiles;
using Master.Repository.WebSite;


namespace Master.Repository;

public static class ServiceRegistration
{
    public static void AddRepositoryInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        #region Repository
        services.AddTransient<IMenu, MenuRepository>();
        services.AddTransient<IRoles, RolesRepository>();
        services.AddTransient<IUnitOfWorkRepository, UnitOfWorkRepository>();
        services.AddTransient<IUserLogin, UserLoginRepository>();
        services.AddTransient<IGenericRepository, SqlRepository>();
        services.AddTransient<IAdminDepartment, AdminDepartmentRepository>();
        services.AddTransient<ICaseDecisionReason, CaseDecisionReasonRepository>();
        services.AddTransient<ICaseDecisionType, CaseDecisionTypeRepository>();
        services.AddTransient<ICircularOrder, CircularOrderRepository>();
        services.AddTransient<ICourtNames, CourtNamesRepository>();
        services.AddTransient<ICourtPlaces, CourtPlacesRepository>();
        services.AddTransient<ICourtTypes, CourtTypesRepository>();
        services.AddTransient<ICrimeAct, CrimeActRepository>();
        services.AddTransient<ICrimeClassification, CrimeClassificationRepository>();
        services.AddTransient<ICrimeSubAct, CrimeSubActRepository>();
        services.AddTransient<IDesignation, DesignationRepository>();
        services.AddTransient<IFirStatus, FirStatusRepository>();
        services.AddTransient<IJanPratinidhi, JanPratinidhiRepository>();
        services.AddTransient<ILevel, LevelRepository>();
        services.AddTransient<INews, NewsRepository>();
        services.AddTransient<INodalOfficer, NodalOfficerRepository>();
        services.AddTransient<IOffices, OfficesRepository>();
        services.AddTransient<IPoliceThana, PoliceThanaRepository>();
        services.AddTransient<IRajMaster, RajMasterRepository>();
        services.AddTransient<IReqInformation, ReqInformationRepository>();
        services.AddTransient<IState, StateRepository>();
        services.AddTransient<IUnitsDepartment, UnitsDepartmentRepository>();
        services.AddTransient<IUploadFilesRepository, UploadFilesRepository>();
        services.AddTransient<IWebSiteRepository, WebSiteRepository>();

        #endregion Repository
    }
}
