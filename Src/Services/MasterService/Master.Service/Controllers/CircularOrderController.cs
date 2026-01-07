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
    public class CircularOrderController : ControllerBase
    {
        private readonly LogsService _logsService;
        private readonly IUnitOfWorkService _IUnitOfWorkService;

        public CircularOrderController(IUnitOfWorkService unitOfWorkService, LogsService logsService)
        {
            _IUnitOfWorkService = unitOfWorkService;
            _logsService = logsService;
        }

        [HttpPost]
        public async Task<ResponseModel> GetCircularOrders(CircularOrderFilterModel objModel)
        {
            try
            {
                return await _IUnitOfWorkService.CircularOrderServiceBus.GetCircularOrders(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetCircularOrders", ex.Message, ex.StackTrace, ex.Source, "MasterService/CircularOrderController/GetCircularOrders");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> AddEditCircularOrder(IFormFile SelectFile, [FromForm] CircularOrderAddModel objModel)
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
                if (SelectFile.Length > 5242880) // Limit to 5 MB  (1,048,576 bytes in 1 MB)
                {
                    return new ResponseWithoutPaginationModel()
                    {
                        Status = false,
                        Message = "File size must be less then 5 MB."
                    };
                }
                string fileName = Convert.ToString(objModel.Title).Replace(" ", "-") + "-" + DateTime.Now.ToString("ddMMyyyyhhmmss") + extension;
                string folderName = "Uploads/CircularOrder/";

                string filePath = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);

                var filePathWithName = Path.Combine(Directory.GetCurrentDirectory(), filePath, fileName);
                using (var stream = new FileStream(filePathWithName, FileMode.Create, FileAccess.Write))
                {
                    await SelectFile.CopyToAsync(stream);
                }

                var objData = new CircularOrderModel()
                {
                    Id = objModel.Id,
                    Title = objModel.Title,
                    //FilesName = objModel.FilePath,
                    FilePath = folderName + fileName,
                };

                //var UserId = UserSession.Current.UserId;
                //objData.UploadedBy = UserId;
                //objData.UpdatedBy = UserId;
                return await _IUnitOfWorkService.CircularOrderServiceBus.AddEditCircularOrder(objData, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditCircularOrder", ex.Message, ex.StackTrace, ex.Source, "MasterService/CircularOrderController/AddEditCircularOrder");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }

        }

        [HttpPost]
        public async Task<ResponseModel> ActiveDeactiveCircularOrder(CircularOrderActiveDeactiveModel objModel)
        {
            try
            {
                var UserId = UserSession.Current.UserId;
                objModel.UpdatedBy = UserId;
                return await _IUnitOfWorkService.CircularOrderServiceBus.ActiveDeactiveCircularOrder(objModel, UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "ActiveDeactiveCircularOrder", ex.Message, ex.StackTrace, ex.Source, "MasterService/CircularOrderController/ActiveDeactiveCircularOrder");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }



    }
}
