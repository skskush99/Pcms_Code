using Case.Dto.DierRegistrations_New;
using Case.Dto.Shared;
using Case.ServiceBus.UnitOfWork;
using CaseService.Middleware;
using Common.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CaseService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class DierRegistrations_NewController : Controller
    {
        private readonly LogsService _logsService;
        private readonly IUnitOfWorkService unitOfWork;
        public DierRegistrations_NewController(IUnitOfWorkService unitOfWorkService, LogsService logsService)
        {
            _logsService = logsService;
            unitOfWork = unitOfWorkService;
        }

        [HttpPost]
        public async Task<ResponseModel> GetDierList(Dier_NewListFilterModel objModel)
        {
            try
            {
                var loginUserData = UserSession.Current;
                return await unitOfWork.DierRegistrations_NewService.GetDierList(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetDierList", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrations_NewController/GetDierList");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<DierRegistrations_NewResponseModel> AddEditDierRegistrationsSteps1(DierRegistrations_NewSteps1Model objModel)
        {
            try
            {
                return await unitOfWork.DierRegistrations_NewService.AddEditDierRegistrationsSteps1(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditDierRegistrationsSteps1", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrations_NewController/AddEditDierRegistrationsSteps1");
                return new DierRegistrations_NewResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<DierRegistrations_NewResponseModel> AddEditDierRegistrationsSteps2(DierRegistrations_NewSteps2Model objModel)
        {
            try
            {
                return await unitOfWork.DierRegistrations_NewService.AddEditDierRegistrationsSteps2(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditDierRegistrationsSteps2", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrations_NewController/AddEditDierRegistrationsSteps2");
                return new DierRegistrations_NewResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<DierRegistrations_NewResponseModel> AddEditCompleteDierRegistrationsSteps2(DierRegistrations_NewSteps2Model objModel)
        {
            try
            {
                return await unitOfWork.DierRegistrations_NewService.AddEditCompleteDierRegistrationsSteps2(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditCompleteDierRegistrationsSteps2", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrations_NewController/AddEditCompleteDierRegistrationsSteps2");
                return new DierRegistrations_NewResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<DierRegistrations_NewResponseModel> AddEditDierRegistrationsSteps3(DierRegistrations_NewSteps3Model objModel)
        {
            try
            {
                return await unitOfWork.DierRegistrations_NewService.AddEditDierRegistrationsSteps3(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditDierRegistrationsSteps3", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrations_NewController/AddEditDierRegistrationsSteps3");
                return new DierRegistrations_NewResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<DierRegistrations_NewResponseModel> AddEditCompleteDierRegistrationsStep3(DierRegistrations_NewSteps3Model objModel)
        {
            try
            {
                // ================= FILE UPLOAD FROM MODEL =================
                if (objModel.DierAccusedModel != null)
                {
                    foreach (var accused in objModel.DierAccusedModel)
                    {
                        var file = await UploadFile(accused.SanctionFile, "Uploads/DierAccused/");
                        if (!file.Status) return Error(file.Message);

                        accused.SanctionDocs = file.FilePath;
                    }
                }
                return await unitOfWork.DierRegistrations_NewService.AddEditCompleteDierRegistrationsStep3(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditCompleteDierRegistrationsStep3", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrations_NewController/AddEditCompleteDierRegistrationsStep3");
                return new DierRegistrations_NewResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        [RequestSizeLimit(52428800)]
        public async Task<DierRegistrations_NewResponseModel> AddEditDierRegistrationsSteps4(IFormFile? SelectFile, IFormFile? SelectFile1, IFormFile? SelectFile2, [FromForm] DierRegistrations_NewSteps4Model objModel)
        {
            try
            {
                //  Charge Sheet Docs Upload
                if (SelectFile != null && SelectFile.Length != 0)
                {
                    if (Path.GetFileNameWithoutExtension(SelectFile.FileName).Contains("."))
                    {
                        return new DierRegistrations_NewResponseModel()
                        {
                            Status = false,
                            Message = "Please enter a filename without any dots (e.g., 'Uploadfile' instead of 'Uploadfile.xyz' )."
                        };
                    }
                    var extension = Path.GetExtension(SelectFile.FileName).ToLowerInvariant();

                    var permittedExtensions = new[] { ".pdf" };
                    if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
                    {
                        return new DierRegistrations_NewResponseModel()
                        {
                            Status = false,
                            Message = "Kindly upload documents in PDF format only."
                        };
                    }

                    //Validate that the file size does not exceed 10 MB.
                    if (SelectFile.Length > 10485760)       // Limit upto 10 MB  (1,048,576 bytes in 1 MB)  
                    {
                        return new DierRegistrations_NewResponseModel()
                        {
                            Status = false,
                            Message = "File size is too large. Maximum allowed size is 10 MB."
                        };
                    }

                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(SelectFile.FileName);
                    string fileName = Convert.ToString(fileNameWithoutExtension).Replace(" ", "-") + DateTime.Now.ToString("ddMMyyyyhhmmss") + extension;
                    string folderName = "Uploads/DierRegistrations/";

                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                    if (!Directory.Exists(filePath))
                        Directory.CreateDirectory(filePath);

                    var filePathWithName = Path.Combine(Directory.GetCurrentDirectory(), filePath, fileName);
                    using (var stream = new FileStream(filePathWithName, FileMode.Create, FileAccess.Write))
                    {
                        await SelectFile.CopyToAsync(stream);
                    }

                    objModel.ChargeSheetDocs = folderName + fileName;
                }

                // Full Charge Sheet Docs Upload

                if (SelectFile1 != null && SelectFile1.Length != 0)
                {
                    if (Path.GetFileNameWithoutExtension(SelectFile1.FileName).Contains("."))
                    {
                        return new DierRegistrations_NewResponseModel()
                        {
                            Status = false,
                            Message = "Please enter a filename without any dots (e.g., 'Uploadfile' instead of 'Uploadfile.xyz' )."
                        };
                    }
                    var extension = Path.GetExtension(SelectFile1.FileName).ToLowerInvariant();

                    var permittedExtensions = new[] { ".pdf" };
                    if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
                    {
                        return new DierRegistrations_NewResponseModel()
                        {
                            Status = false,
                            Message = "Kindly upload documents in PDF format only."
                        };
                    }

                    //Validate that the file size does not exceed 10 MB.
                    if (SelectFile1.Length > 10485760)       // Limit upto 10 MB  (1,048,576 bytes in 1 MB)  
                    {
                        return new DierRegistrations_NewResponseModel()
                        {
                            Status = false,
                            Message = "File size is too large. Maximum allowed size is 10 MB."
                        };
                    }

                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(SelectFile1.FileName);
                    string fileName = Convert.ToString(fileNameWithoutExtension).Replace(" ", "-") + DateTime.Now.ToString("ddMMyyyyhhmmss") + extension;
                    string folderName = "Uploads/DierRegistrations/";

                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                    if (!Directory.Exists(filePath))
                        Directory.CreateDirectory(filePath);

                    var filePathWithName = Path.Combine(Directory.GetCurrentDirectory(), filePath, fileName);
                    using (var stream = new FileStream(filePathWithName, FileMode.Create, FileAccess.Write))
                    {
                        await SelectFile1.CopyToAsync(stream);
                    }

                    objModel.FullChargeSheetDocs = folderName + fileName;
                }

                // Other Docs Upload
                if (SelectFile2 != null && SelectFile2.Length != 0)
                {
                    if (Path.GetFileNameWithoutExtension(SelectFile2.FileName).Contains("."))
                    {
                        return new DierRegistrations_NewResponseModel()
                        {
                            Status = false,
                            Message = "Please enter a filename without any dots (e.g., 'Uploadfile' instead of 'Uploadfile.xyz' )."
                        };
                    }
                    var extension = Path.GetExtension(SelectFile2.FileName).ToLowerInvariant();

                    var permittedExtensions = new[] { ".pdf" };
                    if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
                    {
                        return new DierRegistrations_NewResponseModel()
                        {
                            Status = false,
                            Message = "Kindly upload documents in PDF format only."
                        };
                    }

                    //Validate that the file size does not exceed 10 MB.
                    if (SelectFile2.Length > 10485760)       // Limit upto 10 MB  (1,048,576 bytes in 1 MB)  
                    {
                        return new DierRegistrations_NewResponseModel()
                        {
                            Status = false,
                            Message = "File size is too large. Maximum allowed size is 10 MB."
                        };
                    }

                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(SelectFile2.FileName);
                    string fileName = Convert.ToString(fileNameWithoutExtension).Replace(" ", "-") + DateTime.Now.ToString("ddMMyyyyhhmmss") + extension;
                    string folderName = "Uploads/DierRegistrations/";

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

                return await unitOfWork.DierRegistrations_NewService.AddEditDierRegistrationsSteps4(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditDierRegistrationsSteps4", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrations_NewController/AddEditDierRegistrationsSteps4");
                return new DierRegistrations_NewResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        [RequestSizeLimit(52428800)]
        public async Task<DierRegistrations_NewResponseModel> AddEditCompleteDierRegistrationsFinal(IFormFile? SelectFile, IFormFile? SelectFile1, IFormFile? SelectFile2, [FromForm] DierRegistrations_NewModel objModel)
        {
            try
            {
                // ================= STEP 4 FILES =================

                var chargeSheet = await UploadFile(SelectFile, "Uploads/DierRegistrations/");
                if (!chargeSheet.Status) return Error(chargeSheet.Message);
                objModel.ChargeSheetDocs = chargeSheet.FilePath;

                var fullChargeSheet = await UploadFile(SelectFile1, "Uploads/DierRegistrations/");
                if (!fullChargeSheet.Status) return Error(fullChargeSheet.Message);
                objModel.FullChargeSheetDocs = fullChargeSheet.FilePath;

                var otherDocs = await UploadFile(SelectFile2, "Uploads/DierRegistrations/");
                if (!otherDocs.Status) return Error(otherDocs.Message);
                objModel.OtherDocs = otherDocs.FilePath;

                // ================= ACCUSED FILES =================
                if (objModel.DierAccusedModel != null)
                {
                    foreach (var accused in objModel.DierAccusedModel)
                    {
                        var file = await UploadFile(accused.SanctionFile, "Uploads/DierAccused/");
                        if (!file.Status) return Error(file.Message);

                        accused.SanctionDocs = file.FilePath;
                    }
                }
                // ================= SERVICE CALL =================
                return await unitOfWork.DierRegistrations_NewService.AddEditCompleteDierRegistrationsFinal(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditCompleteDierRegistrationsFinal", ex.Message, ex.StackTrace, ex.Source, "Controller/DierRegistrations_NewController/AddEditCompleteDierRegistrationsFinal");

                return Error(ex.Message);
            }
        }

        [HttpPost]
        [RequestSizeLimit(52428800)]
        public async Task<ResponseWithoutPaginationModel> AddEditDierRegistrations(IFormFile? SelectFile, IFormFile? SelectFile1, IFormFile? SelectFile2, [FromForm] DierRegistrations_New_OldModel objModel)
        {
            try
            {
                //  Charge Sheet Docs Upload
                if (SelectFile != null && SelectFile.Length != 0)
                {
                    if (Path.GetFileNameWithoutExtension(SelectFile.FileName).Contains("."))
                    {
                        return new ResponseWithoutPaginationModel()
                        {
                            Status = false,
                            Message = "Please enter a filename without any dots (e.g., 'Uploadfile' instead of 'Uploadfile.xyz' )."
                        };
                    }
                    var extension = Path.GetExtension(SelectFile.FileName).ToLowerInvariant();

                    var permittedExtensions = new[] { ".pdf" };
                    if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
                    {
                        return new ResponseWithoutPaginationModel()
                        {
                            Status = false,
                            Message = "Kindly upload documents in PDF format only."
                        };
                    }

                    //Validate that the file size does not exceed 10 MB.
                    if (SelectFile.Length > 10485760)       // Limit upto 10 MB  (1,048,576 bytes in 1 MB)  
                    {
                        return new ResponseWithoutPaginationModel()
                        {
                            Status = false,
                            Message = "File size is too large. Maximum allowed size is 10 MB."
                        };
                    }

                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(SelectFile.FileName);
                    string fileName = Convert.ToString(fileNameWithoutExtension).Replace(" ", "-") + DateTime.Now.ToString("ddMMyyyyhhmmss") + extension;
                    string folderName = "Uploads/DierRegistrations/";

                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                    if (!Directory.Exists(filePath))
                        Directory.CreateDirectory(filePath);

                    var filePathWithName = Path.Combine(Directory.GetCurrentDirectory(), filePath, fileName);
                    using (var stream = new FileStream(filePathWithName, FileMode.Create, FileAccess.Write))
                    {
                        await SelectFile.CopyToAsync(stream);
                    }

                    objModel.ChargeSheetDocs = folderName + fileName;
                }

                // Full Charge Sheet Docs Upload

                if (SelectFile1 != null && SelectFile1.Length != 0)
                {
                    if (Path.GetFileNameWithoutExtension(SelectFile1.FileName).Contains("."))
                    {
                        return new ResponseWithoutPaginationModel()
                        {
                            Status = false,
                            Message = "Please enter a filename without any dots (e.g., 'Uploadfile' instead of 'Uploadfile.xyz' )."
                        };
                    }
                    var extension = Path.GetExtension(SelectFile1.FileName).ToLowerInvariant();

                    var permittedExtensions = new[] { ".pdf" };
                    if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
                    {
                        return new ResponseWithoutPaginationModel()
                        {
                            Status = false,
                            Message = "Kindly upload documents in PDF format only."
                        };
                    }

                    //Validate that the file size does not exceed 10 MB.
                    if (SelectFile1.Length > 10485760)       // Limit upto 10 MB  (1,048,576 bytes in 1 MB)  
                    {
                        return new ResponseWithoutPaginationModel()
                        {
                            Status = false,
                            Message = "File size is too large. Maximum allowed size is 10 MB."
                        };
                    }

                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(SelectFile1.FileName);
                    string fileName = Convert.ToString(fileNameWithoutExtension).Replace(" ", "-") + DateTime.Now.ToString("ddMMyyyyhhmmss") + extension;
                    string folderName = "Uploads/DierRegistrations/";

                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                    if (!Directory.Exists(filePath))
                        Directory.CreateDirectory(filePath);

                    var filePathWithName = Path.Combine(Directory.GetCurrentDirectory(), filePath, fileName);
                    using (var stream = new FileStream(filePathWithName, FileMode.Create, FileAccess.Write))
                    {
                        await SelectFile1.CopyToAsync(stream);
                    }

                    objModel.FullChargeSheetDocs = folderName + fileName;
                }

                // Other Docs Upload
                if (SelectFile2 != null && SelectFile2.Length != 0)
                {
                    if (Path.GetFileNameWithoutExtension(SelectFile2.FileName).Contains("."))
                    {
                        return new ResponseWithoutPaginationModel()
                        {
                            Status = false,
                            Message = "Please enter a filename without any dots (e.g., 'Uploadfile' instead of 'Uploadfile.xyz' )."
                        };
                    }
                    var extension = Path.GetExtension(SelectFile2.FileName).ToLowerInvariant();

                    var permittedExtensions = new[] { ".pdf" };
                    if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
                    {
                        return new ResponseWithoutPaginationModel()
                        {
                            Status = false,
                            Message = "Kindly upload documents in PDF format only."
                        };
                    }

                    //Validate that the file size does not exceed 10 MB.
                    if (SelectFile2.Length > 10485760)       // Limit upto 10 MB  (1,048,576 bytes in 1 MB)  
                    {
                        return new ResponseWithoutPaginationModel()
                        {
                            Status = false,
                            Message = "File size is too large. Maximum allowed size is 10 MB."
                        };
                    }

                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(SelectFile2.FileName);
                    string fileName = Convert.ToString(fileNameWithoutExtension).Replace(" ", "-") + DateTime.Now.ToString("ddMMyyyyhhmmss") + extension;
                    string folderName = "Uploads/DierRegistrations/";

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

                return await unitOfWork.DierRegistrations_NewService.AddEditDierRegistrations(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditDierRegistrations", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrations_NewController/AddEditDierRegistrations");
                return new ResponseWithoutPaginationModel()
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
                return await unitOfWork.DierRegistrations_NewService.GetDierAccused(AccusedGroupNo);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetDierAccused", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrations_NewController/GetDierAccused");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        [RequestSizeLimit(52428800)]
        public async Task<ResponseWithoutPaginationModel> AddEditDierAccused(IFormFile? SelectFile, [FromForm] Dier_NewAccusedModel objModel)
        {
            try
            {
                //  Charge Sheet Docs Upload
                if (SelectFile != null && SelectFile.Length != 0)
                {
                    if (Path.GetFileNameWithoutExtension(SelectFile.FileName).Contains("."))
                    {
                        return new ResponseWithoutPaginationModel()
                        {
                            Status = false,
                            Message = "Please enter a filename without any dots (e.g., 'Uploadfile' instead of 'Uploadfile.xyz' )."
                        };
                    }
                    var extension = Path.GetExtension(SelectFile.FileName).ToLowerInvariant();

                    var permittedExtensions = new[] { ".pdf" };
                    if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
                    {
                        return new ResponseWithoutPaginationModel()
                        {
                            Status = false,
                            Message = "Kindly upload documents in PDF format only."
                        };
                    }

                    //Validate that the file size does not exceed 10 MB.
                    if (SelectFile.Length > 10485760)       // Limit upto 10 MB  (1,048,576 bytes in 1 MB)  
                    {
                        return new ResponseWithoutPaginationModel()
                        {
                            Status = false,
                            Message = "File size is too large. Maximum allowed size is 10 MB."
                        };
                    }

                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(SelectFile.FileName);
                    string fileName = Convert.ToString(fileNameWithoutExtension).Replace(" ", "-") + DateTime.Now.ToString("ddMMyyyyhhmmss") + extension;
                    string folderName = "Uploads/DierAccused/";

                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                    if (!Directory.Exists(filePath))
                        Directory.CreateDirectory(filePath);

                    var filePathWithName = Path.Combine(Directory.GetCurrentDirectory(), filePath, fileName);
                    using (var stream = new FileStream(filePathWithName, FileMode.Create, FileAccess.Write))
                    {
                        await SelectFile.CopyToAsync(stream);
                    }

                    objModel.SanctionDocs = folderName + fileName;
                }

                return await unitOfWork.DierRegistrations_NewService.AddEditDierAccused(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditDierAccused", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrations_NewController/AddEditDierAccused");
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
                return await unitOfWork.DierRegistrations_NewService.DeleteDierAccused(AccusedId, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DeleteDierAccused", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrations_NewController/DeleteDierAccused");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }


        [HttpGet]
        public async Task<ResponseWithoutPaginationModel> GetDierVictimWitness(long GroupNo)
        {
            try
            {
                return await unitOfWork.DierRegistrations_NewService.GetDierVictimWitness(GroupNo);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetDierVictimWitness", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrations_NewController/GetDierVictimWitness");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> AddEditDierVictimWitness(Dier_NewVictimWitnessModel objModel)
        {
            try
            {
                return await unitOfWork.DierRegistrations_NewService.AddEditDierVictimWitness(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditDierVictimWitness", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrations_NewController/AddEditDierVictimWitness");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> DeleteDierVictimWitness(long Id)
        {
            try
            {
                return await unitOfWork.DierRegistrations_NewService.DeleteDierVictimWitness(Id, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DeleteDierVictimWitness", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrations_NewController/DeleteDierVictimWitness");
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
                return await unitOfWork.DierRegistrations_NewService.GetDierInvestigation(InvestGroupNo);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetDierInvestigation", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrations_NewController/GetDierInvestigation");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> AddEditDierInvestigation(Dier_NewInvestigationModel objModel)
        {
            try
            {
                return await unitOfWork.DierRegistrations_NewService.AddEditDierInvestigation(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditDierInvestigation", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrations_NewController/AddEditDierInvestigation");
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
                return await unitOfWork.DierRegistrations_NewService.DeleteDierInvestigation(InvestId, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DeleteDierInvestigation", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrations_NewController/DeleteDierInvestigation");
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
                return await unitOfWork.DierRegistrations_NewService.GetDierComplaintAgainstPerson(ComplaintPerGroupNo);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetDierComplaintAgainst", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrations_NewController/GetDierComplaintAgainst");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> AddEditDierComplaintAgainstPerson(Dier_NewComplaintAgainstPersonModel objModel)
        {
            try
            {
                return await unitOfWork.DierRegistrations_NewService.AddEditDierComplaintAgainstPerson(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditDierComplaintAgainst", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrations_NewController/AddEditDierComplaintAgainst");
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
                return await unitOfWork.DierRegistrations_NewService.DeleteDierComplaintAgainstPerson(ComplaintPerId, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DeleteDierComplaintAgainst", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrations_NewController/DeleteDierComplaintAgainst");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }


        [HttpGet]
        public async Task<ResponseWithoutPaginationModel> GetOffenceClassification(long OffenceClassifGroupNo)
        {
            try
            {
                return await unitOfWork.DierRegistrations_NewService.GetOffenceClassification(OffenceClassifGroupNo);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetOffenceClassification", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrations_NewController/GetOffenceClassification");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<DierRegistrations_NewResponseModel> AddEditOffenceClassification(OffenceClassification_NewModel objModel)
        {
            try
            {
                return await unitOfWork.DierRegistrations_NewService.AddEditOffenceClassification(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditOffenceClassification", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrations_NewController/AddEditOffenceClassification");
                return new DierRegistrations_NewResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<DierRegistrations_NewResponseModel> DeleteOffenceClassification(long OffenceClassifId)
        {
            try
            {
                return await unitOfWork.DierRegistrations_NewService.DeleteOffenceClassification(OffenceClassifId, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DeleteOffenceClassification", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrations_NewController/DeleteOffenceClassification");
                return new DierRegistrations_NewResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }  
        
        private async Task<(bool Status, string Message, string FilePath)> UploadFile(IFormFile? file, string folderName)
        {
            try
            {
                if (file == null || file.Length == 0)
                    return (true, "No File", "");

                // Validation
                if (Path.GetFileNameWithoutExtension(file.FileName).Contains("."))
                    return (false, "Please enter filename without extra dots", "");

                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (string.IsNullOrEmpty(extension) || extension != ".pdf")
                    return (false, "Only PDF allowed", "");

                if (file.Length > 10485760)
                    return (false, "File size > 10MB not allowed", "");

                // File name
                string fileName = Path.GetFileNameWithoutExtension(file.FileName).Replace(" ", "-") + DateTime.Now.ToString("ddMMyyyyhhmmss") + extension;

                string fullFolderPath = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (!Directory.Exists(fullFolderPath))
                    Directory.CreateDirectory(fullFolderPath);

                string fullPath = Path.Combine(fullFolderPath, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return (true, "Success", folderName + fileName);
            }
            catch (Exception ex)
            {
                return (false, ex.Message, "");
            }
        }

        private DierRegistrations_NewResponseModel Error(string message)
        {
            return new DierRegistrations_NewResponseModel
            {
                Status = false,
                Message = message
            };
        }
        //[HttpPost]
        //public async Task<DierRegistrations_NewResponseModel> AddEditCompleteDierRegistrationsStep3(DierRegistrations_NewSteps3Model objModel)
        //{
        //    try
        //    {
        //        // ================= FILE UPLOAD FROM MODEL =================
        //        if (objModel.DierAccusedModel != null)
        //        {
        //            foreach (var accused in objModel.DierAccusedModel)
        //            {
        //                var sanctionFile = accused.SanctionFile; // Renamed from SelectFile

        //                if (sanctionFile != null && sanctionFile.Length != 0)
        //                {
        //                    // ===== YOUR SAME VALIDATION =====
        //                    if (Path.GetFileNameWithoutExtension(sanctionFile.FileName).Contains("."))
        //                    {
        //                        return new DierRegistrations_NewResponseModel()
        //                        {
        //                            Status = false,
        //                            Message = "Invalid file name"
        //                        };
        //                    }

        //                    var extension = Path.GetExtension(sanctionFile.FileName).ToLowerInvariant();
        //                    var permittedExtensions = new[] { ".pdf" };

        //                    if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
        //                    {
        //                        return new DierRegistrations_NewResponseModel()
        //                        {
        //                            Status = false,
        //                            Message = "Only PDF allowed"
        //                        };
        //                    }

        //                    if (sanctionFile.Length > 10485760)
        //                    {
        //                        return new DierRegistrations_NewResponseModel()
        //                        {
        //                            Status = false,
        //                            Message = "File size > 10MB not allowed"
        //                        };
        //                    }

        //                    // ===== SAVE FILE =====
        //                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(sanctionFile.FileName);

        //                    string fileName = fileNameWithoutExtension.Replace(" ", "-")
        //                                        + DateTime.Now.ToString("ddMMyyyyhhmmss")
        //                                        + extension;

        //                    string folderName = "Uploads/DierAccused/";
        //                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), folderName);

        //                    if (!Directory.Exists(filePath))
        //                        Directory.CreateDirectory(filePath);

        //                    var filePathWithName = Path.Combine(filePath, fileName);

        //                    using (var stream = new FileStream(filePathWithName, FileMode.Create))
        //                    {
        //                        await sanctionFile.CopyToAsync(stream);
        //                    }

        //                    // ✅ Assign path
        //                    accused.SanctionDocs = folderName + fileName;
        //                }
        //            }
        //        }
        //        return await unitOfWork.DierRegistrations_NewService.AddEditCompleteDierRegistrationsStep3(objModel, UserSession.Current.UserId);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logsService.Logs("Error", "AddEditCompleteDierRegistrationsStep3", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrations_NewController/AddEditCompleteDierRegistrationsStep3");
        //        return new DierRegistrations_NewResponseModel()
        //        {
        //            Status = false,
        //            Message = ex.Message,
        //        };
        //    }
        //}

        //[HttpPost]
        //[RequestSizeLimit(52428800)]
        //public async Task<DierRegistrations_NewResponseModel> AddCompleteDierRegistrationFinal(IFormFile? SelectFile, IFormFile? SelectFile1, IFormFile? SelectFile2, [FromForm] DierRegistrations_NewModel objModel)
        //{
        //    try
        //    {

        //        // ================= FILE UPLOAD FROM MODEL =================
        //        // ================= STEP 4 FILES =================
        //        // Index based mapping:
        //        // 0 = ChargeSheetDocs
        //        // 1 = FullChargeSheetDocs
        //        // 2 = OtherDocs

        //        //  Charge Sheet Docs Upload
        //        if (SelectFile != null && SelectFile.Length != 0)
        //        {
        //            if (Path.GetFileNameWithoutExtension(SelectFile.FileName).Contains("."))
        //            {
        //                return new DierRegistrations_NewResponseModel()
        //                {
        //                    Status = false,
        //                    Message = "Please enter a filename without any dots (e.g., 'Uploadfile' instead of 'Uploadfile.xyz' )."
        //                };
        //            }
        //            var extension = Path.GetExtension(SelectFile.FileName).ToLowerInvariant();

        //            var permittedExtensions = new[] { ".pdf" };
        //            if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
        //            {
        //                return new DierRegistrations_NewResponseModel()
        //                {
        //                    Status = false,
        //                    Message = "Kindly upload documents in PDF format only."
        //                };
        //            }

        //            //Validate that the file size does not exceed 10 MB.
        //            if (SelectFile.Length > 10485760)       // Limit upto 10 MB  (1,048,576 bytes in 1 MB)  
        //            {
        //                return new DierRegistrations_NewResponseModel()
        //                {
        //                    Status = false,
        //                    Message = "File size is too large. Maximum allowed size is 10 MB."
        //                };
        //            }

        //            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(SelectFile.FileName);
        //            string fileName = Convert.ToString(fileNameWithoutExtension).Replace(" ", "-") + DateTime.Now.ToString("ddMMyyyyhhmmss") + extension;
        //            string folderName = "Uploads/DierRegistrations/";

        //            string filePath = Path.Combine(Directory.GetCurrentDirectory(), folderName);
        //            if (!Directory.Exists(filePath))
        //                Directory.CreateDirectory(filePath);

        //            var filePathWithName = Path.Combine(Directory.GetCurrentDirectory(), filePath, fileName);
        //            using (var stream = new FileStream(filePathWithName, FileMode.Create, FileAccess.Write))
        //            {
        //                await SelectFile.CopyToAsync(stream);
        //            }

        //            objModel.ChargeSheetDocs = folderName + fileName;
        //        }

        //        // Full Charge Sheet Docs Upload

        //        if (SelectFile1 != null && SelectFile1.Length != 0)
        //        {
        //            if (Path.GetFileNameWithoutExtension(SelectFile1.FileName).Contains("."))
        //            {
        //                return new DierRegistrations_NewResponseModel()
        //                {
        //                    Status = false,
        //                    Message = "Please enter a filename without any dots (e.g., 'Uploadfile' instead of 'Uploadfile.xyz' )."
        //                };
        //            }
        //            var extension = Path.GetExtension(SelectFile1.FileName).ToLowerInvariant();

        //            var permittedExtensions = new[] { ".pdf" };
        //            if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
        //            {
        //                return new DierRegistrations_NewResponseModel()
        //                {
        //                    Status = false,
        //                    Message = "Kindly upload documents in PDF format only."
        //                };
        //            }

        //            //Validate that the file size does not exceed 10 MB.
        //            if (SelectFile1.Length > 10485760)       // Limit upto 10 MB  (1,048,576 bytes in 1 MB)  
        //            {
        //                return new DierRegistrations_NewResponseModel()
        //                {
        //                    Status = false,
        //                    Message = "File size is too large. Maximum allowed size is 10 MB."
        //                };
        //            }

        //            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(SelectFile1.FileName);
        //            string fileName = Convert.ToString(fileNameWithoutExtension).Replace(" ", "-") + DateTime.Now.ToString("ddMMyyyyhhmmss") + extension;
        //            string folderName = "Uploads/DierRegistrations/";

        //            string filePath = Path.Combine(Directory.GetCurrentDirectory(), folderName);
        //            if (!Directory.Exists(filePath))
        //                Directory.CreateDirectory(filePath);

        //            var filePathWithName = Path.Combine(Directory.GetCurrentDirectory(), filePath, fileName);
        //            using (var stream = new FileStream(filePathWithName, FileMode.Create, FileAccess.Write))
        //            {
        //                await SelectFile1.CopyToAsync(stream);
        //            }

        //            objModel.FullChargeSheetDocs = folderName + fileName;
        //        }

        //        // Other Docs Upload
        //        if (SelectFile2 != null && SelectFile2.Length != 0)
        //        {
        //            if (Path.GetFileNameWithoutExtension(SelectFile2.FileName).Contains("."))
        //            {
        //                return new DierRegistrations_NewResponseModel()
        //                {
        //                    Status = false,
        //                    Message = "Please enter a filename without any dots (e.g., 'Uploadfile' instead of 'Uploadfile.xyz' )."
        //                };
        //            }
        //            var extension = Path.GetExtension(SelectFile2.FileName).ToLowerInvariant();

        //            var permittedExtensions = new[] { ".pdf" };
        //            if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
        //            {
        //                return new DierRegistrations_NewResponseModel()
        //                {
        //                    Status = false,
        //                    Message = "Kindly upload documents in PDF format only."
        //                };
        //            }

        //            //Validate that the file size does not exceed 10 MB.
        //            if (SelectFile2.Length > 10485760)       // Limit upto 10 MB  (1,048,576 bytes in 1 MB)  
        //            {
        //                return new DierRegistrations_NewResponseModel()
        //                {
        //                    Status = false,
        //                    Message = "File size is too large. Maximum allowed size is 10 MB."
        //                };
        //            }

        //            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(SelectFile2.FileName);
        //            string fileName = Convert.ToString(fileNameWithoutExtension).Replace(" ", "-") + DateTime.Now.ToString("ddMMyyyyhhmmss") + extension;
        //            string folderName = "Uploads/DierRegistrations/";

        //            string filePath = Path.Combine(Directory.GetCurrentDirectory(), folderName);
        //            if (!Directory.Exists(filePath))
        //                Directory.CreateDirectory(filePath);

        //            var filePathWithName = Path.Combine(Directory.GetCurrentDirectory(), filePath, fileName);
        //            using (var stream = new FileStream(filePathWithName, FileMode.Create, FileAccess.Write))
        //            {
        //                await SelectFile2.CopyToAsync(stream);
        //            }

        //            objModel.OtherDocs = folderName + fileName;
        //        }

        //        // ================= ACCUSED FILES =================
        //        if (objModel.DierAccusedModel != null)
        //        {
        //            foreach (var accused in objModel.DierAccusedModel)
        //            {
        //                var sanctionFile = accused.SanctionFile; // Renamed from SelectFile

        //                if (sanctionFile != null && sanctionFile.Length != 0)
        //                {
        //                    // ===== YOUR SAME VALIDATION =====
        //                    if (Path.GetFileNameWithoutExtension(sanctionFile.FileName).Contains("."))
        //                    {
        //                        return new DierRegistrations_NewResponseModel()
        //                        {
        //                            Status = false,
        //                            Message = "Invalid file name"
        //                        };
        //                    }
        //                    var extension = Path.GetExtension(sanctionFile.FileName).ToLowerInvariant();
        //                    var permittedExtensions = new[] { ".pdf" };

        //                    if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
        //                    {
        //                        return new DierRegistrations_NewResponseModel()
        //                        {
        //                            Status = false,
        //                            Message = "Only PDF allowed"
        //                        };
        //                    }

        //                    if (sanctionFile.Length > 10485760)
        //                    {
        //                        return new DierRegistrations_NewResponseModel()
        //                        {
        //                            Status = false,
        //                            Message = "File size > 10MB not allowed"
        //                        };
        //                    }

        //                    // ===== SAVE FILE =====
        //                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(sanctionFile.FileName);

        //                    string fileName = fileNameWithoutExtension.Replace(" ", "-") + DateTime.Now.ToString("ddMMyyyyhhmmss") + extension;

        //                    string folderName = "Uploads/DierAccused/";
        //                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), folderName);

        //                    if (!Directory.Exists(filePath))
        //                        Directory.CreateDirectory(filePath);

        //                    var filePathWithName = Path.Combine(filePath, fileName);

        //                    using (var stream = new FileStream(filePathWithName, FileMode.Create))
        //                    {
        //                        await sanctionFile.CopyToAsync(stream);
        //                    }

        //                    // ✅ Assign path
        //                    accused.SanctionDocs = folderName + fileName;
        //                }
        //            }
        //        }

        //        // ================= CALL FINAL TRANSACTION METHOD =================
        //        return await unitOfWork.DierRegistrations_NewService.AddEditCompleteDierRegistrationsFinal(objModel, UserSession.Current.UserId);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logsService.Logs("Error", "AddCompleteDierRegistrationFinal", ex.Message, ex.StackTrace, ex.Source, "Controller/DierRegistrations_NewController/AddCompleteDierRegistrationFinal");

        //        return new DierRegistrations_NewResponseModel
        //        {
        //            Status = false,
        //            Message = ex.Message
        //        };
        //    }
        //}


    }
}
