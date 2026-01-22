using Case.Dto.CaseFileRegister;
using Case.Dto.Shared;
using Case.ServiceBus.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CaseService.Middleware;
using Common.Repository;
using Core.Enums.User;
using System.ServiceModel.Channels;
namespace CaseService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class RegisterCaseFileController : ControllerBase
    {
        private readonly LogsService _logsService;
        private readonly IUnitOfWorkService unitOfWork;
        public RegisterCaseFileController(IUnitOfWorkService unitOfWorkService, LogsService logsService)
        {
            _logsService = logsService;
            unitOfWork = unitOfWorkService;
        }

        [HttpPost]
        public async Task<ResponseModel> GetCaseFileRegisterList(CaseFileRegisterFilterModel objModel)
        {
            try
            {
                var loginUserData = UserSession.Current;
                if (loginUserData.DepartmentId > 0)
                    objModel.AdmDepttId = loginUserData.DepartmentId;
                return await unitOfWork.CaseFileRegisterServiceBus.GetCaseFileRegisterList(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetCaseFileRegisterList", ex.Message, ex.StackTrace, ex.Source, "CaseService/CaseFileRegisterController/GetCaseFileRegisterList");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> GetLawDeptFileNoCount(CaseFileRegisterCountFilterModel objModel)
        {
            try
            {
                return await unitOfWork.CaseFileRegisterServiceBus.GetLawDeptFileNoCount(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetLawDeptFileNoCount", ex.Message, ex.StackTrace, ex.Source, "CaseService/CaseFileRegisterController/GetLawDeptFileNoCount");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> AddEditCaseFileRegister(CaseFileRegisterModel objModel)
        {
            try
            {
                return await unitOfWork.CaseFileRegisterServiceBus.AddEditCaseFileRegister(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditCaseFileRegister", ex.Message, ex.StackTrace, ex.Source, "CaseService/CaseFileRegisterController/AddEditCaseFileRegister");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseModel> GetConnectedCaseList(ConnectedCaseFilterModel objModel)
        {
            try
            {
                var loginUserData = UserSession.Current;
                if (loginUserData.DepartmentId > 0)
                    objModel.AdmDepttId = loginUserData.DepartmentId;
                return await unitOfWork.CaseFileRegisterServiceBus.GetConnectedCaseList(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetConnectedCaseList", ex.Message, ex.StackTrace, ex.Source, "CaseService/CaseFileRegisterController/GetConnectedCaseList");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> AddEditConnectedCase(CaseFileRegisterModel objModel)
        {
            try
            {
                return await unitOfWork.CaseFileRegisterServiceBus.AddEditConnectedCase(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditConnectedCase", ex.Message, ex.StackTrace, ex.Source, "CaseService/CaseFileRegisterController/AddEditConnectedCase");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> DeleteConnectedCase(int CaseFileRegistorId)
        {
            try
            {
                return await unitOfWork.CaseFileRegisterServiceBus.DeleteConnectedCase(CaseFileRegistorId, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DeleteConnectedCase", ex.Message, ex.StackTrace, ex.Source, "CaseService/CaseFileRegisterController/DeleteConnectedCase");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> GetConnectedCaseListByCaseFileRegistorId(int CaseFileRegistorId)
        {
            try
            {
                return await unitOfWork.CaseFileRegisterServiceBus.GetConnectedCaseListByCaseFileRegistorId(CaseFileRegistorId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetConnectedCaseListByCaseFileRegistorId", ex.Message, ex.StackTrace, ex.Source, "CaseService/CaseFileRegisterController/GetConnectedCaseListByCaseFileRegistorId");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseModel> GetUploadDocumentList(int PageNo, int PageSize)
        {
            try
            {
                return await unitOfWork.CaseFileRegisterServiceBus.GetUploadDocumentList(PageNo, PageSize);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetUploadDocumentList", ex.Message, ex.StackTrace, ex.Source, "CaseService/CaseFileRegisterController/GetUploadDocumentList");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        [RequestSizeLimit(52428800)]
        public async Task<ResponseWithoutPaginationModel> AddUploadDocument(IFormFile SelectFile, [FromForm] AddCaseFileRegisterUploadDocumentModel objModel)
        {
            try
            {
                if (SelectFile == null || SelectFile.Length == 0)
                {
                    ModelState.AddModelError("", "File not selected");
                    return new ResponseWithoutPaginationModel()
                    {
                        Status = false,
                        Message = "File not selected"
                    };
                }
                if (Path.GetFileNameWithoutExtension(SelectFile.FileName).Contains("."))
                {
                    return new ResponseWithoutPaginationModel()
                    {
                        Status = false,
                        Message = "Please enter a filename without any dots (e.g., 'myfile' instead of 'myfile.xyz'."
                    };
                }
                var extension = Path.GetExtension(SelectFile.FileName).ToLowerInvariant();

                var permittedExtensions = new[] { ".pdf" };
                if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
                {
                    return new ResponseWithoutPaginationModel()
                    {
                        Status = false,
                        Message = "Please upload only PDF file."
                    };
                }

                //Validating the File Size
                //if (SelectFile.Length > 5000000) // Limit to 5 MB
                if (SelectFile.Length > 52428800) // Limit to 5 MB  (1,048,576 bytes in 1 MB)
                {
                    return new ResponseWithoutPaginationModel()
                    {
                        Status = false,
                        Message = "File size must be less then 25 MB."
                    };
                }

                string CaseFileRegistor = "CaseFileRegistor";
                int CaseFileRegistorId = objModel.CaseFileRegistorId;

                //string fileName = Convert.ToString(objModel.DocumentName).Replace(" ", "-") + CaseFileRegistorId + "_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + extension;
                string fileName = CaseFileRegistor + CaseFileRegistorId + "_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + extension;

                string folderName = "Uploads/CaseFileRegistor/";

                string filePath = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);

                var filePathWithName = Path.Combine(Directory.GetCurrentDirectory(), filePath, fileName);
                using (var stream = new FileStream(filePathWithName, FileMode.Create, FileAccess.Write))
                {
                    await SelectFile.CopyToAsync(stream);
                }

                //objModel.DocumentFile = folderName + fileName;
                objModel.DocumentFile = fileName;

                return await unitOfWork.CaseFileRegisterServiceBus.AddUploadDocument(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddUploadDocument", ex.Message, ex.StackTrace, ex.Source, "CaseService/CaseFileRegisterController/AddUploadDocument");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }

        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> DeactiveUploadDocument(DeactiveCaseFileRegisterUploadDocumentModel objModel)
        {
            try
            {
                return await unitOfWork.CaseFileRegisterServiceBus.DeactiveUploadDocument(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DeactiveUploadDocument", ex.Message, ex.StackTrace, ex.Source, "CaseService/CaseFileRegisterController/DeactiveUploadDocument");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }


    }

}

