using Core.Models.User;
using Core.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;

namespace ApiGateway.Web.Controllers
{
    public class SsoLoginController : Controller
    {
        private IConfiguration Configuration;
        private readonly IHttpContextAccessor httpContextAccessor;
        public SsoLoginController(IConfiguration _configuration, IHttpContextAccessor httpContextAccessor)
        {
            Configuration = _configuration;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<ActionResult> Index()
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
                            //ResponseModel returnStatus = new ResponseModel(Enums.URM.Status.Alert);
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

                                                int UserType = 1;
                                                if (sSOTokenDetails.sAMAccountName != null)
                                                {
                                                    if (sSOTokenDetails.Roles.Count() > 0)
                                                    {
                                                        UserType = 2;
                                                    }
                                                }
                                                var ReturnUrl = this.Configuration["AngualarURL"] + "ssologin?token=" + token;
                                                return Redirect(ReturnUrl);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return PartialView("~/Views/SSOIntegration/PopUp.cshtml");
                //return Redirect("http://10.70.236.252/ApiGatewayService/swagger/index.html");//
            }
            catch (Exception ex)
            {
                return Redirect(BackToSSoURL);//send back to sso
            }
        }
    }
}
