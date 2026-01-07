using Core.Enums.User;
using Core.Insfrastructure;
using Core.Insfrastructure.Controller;
using Core.Models;
using Core.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace ApiGateway.Web.Controllers
{
    public class AuthenticateController : BaseApiController
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public AuthenticateController(IConfiguration Configuration, IHttpContextAccessor _httpContextAccessor) : base(Configuration, _httpContextAccessor)
        {
            httpContextAccessor = _httpContextAccessor;
        }
        [HttpPost("ValidateLogin")]
        public async Task<IActionResult> PostValidateLogin(ApiRequestModel apiRequestModel)
        {
            ResponseModel returnStatus = new ResponseModel(Status.Alert);
            try
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                returnStatus = await GetApiConfigModel((Int32)apiRequestModel.OrgId, apiRequestModel.ApiKey, "AuthenticateController", "PostValidateLogin");
                if (returnStatus.Status == Status.Success)
                {
                    APIConfigModel apiConfig = JsonSerializer.Deserialize<APIConfigModel>(returnStatus.CustomObject, options);
                    var url = configuration["ServiceURL:" + apiConfig.ModuleName.ToUpper() + ""];
                    ResponseModel output = await CallPostWebAPIWithoutSession<dynamic>(apiConfig.FullPath + "?orgId=" + apiRequestModel.OrgId, apiRequestModel.ApiParams, url);
                    if (output.Status == Status.Success)
                    {
                        UserSessionModel userSession = JsonSerializer.Deserialize<UserSessionModel>(output.CustomObject, options);
                        HttpContext.Response.Headers.Add("AuthId", Core.Utils.Security.EncryptionUtility.GenerateAuthId(Convert.ToInt64(userSession.UserId), Convert.ToInt64(userSession.LoginLogId), Convert.ToInt64(userSession.ProfileId), Convert.ToInt16(userSession.UserType)));
                        HttpContext.Response.Headers.Add("AuthKey", Core.Utils.Security.EncryptionUtility.GenerateAuthKey(httpContextAccessor.HttpContext.Request, Convert.ToString(userSession.LoginLogId)));
                        HttpContext.Response.Headers.Add("Language", "en");
                        HttpContext.Response.Headers.Add("FormURL", "");
                        HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "AuthId, AuthKey, Language, FormURL");

                    }
                    return Ok(output);
                }
                else
                    return Ok(returnStatus);
            }
            catch (Exception)
            {
                // LogUtility.WriteErrorLog(System.Web.HttpContext.Current, ex, UserSession.LoginId.GetString(), "Gateway_AuthServiceController.PostValidateLogin", JsonSerializer.Serialize(apiRequestModel));
                returnStatus.Status = Status.Error;
                returnStatus.Message = "Sorry ! An error occured while processing your request. Please try after some time.";
                return Ok();
            }
        }

        [HttpPost("ValidateLoginWithOTP")]
        public async Task<IActionResult> PostValidateLoginWithOTP(ApiRequestModel apiRequestModel)
        {
            ResponseModel returnStatus = new ResponseModel(Status.Alert);
            try
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                returnStatus = await GetApiConfigModel((Int32)apiRequestModel.OrgId, apiRequestModel.ApiKey, "AuthenticateController", "PostValidateLoginWithOTP");
                // var urlNew = configuration["ServiceURL:" + apiRequestModel.Module.ToUpper() + ""];
                // returnStatus = await CallPostWebAPIWithoutSession<dynamic>(apiRequestModel.Path + "?orgId=" + apiRequestModel.OrgId, apiRequestModel, urlNew);

                if (returnStatus.Status == Status.Success)
                {
                    APIConfigModel apiConfig = JsonSerializer.Deserialize<APIConfigModel>(returnStatus.CustomObject, options);
                    var url = configuration["ServiceURL:" + apiConfig.ModuleName.ToUpper() + ""];
                    ResponseModel output = await CallPostWebAPIWithoutSession<dynamic>(apiConfig.FullPath + "?orgId=" + apiRequestModel.OrgId, apiRequestModel.ApiParams, url);
                    if (output.Status == Status.Success)
                    {
                        JsonNode paramsValue = JsonSerializer.Deserialize<JsonNode>(output.CustomObject);
                        if (paramsValue == null || paramsValue["userinfo"] == null)
                        {
                            returnStatus.Status = Status.Alert;
                            returnStatus.Message = EnumUtility.GetDescription(ReturnMessage.InvalidParametersOrDataDoesNotExistForRequestedParameters);
                            returnStatus.CustomObject = null;
                            return Ok(returnStatus);
                        }
                        UserSessionModel userSession = JsonSerializer.Deserialize<UserSessionModel>(paramsValue["userinfo"].ToString(), options);
                        HttpContext.Response.Headers.Add("AuthId", Core.Utils.Security.EncryptionUtility.GenerateAuthId(Convert.ToInt64(userSession.UserId), Convert.ToInt64(userSession.LoginLogId), Convert.ToInt64(userSession.ProfileId), Convert.ToInt16(userSession.UserType)));
                        HttpContext.Response.Headers.Add("AuthKey", Core.Utils.Security.EncryptionUtility.GenerateAuthKey(httpContextAccessor.HttpContext.Request, Convert.ToString(userSession.LoginLogId)));
                        HttpContext.Response.Headers.Add("Language", "en");
                        HttpContext.Response.Headers.Add("FormURL", "");
                        HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "AuthId, AuthKey, Language, FormURL");
                    }
                    return Ok(output);
                }
                else
                    return Ok(returnStatus);
            }
            catch (Exception)
            {
                // LogUtility.WriteErrorLog(System.Web.HttpContext.Current, ex, UserSession.LoginId.GetString(), "Gateway_AuthServiceController.PostValidateLogin", JsonSerializer.Serialize(apiRequestModel));
                returnStatus.Status = Status.Error;
                returnStatus.Message = "Sorry ! An error occured while processing your request. Please try after some time.";
                return Ok();
            }
        }

        [HttpPost("GetUserIdWiseModuleList")]
        public async Task<IActionResult> PostGetUserIdWiseModuleList(ApiRequestModel apiRequestModel)
        {
            ResponseModel returnStatus = new ResponseModel(Status.Alert);
            try
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                returnStatus = await GetApiConfigModel((Int32)apiRequestModel.OrgId, apiRequestModel.ApiKey, "MenuObjectsController", "GetModuleList");
                //returnStatus = await GetApiConfigModel((Int32)apiRequestModel.OrgId, apiRequestModel.ApiKey, "Gateway_PostAuthAdminServiceController", "PostGetData");
                if (returnStatus.Status == Status.Success)
                {
                    APIConfigModel apiConfig = JsonSerializer.Deserialize<APIConfigModel>(returnStatus.CustomObject, options);
                    var url = configuration["ServiceURL:" + apiConfig.ModuleName.ToUpper() + ""];
                    ResponseModel output = await CallPostWebAPIWithoutSession<dynamic>(apiConfig.FullPath + "?orgId=" + apiRequestModel.OrgId, apiRequestModel.ApiParams, url);
                    return Ok(output);
                }
                else
                    return Ok(returnStatus);
            }
            catch (Exception)
            {
                // LogUtility.WriteErrorLog(System.Web.HttpContext.Current, ex, UserSession.LoginId.GetString(), "Gateway_AuthServiceController.PostValidateLogin", JsonSerializer.Serialize(apiRequestModel));
                returnStatus.Status = Status.Error;
                returnStatus.Message = "Sorry ! An error occured while processing your request. Please try after some time.";
                return Ok();
            }
        }

        [HttpPost("LoginWithOutOtp")]
        public async Task<IActionResult> PostLoginWithOutOtp(ApiRequestModel apiRequestModel)
        {
            ResponseModel returnStatus = new ResponseModel(Status.Alert);
            try
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                returnStatus = await GetApiConfigModel((Int32)apiRequestModel.OrgId, apiRequestModel.ApiKey, "AuthenticateController", "PostLoginWithOutOtp");


                if (returnStatus.Status == Status.Success)
                {
                    APIConfigModel apiConfig = JsonSerializer.Deserialize<APIConfigModel>(returnStatus.CustomObject, options);
                    var url = configuration["ServiceURL:" + apiConfig.ModuleName.ToUpper() + ""];
                    ResponseModel output = await CallPostWebAPIWithoutSession<dynamic>(apiConfig.FullPath + "?orgId=" + apiRequestModel.OrgId, apiRequestModel.ApiParams, url);
                    if (output.Status == Status.Success)
                    {
                        JsonNode paramsValue = JsonSerializer.Deserialize<JsonNode>(output.CustomObject);
                        if (paramsValue == null || paramsValue["userinfo"] == null)
                        {
                            returnStatus.Status = Status.Alert;
                            returnStatus.Message = EnumUtility.GetDescription(ReturnMessage.InvalidParametersOrDataDoesNotExistForRequestedParameters);
                            returnStatus.CustomObject = null;
                            return Ok(returnStatus);
                        }
                        UserSessionModel userSession = JsonSerializer.Deserialize<UserSessionModel>(paramsValue["userinfo"].ToString(), options);
                        HttpContext.Response.Headers.Add("AuthId", Core.Utils.Security.EncryptionUtility.GenerateAuthId(Convert.ToInt64(userSession.UserId), Convert.ToInt64(userSession.LoginLogId), Convert.ToInt64(userSession.ProfileId), Convert.ToInt16(userSession.UserType)));
                        HttpContext.Response.Headers.Add("AuthKey", Core.Utils.Security.EncryptionUtility.GenerateAuthKey(httpContextAccessor.HttpContext.Request, Convert.ToString(userSession.LoginLogId)));
                        HttpContext.Response.Headers.Add("Language", "en");
                        HttpContext.Response.Headers.Add("FormURL", "");
                        HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "AuthId, AuthKey, Language, FormURL");
                    }
                    return Ok(output);
                }
                else
                    return Ok(returnStatus);
            }
            catch (Exception)
            {
                // LogUtility.WriteErrorLog(System.Web.HttpContext.Current, ex, UserSession.LoginId.GetString(), "Gateway_AuthServiceController.PostValidateLogin", JsonSerializer.Serialize(apiRequestModel));
                returnStatus.Status = Status.Error;
                returnStatus.Message = "Sorry ! An error occured while processing your request. Please try after some time.";
                return Ok();
            }
        }
    }
}
