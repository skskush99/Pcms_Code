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


namespace Master.ServiceBus.UnitOfWork
{
    public class UnitOfWorkService(IRolesServiceBus Roles, IUserLoginServiceBus UserLogins, IMenuServiceBus Menu, IAdminDepartmentServiceBus AdminDepartmentServiceBus,
        ICircularOrderServiceBus CircularOrderServiceBus, ICourtNamesServiceBus CourtNamesServiceBus, ICourtPlacesServiceBus CourtPlacesServiceBus, 
        ICourtTypesServiceBus CourtTypesServiceBus, ICrimeActServiceBus CrimeActServiceBus, ICrimeClassificationServiceBus CrimeClassificationServiceBus, ICrimeSubActServiceBus CrimeSubActServiceBus, 
        IDesignationServiceBus DesignationServiceBus, IFirStatusServiceBus FirStatusServiceBus, ILevelServiceBus LevelServiceBus, INewsServiceBus NewsServiceBus, 
        INodalOfficerServiceBus NodalOfficerServiceBus, IOfficeServiceBus OfficeServiceBus, IRajMasterServiceBus RajMasterServiceBus, 
        IReqInformationServiceBus ReqInformationServiceBus, IStateServiceBus StateServiceBus, IUnitsDepartmentServiceBus UnitsDepartmentServiceBus, 
        IPoliceThanaServiceBus PoliceThanaServiceBus, IUploadFilesServiceBus UploadFiles, IWebSiteServiceBus WebSite) : IUnitOfWorkService
    {
        public IRolesServiceBus Roles { get; set; } = Roles;
        public IMenuServiceBus Menu { get; set; } = Menu;
        public IUserLoginServiceBus UserLogins { get; set; } = UserLogins;
        public IAdminDepartmentServiceBus AdminDepartmentServiceBus { get; set; } = AdminDepartmentServiceBus;
        public ICircularOrderServiceBus CircularOrderServiceBus { get; set; } = CircularOrderServiceBus;
        public ICourtNamesServiceBus CourtNamesServiceBus { get; set; } = CourtNamesServiceBus;
        public ICourtPlacesServiceBus CourtPlacesServiceBus { get; set; } = CourtPlacesServiceBus;
        public ICourtTypesServiceBus CourtTypesServiceBus { get; set; } = CourtTypesServiceBus;
        public ICrimeActServiceBus CrimeActServiceBus { get; set; } = CrimeActServiceBus;
        public ICrimeClassificationServiceBus CrimeClassificationServiceBus { get; set; } = CrimeClassificationServiceBus;
        public ICrimeSubActServiceBus CrimeSubActServiceBus { get; set; } = CrimeSubActServiceBus;
        public IDesignationServiceBus DesignationServiceBus { get; set; } = DesignationServiceBus;
        public IFirStatusServiceBus FirStatusServiceBus { get; set; } = FirStatusServiceBus;
        public ILevelServiceBus LevelServiceBus { get; set; } = LevelServiceBus;
        public INewsServiceBus NewsServiceBus { get; set; } = NewsServiceBus;
        public INodalOfficerServiceBus NodalOfficerServiceBus { get; set; }= NodalOfficerServiceBus;
        public IOfficeServiceBus OfficeServiceBus { get; set; } = OfficeServiceBus;
        public IPoliceThanaServiceBus PoliceThanaServiceBus { get; set; } = PoliceThanaServiceBus;
        public IRajMasterServiceBus RajMasterServiceBus { get; set; }=RajMasterServiceBus;
        public IReqInformationServiceBus ReqInformationServiceBus { get; set; } = ReqInformationServiceBus;
        public IStateServiceBus StateServiceBus { get; set; } = StateServiceBus;
        public IUnitsDepartmentServiceBus UnitsDepartmentServiceBus { get; set; }= UnitsDepartmentServiceBus;
        public IUploadFilesServiceBus UploadFiles { get; set; } = UploadFiles;
        public IWebSiteServiceBus WebSite { get; set; }= WebSite;
    }
}
