using Case.Dto.CaseDecision;
using Case.Dto.Shared;
using Case.ServiceBus.UnitOfWork;
using CaseService.Middleware;
using Common.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CaseService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class DierCaseDecisionController : ControllerBase
    {
        private readonly LogsService _logsService;
        private readonly IUnitOfWorkService unitOfWork;
        public DierCaseDecisionController(IUnitOfWorkService unitOfWorkService, LogsService logsService)
        {
            _logsService = logsService;
            unitOfWork = unitOfWorkService;
        }

        [HttpGet]
        public async Task<ResponseWithoutPaginationModel> GetCaseDecisionList(long CaseId)
        {
            try
            {
                return await unitOfWork.CaseDecision.GetCaseDecisionList(CaseId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetCaseDecisionList", ex.Message, ex.StackTrace, ex.Source, "CaseService/ArbitrationController/GetCaseDecisionList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<CaseDecisionResponseModel> AddEditCaseDecision([FromForm] CaseDecisionAddModel objData)
        {
            try
            {
                CaseDecisionModel objModel = new CaseDecisionModel()
                {

                    DecisionId = objData.DecisionId,
                    CaseId = objData.CaseId,
                    DecisionDate = objData.DecisionDate,
                    Decision_Comp_Date = objData.Decision_Comp_Date,
                    Decision_Detail = objData.Decision_Detail,
                    Decision_FA = objData.Decision_FA,
                    Web_copy_order_obtained = objData.Web_copy_order_obtained,
                    Web_obtained_date = objData.Web_obtained_date,
                    DocumentName = objData.DocumentName,
                    Implementation_required = objData.Implementation_required,
                    Implementation_required_OrNo = objData.Implementation_required_OrNo,
                    Implementation_required_date = objData.Implementation_required_date,
                    AppliedForCertifiedCopy_YN = objData.AppliedForCertifiedCopy_YN,
                    AppliedForCertifiedCopyInwordNo = objData.AppliedForCertifiedCopyInwordNo,
                    AppliedForCertifiedCopyDate = objData.AppliedForCertifiedCopyDate,
                    CopyReceived_YN = objData.CopyReceived_YN,
                    CopyForwordedOfOic_Hod_YN = objData.CopyForwordedOfOic_Hod_YN,
                    OpinionProvidedToOic_Hod_YN = objData.OpinionProvidedToOic_Hod_YN,
                    PD_DecisionCopyRecYN = objData.PD_DecisionCopyRecYN,
                    PD_DecisionCopyRecDate = objData.PD_DecisionCopyRecDate,
                    PD_DecisionSenttoHOOYN = objData.PD_DecisionSenttoHOOYN,
                    PD_DecisionSenttoHOODate = objData.PD_DecisionSenttoHOODate,
                    PD_DecisionSenttoGovtYN = objData.PD_DecisionSenttoGovtYN,
                    PD_DecisionSenttoGovtDate = objData.PD_DecisionSenttoGovtDate,
                    PD_StayGrantedYN = objData.PD_StayGrantedYN,
                    PD_StayGrantedDate = objData.PD_StayGrantedDate,
                    PD_LawyerOpenionYN = objData.PD_LawyerOpenionYN,
                    PD_DateoffilingAppeal = objData.PD_DateoffilingAppeal,
                    Remark = objData.Remark,
                    DateoSendingCertifiedCopyYN = objData.DateoSendingCertifiedCopyYN,
                    DateoSendingCertifiedCopy = objData.DateoSendingCertifiedCopy,
                    PD_AppealFilingDate = objData.PD_AppealFilingDate,
                    PD_DecisionSenttoHODYN = objData.PD_DecisionSenttoHODYN,
                    PD_DecisionSenttoHODDate = objData.PD_DecisionSenttoHODDate,
                    PD_FinalDecisionofGovtYN = objData.PD_FinalDecisionofGovtYN,
                    PD_FinalDecisionofGovtDate = objData.PD_FinalDecisionofGovtDate,
                    PD_DecisionCompliedYN = objData.PD_DecisionCompliedYN,
                    PD_DecisionCompliedDate = objData.PD_DecisionCompliedDate,
                    PD_DepttOpenionYN = objData.PD_DepttOpenionYN,
                    PD_AppealNo = objData.PD_AppealNo,
                    IsExParty = objData.IsExParty,
                    ExPartyDate = objData.ExPartyDate,
                    DataSendCommYN = objData.DataSendCommYN,
                    Date_Sending_Comment = objData.Date_Sending_Comment,
                    PLC_Date = objData.PLC_Date,
                    OpinionOfOic_YN = objData.OpinionOfOic_YN,
                    PD_DecisionNonCompliedReason = objData.PD_DecisionNonCompliedReason
                };

                var permittedExtensions = new[] { ".jpeg", ".jpg", ".png", ".gif", ".pdf", ".doc", ".docx", ".xls", ".xlsx" };
                if (objData.DateoSendingCertifiedCopyFileType != null && objData.DateoSendingCertifiedCopyFileType.Length != 0)
                {
                    var extension = Path.GetExtension(objData.DateoSendingCertifiedCopyFileType.FileName).ToLowerInvariant();
                    if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
                    {
                        return new CaseDecisionResponseModel()
                        {
                            Status = false,
                            Message = "Invalid file type."
                        };
                    }
                    objModel.DateoSendingCertifiedCopyFileType = extension;
                }
                if (objData.PLC_Document != null && objData.PLC_Document.Length != 0)
                {
                    var extension = Path.GetExtension(objData.PLC_Document.FileName).ToLowerInvariant();
                    if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
                    {
                        return new CaseDecisionResponseModel()
                        {
                            Status = false,
                            Message = "Invalid file type."
                        };
                    }
                    objModel.PLC_Document = extension;
                }
                if (objData.CopyOfDecisionReceivedDocs != null && objData.CopyOfDecisionReceivedDocs.Length != 0)
                {
                    var extension = Path.GetExtension(objData.CopyOfDecisionReceivedDocs.FileName).ToLowerInvariant();
                    if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
                    {
                        return new CaseDecisionResponseModel()
                        {
                            Status = false,
                            Message = "Invalid file type."
                        };
                    }
                    objModel.CopyOfDecisionReceivedDocs = extension;
                }
                if (objData.OpinionOfOicDocs != null && objData.OpinionOfOicDocs.Length != 0)
                {
                    var extension = Path.GetExtension(objData.OpinionOfOicDocs.FileName).ToLowerInvariant();
                    if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
                    {
                        return new CaseDecisionResponseModel()
                        {
                            Status = false,
                            Message = "Invalid file type."
                        };
                    }
                    objModel.OpinionOfOicDocs = extension;
                }

                var objResult = await unitOfWork.CaseDecision.AddEditCaseDecision(objModel, UserSession.Current.UserId);
                if (objResult.Status)
                {
                    long DecisionId = objResult.ReturnID;
                    if (objData.DateoSendingCertifiedCopyFileType != null && objData.DateoSendingCertifiedCopyFileType.Length != 0)
                    {
                        string fileName = DecisionId + objModel.DateoSendingCertifiedCopyFileType;
                        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/CaseDocuments/" + objModel.CaseId);
                        if (!Directory.Exists(filePath))
                            Directory.CreateDirectory(filePath);

                        var filePathWithName = Path.Combine(Directory.GetCurrentDirectory(), filePath, fileName);
                        using (var stream = new FileStream(filePathWithName, FileMode.Create, FileAccess.Write))
                        {
                            await objData.DateoSendingCertifiedCopyFileType.CopyToAsync(stream);
                        }
                    }
                    if (objData.PLC_Document != null && objData.PLC_Document.Length != 0)
                    {
                        string fileName = DecisionId + objModel.PLC_Document;
                        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/CaseDocuments/PlcDocs/" + objModel.CaseId);
                        if (!Directory.Exists(filePath))
                            Directory.CreateDirectory(filePath);

                        var filePathWithName = Path.Combine(Directory.GetCurrentDirectory(), filePath, fileName);
                        using (var stream = new FileStream(filePathWithName, FileMode.Create, FileAccess.Write))
                        {
                            await objData.PLC_Document.CopyToAsync(stream);
                        }
                    }
                    if (objData.CopyOfDecisionReceivedDocs != null && objData.CopyOfDecisionReceivedDocs.Length != 0)
                    {
                        string fileName = DecisionId + objModel.CopyOfDecisionReceivedDocs;
                        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/CaseDocuments/CopyOfPLCDRDocs/" + objModel.CaseId);
                        if (!Directory.Exists(filePath))
                            Directory.CreateDirectory(filePath);

                        var filePathWithName = Path.Combine(Directory.GetCurrentDirectory(), filePath, fileName);
                        using (var stream = new FileStream(filePathWithName, FileMode.Create, FileAccess.Write))
                        {
                            await objData.CopyOfDecisionReceivedDocs.CopyToAsync(stream);
                        }
                    }
                    if (objData.OpinionOfOicDocs != null && objData.OpinionOfOicDocs.Length != 0)
                    {
                        string fileName = DecisionId + objModel.OpinionOfOicDocs;
                        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads/CaseDocuments/OpinionOODocs/" + objModel.CaseId);
                        if (!Directory.Exists(filePath))
                            Directory.CreateDirectory(filePath);

                        var filePathWithName = Path.Combine(Directory.GetCurrentDirectory(), filePath, fileName);
                        using (var stream = new FileStream(filePathWithName, FileMode.Create, FileAccess.Write))
                        {
                            await objData.OpinionOfOicDocs.CopyToAsync(stream);
                        }
                    }
                }
                return objResult;
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditCaseDecision", ex.Message, ex.StackTrace, ex.Source, "CaseService/ArbitrationController/AddEditCaseDecision");
                return new CaseDecisionResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }            
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> DeleteCaseDecision(long DecisionId)
        {
            try
            {
                return await unitOfWork.CaseDecision.DeleteCaseDecision(DecisionId, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DeleteCaseDecision", ex.Message, ex.StackTrace, ex.Source, "CaseService/ArbitrationController/DeleteCaseDecision");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpGet]
        public async Task<ResponseWithoutPaginationModel> GetCaseDecisionPamcList(long CaseId)
        {
            try
            {
                return await unitOfWork.CaseDecision.GetCaseDecisionPamcList(CaseId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetCaseDecisionPamcList", ex.Message, ex.StackTrace, ex.Source, "CaseService/ArbitrationController/GetCaseDecisionPamcList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        [RequestSizeLimit(52428800)]
        public async Task<CaseDecisionResponseModel> AddEditCaseDecisionPamc(IFormFile? SelectFile, IFormFile? SelectFile1, [FromForm] CaseDecisionPamcAddModel objModel)
        {
            try
            {
                if (SelectFile != null && SelectFile.Length != 0)
                {
                    var extension = Path.GetExtension(SelectFile.FileName).ToLowerInvariant();

                    var permittedExtensions = new[] { ".pdf" };
                    if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
                    {
                        return new CaseDecisionResponseModel()
                        {
                            Status = false,
                            Message = "Please upload only PDF file."
                        };
                    }

                    //Validating the File Size Limit to 50 MB
                    if (SelectFile.Length > 52428800) // Limit to 5 MB  (1,048,576 bytes in 1 MB)
                    {
                        return new CaseDecisionResponseModel()
                        {
                            Status = false,
                            Message = "File size must be less then 25 MB."
                        };
                    }
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(SelectFile.FileName);
                    string fileName = Convert.ToString(fileNameWithoutExtension).Replace(" ", "-") + DateTime.Now.ToString("ddMMyyyyhhmmss") + extension;
                    string folderName = "Uploads/CaseDocuments/PamcDocs/" + objModel.CaseId + "/";

                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                    if (!Directory.Exists(filePath))
                        Directory.CreateDirectory(filePath);

                    var filePathWithName = Path.Combine(Directory.GetCurrentDirectory(), filePath, fileName);
                    using (var stream = new FileStream(filePathWithName, FileMode.Create, FileAccess.Write))
                    {
                        await SelectFile.CopyToAsync(stream);
                    }

                    objModel.PamcDocs = folderName + fileName;
                }

                if (SelectFile1 != null && SelectFile1.Length != 0)
                {
                    var extension = Path.GetExtension(SelectFile1.FileName).ToLowerInvariant();

                    var permittedExtensions = new[] { ".pdf" };
                    if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
                    {
                        return new CaseDecisionResponseModel()
                        {
                            Status = false,
                            Message = "Please upload only PDF file."
                        };
                    }

                    //Validating the File Size Limit to 50 MB
                    if (SelectFile1.Length > 52428800) // Limit to 50 MB  (1,048,576 bytes in 1 MB)
                    {
                        return new CaseDecisionResponseModel()
                        {
                            Status = false,
                            Message = "File size must be less then 25 MB."
                        };
                    }
                    string fileNameWithoutExtension1 = Path.GetFileNameWithoutExtension(SelectFile1.FileName);
                    string fileName1 = Convert.ToString(fileNameWithoutExtension1).Replace(" ", "-") + DateTime.Now.ToString("ddMMyyyyhhmmss") + extension;
                    string folderName1 = "Uploads/CaseDocuments/CopyOfPamcDecision/" + objModel.CaseId + "/";

                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), folderName1);
                    if (!Directory.Exists(filePath))
                        Directory.CreateDirectory(filePath);

                    var filePathWithName = Path.Combine(Directory.GetCurrentDirectory(), filePath, fileName1);
                    using (var stream = new FileStream(filePathWithName, FileMode.Create, FileAccess.Write))
                    {
                        await SelectFile1.CopyToAsync(stream);
                    }
                    objModel.CopyOfPamcDecision = folderName1 + fileName1;
                }

                return await unitOfWork.CaseDecision.AddEditCaseDecisionPamc(objModel, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "AddEditCaseDecision", ex.Message, ex.StackTrace, ex.Source, "CaseService/ArbitrationController/AddEditCaseDecision");
                return new CaseDecisionResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }

        }
        
        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> DeactiveCaseDecisionPamc(long PamcId)
        {
            try
            {
                return await unitOfWork.CaseDecision.DeactiveCaseDecisionPamc(PamcId, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DeactiveCaseDecisionPamc", ex.Message, ex.StackTrace, ex.Source, "CaseService/ArbitrationController/DeactiveCaseDecisionPamc");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpPost]
        public async Task<ResponseWithoutPaginationModel> DeleteFromCaseDecisionUpdateList(long caseId)
        {
            try
            {
                return await unitOfWork.CaseDecision.DeleteFromCaseDecisionUpdateList(caseId, UserSession.Current.UserId);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "DeleteFromCaseDecisionUpdateList", ex.Message, ex.StackTrace, ex.Source, "CaseService/ArbitrationController/DeleteFromCaseDecisionUpdateList");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
    }

    public class CaseDecisionAddModel
    {
        public long DecisionId { get; set; }
        public long CaseId { get; set; }
        public string? DecisionDate { get; set; }
        public string? Decision_Comp_Date { get; set; }
        public string? Decision_Detail { get; set; }
        public bool Decision_FA { get; set; }
        public bool Web_copy_order_obtained { get; set; }
        public string? Web_obtained_date { get; set; }
        public string? DocumentName { get; set; }
        public bool Implementation_required { get; set; }
        public int Implementation_required_OrNo { get; set; }
        public string? Implementation_required_date { get; set; }
        public bool AppliedForCertifiedCopy_YN { get; set; }
        public int AppliedForCertifiedCopyInwordNo { get; set; }
        public string? AppliedForCertifiedCopyDate { get; set; }
        public bool CopyReceived_YN { get; set; }
        public bool CopyForwordedOfOic_Hod_YN { get; set; }
        public bool OpinionProvidedToOic_Hod_YN { get; set; }
        public bool PD_DecisionCopyRecYN { get; set; }
        public string? PD_DecisionCopyRecDate { get; set; }
        public bool PD_DecisionSenttoHOOYN { get; set; }
        public string? PD_DecisionSenttoHOODate { get; set; }
        public bool PD_DecisionSenttoGovtYN { get; set; }
        public string? PD_DecisionSenttoGovtDate { get; set; }
        public bool PD_StayGrantedYN { get; set; }
        public string? PD_StayGrantedDate { get; set; }
        public bool PD_LawyerOpenionYN { get; set; }
        public string? PD_DateoffilingAppeal { get; set; }
        public string? Remark { get; set; }
        public bool DateoSendingCertifiedCopyYN { get; set; }
        public string? DateoSendingCertifiedCopy { get; set; }
        public string? PD_AppealFilingDate { get; set; }
        public bool PD_DecisionSenttoHODYN { get; set; }
        public string? PD_DecisionSenttoHODDate { get; set; }
        public bool PD_FinalDecisionofGovtYN { get; set; }
        public string? PD_FinalDecisionofGovtDate { get; set; }
        public bool PD_DecisionCompliedYN { get; set; }
        public string? PD_DecisionCompliedDate { get; set; }
        public bool PD_DepttOpenionYN { get; set; }
        public string? PD_AppealNo { get; set; }
        public bool IsExParty { get; set; }
        public string? ExPartyDate { get; set; }
        public bool DataSendCommYN { get; set; }
        public string? Date_Sending_Comment { get; set; }
        public IFormFile? DateoSendingCertifiedCopyFileType { get; set; }
        public string? PLC_Date { get; set; }
        public IFormFile? PLC_Document { get; set; }
        public IFormFile? CopyOfDecisionReceivedDocs { get; set; }
        public bool OpinionOfOic_YN { get; set; }
        public IFormFile? OpinionOfOicDocs { get; set; }
        public string? PD_DecisionNonCompliedReason { get; set; }
    }

}
