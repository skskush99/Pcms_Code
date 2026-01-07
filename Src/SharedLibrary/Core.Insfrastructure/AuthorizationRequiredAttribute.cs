using Core.Enums;
using Core.Enums.User;
using Core.Models;
using Core.Utils;
using Core.Utils.Security;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Core.Insfrastructure
{
#nullable disable
    [AttributeUsage(AttributeTargets.Class)]
    public class AuthorizationRequiredAttribute : Attribute, IAuthorizationFilter
    {
        private readonly IConfiguration configuration;

        public AuthorizationRequiredAttribute(IConfiguration _configuration)
        {
            this.configuration = _configuration;
        }
        private const string AuthId = "AuthId";
        private const string AuthKey = "AuthKey";
        private const string AuthUserId = "AuthUserId";
        private const string AuthUserPwd = "AuthUserPwd";
        private const string AuthToken = "AuthToken";
        private const string AuthTokenKey = "AuthTokenKey";
        private const string ArchitectToken = "ArchitectToken";
        private const string ArchitectTokenKey = "ArchitectTokenKey";
        private const string AdminAuthId = "AdminAuthId";
        private const string AdminAuthKey = "AdminAuthKey";
        private const string FormURL = "FormURL";
        Language userLanguage = Language.English;
        Int64 userId = 0;
        Int32 roleId = 0;
        Int32 typeId = 0;
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            if (filterContext.HttpContext.Request.Headers.Keys.Contains(AuthKey, StringComparer.OrdinalIgnoreCase) && filterContext.HttpContext.Request.Headers.Keys.Contains(AuthId, StringComparer.OrdinalIgnoreCase))
            {
                string formURLValue = string.Empty;
                string AuthIdValue = filterContext.HttpContext.Request.Headers[AuthId].ToString();
                string AuthKeyValue = filterContext.HttpContext.Request.Headers[AuthKey].ToString();
                //var jj = new StreamReader(filterContext.HttpContext.Request.Body).ReadToEnd(); 
                //var jsonoptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                //var apiString = new StreamReader(filterContext.HttpContext.Request.Body).ReadToEndAsync();
                //ApiRequestModel apiRequestModel = JsonSerializer.Deserialize<ApiRequestModel>(apiString.Result, jsonoptions);
                //string apiKey = apiRequestModel.ApiKey.ToString();
                //string apiKey = "GetModuleList";

                if (filterContext.HttpContext.Request.Headers.Keys.Contains(FormURL, StringComparer.OrdinalIgnoreCase))
                {
                    formURLValue = filterContext.HttpContext.Request.Headers[FormURL].ToString();
                    formURLValue = formURLValue.Substring(1);

                }
                //formURLValue = "/UPM/OfficerModule/DepartmentType";
                string plainAuthId = EncryptionUtility.Decrypt(AuthIdValue);
                if (plainAuthId.Contains("~"))
                {
                    string[] ids = plainAuthId.Split('~');
                    if (Session.UserSession != null)
                    {
                        userId = Session.UserSession.UserId;
                        roleId = Session.UserSession.RoleId;
                        typeId = Session.UserSession.TypeId;

                    }
                    //Int64 userId = Convert.ToInt32(ids[0]);// old code
                    long loginLogId = Convert.ToInt64(ids[1]);
                    Int64 profileId = Convert.ToInt32(ids[2]);
                    Int16 userTypeId = 0;
                    if (ids.Length > 3)
                    {
                        userTypeId = Convert.ToInt16(ids[3]);
                    }

                    if (Utils.Security.EncryptionUtility.GenerateAuthKey(filterContext.HttpContext.Request, loginLogId.ToString()) == AuthKeyValue)
                    {//
                        //string baseURL = "https://localhost:7127/api/";//configuration["ServiceURL:URM"]; //BaseApiController.GetAPIBaseURL("URM");
                        //if (!filterContext.HttpContext.Request.Headers["Host"].ToString().ToLower().Contains("localhost"))
                        //    baseURL = "http://193.16.100.17:98/api/";
                        string baseURL = configuration["ServiceURL:Master"];
                        using (var client = new HttpClient())
                        {
                            //var jj = new StreamReader(filterContext.HttpContext.Request.Body).ReadToEndAsync();
                            //jj.Wait();
                            client.BaseAddress = new Uri(baseURL);
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            var responseTask = client.GetAsync("UserAuthenticate/ValidateAndGetUserSession?OrgId=" + 0 + "&loginLogId=" + loginLogId + "&profileId=" + profileId + "&userId=" + userId + "&roleId=" + roleId + "&typeId=" + typeId + "&formURLValue=" + formURLValue + "&requestSource=WEB-PORTAL");
                            responseTask.Wait();
                            var result = responseTask.Result;
                            var readTask = result.Content.ReadAsStringAsync();
                            readTask.Wait();
                            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                            //ResponseModel returnStatus = ConversionUtility.ConvertFromDynamicObject<ResponseModel>(readTask.Result);
                            ResponseModel returnStatus = JsonSerializer.Deserialize<ResponseModel>(readTask.Result, options);
                            if (returnStatus.Status == Core.Enums.User.Status.Success)
                            {
                                if (filterContext.HttpContext.Request.Headers.Keys.Contains("Language", StringComparer.OrdinalIgnoreCase))
                                {

                                    string lang = filterContext.HttpContext.Request.Headers["Language"].ToString().ToLower();
                                    if (lang == "hi")
                                        userLanguage = Language.Hindi;
                                }
                                Session.SetUserSession(ConversionUtility.ConvertFromDynamicObject<UserSessionModel>(returnStatus.CustomObject), userLanguage);

                            }
                            else
                            {
                                var sessionDataNew = filterContext.HttpContext.Request.Headers["UserSessionObject"].ToList();
                                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                                filterContext.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Not Authorized";
                                if (returnStatus.Status == Status.SessionExpired)
                                {
                                    filterContext.Result = new JsonResult("NotAuthorized")
                                    {
                                        Value = new
                                        {
                                            //Status = Status.TokenExpired,
                                            Status = EnumUtility.GetDescription(Status.SessionExpired),
                                            Message = returnStatus.Message.ToString(),
                                            UserType = userTypeId
                                        },
                                    };
                                }
                                else
                                {
                                    filterContext.Result = new JsonResult("NotAuthorized")
                                    {
                                        Value = new
                                        {
                                            Status = "Error",
                                            Message = "Invalid Token :" + returnStatus.Message
                                        },
                                    };
                                }
                            }
                        }
                    }
                    else
                    {
                        filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        filterContext.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Not Authorized";
                        filterContext.Result = new JsonResult("NotAuthorized")
                        {
                            Value = new
                            {
                                Status = "Error",
                                Message = EnumUtility.GetDescription(Enums.ReturnMessage.AuthKeyMismatch)
                            },
                        };
                    }
                }
                else
                {
                    filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    filterContext.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Not Authorized";
                    filterContext.Result = new JsonResult("NotAuthorized")
                    {
                        Value = new
                        {
                            Status = "Error",
                            Message = EnumUtility.GetDescription(Enums.ReturnMessage.AuthIdMismatch)
                        },
                    };
                }
            }
            else if (filterContext.HttpContext.Request.Headers.Keys.Contains("UserSessionObject", StringComparer.OrdinalIgnoreCase))
            {
                string sessionData = filterContext.HttpContext.Request.Headers["UserSessionObject"].First().ToString();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                if (filterContext.HttpContext.Request.Headers.ContainsKey("Language"))
                {

                    string lang = filterContext.HttpContext.Request.Headers["Language"].ToString().ToLower();
                    if (lang == "hi")
                        userLanguage = Language.Hindi;
                }
                ;
                Session.SetUserSession(JsonSerializer.Deserialize<UserSessionModel>(sessionData, options), userLanguage);
            }
            else
            {
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                filterContext.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Not Authorized";
                filterContext.Result = new JsonResult("NotAuthorized")
                {
                    Value = new
                    {
                        Status = "Error",
                        Message = EnumUtility.GetDescription(Enums.ReturnMessage.ModelError)
                    },
                };
            }
        }
    }
}
