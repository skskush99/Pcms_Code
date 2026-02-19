using Common.Repository;
using Core;
using Core.Enums.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Report.Dto.DetailReports;
//using Microsoft.IdentityModel.Tokens;
using Report.Dto.Global;
using Report.Dto.SummaryReports.DistrictLevel;
using Report.Dto.SummaryReports.PravivaranWise;
using Report.ServiceBus.UnitOfWork;
using ReportService.Common;
using ReportService.Middleware;
using System.Globalization;
using System.Security.Cryptography;

namespace ReportService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class SummaryReportController : ControllerBase
    {
        private readonly IUnitOfWorkService _IUnitOfWorkService;
        private readonly LogsService _logsService;
        public SummaryReportController(IUnitOfWorkService unitOfWorkService, LogsService logsService)
        {
            _IUnitOfWorkService = unitOfWorkService;
            _logsService = logsService;
        }



        [HttpPost]
        public ResponseModel CNR_ReportGrid(DataPagingModel TablePaging)
        {
            try
            {
                int AdmDepttId = 0, unitId = 0, officeId = 0, districtId = 0, lavelId = 0;

                foreach (var item in TablePaging.SearchParameter)
                {
                    string value = item.Value.Trim();
                    if (item.Key.ToLower() == "admdepttid" && !String.IsNullOrEmpty(value))
                    {
                        AdmDepttId = Convert.ToInt32(value);
                    }
                    if (item.Key.ToLower() == "unitid" && !String.IsNullOrEmpty(value))
                    {
                        unitId = Convert.ToInt32(value);
                    }
                    if (item.Key.ToLower() == "officeid" && !String.IsNullOrEmpty(value))
                    {
                        officeId = Convert.ToInt32(value);
                    }
                    if (item.Key.ToLower() == "districtid" && !String.IsNullOrEmpty(value))
                    {
                        districtId = Convert.ToInt32(value);
                    }
                    if (item.Key.ToLower() == "lavelid" && !String.IsNullOrEmpty(value))
                    {
                        lavelId = Convert.ToInt32(value);
                    }
                }
                #region User Wise Filter
                var loginUserData = UserSession.Current;
                if (loginUserData.DepartmentId > 0)
                    AdmDepttId = loginUserData.DepartmentId;
                if (loginUserData.UnitId > 0)
                    unitId = loginUserData.UnitId;
                if (loginUserData.OfficeId > 0)
                    officeId = loginUserData.OfficeId;
                if (loginUserData.DistrictId > 0)
                    districtId = loginUserData.DistrictId;
                #endregion

                return _IUnitOfWorkService.CNRReport.GetCNRReport(AdmDepttId, districtId, unitId, officeId, lavelId, TablePaging.PageSize, TablePaging.StartPageNumber);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "CNR_ReportGrid", ex.Message, ex.StackTrace, ex.Source, "ReportService/SummaryReportController/CNR_ReportGrid");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public ResponseModel CNRGrid(DataPagingModel TablePaging)
        {
            try
            {
                int AuthRoleId = 1, DepartmentId = 0, UnitId = 0, OfficeId = 0;
                ResponseModel cnrFianlModel = new ResponseModel();
                if (AuthRoleId == 6)
                {
                    cnrFianlModel = _IUnitOfWorkService.CNRReport.GetCNRListSadReport(ref TablePaging);
                }
                else
                {
                    cnrFianlModel = _IUnitOfWorkService.CNRReport.GetCNRListReport(ref TablePaging, DepartmentId, UnitId, OfficeId);
                }

                return cnrFianlModel;
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "CNRGrid", ex.Message, ex.StackTrace, ex.Source, "ReportService/SummaryReportController/CNRGrid");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpGet]
        public IEnumerable<DistrictWiseMonitoring> DistrictLevelWiseReport(int AdmDeptt = 0, int UnitName = 0, int OfficeName = 0, int DistrictId = 0, string Status = "All", string Level = "Admindeptt Wise", string Type = "Office Wise")
        {
            try
            {
                #region User Wise Filter
                var loginUserData = UserSession.Current;
                if (loginUserData.RoleId == (int)AccessRoles.SAD || loginUserData.RoleId == (int)AccessRoles.DepartmentD)
                {
                    DistrictId = loginUserData.DistrictId;
                }
                if (loginUserData.RoleId == (int)AccessRoles.Department || loginUserData.RoleId == (int)AccessRoles.NodalHod
                  || loginUserData.RoleId == (int)AccessRoles.DepartmentD)
                {
                    AdmDeptt = loginUserData.DepartmentId;
                }
                if (loginUserData.RoleId == (int)AccessRoles.Unit)
                {
                    UnitName = loginUserData.UnitId;
                }
                if (loginUserData.RoleId == (int)AccessRoles.Office)
                {
                    OfficeName = loginUserData.OfficeId;
                }
                if (loginUserData.RoleId == (int)AccessRoles.OIC)
                {
                    OfficeName = loginUserData.OfficeId;
                    UnitName = loginUserData.UnitId;
                }
                #endregion

                int statusId = (Status == "All" ? 2 : (Status == "Pending" ? 0 : 1));
                int LavalId = (Level == "Admindeptt Wise" ? 1 : (Level == "Unit Wise" ? 2 : 3));
                int TypeId = (Type == "Office Wise" ? 1 : 2);

                return _IUnitOfWorkService.DistrictWiseMonitoring.GetDistrictWiseMonitoringReport(AdmDeptt, UnitName, OfficeName, DistrictId, statusId, LavalId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DistrictLevelWiseReport", ex.Message, ex.StackTrace, ex.Source, "ReportService/SummaryReportController/DistrictLevelWiseReport");
            }
            return null;
        }


        [HttpGet]
        public ResponseModel UserRegistrationReportGrid(int departmentId = 0, int unitId = 0, int officeId = 0, int Role = 0, int pageSize = 10, int currentPage = 1)
        {
            try
            {
                return _IUnitOfWorkService.UserRegistration.GetUserRegistrationSummaryReport(departmentId, unitId, officeId, Role, pageSize, currentPage);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "UserRegistrationReportGrid", ex.Message, ex.StackTrace, ex.Source, "ReportService/SummaryReportController/UserRegistrationReportGrid");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpGet]
        public ResponseModel MonthlyEntryStatusReportGridNew(int AdmDepttName = 0, int UnitName = 0, int OfficeName = 0, int Month = 0, int Year = 0, int districtId = 0, int roleid = 0, int pageSize = 10, int currentPage = 1)
        {
            try
            {
                return _IUnitOfWorkService.MonthlyEntry.GetMonthlyEntryStatusReport(AdmDepttName, UnitName, OfficeName, Month, Year, districtId, roleid, pageSize, currentPage);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "MonthlyEntryStatusReportGridNew", ex.Message, ex.StackTrace, ex.Source, "ReportService/SummaryReportController/MonthlyEntryStatusReportGridNew");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }


        [HttpPost]
        public async Task<PravivaranResponseModel> GetPravivaran2(Pravivaran_2Model objModel)
        {
            try
            {
                #region User Wise Filter
                var loginUserData = UserSession.Current;
                if (loginUserData.RoleId == (int)AccessRoles.Department)
                {
                    objModel.DepartmentId = loginUserData.DepartmentId;
                }
                #endregion
                return await _IUnitOfWorkService.PravivaranWiseService.GetPravivaran2(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetPravivaran2", ex.Message, ex.StackTrace, ex.Source, "ReportService/SummaryReportController/GetPravivaran2");
                return new PravivaranResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<PravivaranResponseModel> GetPravivaran3(Pravivaran_2Model objModel)
        {
            try
            {
                #region User Wise Filter
                var loginUserData = UserSession.Current;
                if (loginUserData.RoleId == (int)AccessRoles.Department)
                {
                    objModel.DepartmentId = loginUserData.DepartmentId;
                }
                #endregion
                return await _IUnitOfWorkService.PravivaranWiseService.GetPravivaran3(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetPravivaran3", ex.Message, ex.StackTrace, ex.Source, "ReportService/SummaryReportController/GetPravivaran3");
                return new PravivaranResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<PravivaranResponseModel> GetPravivaran3K(Pravivaran_2Model objModel)
        {
            try
            {
                #region User Wise Filter
                var loginUserData = UserSession.Current;
                if (loginUserData.RoleId == (int)AccessRoles.Department)
                {
                    objModel.DepartmentId = loginUserData.DepartmentId;
                }
                #endregion
                return await _IUnitOfWorkService.PravivaranWiseService.GetPravivaran3K(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetPravivaran3K", ex.Message, ex.StackTrace, ex.Source, "ReportService/SummaryReportController/GetPravivaran3K");
                return new PravivaranResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<PravivaranResponseModel> GetPravivaran3Kha(Pravivaran_2Model objModel)
        {
            try
            {
                #region User Wise Filter
                var loginUserData = UserSession.Current;
                if (loginUserData.RoleId == (int)AccessRoles.Department)
                {
                    objModel.DepartmentId = loginUserData.DepartmentId;
                }
                #endregion
                return await _IUnitOfWorkService.PravivaranWiseService.GetPravivaran3Kha(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetPravivaran3Kha", ex.Message, ex.StackTrace, ex.Source, "ReportService/SummaryReportController/GetPravivaran3Kha");
                return new PravivaranResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<PravivaranResponseModel> GetPravivaran7(Pravivaran_2Model objModel)
        {
            try
            {
                #region User Wise Filter
                var loginUserData = UserSession.Current;
                if (loginUserData.RoleId == (int)AccessRoles.Department)
                {
                    objModel.DepartmentId = loginUserData.DepartmentId;
                }
                #endregion
                return await _IUnitOfWorkService.PravivaranWiseService.GetPravivaran7(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetPravivaran7", ex.Message, ex.StackTrace, ex.Source, "ReportService/SummaryReportController/GetPravivaran7");
                return new PravivaranResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }


        [HttpPost]
        public async Task<PravivaranResponseModel> GetReturn4(Pravivaran_2Model objModel)
        {
            try
            {
                #region User Wise Filter
                var loginUserData = UserSession.Current;
                if (loginUserData.RoleId == (int)AccessRoles.Department)
                {
                    objModel.DepartmentId = loginUserData.DepartmentId;
                }
                #endregion
                return await _IUnitOfWorkService.PravivaranWiseService.GetReturn4(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetReturn4", ex.Message, ex.StackTrace, ex.Source, "ReportService/SummaryReportController/GetReturn4");
                return new PravivaranResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }






        [HttpGet]
        public ResponseModel ActionTobeTakenGrid(int deptId = 0, int districtId = 0, int oicId = 0, int unitId = 0, int officeId = 0, int level = 0, int roleid = 0, string main_Party = "", int pageSize = 10, int currentPage = 1)
        {
            try
            {
                return _IUnitOfWorkService.ActionTaken.GetActionToBeTakenReport(deptId, unitId, officeId, districtId, oicId, level, roleid, "", pageSize, currentPage);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ActionTobeTakenGrid", ex.Message, ex.StackTrace, ex.Source, "ReportService/SummaryReportController/ActionTobeTakenGrid");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpGet]
        public ResponseModel ActionTobeTakenGridNew(int deptId = 0, int districtId = 0, int oicId = 0, int unitId = 0, int officeId = 0, int level = 0, int roleid = 0, string main_Party = "", int pageSize = 10, int currentPage = 1)
        {
            try
            {
                return _IUnitOfWorkService.ActionTaken.ActionTobeTakenGridNew(deptId, unitId, officeId, districtId, oicId, level, roleid, "", pageSize, currentPage);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ActionTobeTakenGridNew", ex.Message, ex.StackTrace, ex.Source, "ReportService/SummaryReportController/ActionTobeTakenGridNew");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

    }
}
