using Case.Dto.CaseRegistrations;
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
    public class DierCaseRegistrationsController : ControllerBase
    {
        private readonly LogsService _logsService;
        private readonly IUnitOfWorkService unitOfWork;
        public DierCaseRegistrationsController(IUnitOfWorkService unitOfWorkService, LogsService logsService)
        {
            _logsService = logsService;
            unitOfWork = unitOfWorkService;
        }

        [HttpPost]
        public async Task<ResponseModel> GetCaseList(CaseListFilterModel objModel)
        {
            try
            {
                var loginUserData = UserSession.Current;
                if (loginUserData.DepartmentId > 0)
                    objModel.AdmDepttId = loginUserData.DepartmentId;
                if (loginUserData.UnitId > 0)
                    objModel.UnitId = loginUserData.UnitId;
                if (loginUserData.OfficeId > 0)
                    objModel.OfficeId = loginUserData.OfficeId;
                if (loginUserData.OICId > 0)
                    objModel.OICId = loginUserData.OICId;
                if (loginUserData.LawyerId > 0)
                    objModel.LawyerId = loginUserData.LawyerId;
                if (loginUserData.DistrictId > 0 && (loginUserData.RoleId == 6 || loginUserData.RoleId == 7))
                    objModel.DistrictId = loginUserData.DistrictId;
                objModel.RoleId = loginUserData.RoleId;
                return await unitOfWork.CaseRegistrations.GetCaseList(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetCaseList", ex.Message, ex.StackTrace, ex.Source, "CaseService/CaseRegistrationsController/GetCaseList");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<CaseRegistrationsResponseModel> AddEditCase(CaseRegistrationsModel objModel)
        {
            try
            {
                return await unitOfWork.CaseRegistrations.AddEditCaseRegistrations(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditCase", ex.Message, ex.StackTrace, ex.Source, "CaseService/CaseRegistrationsController/AddEditCase");
                return new CaseRegistrationsResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<CaseRegistrationsResponseModel> DeleteCase(long CaseId, string DeleteMobileNo = "", string Reason = "")
        {
            try
            {
                return await unitOfWork.CaseRegistrations.DeleteCase(CaseId, DeleteMobileNo, Reason, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DeleteCase", ex.Message, ex.StackTrace, ex.Source, "CaseService/CaseRegistrationsController/DeleteCase");
                return new CaseRegistrationsResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<CaseRegistrationsResponseModel> AddGroup(AddCaseGroupModel objModel)
        {
            try
            {
                return await unitOfWork.CaseRegistrations.AddCaseGroup(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddGroup", ex.Message, ex.StackTrace, ex.Source, "CaseService/CaseRegistrationsController/AddGroup");
                return new CaseRegistrationsResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<CaseRegistrationsResponseModel> AddLinking(AddCaseLinkingModel objModel)
        {
            try
            {
                return await unitOfWork.CaseRegistrations.AddCaseLinking(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddLinking", ex.Message, ex.StackTrace, ex.Source, "CaseService/CaseRegistrationsController/AddLinking");
                return new CaseRegistrationsResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<CaseRegistrationsResponseModel> AddRemand(AddCaseRemandModel objModel)
        {
            try
            {
                return await unitOfWork.CaseRegistrations.AddCaseRemand(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddRemand", ex.Message, ex.StackTrace, ex.Source, "CaseService/CaseRegistrationsController/AddRemand");
                return new CaseRegistrationsResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpGet]
        public async Task<ResponseWithoutPaginationModel> GetAppellantsList(long CaseId)
        {
            try
            {
                return await unitOfWork.CaseRegistrations.GetCaseAppellantsList(CaseId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetAppellantsList", ex.Message, ex.StackTrace, ex.Source, "CaseService/CaseRegistrationsController/GetAppellantsList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> AddEditAppellant(CaseAppellantsModel objModel)
        {
            try
            {
                return await unitOfWork.CaseRegistrations.AddEditCaseAppellants(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditAppellant", ex.Message, ex.StackTrace, ex.Source, "CaseService/CaseRegistrationsController/AddEditAppellant");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> DeleteAppellant(long CaseAppellantId)
        {
            try
            {
                return await unitOfWork.CaseRegistrations.DeleteCaseAppellants(CaseAppellantId, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DeleteAppellant", ex.Message, ex.StackTrace, ex.Source, "CaseService/CaseRegistrationsController/DeleteAppellant");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpGet]
        public async Task<ResponseWithoutPaginationModel> GetRespondentsList(long CaseId)
        {
            try
            {
                return await unitOfWork.CaseRegistrations.GetCaseRespondentsList(CaseId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetRespondentsList", ex.Message, ex.StackTrace, ex.Source, "CaseService/CaseRegistrationsController/GetRespondentsList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> AddEditRespondents(CaseRespondentsModel objModel)
        {
            try
            {
                return await unitOfWork.CaseRegistrations.AddEditCaseRespondents(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditRespondents", ex.Message, ex.StackTrace, ex.Source, "CaseService/CaseRegistrationsController/AddEditRespondents");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> DeleteRespondents(long RespondentId)
        {
            try
            {
                return await unitOfWork.CaseRegistrations.DeleteCaseRespondents(RespondentId, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DeleteRespondents", ex.Message, ex.StackTrace, ex.Source, "CaseService/CaseRegistrationsController/DeleteRespondents");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpGet]
        public async Task<ResponseWithoutPaginationModel> GetCaseDocumentsList(long CaseId)
        {
            try
            {
                return await unitOfWork.CaseRegistrations.GetCaseDocumentsList(CaseId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetCaseDocumentsList", ex.Message, ex.StackTrace, ex.Source, "CaseService/CaseRegistrationsController/GetCaseDocumentsList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        [RequestSizeLimit(52428800)]
        public async Task<CaseRegistrationsResponseModel> AddCaseDocuments(IFormFile SingleFile, [FromForm] CaseDocumentsModel objModel)
        {
            try
            {
                if (SingleFile == null || SingleFile.Length == 0)
                {
                    ModelState.AddModelError("", "File not selected");
                    return new CaseRegistrationsResponseModel()
                    {
                        Status = false,
                        Message = "File not selected"
                    };
                }
                if (Path.GetFileNameWithoutExtension(SingleFile.FileName).Contains("."))
                {
                    return new CaseRegistrationsResponseModel()
                    {
                        Status = false,
                        Message = "Please enter a filename without any dots (e.g., 'myfile' instead of 'myfile.xyz'."
                    };
                }
                var permittedExtensions = new[] { ".jpg", ".png", ".gif", ".pdf", ".doc", ".docx", ".xls", ".xlsx" };
                var extension = Path.GetExtension(SingleFile.FileName).ToLowerInvariant();

                if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
                {
                    return new CaseRegistrationsResponseModel()
                    {
                        Status = false,
                        Message = "Invalid file type."
                    };
                }

                //// Optional: Validate MIME type as well
                //var mimeType = SingleFile.ContentType;
                //var permittedMimeTypes = new[] { "image/jpeg", "image/png", "image/gif" };
                //if (!permittedMimeTypes.Contains(mimeType))
                //{
                //    return new CaseRegistrationsResponseModel()
                //    {
                //        Status = false,
                //        Message = "Invalid MIME type."
                //    };
                //}

                //Validating the File Size
                if (SingleFile.Length > 52428800) // Limit to 50 MB
                {
                    return new CaseRegistrationsResponseModel()
                    {
                        Status = false,
                        Message = "The file is too large."
                    };
                }

                var objData = new CaseAddDocumentModel()
                {
                    CaseId = objModel.CaseId,
                    DocType = objModel.DocType,
                    DocumentName = objModel.DocumentName,
                    DocumentFile = extension
                };
                var objResult = await unitOfWork.CaseRegistrations.AddCaseDocuments(objData, UserSession.Current.UserId);
                if (objResult.Status)
                {
                    string fileName = objResult.ReturnID + extension;
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/CaseDocuments/" + objModel.CaseId);
                    if (!Directory.Exists(filePath))
                        Directory.CreateDirectory(filePath);

                    var filePathWithName = Path.Combine(Directory.GetCurrentDirectory(), filePath, fileName);
                    using (var stream = new FileStream(filePathWithName, FileMode.Create, FileAccess.Write))
                    {
                        await SingleFile.CopyToAsync(stream);
                    }
                }
                return objResult;
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddCaseDocuments", ex.Message, ex.StackTrace, ex.Source, "CaseService/CaseRegistrationsController/AddCaseDocuments");
                return new CaseRegistrationsResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> DeleteCaseDocuments(long CaseDocumentId)
        {
            try
            {
                return await unitOfWork.CaseRegistrations.DeleteCaseDocuments(CaseDocumentId, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DeleteCaseDocuments", ex.Message, ex.StackTrace, ex.Source, "CaseService/CaseRegistrationsController/DeleteCaseDocuments");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseModel> GetCaseListWithoutCaseNo(CaseListFilterModel objModel)
        {
            try
            {
                var loginUserData = UserSession.Current;
                if (loginUserData.DepartmentId > 0)
                    objModel.AdmDepttId = loginUserData.DepartmentId;
                if (loginUserData.UnitId > 0)
                    objModel.UnitId = loginUserData.UnitId;
                if (loginUserData.OfficeId > 0)
                    objModel.OfficeId = loginUserData.OfficeId;
                if (loginUserData.OICId > 0)
                    objModel.OICId = loginUserData.OICId;
                if (loginUserData.LawyerId > 0)
                    objModel.LawyerId = loginUserData.LawyerId;
                if (loginUserData.DistrictId > 0 && (loginUserData.RoleId == 6 || loginUserData.RoleId == 7))
                    objModel.DistrictId = loginUserData.DistrictId;
                objModel.RoleId = loginUserData.RoleId;
                return await unitOfWork.CaseRegistrations.GetCaseListWithoutCaseNo(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetCaseListWithoutCaseNo", ex.Message, ex.StackTrace, ex.Source, "CaseService/CaseRegistrationsController/GetCaseListWithoutCaseNo");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<CaseRegistrationsResponseModel> AddEditCaseWithoutCaseNo(CaseWithoutCaseNoModel objModel)
        {
            try
            {
                return await unitOfWork.CaseRegistrations.AddEditCaseWithoutCaseNo(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditCaseWithoutCaseNo", ex.Message, ex.StackTrace, ex.Source, "CaseService/CaseRegistrationsController/AddEditCaseWithoutCaseNo");
                return new CaseRegistrationsResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<CaseRegistrationsResponseModel> UpdateCaseNoForWithoutCaseNo(CaseRegistrationsModel objModel)
        {
            try
            {
                if (objModel.CaseId == 0)
                {
                    return new CaseRegistrationsResponseModel()
                    {
                        Status = false,
                        Message = "Case id can not be zero."
                    };
                }
                return await unitOfWork.CaseRegistrations.AddEditCaseRegistrations(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "UpdateCaseNoForWithoutCaseNo", ex.Message, ex.StackTrace, ex.Source, "CaseService/CaseRegistrationsController/UpdateCaseNoForWithoutCaseNo");
                return new CaseRegistrationsResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<CaseRegistrationsResponseModel> GetCaseDataByCaseId(long CaseId)
        {
            try
            {
                if (CaseId == 0)
                {
                    return new CaseRegistrationsResponseModel()
                    {
                        Status = false,
                        Message = "Case id can not be zero."
                    };
                }
                return await unitOfWork.CaseRegistrations.GetCaseRegistrationDataByCaseId(CaseId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetCaseDataByCaseId", ex.Message, ex.StackTrace, ex.Source, "CaseService/CaseRegistrationsController/GetCaseDataByCaseId");
                return new CaseRegistrationsResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<CaseRegistrationsResponseModel> CheckCaseEntry(CheckCaseEntryModel objModel)
        {
            try
            {
                return await unitOfWork.CaseRegistrations.CheckCaseEntry(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "CheckCaseEntry", ex.Message, ex.StackTrace, ex.Source, "CaseService/CaseRegistrationsController/CheckCaseEntry");
                return new CaseRegistrationsResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> GetLinkCaseList(long LinkCaseId)
        {
            try
            {
                if (LinkCaseId == 0)
                {
                    return new ResponseWithoutPaginationModel()
                    {
                        Status = false,
                        Message = "LinkCaseId can not be zero."
                    };
                }
                return await unitOfWork.CaseRegistrations.GetLinkCaseList(LinkCaseId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetLinkCaseList", ex.Message, ex.StackTrace, ex.Source, "CaseService/CaseRegistrationsController/GetLinkCaseList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        // Add sandeep 25/07/2025

        [HttpPost]
        public async Task<ResponseModel> GetCaseRegistrationGovtEmpList(CaseRegistrationGovtEmpListFilterModel objModel)
        {
            try
            {
                return await unitOfWork.CaseRegistrations.GetCaseRegistrationGovtEmpList(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetCaseRegistrationGovtEmpList", ex.Message, ex.StackTrace, ex.Source, "CaseService/CaseRegistrationsController/GetCaseRegistrationGovtEmpList");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> AddEditCaseRegistrationGovtEmp(CaseRegistrationGovtEmpModel objModel)
        {
            try
            {
                return await unitOfWork.CaseRegistrations.AddEditCaseRegistrationGovtEmp(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditCaseRegistrationGovtEmp", ex.Message, ex.StackTrace, ex.Source, "CaseService/CaseRegistrationsController/AddEditCaseRegistrationGovtEmp");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> DeactiveCaseRegistrationGovtEmp(int CRGEId)
        {
            try
            {
                return await unitOfWork.CaseRegistrations.DeactiveCaseRegistrationGovtEmp(CRGEId, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DeactiveCaseRegistrationGovtEmp", ex.Message, ex.StackTrace, ex.Source, "CaseService/CaseRegistrationsController/DeactiveCaseRegistrationGovtEmp");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        // Add sandeep 25/07/2025
    }
}