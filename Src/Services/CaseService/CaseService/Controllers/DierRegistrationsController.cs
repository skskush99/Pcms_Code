using Case.Dto.DierRegistrations;
using Case.Dto.Shared;
using Case.ServiceBus.UnitOfWork;
using CaseService.Middleware;
using Common.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CaseService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]

    public class DierRegistrationsController : ControllerBase
    {
        private readonly LogsService _logsService;
        private readonly IUnitOfWorkService unitOfWork;
        public DierRegistrationsController(IUnitOfWorkService unitOfWorkService, LogsService logsService)
        {
            _logsService = logsService;
            unitOfWork = unitOfWorkService;
        }

        [HttpPost]
        public async Task<ResponseModel> GetDierList(DierListFilterModel objModel)
        {
            try
            {
                var loginUserData = UserSession.Current;
                //if (loginUserData.DepartmentId > 0)
                //    objModel.AdmDepttId = loginUserData.DepartmentId;
                //if (loginUserData.UnitId > 0)
                //    objModel.UnitId = loginUserData.UnitId;
                //if (loginUserData.OfficeId > 0)
                //    objModel.OfficeId = loginUserData.OfficeId;
                //if (loginUserData.OICId > 0)
                //    objModel.OICId = loginUserData.OICId;
                //if (loginUserData.LawyerId > 0)
                //    objModel.LawyerId = loginUserData.LawyerId;
                //if (loginUserData.DistrictId > 0 && (loginUserData.RoleId == 6 || loginUserData.RoleId == 7))
                //    objModel.DistrictId = loginUserData.DistrictId;
                //objModel.RoleId = loginUserData.RoleId;
                return await unitOfWork.DierRegistrationsService.GetDierList(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetDierList", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrationsController/GetDierList");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        [HttpPost]
        public async Task<DierRegistrationsResponseModel> AddEditDierRegistrations(DierRegistrationsModel objModel)
        {
            try
            {
                return await unitOfWork.DierRegistrationsService.AddEditDierRegistrations(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditDierRegistrations", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrationsController/AddEditDierRegistrations");
                return new DierRegistrationsResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }


        [HttpGet]
        public async Task<ResponseWithoutPaginationModel> GetDierAccused(long AccusedGroupNo)
        {
            try
            {
                return await unitOfWork.DierRegistrationsService.GetDierAccused(AccusedGroupNo);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetDierAccused", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrationsController/GetDierAccused");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> AddEditDierAccused(DierAccusedModel objModel)
        {
            try
            {
                return await unitOfWork.DierRegistrationsService.AddEditDierAccused(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditDierAccused", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrationsController/AddEditDierAccused");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> DeleteDierAccused(long AccusedId)
        {
            try
            {
                return await unitOfWork.DierRegistrationsService.DeleteDierAccused(AccusedId, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DeleteDierAccused", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrationsController/DeleteDierAccused");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }


        [HttpGet]
        public async Task<ResponseWithoutPaginationModel> GetDierVictim(long VictimGroupNo)
        {
            try
            {
                return await unitOfWork.DierRegistrationsService.GetDierVictim(VictimGroupNo);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetDierVictim", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrationsController/GetDierVictim");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> AddEditDierVictim(DierVictimModel objModel)
        {
            try
            {
                return await unitOfWork.DierRegistrationsService.AddEditDierVictim(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditDierVictim", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrationsController/AddEditDierVictim");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> DeleteDierVictim(long VictimId)
        {
            try
            {
                return await unitOfWork.DierRegistrationsService.DeleteDierVictim(VictimId, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DeleteDierVictim", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrationsController/DeleteDierVictim");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpGet]
        public async Task<ResponseWithoutPaginationModel> GetDierWitness(long WitnessGroupNo)
        {
            try
            {
                return await unitOfWork.DierRegistrationsService.GetDierWitness(WitnessGroupNo);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetDierWitness", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrationsController/GetDierWitness");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> AddEditDierWitness(DierWitnessModel objModel)
        {
            try
            {
                return await unitOfWork.DierRegistrationsService.AddEditDierWitness(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditDierWitness", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrationsController/AddEditDierWitness");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> DeleteDierWitness(long WitnessId)
        {
            try
            {
                return await unitOfWork.DierRegistrationsService.DeleteDierWitness(WitnessId, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DeleteDierWitness", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrationsController/DeleteDierWitness");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }


        [HttpGet]
        public async Task<ResponseWithoutPaginationModel> GetDierInvestigation(long InvestGroupNo)
        {
            try
            {
                return await unitOfWork.DierRegistrationsService.GetDierInvestigation(InvestGroupNo);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetDierInvestigation", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrationsController/GetDierInvestigation");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> AddEditDierInvestigation(DierInvestigationModel objModel)
        {
            try
            {
                return await unitOfWork.DierRegistrationsService.AddEditDierInvestigation(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditDierInvestigation", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrationsController/AddEditDierInvestigation");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> DeleteDierInvestigation(long InvestId)
        {
            try
            {
                return await unitOfWork.DierRegistrationsService.DeleteDierInvestigation(InvestId, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DeleteDierInvestigation", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrationsController/DeleteDierInvestigation");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }


        [HttpGet]
        public async Task<ResponseWithoutPaginationModel> GetDierComplaintAgainstPerson(long ComplaintPerGroupNo)
        {
            try
            {
                return await unitOfWork.DierRegistrationsService.GetDierComplaintAgainstPerson(ComplaintPerGroupNo);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetDierComplaintAgainst", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrationsController/GetDierComplaintAgainst");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> AddEditDierComplaintAgainstPerson(DierComplaintAgainstPersonModel objModel)
        {
            try
            {
                return await unitOfWork.DierRegistrationsService.AddEditDierComplaintAgainstPerson(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditDierComplaintAgainst", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrationsController/AddEditDierComplaintAgainst");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> DeleteDierComplaintAgainstPerson(long ComplaintPerId)
        {
            try
            {
                return await unitOfWork.DierRegistrationsService.DeleteDierComplaintAgainstPerson(ComplaintPerId, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DeleteDierComplaintAgainst", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrationsController/DeleteDierComplaintAgainst");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }


    }
}
