using Master.Repository.Menu;
using Master.Repository.Roles;
using Master.Repository.Users;
using Master.Repository.AdminDepartment;
using Master.Repository.CircularOrder;
using Master.Repository.CourtName;
using Master.Repository.CourtPlaces;
using Master.Repository.CourtTypes;
using Master.Repository.Designation;
using Master.Repository.Level;
using Master.Repository.News;
using Master.Repository.NodalOfficer;
using Master.Repository.Offices;
using Master.Repository.PoliceThana;
using Master.Repository.RajMaster;
using Master.Repository.ReqInformation;
using Master.Repository.State;
using Master.Repository.UnitsDepartment;
using Master.Repository.UploadFiles;
using Master.Repository.WebSite;


namespace Master.Repository.UnitOfwork;

public class UnitOfWorkRepository(IRoles Roles, IUserLogin UserLogins, IMenu Menu, IAdminDepartment AdminDepartments, ICircularOrder CircularOrder, ICourtNames CourtNames, 
    ICourtPlaces CourtPlaces, ICourtTypes CourtTypes, IDesignation Designation, ILevel Level, INews News, INodalOfficer NodalOfficer, IOffices Offices, IPoliceThana PoliceThana, IRajMaster RajMaster, 
    IReqInformation ReqInformation, IState State, IUnitsDepartment UnitsDepartment, IUploadFilesRepository UploadFiles, IWebSiteRepository WebSite) : IUnitOfWorkRepository
{
    public IRoles Roles { get; set; } = Roles;
    public IUserLogin UserLogins { get; set; } = UserLogins;
    public IMenu Menu { get; set; } = Menu;
    public IAdminDepartment AdminDepartments { get; set; } = AdminDepartments;
    public ICircularOrder CircularOrder { get; set; } = CircularOrder;
    public ICourtNames CourtNames { get; set; } = CourtNames;
    public ICourtPlaces CourtPlaces { get; set; } = CourtPlaces;
    public ICourtTypes CourtTypes { get; set; } = CourtTypes;
    public IDesignation Designation { get; set; } = Designation;
    public ILevel Level { get; set; } = Level;
    public INews News { get; set; } = News;
    public INodalOfficer NodalOfficer { get; set; }= NodalOfficer;
    public IOffices Offices { get; set; } = Offices;
    public IPoliceThana PoliceThana { get; set; } = PoliceThana;
    public IRajMaster RajMaster { get; set; } = RajMaster;
    public IReqInformation ReqInformation { get; set; } = ReqInformation;
    public IState State { get; set; } = State;
    public IUnitsDepartment UnitsDepartment { get; set; }= UnitsDepartment;
    public IUploadFilesRepository UploadFiles { get; set; } = UploadFiles;
    public IWebSiteRepository WebSite {  get; set; }= WebSite;

}
