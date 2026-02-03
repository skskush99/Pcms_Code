using Master.Repository.Menu;
using Master.Repository.Roles;
using Master.Repository.Users;
using Master.Repository.AdminDepartment;
using Master.Repository.CircularOrder;
using Master.Repository.CourtName;
using Master.Repository.CourtPlaces;
using Master.Repository.CourtTypes;
using Master.Repository.CrimeAct;
using Master.Repository.CrimeSubAct;
using Master.Repository.CrimeClassification;
using Master.Repository.Designation;
using Master.Repository.FirStatus;
using Master.Repository.JanPratinidhi;
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

public interface IUnitOfWorkRepository
{
    IRoles Roles { get; }
    IUserLogin UserLogins { get; set; }
    IMenu Menu { get; set; }
    IAdminDepartment AdminDepartments { get; set; }
    ICircularOrder CircularOrder { get; set; }
    ICourtNames CourtNames { get; set; }
    ICourtPlaces CourtPlaces { get; set; }
    ICourtTypes CourtTypes { get; set; }
    ICrimeAct CrimeAct { get; set; }
    ICrimeSubAct CrimeSubAct { get; set; }
    ICrimeClassification CrimeClassification { get; set; }
    IDesignation Designation { get; set; }
    IFirStatus FirStatus { get; set; }
    IJanPratinidhi JanPratinidhi { get; set; }
    ILevel Level { get; set; }
    INews News { get; set; }
    INodalOfficer NodalOfficer { get; set; }
    IOffices Offices { get; set; }
    IPoliceThana PoliceThana { get; set; }
    IRajMaster RajMaster { get; set; }
    IReqInformation ReqInformation { get; set; }
    IState State { get; set; }
    IUnitsDepartment UnitsDepartment { get; set; }
    IUploadFilesRepository UploadFiles { get; set; }
    IWebSiteRepository WebSite { get; set; }

}
