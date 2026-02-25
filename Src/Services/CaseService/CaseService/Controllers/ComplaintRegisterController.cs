using Case.Dto.ComplaintRegister;
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
    public class ComplaintRegisterController : ControllerBase
    {
        private readonly LogsService _logsService;
        private readonly IUnitOfWorkService unitOfWork;
        public ComplaintRegisterController(IUnitOfWorkService unitOfWorkService, LogsService logsService)
        {
            _logsService = logsService;
            unitOfWork = unitOfWorkService;
        }
        [HttpPost]
        public async Task<ResponseModel> GetComplaintList(ComplaintListFilterModel objModel)
        {
            try
            {
                var loginUserData = UserSession.Current;
                return await unitOfWork.ComplaintRegisterService.GetComplaintList(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetComplaintList", ex.Message, ex.StackTrace, ex.Source, "CaseService/ComplaintRegisterController/GetComplaintList");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        [RequestSizeLimit(52428800)]
        public async Task<ComplaintRegisterResponseModel> AddEditComplaintRegister(IFormFile? SelectFile, IFormFile? SelectFile1, IFormFile? SelectFile2, [FromForm] ComplaintRegisterModel objModel)
        {
            try
            {
                //  Complaint First Page Docs Upload
                if (SelectFile != null && SelectFile.Length != 0)
                {
                    if (Path.GetFileNameWithoutExtension(SelectFile.FileName).Contains("."))
                    {
                        return new ComplaintRegisterResponseModel()
                        {
                            Status = false,
                            Message = "Please enter a filename without any dots (e.g., 'Uploadfile' instead of 'Uploadfile.xyz' )."
                        };
                    }
                    var extension = Path.GetExtension(SelectFile.FileName).ToLowerInvariant();

                    var permittedExtensions = new[] { ".pdf" };
                    if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
                    {
                        return new ComplaintRegisterResponseModel()
                        {
                            Status = false,
                            Message = "Kindly upload documents in PDF format only."
                        };
                    }

                    //Validate that the file size does not exceed 10 MB.
                    if (SelectFile.Length > 10485760)       // Limit upto 10 MB  (1,048,576 bytes in 1 MB)  
                    {
                        return new ComplaintRegisterResponseModel()
                        {
                            Status = false,
                            Message = "File size is too large. Maximum allowed size is 10 MB."
                        };
                    }

                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(SelectFile.FileName);
                    string fileName = Convert.ToString(fileNameWithoutExtension).Replace(" ", "-") + DateTime.Now.ToString("ddMMyyyyhhmmss") + extension;
                    string folderName = "Uploads/ComplaintRegister/";

                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                    if (!Directory.Exists(filePath))
                        Directory.CreateDirectory(filePath);

                    var filePathWithName = Path.Combine(Directory.GetCurrentDirectory(), filePath, fileName);
                    using (var stream = new FileStream(filePathWithName, FileMode.Create, FileAccess.Write))
                    {
                        await SelectFile.CopyToAsync(stream);
                    }

                    objModel.ComplaintFirstPageDocs = folderName + fileName;
                }

                // Full Complaint Docs Upload

                if (SelectFile1 != null && SelectFile1.Length != 0)
                {
                    if (Path.GetFileNameWithoutExtension(SelectFile1.FileName).Contains("."))
                    {
                        return new ComplaintRegisterResponseModel()
                        {
                            Status = false,
                            Message = "Please enter a filename without any dots (e.g., 'Uploadfile' instead of 'Uploadfile.xyz' )."
                        };
                    }
                    var extension = Path.GetExtension(SelectFile1.FileName).ToLowerInvariant();

                    var permittedExtensions = new[] { ".pdf" };
                    if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
                    {
                        return new ComplaintRegisterResponseModel()
                        {
                            Status = false,
                            Message = "Kindly upload documents in PDF format only."
                        };
                    }

                    //Validate that the file size does not exceed 10 MB.
                    if (SelectFile1.Length > 10485760)       // Limit upto 10 MB  (1,048,576 bytes in 1 MB)  
                    {
                        return new ComplaintRegisterResponseModel()
                        {
                            Status = false,
                            Message = "File size is too large. Maximum allowed size is 10 MB."
                        };
                    }

                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(SelectFile1.FileName);
                    string fileName = Convert.ToString(fileNameWithoutExtension).Replace(" ", "-") + DateTime.Now.ToString("ddMMyyyyhhmmss") + extension;
                    string folderName = "Uploads/ComplaintRegister/";

                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                    if (!Directory.Exists(filePath))
                        Directory.CreateDirectory(filePath);

                    var filePathWithName = Path.Combine(Directory.GetCurrentDirectory(), filePath, fileName);
                    using (var stream = new FileStream(filePathWithName, FileMode.Create, FileAccess.Write))
                    {
                        await SelectFile1.CopyToAsync(stream);
                    }

                    objModel.FullComplaintDocs = folderName + fileName;
                }

                // Other Docs Upload
                if (SelectFile2 != null && SelectFile2.Length != 0)
                {
                    if (Path.GetFileNameWithoutExtension(SelectFile2.FileName).Contains("."))
                    {
                        return new ComplaintRegisterResponseModel()
                        {
                            Status = false,
                            Message = "Please enter a filename without any dots (e.g., 'Uploadfile' instead of 'Uploadfile.xyz' )."
                        };
                    }
                    var extension = Path.GetExtension(SelectFile2.FileName).ToLowerInvariant();

                    var permittedExtensions = new[] { ".pdf" };
                    if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
                    {
                        return new ComplaintRegisterResponseModel()
                        {
                            Status = false,
                            Message = "Kindly upload documents in PDF format only."
                        };
                    }

                    //Validate that the file size does not exceed 10 MB.
                    if (SelectFile2.Length > 10485760)       // Limit upto 10 MB  (1,048,576 bytes in 1 MB)  
                    {
                        return new ComplaintRegisterResponseModel()
                        {
                            Status = false,
                            Message = "File size is too large. Maximum allowed size is 10 MB."
                        };
                    }

                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(SelectFile2.FileName);
                    string fileName = Convert.ToString(fileNameWithoutExtension).Replace(" ", "-") + DateTime.Now.ToString("ddMMyyyyhhmmss") + extension;
                    string folderName = "Uploads/ComplaintRegister/";

                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                    if (!Directory.Exists(filePath))
                        Directory.CreateDirectory(filePath);

                    var filePathWithName = Path.Combine(Directory.GetCurrentDirectory(), filePath, fileName);
                    using (var stream = new FileStream(filePathWithName, FileMode.Create, FileAccess.Write))
                    {
                        await SelectFile2.CopyToAsync(stream);
                    }

                    objModel.OtherDocs = folderName + fileName;
                }

                return await unitOfWork.ComplaintRegisterService.AddEditComplaintRegister(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditComplaintRegister", ex.Message, ex.StackTrace, ex.Source, "CaseService/ComplaintRegisterController/AddEditComplaintRegister");
                return new ComplaintRegisterResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }


        [HttpGet]
        public async Task<ResponseWithoutPaginationModel> GetPersonAgainstDetails(long ComplaintRegId)
        {
            try
            {
                return await unitOfWork.ComplaintRegisterService.GetPersonAgainstDetails(ComplaintRegId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetPersonAgainstDetails", ex.Message, ex.StackTrace, ex.Source, "CaseService/ComplaintRegisterController/GetPersonAgainstDetails");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        [HttpPost]
        public async Task<ComplaintRegisterResponseModel> AddEditPersonAgainstDetails(PersonAgainstDetailsModel objModel)
        {
            try
            {
                return await unitOfWork.ComplaintRegisterService.AddEditPersonAgainstDetails(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditPersonAgainstDetails", ex.Message, ex.StackTrace, ex.Source, "CaseService/ComplaintRegisterController/AddEditPersonAgainstDetails");
                return new ComplaintRegisterResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        [HttpPost]
        public async Task<ComplaintRegisterResponseModel> DeletePersonAgainstDetails(int PersonAgainstId)
        {
            try
            {
                return await unitOfWork.ComplaintRegisterService.DeletePersonAgainstDetails(PersonAgainstId, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DeletePersonAgainstDetails", ex.Message, ex.StackTrace, ex.Source, "CaseService/ComplaintRegisterController/DeletePersonAgainstDetails");
                return new ComplaintRegisterResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
