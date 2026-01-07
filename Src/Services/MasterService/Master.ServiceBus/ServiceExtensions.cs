using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Master.ServiceBus.Menu;
using Master.ServiceBus.Roles;
using Master.ServiceBus.UnitOfWork;
using Master.ServiceBus.Users;
using Master.ServiceBus.AdminDepartmentService;
using Master.ServiceBus.CircularOrderService;
using Master.ServiceBus.CourtNamesService;
using Master.ServiceBus.CourtPlacesService;
using Master.ServiceBus.CourtTypesService;
using Master.ServiceBus.DesignationService;
using Master.ServiceBus.LevelService;
using Master.ServiceBus.NewsService;
using Master.ServiceBus.NodalOfficerService;
using Master.ServiceBus.OfficeService;
using Master.ServiceBus.PoliceThanaService;
using Master.ServiceBus.RajMasterService;
using Master.ServiceBus.ReqInformationService;
using Master.ServiceBus.StateService;
using Master.ServiceBus.UnitsDepartmentService;
using Master.ServiceBus.UploadFiles;
using Master.ServiceBus.WebSiteService;


namespace Master.ServiceBus;

public static class ServiceExtensions
{
    public static void AddServiceInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IMenuServiceBus, MenuServiceBus>();
        services.AddTransient<IRolesServiceBus, RolesServiceBus>();
        services.AddTransient<IUnitOfWorkService, UnitOfWorkService>();
        services.AddTransient<IUserLoginServiceBus, UserLoginServiceBus>();
        services.AddTransient<IAdminDepartmentServiceBus, AdminDepartmentServiceBus>();
        services.AddTransient<ICircularOrderServiceBus, CircularOrderServiceBus>();
        services.AddTransient<ICourtNamesServiceBus, CourtNamesServiceBus>();
        services.AddTransient<ICourtPlacesServiceBus, CourtPlacesServiceBus>();
        services.AddTransient<ICourtTypesServiceBus, CourtTypesServiceBus>();
        services.AddTransient<IDesignationServiceBus, DesignationServiceBus>();
        services.AddTransient<ILevelServiceBus, LevelServiceBus>();
        services.AddTransient<INewsServiceBus, NewsServiceBus>();
        services.AddTransient<INodalOfficerServiceBus, NodalOfficerServiceBus>();
        services.AddTransient<IOfficeServiceBus, OfficeServiceBus>();
        services.AddTransient<IPoliceThanaServiceBus, PoliceThanaServiceBus>();
        services.AddTransient<IRajMasterServiceBus, RajMasterServiceBus>();
        services.AddTransient<IReqInformationServiceBus, ReqInformationServiceBus>();
        services.AddTransient<IStateServiceBus, StateServiceBus>();
        services.AddTransient<IUnitsDepartmentServiceBus, UnitsDepartmentServiceBus>();
        services.AddTransient<IUploadFilesServiceBus, UploadFilesServiceBus>();
        services.AddTransient<IWebSiteServiceBus, WebSiteServiceBus>();
    }
}
