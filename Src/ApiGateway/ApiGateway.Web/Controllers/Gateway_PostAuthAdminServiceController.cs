using Microsoft.AspNetCore.Mvc;
using Core.Insfrastructure;
using Core.Utils;
using Core.Enums.User;
using System.Reflection;
using System.Text.Json;
using Core.Insfrastructure.Controller;
using Core.Models;
using System.Text.Json.Nodes;
using ExcelDataReader;

namespace ApiGateway.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizationRequiredAttribute))]
    public class Gateway_PostAuthAdminServiceController : BaseApiController
    {
        private const string AuthId = "AuthId";
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IWebHostEnvironment environment;
        IExcelDataReader reader;
        public Gateway_PostAuthAdminServiceController(IConfiguration Configuration, IHttpContextAccessor _httpContextAccessor, IWebHostEnvironment environment) : base(Configuration, _httpContextAccessor)
        {

            this.environment = environment;
        }

        [HttpPost("GetList")]
        public async Task<IActionResult> PostGetList(ApiRequestModel apiRequestModel)
        {
            ResponseModel returnStatus = new ResponseModel(Status.Success);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            bool hasAccess = true;// Session.HasAccess(Convert.ToInt32(PrivilegesAction.ALL));
            Int64 userId = 0;

            string language = HttpContext.Request.Headers["Language"].ToString().ToLower();


            //JsonNode paramValues = JsonSerializer.Deserialize<JsonNode>(apiRequestModel.ApiParams);
            //paramValues["Language"] = language; 
            //apiRequestModel.ApiParams = paramValues;
            try
            {
                if (hasAccess)
                {
                    //returnStatus = await GetApiConfigModel((Int32)apiRequestModel.OrgId, apiRequestModel.ApiKey, "Gateway_PostAuthAdminServiceController", "PostGetList");
                    if (apiRequestModel != null)
                    {
                        userId = Session.UserSession != null ? Session.UserSession.UserId : 0;
                        var url = configuration["ServiceURL:" + apiRequestModel.Module.ToUpper() + ""];
                        ResponseModel output = await CallPostWebAPI<dynamic>(apiRequestModel.Path + "?orgId=" + apiRequestModel.OrgId + "&userId=" + userId + "&Language=" + language, apiRequestModel.ApiParams, url);
                        return Ok(output);
                    }
                    else
                    {
                        return Ok();
                    }

                }
                else
                {
                    returnStatus.Message = EnumUtility.GetDescription(Core.Enums.ReturnMessage.InsuffucientPrivilegesToAccessThisForm);
                    returnStatus.Status = Core.Enums.User.Status.Error;
                    return Ok(returnStatus);

                }
            }
            catch (Exception ex)
            {
                LogUtility.WriteErrorLog(httpContextAccessor.HttpContext, ex, "", "Gateway_PostAuthAdminService.PostGetList", JsonSerializer.Serialize(1));
                returnStatus.Status = Status.Error;
                returnStatus.Message = "Sorry ! An error occured while processing your request. " + ex.Message.ToString();
                return Ok(returnStatus);
            }
        }

        [HttpPost("GetListV1")]
        public async Task<IActionResult> PostGetListV1(ApiRequestModel apiRequestModel)
        {
            ResponseModel returnStatus = new ResponseModel(Status.Success);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            bool hasAccess = true;// Session.HasAccess(Convert.ToInt32(PrivilegesAction.ALL));
            Int64 userId = 0;
            userId = Session.UserSession != null ? Session.UserSession.UserId : 0;
            //JsonNode paramValues = JsonSerializer.Deserialize<JsonNode>(apiRequestModel.ApiParams);
            //paramValues["Language"] = HttpContext.Request.Headers["Language"].ToString().ToLower();
            //paramValues["UserId"] = userId;

            apiRequestModel.Language = HttpContext.Request.Headers["Language"].ToString().ToLower();
            apiRequestModel.UserId = userId;

            try
            {
                if (hasAccess)
                {
                    //returnStatus = await GetApiConfigModel((Int32)apiRequestModel.OrgId, apiRequestModel.ApiKey, "Gateway_PostAuthAdminServiceController", "PostGetList");
                    if (apiRequestModel != null)
                    {
                        userId = Session.UserSession != null ? Session.UserSession.UserId : 0;
                        var url = configuration["ServiceURL:" + apiRequestModel.Module.ToUpper() + ""];
                        ResponseModel output = await CallPostWebAPI<dynamic>(apiRequestModel.Path + "?orgId=" + apiRequestModel.OrgId, apiRequestModel, url);
                        return Ok(output);
                    }
                    else
                    {
                        return Ok();
                    }

                }
                else
                {
                    returnStatus.Message = EnumUtility.GetDescription(Core.Enums.ReturnMessage.InsuffucientPrivilegesToAccessThisForm);
                    returnStatus.Status = Core.Enums.User.Status.Error;
                    return Ok(returnStatus);

                }
            }
            catch (Exception ex)
            {
                LogUtility.WriteErrorLog(httpContextAccessor.HttpContext, ex, "", "Gateway_PostAuthAdminService.PostGetListV1", JsonSerializer.Serialize(1));
                returnStatus.Status = Status.Error;
                returnStatus.Message = "Sorry ! An error occured while processing your request. " + ex.Message.ToString();
                return Ok(returnStatus);
            }
        }

        [HttpPost("AddEdit")]
        public async Task<IActionResult> PostAddEdit(ApiRequestModel apiRequestModel)
        {

            ResponseModel returnStatus = new ResponseModel(Status.Alert);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            bool hasAccess = false;

            if (apiRequestModel.ResponseId > 0)
            {
                hasAccess = true;//Session.HasAccess(Convert.ToInt32(PrivilegesAction.Edit)); // Edit
            }
            else
            {
                hasAccess = true;//Session.HasAccess(Convert.ToInt32(PrivilegesAction.Add)); // Add
            }
            apiRequestModel.UserId = Session.UserSession != null ? Session.UserSession.UserId : 0;
            apiRequestModel.UserDetails = Session.UserSession != null ? Session.UserSession.UserDetails != null ? Session.UserSession.UserDetails : null : null;
            apiRequestModel.Language = HttpContext.Request.Headers["Language"].ToString().ToLower();


            try
            {
                if (hasAccess)
                {
                    //returnStatus = await GetApiConfigModel((Int32)apiRequestModel.OrgId, apiRequestModel.ApiKey, "Gateway_PostAuthAdminServiceController", "PostAddEdit");
                    if (apiRequestModel != null)
                    {
                        //var auditModelEnty = new AuditTrialModel
                        //{
                        //    Module = apiRequestModel.Module,
                        //    API = apiRequestModel.Path,
                        //    EntryPoint = DateTime.Now,
                        //    Model = JsonSerializer.Serialize(apiRequestModel.ApiParams),
                        //    UserId = apiRequestModel.UserId,
                        //    CreatedOn = DateTime.Now
                        //};
                        //// Note Entry Point
                        //var Entryurl = configuration["ServiceURL:CFG"];
                        //Int64 AuditId = 0;
                        //try
                        //{
                        //    ResponseModel RespEntryPoint = await CallPostWebAPI<dynamic>("AuditTrial/AddEditAuditTrial", auditModelEnty, Entryurl);
                        //    if (RespEntryPoint.Status == Enums.URM.Status.Success)
                        //    {
                        //        AuditId = ConversionUtility.ConvertFromDynamicObject<Int64>(RespEntryPoint.EntityId);
                        //    }
                        //}
                        //catch (Exception)
                        //{

                        //APIConfigModel apiConfig = JsonSerializer.Deserialize<APIConfigModel>(returnStatus.CustomObject, options);
                        var url = configuration["ServiceURL:" + apiRequestModel.Module.ToUpper() + ""];
                        ResponseModel output = await CallPostWebAPI<dynamic>(apiRequestModel.Path + "?orgId=" + apiRequestModel.OrgId, apiRequestModel, url);
                        //try
                        //{
                        //    //Ending
                        //    var auditModelExits = new AuditTrialModel
                        //    {
                        //        AuditId = AuditId
                        //    };
                        //    ResponseModel RespExitsPoint = await CallPostWebAPI<dynamic>("AuditTrial/AddEditAuditTrial", auditModelExits, Entryurl);
                        //}
                        //catch (Exception)
                        //{


                        //}
                        return Ok(output);

                    }
                    else
                    {
                        return Ok();
                    }
                }
                else
                {
                    returnStatus.Message = EnumUtility.GetDescription(Core.Enums.ReturnMessage.InsuffucientPrivilegesToAccessThisForm);
                    returnStatus.Status = Core.Enums.User.Status.Error;
                    return Ok(returnStatus);
                }

            }
            catch (Exception ex)
            {
                LogUtility.WriteErrorLog(httpContextAccessor.HttpContext, ex, "", "Gateway_PostAuthAdminService.PostAddEdit", JsonSerializer.Serialize(1));
                returnStatus.Status = Status.Error;
                returnStatus.Message = "Sorry ! An error occured while processing your request. " + ex.Message.ToString();
                return Ok(returnStatus);
            }
        }

        [HttpPost("GetOnLoad")]
        public async Task<IActionResult> PostGetOnLoad(ApiRequestModel apiRequestModel)
        {

            ResponseModel returnStatus = new ResponseModel(Status.Alert);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            bool hasAccess = true;// Session.HasAccess(Convert.ToInt32(PrivilegesAction.ALL));
                                  //JsonNode paramValues = JsonSerializer.Deserialize<JsonNode>(apiRequestModel.ApiParams);
                                  //paramValues["Language"] = HttpContext.Request.Headers["Language"].ToString().ToLower();
                                  //apiRequestModel.ApiParams = paramValues;
            try
            {
                apiRequestModel.UserId = Session.UserSession != null ? Session.UserSession.UserId : 0;
                string language = HttpContext.Request.Headers["Language"].ToString().ToLower();

                if (hasAccess)
                {
                    //returnStatus = await GetApiConfigModel((Int32)apiRequestModel.OrgId, apiRequestModel.ApiKey, "Gateway_PostAuthAdminServiceController", "PostGetOnLoad");
                    if (apiRequestModel != null)
                    {
                        //APIConfigModel apiConfig = JsonSerializer.Deserialize<APIConfigModel>(returnStatus.CustomObject, options);
                        var url = configuration["ServiceURL:" + apiRequestModel.Module.ToUpper() + ""];
                        ResponseModel output = await CallPostWebAPI<dynamic>(apiRequestModel.Path + "?orgId=" + apiRequestModel.OrgId + "&Language=" + language, apiRequestModel.ApiParams, url);
                        return Ok(output);
                    }
                    else
                    {
                        return Ok();
                    }

                }
                else
                {
                    returnStatus.Message = EnumUtility.GetDescription(Core.Enums.ReturnMessage.InsuffucientPrivilegesToAccessThisForm);
                    returnStatus.Status = Core.Enums.User.Status.Error;
                    return Ok(returnStatus);

                }
            }
            catch (Exception ex)
            {
                LogUtility.WriteErrorLog(httpContextAccessor.HttpContext, ex, "", "Gateway_PostAuthAdminService.PostGetOnLoad", JsonSerializer.Serialize(1));
                returnStatus.Status = Status.Error;
                returnStatus.Message = "Sorry ! An error occured while processing your request. " + ex.Message.ToString();
                return Ok(returnStatus);
            }
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> PostDelete(ApiRequestModel apiRequestModel)
        {

            ResponseModel returnStatus = new ResponseModel(Status.Alert);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            bool hasAccess = true;// Session.HasAccess(Convert.ToInt32(PrivilegesAction.Delete));
            apiRequestModel.UserId = Session.UserSession != null ? Session.UserSession.UserId : 0;
            apiRequestModel.Language = HttpContext.Request.Headers["Language"].ToString().ToLower();
            try
            {
                if (hasAccess)
                {
                    //returnStatus = await GetApiConfigModel((Int32)apiRequestModel.OrgId, apiRequestModel.ApiKey, "Gateway_PostAuthAdminServiceController", "PostDelete");
                    if (apiRequestModel != null)
                    {
                        //APIConfigModel apiConfig = JsonSerializer.Deserialize<APIConfigModel>(returnStatus.CustomObject, options);
                        var url = configuration["ServiceURL:" + apiRequestModel.Module.ToUpper() + ""];
                        ResponseModel output = await CallPostWebAPI<dynamic>(apiRequestModel.Path + "?orgId=" + apiRequestModel.OrgId, apiRequestModel, url);
                        return Ok(output);
                    }
                    else
                    {
                        return Ok();
                    }

                }
                else
                {
                    returnStatus.Message = EnumUtility.GetDescription(Core.Enums.ReturnMessage.InsuffucientPrivilegesToAccessThisForm);
                    returnStatus.Status = Core.Enums.User.Status.Error;
                    return Ok(returnStatus);

                }
            }
            catch (Exception ex)
            {
                LogUtility.WriteErrorLog(httpContextAccessor.HttpContext, ex, "", "Gateway_PostAuthAdminService.PostDelete", JsonSerializer.Serialize(1));
                returnStatus.Status = Status.Error;
                returnStatus.Message = "Sorry ! An error occured while processing your request. " + ex.Message.ToString();
                return Ok(returnStatus);
            }
        }


        #region DMS Document Add by Giriraj Mali
        [HttpPost("UploadDocument")]
        public async Task<IActionResult> PostUploadDocument(IFormFile file, Int64 ModuleId, Int32 EntityTypeId, Int64 EntityId)
        {
            ResponseModel returnStatus = new ResponseModel(Status.Alert);
            Int64 UserId = Session.UserSession != null ? Session.UserSession.UserId : 0;
            string saveToPath = "";
            try
            {
                if (file == null)
                {
                    returnStatus.Message = "Please Add Document..";
                    return Ok(returnStatus);
                }
                string baseURL = configuration["ServiceURL:DMS"];
                using (var client = new HttpClient())
                {
                    string dirPath = Path.Combine(environment.ContentRootPath, "File");
                    if (!Directory.Exists(dirPath))
                    {
                        Directory.CreateDirectory(dirPath);
                    }
                    var fileName = Path.GetFileName(file.FileName);
                    saveToPath = Path.Combine(dirPath, fileName);

                    using (var content = new MultipartFormDataContent())
                    {

                        client.BaseAddress = new Uri(baseURL);
                        using (FileStream stream = new FileStream(saveToPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                        var fileStream = System.IO.File.Open(saveToPath, FileMode.Open);
                        content.Add(new StreamContent(fileStream), "files", fileName);
                        var requestUri = client.BaseAddress + "Upload/GetUploadDocument?ModuleId=" + ModuleId + "&EntityTypeId=" + EntityTypeId + "&EntityId=" + EntityId + "&UserId=" + UserId;
                        var request = new HttpRequestMessage(HttpMethod.Post, requestUri) { Content = content };
                        var result = await client.SendAsync(request);
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                        returnStatus = JsonSerializer.Deserialize<ResponseModel>(readTask.Result, options);

                    }
                    if (System.IO.File.Exists(saveToPath))
                    {
                        System.IO.File.Delete(saveToPath);

                    }
                    return Ok(returnStatus);

                }

            }

            catch (Exception ex)
            {

                returnStatus.Status = Status.Error;
                returnStatus.Message = "Sorry ! An error occured while processing your request. Please try after some time." + ex.Message;
                return Ok();
            }
        }
        [HttpPost("DownloadDocument")]
        public async Task<IActionResult> PostDownloadDocument(Int64 DocumentDetailId)
        {
            ResponseModel returnStatus = new ResponseModel(Status.Alert);
            try
            {
                string baseURL = configuration["ServiceURL:DMS"];
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseURL);
                    var requestUri = client.BaseAddress + "Upload/GetDownloadDocument?DocumentDetailId=" + DocumentDetailId;
                    var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
                    var result = await client.SendAsync(request);
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    returnStatus = JsonSerializer.Deserialize<ResponseModel>(readTask.Result, options);
                }
                return Ok(returnStatus);
            }
            catch (Exception ex)
            {
                returnStatus.Status = Status.Error;
                returnStatus.Message = "Sorry ! An error occured while processing your request. " + ex.Message.ToString();
                return Ok(returnStatus);
            }
        }
        [HttpPost("DeleteDocumentDetail")]
        public async Task<IActionResult> PostDeleteDetailDocument(Int64 DocumentDetailId)
        {
            ResponseModel returnStatus = new ResponseModel(Status.Alert);
            try
            {
                Int64 UserSection = Session.UserSession != null ? Session.UserSession.UserId : 0;
                string baseURL = configuration["ServiceURL:DMS"];
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseURL);
                    var requestUri = client.BaseAddress + "Upload/GetDeleteDetailDocument?DocumentDetailId=" + DocumentDetailId + "&UserSection=" + UserSection;
                    var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
                    var result = await client.SendAsync(request);
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    returnStatus = JsonSerializer.Deserialize<ResponseModel>(readTask.Result, options);
                }
                return Ok(returnStatus);
            }
            catch (Exception ex)
            {
                returnStatus.Status = Status.Error;
                returnStatus.Message = "Sorry ! An error occured while processing your request. " + ex.Message.ToString();
                return Ok(returnStatus);
            }
        }

        [HttpPost("DocumentValidateById")]
        public async Task<IActionResult> PostDocumentValidationById(Int64 ModuleId, Int32 EntityTypeId)
        {
            ResponseModel returnStatus = new ResponseModel(Status.Alert);
            try
            {
                string baseURL = configuration["ServiceURL:DMS"];
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseURL);
                    var requestUri = client.BaseAddress + "Upload/GetValidateMappingFile?ModuleId=" + ModuleId + "&EntityTypeId=" + EntityTypeId;
                    var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
                    var result = await client.SendAsync(request);
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    returnStatus = JsonSerializer.Deserialize<ResponseModel>(readTask.Result, options);
                }
                return Ok(returnStatus);
            }
            catch (Exception)
            {

                returnStatus.Status = Status.Error;
                returnStatus.Message = "Sorry ! An error occured while processing your request. Please try after some time.";
                return Ok();
            }
        }

        [HttpPost("DocumentById")]
        public async Task<IActionResult> PostDocumentById(Int64 DocumentDetailId)
        {
            ResponseModel returnStatus = new ResponseModel(Status.Alert);
            try
            {
                string baseURL = configuration["ServiceURL:DMS"];
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseURL);
                    var requestUri = client.BaseAddress + "Upload/GetDocumentById?DocumentDetailId=" + DocumentDetailId;
                    var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
                    var result = await client.SendAsync(request);
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    returnStatus = JsonSerializer.Deserialize<ResponseModel>(readTask.Result, options);
                }
                return Ok(returnStatus);
            }
            catch (Exception ex)
            {
                returnStatus.Status = Status.Error;
                returnStatus.Message = "Sorry ! An error occured while processing your request. " + ex.Message.ToString();
                return Ok(returnStatus);
            }
        }

        [HttpPost("DocumentFileByIds")]
        public async Task<IActionResult> PostDocumentFileByIds(Int64[] DocumentDetailId)
        {
            ResponseModel returnStatus = new ResponseModel(Status.Alert);
            try
            {
                string baseURL = configuration["ServiceURL:DMS"];
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseURL);
                    var requestUri = client.BaseAddress + "Upload/GetDocumentFileByIds?DocumentIds=" + DocumentDetailId;
                    var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
                    var result = await client.SendAsync(request);
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    returnStatus = JsonSerializer.Deserialize<ResponseModel>(readTask.Result, options);
                }
                return Ok(returnStatus);
            }
            catch (Exception ex)
            {
                returnStatus.Status = Status.Error;
                returnStatus.Message = "Sorry ! An error occured while processing your request. " + ex.Message.ToString();
                return Ok(returnStatus);
            }
        }

        [HttpPost("DeleteDocByDocId")]
        public async Task<IActionResult> PostDeleteDocByDocId(Int64[] DocumentDetailId)
        {
            ResponseModel returnStatus = new ResponseModel(Status.Alert);
            try
            {
                Int64 UserId = Session.UserSession != null ? Session.UserSession.UserId : 0;
                string baseURL = configuration["ServiceURL:DMS"];
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseURL);
                    var requestUri = client.BaseAddress + "Upload/DeleteDocByDocId?DocumentDetailId=" + DocumentDetailId + "&UserId=" + UserId;
                    var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
                    var result = await client.SendAsync(request);
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    returnStatus = JsonSerializer.Deserialize<ResponseModel>(readTask.Result, options);
                }
                return Ok(returnStatus);
            }
            catch (Exception ex)
            {
                returnStatus.Status = Status.Error;
                returnStatus.Message = "Sorry ! An error occured while processing your request. " + ex.Message.ToString();
                return Ok(returnStatus);
            }
        }

        [HttpPost("UpdateDocumentEntityId")]
        public async Task<IActionResult> PostGetUpdateDocumentEntityId(Int64[] DocumentIds, Int64 ModuleId = 0, Int64 EntityTypeId = 0, Int64 EntityId = 0)
        {
            ResponseModel returnStatus = new ResponseModel(Status.Alert);
            try
            {
                Int64 UserId = Session.UserSession != null ? Session.UserSession.UserId : 0;
                string baseURL = configuration["ServiceURL:DMS"];
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseURL);
                    var requestUri = client.BaseAddress + "Upload/GetUpdateDocumentEntityId?DocumentIds=" + DocumentIds + "&ModuleId=" + ModuleId + "&EntityTypeId=" + EntityTypeId + "&EntityId=" + EntityId + "& UserId=" + UserId;
                    var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
                    var result = await client.SendAsync(request);
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    returnStatus = JsonSerializer.Deserialize<ResponseModel>(readTask.Result, options);
                }
                return Ok(returnStatus);
            }
            catch (Exception ex)
            {
                returnStatus.Status = Status.Error;
                returnStatus.Message = "Sorry ! An error occured while processing your request. " + ex.Message.ToString();
                return Ok(returnStatus);
            }
        }

        [HttpPost("UploadImage")]
        public async Task<IActionResult> PostUploadImage(IFormFile file, string fileName)
        {
            ResponseModel returnStatus = new ResponseModel(Status.Alert);
            String _uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "Image");
            try
            {
                string Name = string.Empty;
                if (file == null)
                {
                    returnStatus.Message = "Please Add Document..";
                    return Ok(returnStatus);
                }
                if (file.Length > 0)
                {
                    //string fi= Path.GetFileNameWithoutExtension(file.FileName);
                    string ex = System.IO.Path.GetExtension(file.FileName);
                    Name = fileName + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Date.Day + DateTime.Now.Date.Minute + DateTime.Now.ToString("mmss");
                    var filePath = Path.Combine(_uploadFolder, Name + ex);

                    Directory.CreateDirectory(_uploadFolder);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    returnStatus.Status = Core.Enums.User.Status.Success;
                    returnStatus.CustomObject = "Image/" + Name + ex;
                    return Ok(returnStatus);
                }
                else
                {
                    returnStatus.Message = "Please Add Document..";
                    return Ok(returnStatus);
                }

            }

            catch (Exception ex)
            {

                returnStatus.Status = Status.Error;
                returnStatus.Message = "Sorry ! An error occured while processing your request. Please try after some time." + ex.Message;
                return Ok();
            }
            return Ok(returnStatus);
        }
        #endregion

        #region Read Excel Data

        [HttpPost("ReadExcelData")]
        public async Task<IActionResult> postReadExcelData(IFormFile file, Int64 ModuleId, Int32 EntityTypeId, Int64 EntityId, string Module, string ApiPath, int ModeId = 0)
        {
            ResponseModel returnStatus = new ResponseModel(Status.Alert);
            ResponseModel ReturnStatus = new ResponseModel(Status.Alert);
            Int64 UserId = Session.UserSession != null ? Session.UserSession.UserId : 0;
            string saveToPath = "";
            try
            {
                using (var client = new HttpClient())
                {
                    string dirPath = Path.Combine(environment.ContentRootPath, "File");
                    if (!Directory.Exists(dirPath))
                    {
                        Directory.CreateDirectory(dirPath);
                    }
                    string dataFileName = Path.GetFileName(file.FileName);
                    string[] splitName = dataFileName.Split(".");
                    string newName = splitName[0] + Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(dataFileName);
                    string name = newName + extension;
                    string[] allowedExtsnions = new string[] { ".xls", ".xlsx" };
                    if (!allowedExtsnions.Contains(extension))
                        returnStatus.Message = "Sorry! This file is not allowed,make sure that file having extension as either.xls or.xlsx is uploaded.";
                    saveToPath = Path.Combine(dirPath, name);
                    using (FileStream stream = new FileStream(saveToPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    var fileName = Path.GetFileName(file.FileName);
                    saveToPath = Path.Combine(dirPath, fileName);

                    using (var content = new MultipartFormDataContent())
                    {
                        string baseURLDMS = configuration["ServiceURL:DMS"];
                        client.BaseAddress = new Uri(baseURLDMS);
                        using (FileStream stream = new FileStream(saveToPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                        var fileStream = System.IO.File.Open(saveToPath, FileMode.Open);
                        content.Add(new StreamContent(fileStream), "files", fileName);
                        var requestUri = client.BaseAddress + "Upload/GetUploadDocument?ModuleId=" + ModuleId + "&EntityTypeId=" + EntityTypeId + "&EntityId=" + EntityId + "&UserId=" + UserId;
                        var request = new HttpRequestMessage(HttpMethod.Post, requestUri) { Content = content };
                        var result = await client.SendAsync(request);
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                        ReturnStatus = JsonSerializer.Deserialize<ResponseModel>(readTask.Result, options);
                    }
                }
                using (var ClientNTF = new HttpClient())
                {
                    string dirPath = Path.Combine(environment.ContentRootPath, "File");
                    if (!Directory.Exists(dirPath))
                    {
                        Directory.CreateDirectory(dirPath);
                    }
                    using (var ContentNTF = new MultipartFormDataContent())
                    {
                        JsonNode paramsValue = System.Text.Json.JsonSerializer.Deserialize<JsonNode>(ReturnStatus.CustomObject);
                        if (paramsValue != null && paramsValue["DocumentDetailId"] != null && !string.IsNullOrEmpty(paramsValue["DocumentDetailId"].ToString()) && Convert.ToInt32(paramsValue["DocumentDetailId"].ToString()) > 0)
                        {
                            int DocumentDetailId = Convert.ToInt32(paramsValue["DocumentDetailId"].ToString());
                            string dataFileName = Path.GetFileName(file.FileName);
                            string[] splitName = dataFileName.Split(".");
                            string newName = splitName[0] + Guid.NewGuid().ToString();
                            string extension = Path.GetExtension(dataFileName);
                            string name = newName + extension;
                            var fileName = Path.GetFileName(file.FileName);
                            saveToPath = Path.Combine(dirPath, fileName);
                            string BaseUrl = "ServiceURL:" + Module;
                            string baseURL = this.configuration[BaseUrl.ToString()];
                            ClientNTF.BaseAddress = new Uri(baseURL);
                            using (FileStream streamNtf = new FileStream(saveToPath, FileMode.Create))
                            {
                                file.CopyTo(streamNtf);
                            }
                            var fileStream = System.IO.File.Open(saveToPath, FileMode.Open);
                            ContentNTF.Add(new StreamContent(fileStream), "file", fileName);
                            var requestUri = ClientNTF.BaseAddress + ApiPath + "?DocumentDetailId=" + DocumentDetailId + "&ModeId=" + ModeId;
                            var request = new HttpRequestMessage(HttpMethod.Post, requestUri) { Content = ContentNTF };
                            var result = await ClientNTF.SendAsync(request);
                            var readTask = result.Content.ReadAsStringAsync();
                            readTask.Wait();
                            var Options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                            ResponseModel returnstatus = JsonSerializer.Deserialize<ResponseModel>(readTask.Result, Options);

                            if (returnstatus.Status == Status.Success)
                            {
                                JsonNode paramValue = System.Text.Json.JsonSerializer.Deserialize<JsonNode>(returnstatus.CustomObject);
                                returnStatus.CustomObject = new
                                {
                                    NoOfContacts = (ApiPath.ToLower() == "NotiFication/UploadContactDetailFile".ToLower() ? Convert.ToInt32(paramValue["NoOfContacts"].ToString()) : 0),
                                    DocumentDetailId = Convert.ToInt32(paramsValue["DocumentDetailId"].ToString()),
                                    FileName = paramsValue["FileName"].ToString(),
                                    Size = Convert.ToInt32(paramsValue["Size"].ToString())
                                };
                            }
                            else
                            {
                                returnStatus.Status = returnstatus.Status;
                                returnStatus.Message = returnstatus.Message;
                                return Ok(returnStatus);
                                //returnStatus.CustomObject = ReturnStatus.CustomObject;
                            }
                            returnStatus.Status = returnstatus.Status;
                            returnStatus.Message = returnstatus.Message;
                        }
                    }
                }
                return Ok(returnStatus);
            }

            catch (Exception ex)
            {
                LogUtility.WriteErrorLog(httpContextAccessor.HttpContext, ex, string.Empty, MethodBase.GetCurrentMethod().DeclaringType.ToString() + "." + MethodBase.GetCurrentMethod().Name, JsonSerializer.Serialize(1));
                returnStatus.Status = Status.Error;
                returnStatus.Message = "Sorry ! An error occured while processing your request. " + ex.Message.ToString();
                return Ok(returnStatus);
            }
        }

        #endregion
        #region Export Data
        [HttpPost("ExportData")]
        public async Task<IActionResult> PostExportData(ApiRequestModel apiRequestModel)
        {
            ResponseModel returnStatus = new ResponseModel(Status.Alert);
            try
            {
                Int64 userId = 0;
                userId = Session.UserSession != null ? Session.UserSession.UserId : 0;
                apiRequestModel.Language = HttpContext.Request.Headers["Language"].ToString().ToLower();
                apiRequestModel.UserId = userId;
                var url = configuration["ServiceURL:" + apiRequestModel.Module.ToUpper() + ""];
                ResponseModel output = await CallPostWebAPI<dynamic>(apiRequestModel.Path + "?orgId=" + apiRequestModel.OrgId, apiRequestModel, url);
                return Ok(output);
            }
            catch (Exception ex)
            {
                returnStatus.Status = Status.Error;
                returnStatus.Message = "Sorry ! An error occured while processing your request. Please try after some time." + ex.Message;
                return Ok();
            }

        }
        #endregion

        #region  Create Sample File

        [HttpPost("SampleFile")]
        public async Task<IActionResult> PostSampleFile(IFormFile file)
        {
            ResponseModel returnStatus = new ResponseModel(Status.Alert);
            string saveToPath = string.Empty;
            try
            {

                using (var client = new HttpClient())
                {
                    string dirPath = Path.Combine(environment.ContentRootPath, "File");
                    if (!Directory.Exists(dirPath))
                    {
                        Directory.CreateDirectory(dirPath);

                    }

                    var fileName = Path.GetFileName(file.FileName);
                    saveToPath = Path.Combine(dirPath, fileName);
                    using (var content = new MultipartFormDataContent())
                    {
                        string baseURLDMS = configuration["ServiceURL:GENERIC"];
                        client.BaseAddress = new Uri(baseURLDMS);
                        using (FileStream stream = new FileStream(saveToPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                        var fileStream = System.IO.File.Open(saveToPath, FileMode.Open);
                        content.Add(new StreamContent(fileStream), "files", fileName);

                        var requestUri = client.BaseAddress + "RealityCheck/ValidateExternalSampleFile";
                        var request = new HttpRequestMessage(HttpMethod.Post, requestUri) { Content = content };
                        var result = client.Send(request);
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                        returnStatus = JsonSerializer.Deserialize<ResponseModel>(readTask.Result, options);
                        content.Dispose();
                    }
                    if (System.IO.File.Exists(saveToPath))
                    {
                        try
                        {
                            System.IO.File.Delete(saveToPath);
                        }
                        catch (Exception)
                        {
                        }

                    }

                }
                return Ok(returnStatus);
            }

            catch (Exception ex)
            {
                if (System.IO.File.Exists(saveToPath))
                {
                    System.IO.File.Delete(saveToPath);

                }
                LogUtility.WriteErrorLog(httpContextAccessor.HttpContext, ex, string.Empty, MethodBase.GetCurrentMethod().DeclaringType.ToString() + "." + MethodBase.GetCurrentMethod().Name, JsonSerializer.Serialize(1));
                returnStatus.Status = Status.Error;
                returnStatus.Message = "Sorry ! An error occured while processing your request. " + ex.Message.ToString();
                return Ok(returnStatus);
            }
        }
        #endregion
    }
}
