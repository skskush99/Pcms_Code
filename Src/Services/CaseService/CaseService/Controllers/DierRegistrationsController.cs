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
        public async Task<ResponseModel> GetDisposalList(DierListFilterModel objModel)
        {
            try
            {
                var loginUserData = UserSession.Current;
                return await unitOfWork.DierRegistrationsService.GetDisposalList(objModel);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetDisposalList", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrationsController/GetDisposalList");
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<DierRegistrationsResponseModel> AddEditDierRegistrationsSteps1(DierRegistrationsSteps1Model objModel)
        {
            try
            {
                return await unitOfWork.DierRegistrationsService.AddEditDierRegistrationsSteps1(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditDierRegistrationsSteps1", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrationsController/AddEditDierRegistrationsSteps1");
                return new DierRegistrationsResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<DierRegistrationsResponseModel> AddEditDierRegistrationsSteps2(DierRegistrationsSteps2Model objModel)
        {
            try
            {
                return await unitOfWork.DierRegistrationsService.AddEditDierRegistrationsSteps2(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditDierRegistrationsSteps2", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrationsController/AddEditDierRegistrationsSteps2");
                return new DierRegistrationsResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<DierRegistrationsResponseModel> AddEditDierRegistrationsSteps3(DierRegistrationsSteps3Model objModel)
        {
            try
            {
                return await unitOfWork.DierRegistrationsService.AddEditDierRegistrationsSteps3(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditDierRegistrationsSteps3", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrationsController/AddEditDierRegistrationsSteps3");
                return new DierRegistrationsResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        [RequestSizeLimit(52428800)]
        public async Task<DierRegistrationsResponseModel> AddEditDierRegistrationsSteps4(IFormFile? SelectFile, IFormFile? SelectFile1, IFormFile? SelectFile2, [FromForm] DierRegistrationsSteps4Model objModel)
        {
            try
            {
                //  Charge Sheet Docs Upload
                if (SelectFile != null && SelectFile.Length != 0)
                {
                    if (Path.GetFileNameWithoutExtension(SelectFile.FileName).Contains("."))
                    {
                        return new DierRegistrationsResponseModel()
                        {
                            Status = false,
                            Message = "Please enter a filename without any dots (e.g., 'Uploadfile' instead of 'Uploadfile.xyz' )."
                        };
                    }
                    var extension = Path.GetExtension(SelectFile.FileName).ToLowerInvariant();

                    var permittedExtensions = new[] { ".pdf" };
                    if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
                    {
                        return new DierRegistrationsResponseModel()
                        {
                            Status = false,
                            Message = "Kindly upload documents in PDF format only."
                        };
                    }

                    //Validate that the file size does not exceed 10 MB.
                    if (SelectFile.Length > 10485760)       // Limit upto 10 MB  (1,048,576 bytes in 1 MB)  
                    {
                        return new DierRegistrationsResponseModel()
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
                        return new DierRegistrationsResponseModel()
                        {
                            Status = false,
                            Message = "Please enter a filename without any dots (e.g., 'Uploadfile' instead of 'Uploadfile.xyz' )."
                        };
                    }
                    var extension = Path.GetExtension(SelectFile1.FileName).ToLowerInvariant();

                    var permittedExtensions = new[] { ".pdf" };
                    if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
                    {
                        return new DierRegistrationsResponseModel()
                        {
                            Status = false,
                            Message = "Kindly upload documents in PDF format only."
                        };
                    }

                    //Validate that the file size does not exceed 10 MB.
                    if (SelectFile1.Length > 10485760)       // Limit upto 10 MB  (1,048,576 bytes in 1 MB)  
                    {
                        return new DierRegistrationsResponseModel()
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
                        return new DierRegistrationsResponseModel()
                        {
                            Status = false,
                            Message = "Please enter a filename without any dots (e.g., 'Uploadfile' instead of 'Uploadfile.xyz' )."
                        };
                    }
                    var extension = Path.GetExtension(SelectFile2.FileName).ToLowerInvariant();

                    var permittedExtensions = new[] { ".pdf" };
                    if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
                    {
                        return new DierRegistrationsResponseModel()
                        {
                            Status = false,
                            Message = "Kindly upload documents in PDF format only."
                        };
                    }

                    //Validate that the file size does not exceed 10 MB.
                    if (SelectFile2.Length > 10485760)       // Limit upto 10 MB  (1,048,576 bytes in 1 MB)  
                    {
                        return new DierRegistrationsResponseModel()
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

                return await unitOfWork.DierRegistrationsService.AddEditDierRegistrationsSteps4(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditDierRegistrationsSteps4", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrationsController/AddEditDierRegistrationsSteps4");
                return new DierRegistrationsResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        [HttpPost]
        [RequestSizeLimit(52428800)]
        public async Task<DisposalRegistrationsResponseModel> AddEditDierDisposalRegistrationsSteps5(IFormFile? SelectFile, [FromForm] FinalDisposalRegister objModel)
        {
            try
            {
                //  Judgement Copy Docs Upload
                if (SelectFile != null && SelectFile.Length != 0)
                {
                    if (Path.GetFileNameWithoutExtension(SelectFile.FileName).Contains("."))
                    {
                        return new DisposalRegistrationsResponseModel()
                        {
                            Status = false,
                            Message = "Please enter a filename without any dots (e.g., 'Uploadfile' instead of 'Uploadfile.xyz' )."
                        };
                    }
                    var extension = Path.GetExtension(SelectFile.FileName).ToLowerInvariant();

                    var permittedExtensions = new[] { ".pdf" };
                    if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
                    {
                        return new DisposalRegistrationsResponseModel()
                        {
                            Status = false,
                            Message = "Kindly upload documents in PDF format only."
                        };
                    }

                    //Validate that the file size does not exceed 10 MB.
                    if (SelectFile.Length > 10485760)       // Limit upto 10 MB  (1,048,576 bytes in 1 MB)  
                    {
                        return new DisposalRegistrationsResponseModel()
                        {
                            Status = false,
                            Message = "File size is too large. Maximum allowed size is 10 MB."
                        };
                    }

                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(SelectFile.FileName);
                    string fileName = Convert.ToString(fileNameWithoutExtension).Replace(" ", "-") + DateTime.Now.ToString("ddMMyyyyhhmmss") + extension;
                    string folderName = "Uploads/DisposalRegistrations/";

                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                    if (!Directory.Exists(filePath))
                        Directory.CreateDirectory(filePath);

                    var filePathWithName = Path.Combine(Directory.GetCurrentDirectory(), filePath, fileName);
                    using (var stream = new FileStream(filePathWithName, FileMode.Create, FileAccess.Write))
                    {
                        await SelectFile.CopyToAsync(stream);
                    }

                    objModel.JudgementCopyDocs = folderName + fileName;
                }
                return await unitOfWork.DierRegistrationsService.AddEditDierDisposalRegistrationsSteps5(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditDierDisposalRegistrationsSteps5", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrationsController/AddEditDierDisposalRegistrationsSteps5");
                return new DisposalRegistrationsResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }


        [HttpPost]
        [RequestSizeLimit(52428800)]
        public async Task<ResponseWithoutPaginationModel> AddEditDierRegistrations(IFormFile? SelectFile, IFormFile? SelectFile1, IFormFile? SelectFile2, [FromForm] DierRegistrations_OldModel objModel)
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

                return await unitOfWork.DierRegistrationsService.AddEditDierRegistrations(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditDierRegistrations", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrationsController/AddEditDierRegistrations");
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
        [RequestSizeLimit(52428800)]
        public async Task<ResponseWithoutPaginationModel> AddEditDierAccused(IFormFile? SelectFile, [FromForm] DierAccusedModel objModel)
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
        public async Task<ResponseWithoutPaginationModel> GetDierVictimWitness(long GroupNo)
        {
            try
            {
                return await unitOfWork.DierRegistrationsService.GetDierVictimWitness(GroupNo);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetDierVictimWitness", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrationsController/GetDierVictimWitness");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> AddEditDierVictimWitness(DierVictimWitnessModel objModel)
        {
            try
            {
                return await unitOfWork.DierRegistrationsService.AddEditDierVictimWitness(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditDierVictimWitness", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrationsController/AddEditDierVictimWitness");
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
                return await unitOfWork.DierRegistrationsService.DeleteDierVictimWitness(Id, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DeleteDierVictimWitness", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrationsController/DeleteDierVictimWitness");
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


        [HttpGet]
        public async Task<ResponseWithoutPaginationModel> GetOffenceClassification(long OffenceClassifGroupNo)
        {
            try
            {
                return await unitOfWork.DierRegistrationsService.GetOffenceClassification(OffenceClassifGroupNo);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetOffenceClassification", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrationsController/GetOffenceClassification");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<DierRegistrationsResponseModel> AddEditOffenceClassification(OffenceClassificationModel objModel)
        {
            try
            {
                return await unitOfWork.DierRegistrationsService.AddEditOffenceClassification(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditOffenceClassification", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrationsController/AddEditOffenceClassification");
                return new DierRegistrationsResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<DierRegistrationsResponseModel> DeleteOffenceClassification(long OffenceClassifId)
        {
            try
            {
                return await unitOfWork.DierRegistrationsService.DeleteOffenceClassification(OffenceClassifId, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DeleteOffenceClassification", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrationsController/DeleteOffenceClassification");
                return new DierRegistrationsResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }


        [HttpGet]
        public async Task<ResponseWithoutPaginationModel> GetDisposalSentence(long DisposalGroupNo)
        {
            try
            {
                return await unitOfWork.DierRegistrationsService.GetDisposalSentence(DisposalGroupNo);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetDisposalSentence", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrationsController/GetDisposalSentence");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        [RequestSizeLimit(52428800)]
        public async Task<ResponseWithoutPaginationModel> AddEditDisposalSentence(DisposalSentenceModel objModel)
        {
            try
            {
                return await unitOfWork.DierRegistrationsService.AddEditDisposalSentence(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditDisposalSentence", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrationsController/AddEditDisposalSentence");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> DeleteDisposalSentence(long SentenceId)
        {
            try
            {
                return await unitOfWork.DierRegistrationsService.DeleteDisposalSentence(SentenceId, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DeleteDisposalSentence", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrationsController/DeleteDisposalSentence");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        //[HttpGet]
        //public async Task<ResponseWithoutPaginationModel> GetDierVictim(long VictimGroupNo)
        //{
        //    try
        //    {
        //        return await unitOfWork.DierRegistrationsService.GetDierVictim(VictimGroupNo);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logsService.Logs("Error", "GetDierVictim", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrationsController/GetDierVictim");
        //        return new ResponseWithoutPaginationModel()
        //        {
        //            Status = false,
        //            Message = ex.Message,
        //        };
        //    }
        //}
        //[HttpPost]
        //public async Task<ResponseWithoutPaginationModel> AddEditDierVictim(DierVictimModel objModel)
        //{
        //    try
        //    {
        //        return await unitOfWork.DierRegistrationsService.AddEditDierVictim(objModel, UserSession.Current.UserId);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logsService.Logs("Error", "AddEditDierVictim", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrationsController/AddEditDierVictim");
        //        return new ResponseWithoutPaginationModel()
        //        {
        //            Status = false,
        //            Message = ex.Message,
        //        };
        //    }
        //}
        //[HttpPost]
        //public async Task<ResponseWithoutPaginationModel> DeleteDierVictim(long VictimId)
        //{
        //    try
        //    {
        //        return await unitOfWork.DierRegistrationsService.DeleteDierVictim(VictimId, UserSession.Current.UserId);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logsService.Logs("Error", "DeleteDierVictim", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrationsController/DeleteDierVictim");
        //        return new ResponseWithoutPaginationModel()
        //        {
        //            Status = false,
        //            Message = ex.Message,
        //        };
        //    }
        //}

        //[HttpGet]
        //public async Task<ResponseWithoutPaginationModel> GetDierWitness(long WitnessGroupNo)
        //{
        //    try
        //    {
        //        return await unitOfWork.DierRegistrationsService.GetDierWitness(WitnessGroupNo);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logsService.Logs("Error", "GetDierWitness", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrationsController/GetDierWitness");
        //        return new ResponseWithoutPaginationModel()
        //        {
        //            Status = false,
        //            Message = ex.Message,
        //        };
        //    }
        //}
        //[HttpPost]
        //public async Task<ResponseWithoutPaginationModel> AddEditDierWitness(DierWitnessModel objModel)
        //{
        //    try
        //    {
        //        return await unitOfWork.DierRegistrationsService.AddEditDierWitness(objModel, UserSession.Current.UserId);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logsService.Logs("Error", "AddEditDierWitness", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrationsController/AddEditDierWitness");
        //        return new ResponseWithoutPaginationModel()
        //        {
        //            Status = false,
        //            Message = ex.Message,
        //        };
        //    }
        //}
        //[HttpPost]
        //public async Task<ResponseWithoutPaginationModel> DeleteDierWitness(long WitnessId)
        //{
        //    try
        //    {
        //        return await unitOfWork.DierRegistrationsService.DeleteDierWitness(WitnessId, UserSession.Current.UserId);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logsService.Logs("Error", "DeleteDierWitness", ex.Message, ex.StackTrace, ex.Source, "CaseService/DierRegistrationsController/DeleteDierWitness");
        //        return new ResponseWithoutPaginationModel()
        //        {
        //            Status = false,
        //            Message = ex.Message,
        //        };
        //    }
        //}
    }
}
