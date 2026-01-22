using Master.ServiceBus.Menu;
using Master.ServiceBus.Roles;
using Master.ServiceBus.Users;
using Master.ServiceBus.AdminDepartmentService;
using Master.ServiceBus.CircularOrderService;
using Master.ServiceBus.CourtNamesService;
using Master.ServiceBus.CourtPlacesService;
using Master.ServiceBus.CourtTypesService;
using Master.ServiceBus.CrimeActService;
using Master.ServiceBus.CrimeClassificationService;
using Master.ServiceBus.CrimeSubActService;
using Master.ServiceBus.DesignationService;
using Master.ServiceBus.FirStatusService;
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


namespace Master.ServiceBus.UnitOfWork;

public interface IUnitOfWorkService
{
    IRolesServiceBus Roles { get; }
    IUserLoginServiceBus UserLogins { get; set; }
    IMenuServiceBus Menu { get; set; }
    IAdminDepartmentServiceBus AdminDepartmentServiceBus { get; set; }
    ICircularOrderServiceBus CircularOrderServiceBus { get; set; }
    ICourtNamesServiceBus CourtNamesServiceBus { get; set; }
    ICourtPlacesServiceBus CourtPlacesServiceBus { get; set; }
    ICourtTypesServiceBus CourtTypesServiceBus { get; set; }
    ICrimeActServiceBus CrimeActServiceBus { get; set; }
    ICrimeClassificationServiceBus CrimeClassificationServiceBus { get; set; }
    ICrimeSubActServiceBus CrimeSubActServiceBus { get; set; }
    IDesignationServiceBus DesignationServiceBus { get; set; }
    IFirStatusServiceBus FirStatusServiceBus { get; set; }
    ILevelServiceBus LevelServiceBus { get; set; }
    INewsServiceBus NewsServiceBus { get; set; }
    INodalOfficerServiceBus NodalOfficerServiceBus { get; set; }
    IOfficeServiceBus OfficeServiceBus { get; set; }
    IPoliceThanaServiceBus PoliceThanaServiceBus { get; set; }
    IRajMasterServiceBus RajMasterServiceBus { get; set; }
    IReqInformationServiceBus ReqInformationServiceBus { get; set; }
    IStateServiceBus StateServiceBus { get; set; }
    IUnitsDepartmentServiceBus UnitsDepartmentServiceBus { get; set; }
    IUploadFilesServiceBus UploadFiles { get; set; }
    IWebSiteServiceBus WebSite { get; set; }

}

