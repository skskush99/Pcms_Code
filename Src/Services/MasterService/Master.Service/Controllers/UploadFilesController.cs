using Master.Dto.UploadFiles;
using Master.Dto.Shared;
using Master.ServiceBus.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Master.Service.Middleware;
using Common.Repository;

namespace Master.Service.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UploadFilesController : ControllerBase
    {
        private readonly IUnitOfWorkService unitOfWork;
        private readonly LogsService _logsService;
        public UploadFilesController(IUnitOfWorkService unitOfWorkService, LogsService logsService)
        {
            unitOfWork = unitOfWorkService;
            _logsService = logsService;
        }

        [HttpGet]
        public async Task<ResponseWithoutPaginationModel> GetUploadFileCategoryList()
        {
            try
            {
                return await unitOfWork.UploadFiles.GetUploadFileCategoryList();
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetUploadFileCategoryList", ex.Message, ex.StackTrace, ex.Source, "MasterService/UploadFilesController/GetUploadFileCategoryList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseModel> GetUploadFilesList(UploadFilesFIlterModel objModel)
        {
            try
            {
                return await unitOfWork.UploadFiles.GetUploadFilesList(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetUploadFilesList", ex.Message, ex.StackTrace, ex.Source, "MasterService/UploadFilesController/GetUploadFilesList");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> UploadFile(IFormFile SingleFile, [FromForm] UploadFilesAddModel objModel)
        {
            try
            {
                if (SingleFile == null || SingleFile.Length == 0)
                {
                    ModelState.AddModelError("", "File not selected");
                    return new ResponseWithoutPaginationModel()
                    {
                        Status = false,
                        Message = "File not selected"
                    };
                }
                var extension = Path.GetExtension(SingleFile.FileName).ToLowerInvariant();

                //Validating the File Size
                if (SingleFile.Length > 20000000) // Limit to 20 MB
                {
                    return new ResponseWithoutPaginationModel()
                    {
                        Status = false,
                        Message = "File size must be less then 20 MB."
                    };
                }
                string fileName = Convert.ToString(objModel.FilesName).Replace(" ", "-") + "-" + DateTime.Now.ToString("ddMMyyyyhhmmss") + extension;
                string folderName = "Uploads/" + Convert.ToString(objModel.CategoryName).Replace(" ", "") + "/";

                string filePath = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);

                var filePathWithName = Path.Combine(Directory.GetCurrentDirectory(), filePath, fileName);
                using (var stream = new FileStream(filePathWithName, FileMode.Create, FileAccess.Write))
                {
                    await SingleFile.CopyToAsync(stream);
                }

                var objData = new UploadFilesModel()
                {
                    CategoryId = objModel.CategoryId,
                    FilesName = objModel.FilesName,
                    FilesPath = folderName + fileName,
                    DisplayOrder = objModel.DisplayOrder,
                    StartDate = objModel.StartDate,
                    EndDate = objModel.EndDate

                };
                return await unitOfWork.UploadFiles.UploadFile(objData, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "UploadFile", ex.Message, ex.StackTrace, ex.Source, "MasterService/UploadFilesController/UploadFile");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> DeleteFile(int FileId)
        {
            try
            {
                return await unitOfWork.UploadFiles.DeleteFile(FileId, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DeleteFile", ex.Message, ex.StackTrace, ex.Source, "MasterService/UploadFilesController/DeleteFile");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseModel> GetUserManualList(UserManualFIlterModel objModel)
        {
            try
            {
                var loginUserData = UserSession.Current;
                if (loginUserData.RoleId > 1)
                   objModel.RoleId = loginUserData.RoleId;
                    
                return await unitOfWork.UploadFiles.GetUserManualList(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetUserManualList", ex.Message, ex.StackTrace, ex.Source, "MasterService/UploadFilesController/GetUserManualList");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        [RequestSizeLimit(52428800)]
        public async Task<ResponseWithoutPaginationModel> UploadUserManual(IFormFile SingleFile, [FromForm] UserManualModel objModel)
        {
            try
            {
                if (SingleFile == null)
                {
                    ModelState.AddModelError("", "File not selected");
                    return new ResponseWithoutPaginationModel()
                    {
                        Status = false,
                        Message = "File not selected"
                    };
                }
                var extension = Path.GetExtension(SingleFile.FileName).ToLowerInvariant();

                string fileName = Convert.ToString(objModel.FilesName).Replace(" ", "-") + "_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + extension;
                string folderName = "Uploads/UserManual/" ;

                string filePath = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);

                var filePathWithName = Path.Combine(Directory.GetCurrentDirectory(), filePath, fileName);
                using (var stream = new FileStream(filePathWithName, FileMode.Create, FileAccess.Write))
                {
                    await SingleFile.CopyToAsync(stream);
                }


                var objData = new UserManualAddEditModel()
                {
                    Id = objModel.Id,
                    RoleId = objModel.RoleId,
                    FilesName = objModel.FilesName,
                    FilesPath = folderName + fileName,
                    
                };
                return await unitOfWork.UploadFiles.UploadUserManual(objData, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "UploadUserManual", ex.Message, ex.StackTrace, ex.Source, "MasterService/UploadFilesController/UploadUserManual");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> DeleteUserManual(int Id)
        {
            try
            {
                return await unitOfWork.UploadFiles.DeleteUserManual(Id, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DeleteUserManual", ex.Message, ex.StackTrace, ex.Source, "MasterService/UploadFilesController/DeleteUserManual");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }


    }
}
