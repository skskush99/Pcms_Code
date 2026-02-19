using Common.Repository;
using Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Report.Dto.Dashboard;
using Report.Dto.DetailReports;
using Report.Dto.Global;
using Report.Dto.MISReport.FormatWise;
using Report.Dto.MISReport.NextHearing;
using Report.Repository.Global;
using Report.ServiceBus.UnitOfWork;
using ReportService.Common;
using ReportService.Middleware;
using System.Globalization;
using System.Web;
//using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ReportService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class MISReportController : ControllerBase
    {
        private readonly IUnitOfWorkService _IUnitOfWorkService;
        private readonly LogsService _logsService;
        int deptId = 0;
        int unitId = 0;
        int officeId = 0;
        int oicId = 0;
        int districtId = 0;
        int statusId = 2;
        int rpttype = 1;

        string deptName = "";
        string unitName = "";
        string officeName = "";
        string oicName = "";
        string SAName = "";
        string StatusValue = "";
        string MainperValue = "";
        string mainper = "0";
        string districtName = "";

        public MISReportController(IUnitOfWorkService unitOfWorkService, LogsService logsService)
        {
            _IUnitOfWorkService = unitOfWorkService;
            _logsService = logsService;
        }

        #region MIS Report

        [HttpPost]
        public async Task<FormatWiseReportsModel> GetFormat_AReport(Format_AReportModel objModel)
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
                return await _IUnitOfWorkService.FormatWiseService.GetFormat_AReport(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetFormat_AReport", ex.Message, ex.StackTrace, ex.Source, "ReportService/MISReportController/GetFormat_AReport");
                return new FormatWiseReportsModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<FormatWiseReportsModel> GetFormat_BReport(Format_BReportModel objModel)
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
                return await _IUnitOfWorkService.FormatWiseService.GetFormat_BReport(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetFormat_BReport", ex.Message, ex.StackTrace, ex.Source, "ReportService/MISReportController/GetFormat_BReport");
                return new FormatWiseReportsModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }


        [HttpPost]
        public ResponseModel LoginDetaiReportGird(DataPagingModel TablePaging)
        {
            try
            {
                int? AdmDepttId = 0;
                int UnitId = 0;
                int OfficeId = 0;
                int OicId = 0;
                DateTime fromDate = DateTime.Now;
                DateTime toDate = DateTime.Now;
                foreach (var item in TablePaging.SearchParameter)
                {
                    string value = item.Value.Trim();
                    if (item.Key.ToLower() == "admdepttid" && !String.IsNullOrEmpty(value))
                    {
                        AdmDepttId = Convert.ToInt32(value);
                    }
                    if (item.Key.ToLower() == "unitid" && !String.IsNullOrEmpty(value))
                    {
                        UnitId = Convert.ToInt32(value);
                    }
                    if (item.Key.ToLower() == "officeid" && !String.IsNullOrEmpty(value))
                    {
                        OfficeId = Convert.ToInt32(value);
                    }
                    if (item.Key.ToLower() == "oicid" && !String.IsNullOrEmpty(value))
                    {
                        OicId = Convert.ToInt32(value);
                    }

                    if (item.Key.ToLower() == "fromdate" && !String.IsNullOrEmpty(value))
                    {
                        fromDate = DateTime.ParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    if (item.Key.ToLower() == "todate" && !String.IsNullOrEmpty(value))
                    {
                        toDate = DateTime.ParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                }
                return _IUnitOfWorkService.LoginReport.GetLoginDetailReport(fromDate, toDate, AdmDepttId, UnitId, OfficeId, OicId, TablePaging.PageSize, TablePaging.StartPageNumber);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "LoginDetaiReportGird", ex.Message, ex.StackTrace, ex.Source, "ReportService/MISReportController/LoginDetaiReportGird");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public IEnumerable<UpdateNextHearingHistory> NextHearingUpdatedGrid(DataPagingModel TablePaging)
        {
            try
            {
                return _IUnitOfWorkService.NextHearingUpdate.GetNextHearingUpdateReport(TablePaging);
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        

        [HttpGet]
        public Trn_CaseRegistrations GetCaseDetails(string caseID)
        {
            try
            {
                int Id = caseID.DecryptID();
                Trn_CaseRegistrations model = new Trn_CaseRegistrations();
                model.CaseRegDate = DateTime.Now.ToString("dd/MM/yyyy");

                model = _IUnitOfWorkService.Dashboard.GetCaseDetails(Id);
                if (model.CaseRegistrationDate != null)
                {
                    model.CaseRegDate = model.CaseRegistrationDate.Value.ToString("dd/MM/yyyy");
                }
                if (model.AppellantOrResponded == "A")
                    model.AppellantOrResponded = "Appellant";
                else
                    model.AppellantOrResponded = "Responded";
                if (model.CaseId != 0)
                {
                }

                return model;
            }
            catch (Exception ex)
            {

            }
            return null;
        }

 



        #endregion
    }
}
