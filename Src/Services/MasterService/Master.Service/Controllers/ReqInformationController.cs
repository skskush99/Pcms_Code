using Master.Dto.Masters;
using Master.Dto.Shared;
using Master.ServiceBus.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Master.Service.Middleware;
using Common.Repository;

namespace Master.Service.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ReqInformationController : ControllerBase
    {
        private readonly IUnitOfWorkService _IUnitOfWorkService;
        private readonly LogsService _logsService;
        public ReqInformationController(IUnitOfWorkService unitOfWorkService, LogsService logsService)
        {
            _IUnitOfWorkService = unitOfWorkService;
            _logsService = logsService;
        }

        [HttpPost]
        public async Task<ResponseModel> GetReqInformation(ReqInformationFilterModel objModel)
        {
            try
            {
                return await _IUnitOfWorkService.ReqInformationServiceBus.GetReqInformation(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetReqInformation", ex.Message, ex.StackTrace, ex.Source, "MasterService/ReqInformationController/GetReqInformation");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseModel> GetReqInformationPopUp(GetReqInformationPopUpFilterModel objModel)
        {
            try
            {
                var loginUserData = UserSession.Current;
                if (loginUserData.RoleId == 2 || loginUserData.RoleId == 8 || loginUserData.RoleId == 10)
                {
                    objModel.DistDept = loginUserData.DepartmentId;
                    objModel.DPDT = "DP";
                }
                    
                if (loginUserData.RoleId == 6 || loginUserData.RoleId == 13)
                {
                    objModel.DistDept = loginUserData.DistrictId;
                    objModel.DPDT = "DT";
                }
                return await _IUnitOfWorkService.ReqInformationServiceBus.GetReqInformationPopUp(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetReqInformationPopUp", ex.Message, ex.StackTrace, ex.Source, "MasterService/ReqInformationController/GetReqInformationPopUp");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpGet]
        public async Task<ResponseWithoutPaginationModel> GetReqInformationDropdownList()
        {
            try
            {
                return await _IUnitOfWorkService.ReqInformationServiceBus.GetReqInformationDropdownList();
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetReqInformationDropdownList", ex.Message, ex.StackTrace, ex.Source, "MasterService/ReqInformationController/GetReqInformationDropdownList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseModel> AddEditReqInformation(ReqInformationModel objModel)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                objModel.CreatedBy = UserId;
                objModel.UpdatedBy = UserId;
                return await _IUnitOfWorkService.ReqInformationServiceBus.AddEditReqInformation(objModel, UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditReqInformation", ex.Message, ex.StackTrace, ex.Source, "MasterService/ReqInformationController/AddEditReqInformation");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseModel> ActiveDeactiveReqInformation(ReqInformationActiveDeactiveModel objModel)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                objModel.UpdatedBy = UserId;
                return await _IUnitOfWorkService.ReqInformationServiceBus.ActiveDeactiveReqInformation(objModel, UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ActiveDeactiveReqInformation", ex.Message, ex.StackTrace, ex.Source, "MasterService/ReqInformationController/ActiveDeactiveReqInformation");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseModel> ReqInformationUpdate(ReqInformationUpdateModel objModel)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                objModel.UpdatedBy = UserId;
                return await _IUnitOfWorkService.ReqInformationServiceBus.ReqInformationUpdate(objModel, UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ReqInformationUpdate", ex.Message, ex.StackTrace, ex.Source, "MasterService/ReqInformationController/ReqInformationUpdate");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseModel> ReqInformationReset(ReqInformationUpdateModel objModel)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                objModel.UpdatedBy = UserId;
                return await _IUnitOfWorkService.ReqInformationServiceBus.ReqInformationReset(objModel, UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ReqInformationReset", ex.Message, ex.StackTrace, ex.Source, "MasterService/ReqInformationController/ReqInformationReset");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }


    }
}
