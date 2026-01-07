using Core.Enums.User;
using Core.Insfrastructure;
using Core.Models;
using Core.Models.User;
using Core.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace ApiGateway.Web.Controllers
{
    public class SSOIntegrationController : Controller
    {
        private IConfiguration Configuration;
        private readonly IHttpContextAccessor httpContextAccessor;
        public SSOIntegrationController(IConfiguration _configuration, IHttpContextAccessor httpContextAccessor)
        {
            Configuration = _configuration;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> SSOLogin()
        {

            string BackToSSoURL = this.Configuration["SSOURL:BackToSSO"];
            try
            {
                string ssoTokenNewUrl = this.Configuration["SSOURL:SSOTokenNew"];
                string ssoTokenUrl = this.Configuration["SSOURL:SSOToken"];
                string wsUsername = this.Configuration["SSOURL:WSUSERNAME"];
                string wsPassword = this.Configuration["SSOURL:WSPASSWORD"];
                SSOUserDetails userdetails = new SSOUserDetails();
                CommonSsoLoginModel commonSsoLoginModel = new CommonSsoLoginModel();
                SSOTokenDetails sSOTokenDetails = new SSOTokenDetails();
                string token = Request.Form["userdetails"];

                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response = httpClient.GetAsync(ssoTokenUrl + token).Result;

                if (response.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var stateInfo = response.Content.ReadAsStringAsync().Result;
                    dynamic result = JsonSerializer.Deserialize<Object>(stateInfo, options);
                    sSOTokenDetails = ConversionUtility.ConvertFromDynamicObject<SSOTokenDetails>(result);
                    if (sSOTokenDetails != null)
                    {
                        if (sSOTokenDetails.sAMAccountName != null)
                        {
                            // sSOTokenDetails.SsoToken = token;
                            // return PartialView("~/Views/SSOIntegration/_LoginPartial.cshtml", sSOTokenDetails);

                            string userProfileUrl = this.Configuration["SSOURL:UserProfileNew"];
                            userProfileUrl = userProfileUrl + "/" + sSOTokenDetails.sAMAccountName;
                            //SSOTokenDetails ssoTokenDetails = new SSOTokenDetails();
                            ResponseModel returnStatus = new ResponseModel(Core.Enums.User.Status.Alert);
                            using (var client = new HttpClient())
                            {
                                using (var request = new HttpRequestMessage(HttpMethod.Get, userProfileUrl))
                                {
                                    request.Headers.Add("SSO-TOKEN", token);
                                    request.Headers.Add("Authorization", "Basic " +
                                    Convert.ToBase64String(Encoding.Default.GetBytes(wsUsername + ":" + wsPassword)));
                                    using (var responseSsoUser = client.Send(request))
                                    {
                                        if (responseSsoUser.IsSuccessStatusCode)
                                        {
                                            var optionsSso = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                                            var stateInfoSso = responseSsoUser.Content.ReadAsStringAsync().Result;
                                            dynamic resultSso = JsonSerializer.Deserialize<Object>(stateInfoSso, optionsSso);
                                            userdetails = ConversionUtility.ConvertFromDynamicObject<SSOUserDetails>(resultSso);
                                            if (userdetails != null)
                                            {
                                                commonSsoLoginModel.ssoUserDetails = userdetails;
                                                commonSsoLoginModel.ssoTokenDetails = sSOTokenDetails;

                                                //  var apiParams = new Dictionary<string, string>();
                                                //   apiParams.Add("ssoUserDetails", userdetails.ToString());
                                                // apiParams.Add("ssoTokenDetails", sSOTokenDetails.ToString());

                                                int UserType = 1;
                                                if (sSOTokenDetails.sAMAccountName != null)
                                                {
                                                    if (sSOTokenDetails.Roles.Count() > 0)
                                                    {
                                                        UserType = 2;
                                                    }
                                                }
                                                returnStatus = await CallPostWebAPIWithoutSession<dynamic>("UserAuthenticate/ValidateSSOUserDetails", commonSsoLoginModel, this.Configuration["ServiceURL:UPM"]);
                                                string RedirectKey = returnStatus.FilteredRecordCount.ToString();
                                                var AuthId = "";
                                                var Authkey = "";
                                                var ReactURL = returnStatus.SaveOption.ToLower();
                                                Int64 LoginLogId = 0;
                                                Int64 ProfileId = 0;

                                                if (returnStatus.Status == Status.Success)
                                                {
                                                    returnStatus = await CallPostWebAPIWithoutSession<dynamic>("UserAuthenticate/CitizenLoginWithSSOId", commonSsoLoginModel, this.Configuration["ServiceURL:Master"]);

                                                    if (returnStatus.Status == Status.Success)
                                                    {
                                                        UserSessionModel userSession = JsonSerializer.Deserialize<UserSessionModel>(returnStatus.CustomObject, optionsSso);
                                                        if (userSession != null)
                                                        {
                                                            LoginLogId = userSession.LoginLogId;
                                                            ProfileId = userSession.ProfileId;
                                                            returnStatus.Status = Status.Success;
                                                            returnStatus.Message = EnumUtility.GetDescription(Core.Enums.ReturnMessage.LoginSuccessfully);
                                                            AuthId = Core.Utils.Security.EncryptionUtility.GenerateAuthId(Convert.ToInt64(userSession.UserId), Convert.ToInt64(userSession.LoginLogId), Convert.ToInt64(userSession.ProfileId), Convert.ToInt16(userSession.UserTypeId));
                                                            Authkey = Core.Utils.Security.EncryptionUtility.GenerateAuthKey(httpContextAccessor.HttpContext.Request, Convert.ToString(userSession.LoginLogId));
                                                            HttpContext.Response.Headers.Add("AuthId", AuthId);
                                                            HttpContext.Response.Headers.Add("AuthKey", Authkey);
                                                            HttpContext.Response.Headers.Add("Language", "en");
                                                            HttpContext.Response.Headers.Add("FormURL", "");
                                                            HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "AuthId, AuthKey, Language, FormURL");

                                                        }
                                                    }
                                                }
                                                ViewBag.AuthId = AuthId;
                                                ViewBag.AuthKey = Authkey;
                                                ViewBag.SsoToken = token.ToString();
                                                ViewBag.ComeFrom = "SSOPortal";
                                                ViewBag.RedirectKey = RedirectKey;
                                                ViewBag.ReactURL = ReactURL.ToString();
                                                ViewBag.Language = "en";
                                                ViewBag.UserType = UserType;
                                                ViewBag.LoginLogId = LoginLogId;
                                                ViewBag.SsoURL = BackToSSoURL;
                                                ViewBag.ProfileId = ProfileId;


                                                return View("~/Views/SSOIntegration/SSOLogin.cshtml");

                                            }
                                        }
                                    }
                                }
                            }

                        }

                    }
                }

                return PartialView("~/Views/SSOIntegration/PopUp.cshtml");
            }
            catch (Exception ex)
            {
                LogUtility.WriteEventErrorLog(ex, string.Empty, MethodBase.GetCurrentMethod().DeclaringType.ToString() + "." + MethodBase.GetCurrentMethod().Name, JsonSerializer.Serialize(1));
                return Redirect(BackToSSoURL);//send back to sso
            }
        }
        public async Task<IActionResult> SsoDetail(SSOTokenDetails ssoTokenDetails)
        {

            ResponseModel returnStatus = new ResponseModel(Core.Enums.User.Status.Alert);
            SSOUserDetails userdetails = new SSOUserDetails();
            CommonSsoLoginModel commonSsoLoginModel = new CommonSsoLoginModel();
            string landingURL = this.Configuration["landingURL"];

            string wsUsername = this.Configuration["SSOURL:WSUSERNAME"];
            string wsPassword = this.Configuration["SSOURL:WSPASSWORD"];
            string userProfileUrl = this.Configuration["SSOURL:UserProfileNew"];
            userProfileUrl = userProfileUrl + "/" + ssoTokenDetails.sAMAccountName;
            if (ssoTokenDetails != null && !string.IsNullOrEmpty(ssoTokenDetails.sAMAccountName))
            {
                using (var client = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(HttpMethod.Get, userProfileUrl))
                    {
                        request.Headers.Add("SSO-TOKEN", ssoTokenDetails.SsoToken);
                        request.Headers.Add("Authorization", "Basic " +
                        Convert.ToBase64String(Encoding.Default.GetBytes(wsUsername + ":" + wsPassword)));
                        using (var response = client.Send(request))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                                var stateInfo = response.Content.ReadAsStringAsync().Result;
                                dynamic result = JsonSerializer.Deserialize<Object>(stateInfo, options);
                                userdetails = ConversionUtility.ConvertFromDynamicObject<SSOUserDetails>(result);
                                if (userdetails != null)
                                {
                                    commonSsoLoginModel.ssoUserDetails = userdetails;
                                    commonSsoLoginModel.ssoTokenDetails = ssoTokenDetails;
                                    var apiParams = new Dictionary<string, string>();
                                    apiParams.Add("ssoUserDetails", userdetails.ToString());
                                    apiParams.Add("ssoTokenDetails", ssoTokenDetails.ToString());

                                    returnStatus = await CallPostWebAPIWithoutSession<dynamic>("UserAuthenticate/ValidateSSOUserDetails", commonSsoLoginModel, this.Configuration["ServiceURL:Master"]);
                                    string RedirectKey = returnStatus.FilteredRecordCount.ToString();
                                    var AuthId = "";
                                    var Authkey = "";
                                    if (returnStatus.Status == Core.Enums.User.Status.Success)
                                    {
                                        returnStatus.CustomObject = await CallPostWebAPIWithoutSession<dynamic>("UserAuthenticate/CitizenLoginWithSSOId", commonSsoLoginModel, this.Configuration["ServiceURL:Master"]);

                                        if (returnStatus.Status == Status.Success)
                                        {
                                            string customObject = JsonSerializer.Serialize<UserSessionModel>(returnStatus.CustomObject, options);
                                            UserSessionModel userSession = JsonSerializer.Deserialize<UserSessionModel>(customObject, options);
                                            if (userSession != null)
                                            {

                                                returnStatus.Status = Core.Enums.User.Status.Success;
                                                returnStatus.Message = EnumUtility.GetDescription(Core.Enums.ReturnMessage.LoginSuccessfully);
                                                AuthId = Core.Utils.Security.EncryptionUtility.GenerateAuthId(Convert.ToInt64(userSession.UserId), Convert.ToInt64(userSession.LoginLogId), Convert.ToInt64(userSession.ProfileId), Convert.ToInt16(userSession.UserTypeId));
                                                Authkey = Core.Utils.Security.EncryptionUtility.GenerateAuthKey(httpContextAccessor.HttpContext.Request, Convert.ToString(userSession.LoginLogId));
                                                HttpContext.Response.Headers.Add("AuthId", AuthId);
                                                HttpContext.Response.Headers.Add("AuthKey", Authkey);
                                                HttpContext.Response.Headers.Add("Language", "en");
                                                HttpContext.Response.Headers.Add("FormURL", "");
                                                HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "AuthId, AuthKey, Language, FormURL");
                                                //ViewBag.AuthId = AuthId;
                                                //ViewBag.AuthKey = Authkey;
                                                ////ViewBag.SsoToken = ssoTokenDetails.SsoToken;
                                                //ViewBag.ReactURL = "";
                                                //ViewBag.Language = "Eng";
                                                //ViewBag.ComeFrom = "SSOPortal";
                                                //  ViewBag.IsMobileUpdate = 0;//or 1
                                                //  ViewBag.IsProfileIncomplete = 0; // 1 or 2
                                                // ViewBag.SSOMobileNo = 0;

                                                //  return View("~/Views/SSOIntegration/SSOLogin.cshtml", userSession);
                                            }
                                        }
                                    }
                                    ViewBag.AuthId = AuthId;
                                    ViewBag.AuthKey = Authkey;
                                    ViewBag.SsoToken = ssoTokenDetails.SsoToken;
                                    ViewBag.ComeFrom = "SSOPortal";
                                    ViewBag.RedirectKey = RedirectKey;
                                    ViewBag.ReactURL = "";
                                    ViewBag.Language = "Eng";

                                    return View("~/Views/SSOIntegration/SSOLogin.cshtml");

                                }
                            }
                        }
                    }
                }
                return PartialView("~/Views/SSOIntegration/PopUp.cshtml");
            }
            else
            {
                return PartialView("~/Views/SSOIntegration/PopUp.cshtml");
            }
        }
        public ActionResult BackToSSO()
        {
            string BackToSSoURL = this.Configuration["SSOURL:BackToSSO"];

            return Redirect(BackToSSoURL);

        }
        protected async Task<dynamic> CallPostWebAPIWithoutSession<T>(string apiNameWithParameter, T postModel, string baseAddress)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    PrepareHttpClientRequest(client, baseAddress, false);
                    baseAddress = client.BaseAddress.ToString();
                    var result = await client.PostAsJsonAsync<T>(apiNameWithParameter, postModel);
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var responseString = await result.Content.ReadAsStringAsync();
                    ResponseModel responseModel = JsonSerializer.Deserialize<ResponseModel>(responseString, options);
                    return responseModel;
                }
            }
            catch (Exception)
            {
                //ServiceUserSession.writeErrorLog("Base Address=" + baseAddress + "apiNameWithParameter=" + apiNameWithParameter, "Error in CallPostWebAPI", ex.ToString());
                //Logs.Log.WriteErrorLog("Base Address=" + baseAddress + "apiNameWithParameter=" + apiNameWithParameter, Logs.ErrorType.HTTP, ex);
                throw;
            }
        }
        public dynamic CallPostWebAPIWithoutSessionSync<T>(string apiNameWithParameter, T postModel, string baseAddress)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    PrepareHttpClientRequest(client, baseAddress, false);
                    baseAddress = client.BaseAddress.ToString();
                    var responseTask = client.PostAsJsonAsync<T>(apiNameWithParameter, postModel);
                    responseTask.Wait();
                    var result = responseTask.Result;
                    var responseString = result.Content.ReadAsStringAsync();
                    responseString.Wait();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    ResponseModel responseModel = JsonSerializer.Deserialize<ResponseModel>(responseString.Result, options);
                    return responseModel;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private void PrepareHttpClientRequest(HttpClient client, string baseAddress, bool addSession = true)
        {
            try
            {
                if (addSession == true)
                    client.DefaultRequestHeaders.Add("UserSessionObject", Session.SeriaizeUserSessionData());

                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("UserIPAddress", httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString());
                foreach (var header in httpContextAccessor.HttpContext.Request.Headers)
                {
                    string headerName = header.Key;
                    string headerContent = string.Join(",", header.Value.ToArray());
                    client.DefaultRequestHeaders.TryAddWithoutValidation(headerName, headerContent);
                }
            }
            catch (Exception)
            {
                //throw ex;
            }
        }
    }
}
