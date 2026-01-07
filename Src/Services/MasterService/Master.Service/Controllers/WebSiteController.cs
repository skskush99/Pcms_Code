using Master.Dto.WebSite;
using Master.Dto.Shared;
using Master.ServiceBus.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Master.Service.Middleware;
using Common.Repository;
using System.Linq;

namespace Master.Service.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class WebSiteController : ControllerBase
    {
        private readonly IUnitOfWorkService unitOfWork;
        private readonly LogsService _logsService;
        public WebSiteController(IUnitOfWorkService unitOfWorkService, LogsService logsService)
        {
            unitOfWork = unitOfWorkService;
            _logsService = logsService;
        }

        [HttpPost]
        public async Task<ResponseModel> GetWebSiteUploadFilesList(WebSitesFIlterModel objModel)
        {
            try
            {
                return await unitOfWork.WebSite.GetWebSiteUploadFilesList(objModel);
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
        [RequestSizeLimit(52428800)]
        public async Task<ResponseWithoutPaginationModel> WebSiteUploadFile(IFormFile SelectFile, [FromForm] WebSitesModel objModel)
        {
            try
            {
                if (SelectFile != null)
                {
                    if (Path.GetFileNameWithoutExtension(SelectFile.FileName).Contains("."))
                    {
                        return new ResponseWithoutPaginationModel()
                        {
                            Status = false,
                            Message = "Please enter a filename without any dots (e.g., 'myfile' instead of 'myfile.xyz'."
                        };
                    }

                    var extension = Path.GetExtension(SelectFile.FileName).ToLowerInvariant();

                    var permittedExtensions = new[] { ".pdf", ".jpg", ".jpeg" }; 
                    if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
                    {
                        return new ResponseWithoutPaginationModel()
                        {
                            Status = false,
                            Message = "Please upload only PDF, JPG & JPEG file."
                        };
                    }

                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(SelectFile.FileName);
                    string fileName = Convert.ToString(fileNameWithoutExtension).Replace(" ", "-") + DateTime.Now.ToString("ddMMyyyyhhmmss") + extension;
                    string folderName = "Uploads/WebSiteUploadFile/";

                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                    if (!Directory.Exists(filePath))
                        Directory.CreateDirectory(filePath);

                    var filePathWithName = Path.Combine(Directory.GetCurrentDirectory(), filePath, fileName);
                    using (var stream = new FileStream(filePathWithName, FileMode.Create, FileAccess.Write))
                    {
                        await SelectFile.CopyToAsync(stream);
                    }

                    objModel.ImagePath = folderName + fileName;
                }
                return await unitOfWork.WebSite.WebSiteUploadFile(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "WebSiteUploadFile", ex.Message, ex.StackTrace, ex.Source, "MasterService/WebSiteController/WebSiteUploadFile");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> WebSiteContact(WebSitesContactAddModel objModel)
        {
            try
            {
                return await unitOfWork.WebSite.WebSiteContact(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "WebSiteContact", ex.Message, ex.StackTrace, ex.Source, "MasterService/UploadFilesController/WebSiteContact");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> WebSiteActiveDeActiveFile(int Id, int Active)
        {
            try
            {
                return await unitOfWork.WebSite.WebSiteActiveDeActiveFile(Id, Active, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "WebSiteActiveDeActiveFile", ex.Message, ex.StackTrace, ex.Source, "MasterService/UploadFilesController/WebSiteActiveDeActiveFile");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

   
    }
}
