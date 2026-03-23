using Authentication.Dto.Shared;
using Authentication.ServiceBus.UnitOfWork;
using Core.Models.User;
using Core.Utils;
using JwtAuthenticationManager;
using JwtAuthenticationManager.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Text;
using System.Text.Json;

using Core.SsoEncryption;
using System.Diagnostics.Eventing.Reader;


namespace Authentication.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtTokenHandler _jwtTokenHandler;
        private readonly IUnitOfWorkService _unitOfWork;
        private IConfiguration Configuration;
        private readonly IHttpContextAccessor httpContextAccessor;
        public AuthController(IConfiguration _configuration, IHttpContextAccessor httpContextAccessor, JwtTokenHandler jwtTokenHandler, IUnitOfWorkService unitOfWork)
        {
            Configuration = _configuration;
            this.httpContextAccessor = httpContextAccessor;
            _jwtTokenHandler = jwtTokenHandler ?? throw new ArgumentNullException(nameof(jwtTokenHandler));
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("Authenticate")]
        public ActionResult<AuthenticationResponse?> Authenticate([FromBody] AuthenticationRequest authenticationRequest)
        {

            var authenticationResponse = _jwtTokenHandler.GenerateAuthToken(authenticationRequest);
            if (authenticationResponse == null) return Unauthorized();
            return authenticationResponse;
        }

        [HttpPost]
        [Route("SSOAuth")]
        public ActionResult<AuthenticationResponse?> SSOAuthenticate([FromBody] SSOAuthenticationRequest authenticationRequest)
        {
            //Add SSO token parsing and other userinfo.
            var userauthenticationRequest = new AuthenticationRequest
            {
                UserName = "admin",
                Password = "admin@123",
            };

            var authenticationResponse = _jwtTokenHandler.GenerateAuthToken(userauthenticationRequest);
            if (authenticationResponse == null) return Unauthorized();
            return authenticationResponse;
        }

        [HttpGet]
        [Route("SSO")]
        //[Consumes("application/x-www-form-urlencoded")]
        public async Task<RedirectResult?> SSOLanding([FromForm] SSORequest data)
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
                                                //var response = new HttpResponseMessage(HttpStatusCode.Redirect);
                                                //response.Headers.Location = new Uri(ReturnUrl);
                                                //return response;
                                                return RedirectPermanent(ReturnUrl);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return Redirect("~/Views/SSOIntegration/PopUp.cshtml");
                //return Redirect("http://10.70.236.252/ApiGatewayService/swagger/index.html");//
            }
            catch (Exception ex)
            {
                return Redirect(BackToSSoURL);//send back to sso
            }
        }

        [HttpPost]
        [Route("SSOOld")]
        public async Task<ActionResult<List<AuthenticationResponse>?>> AuthenticateNew1([FromForm] AuthenticationRequest authenticationRequest)
        {
            authenticationRequest.IsSSOLogin = true;
            var obj = new LoginModel
            {
                SSOToken = authenticationRequest.UserDetails,
                UserName = "PCMS.TEST",
                Password = "R@jS$opcm21#",
                IsSSOLogin = authenticationRequest.IsSSOLogin,
                IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString()
                //IPAddress = authenticationRequest.IPAddress
            };
            var data = await _unitOfWork.UserLogins.SSOLogin(obj);
            var collection = ((IEnumerable)data.Data).Cast<Authentication.Dto.Shared.LoginDetailsModel>().ToList();
            var authenticationResponse = _jwtTokenHandler.GenerateAuthToken(collection);
            if (authenticationResponse == null) return Unauthorized();
            return authenticationResponse;
        }

        [HttpPost]
        [Route("NewAuth")]
        public async Task<IActionResult> AuthenticateNew([FromBody] AuthenticationRequest authenticationRequest)
        {
            LoginModel obj;

            if (authenticationRequest.IsSSOLogin)
            {
                obj = new LoginModel
                {
                    SSOToken = authenticationRequest.SSOToken,
                    UserName = "PCMS.TEST",
                    Password = "R@jS$opcm21#",
                    IsSSOLogin = true,
                    IPAddress = authenticationRequest.IPAddress
                };
            }
            else
            {
                obj = new LoginModel
                {
                    UserName = authenticationRequest.UserName,
                    Password = authenticationRequest.Password,
                    IsSSOLogin = false,
                    IPAddress = authenticationRequest.IPAddress
                };
            }

            var result = await _unitOfWork.UserLogins.SSOLogin(obj);

            // ❌ FAIL OR ONLY MAPPING INFO
            if (!result.Status)
            {
                if (!result.UserMappingReq)
                {
                    return Ok(new
                    {
                        status = false,
                        UserMappingReq = false,
                        message = result.Message,
                        data = result.Data
                    });
                }
                else
                {
                    return Ok(new
                    {
                        status = false,
                        UserMappingReq = true,
                        message = result.Message,
                        data = result.Data
                    });
                }

            }

            //// 🔹 IF DATA IS NOT LOGINDETAILSMODEL → DON'T CREATE JWT
            //if (result.Data is IEnumerable<LoginDetails2Model>)
            //{
            //    return Ok(new
            //    {
            //        status = false,
            //        UserMappingReq = true,
            //        message = result.Message,
            //        data = result.Data   // mapping info
            //    });
            //}

            // ✅ REAL LOGIN → CREATE JWT
            var loginDetails = ((IEnumerable)result.Data).Cast<LoginDetailsModel>().ToList();

            var tokenResponse = _jwtTokenHandler.GenerateAuthToken(loginDetails);

            if (tokenResponse == null)
            {
                return Unauthorized(new
                {
                    status = false,
                    message = "Token generation failed."
                });
            }

            return Ok(new
            {
                status = true,
                UserMappingReq = false,
                message = result.Message,
                data = tokenResponse
            });
        }




        //[HttpPost]
        //[Route("NewAuth")]

        //public async Task<ActionResult<List<AuthenticationResponse>?>> AuthenticateNew([FromBody] AuthenticationRequest authenticationRequest)
        // {
        //     var obj = new LoginModel();
        //     if (authenticationRequest.IsSSOLogin == true)
        //     {
        //         obj = new LoginModel
        //         {
        //             SSOToken = authenticationRequest.SSOToken,
        //             UserName = "PCMS.TEST",
        //             Password = "R@jS$opcm21#",
        //             IsSSOLogin = authenticationRequest.IsSSOLogin,
        //             IPAddress = authenticationRequest.IPAddress
        //         };
        //     }
        //     else
        //     {
        //         obj = new LoginModel
        //         {
        //             SSOToken = authenticationRequest.SSOToken,
        //             UserName = authenticationRequest.UserName,
        //             Password = authenticationRequest.Password,
        //             IsSSOLogin = authenticationRequest.IsSSOLogin,
        //             IPAddress = authenticationRequest.IPAddress
        //         };
        //     }
        //     var data = await _unitOfWork.UserLogins.SSOLogin(obj);
        //     // 🔴 FAIL CASE → Message frontend
        //     if (!data.Status)
        //     {
        //         return Ok(new
        //         {
        //             status = false,
        //             message = data.Message,
        //             data = data.Data   // SSOId 
        //         });
        //     }
        //     // ✅ SUCCESS CASE
        //     var collection = ((IEnumerable)data.Data).Cast<Authentication.Dto.Shared.LoginDetailsModel>().ToList();
        //     var authenticationResponse = _jwtTokenHandler.GenerateAuthToken(collection);

        //     if (authenticationResponse == null)
        //     {
        //         return Unauthorized();
        //     }
        //     return Ok(new
        //     {
        //         status = true,
        //         message = data.Message,
        //         data = authenticationResponse
        //     });
        //     //if (authenticationResponse == null) return new List<AuthenticationResponse>();//Unauthorized();
        //     //return authenticationResponse;
        // }



        //public async Task<ActionResult<List<AuthenticationResponse>?>> AuthenticateNew([FromBody] AuthenticationRequest authenticationRequest)
        //{
        //    var obj = new LoginModel();
        //    if (authenticationRequest.IsSSOLogin == true)
        //    {
        //        obj = new LoginModel
        //        {
        //            SSOToken = authenticationRequest.SSOToken,
        //            UserName = "PCMS.TEST",
        //            Password = "R@jS$opcm21#",
        //            IsSSOLogin = authenticationRequest.IsSSOLogin,
        //            IPAddress = authenticationRequest.IPAddress
        //        };
        //    }
        //    else
        //    {
        //        obj = new LoginModel
        //        {
        //            SSOToken = authenticationRequest.SSOToken,
        //            UserName = authenticationRequest.UserName,
        //            Password = authenticationRequest.Password,
        //            IsSSOLogin = authenticationRequest.IsSSOLogin,
        //            IPAddress = authenticationRequest.IPAddress
        //        };
        //    }
        //    var data = await _unitOfWork.UserLogins.SSOLogin(obj);
        //    var collection = ((IEnumerable)data.Data).Cast<Authentication.Dto.Shared.LoginDetailsModel>().ToList();
        //    var authenticationResponse = _jwtTokenHandler.GenerateAuthToken(collection);
        //    if (authenticationResponse == null) return new List<AuthenticationResponse>();//Unauthorized();
        //    return authenticationResponse;
        //}

        [HttpPost]
        [Route("SwitchUser")]
        public async Task<ActionResult<List<AuthenticationResponse>?>> SwitchUser([FromBody] AuthenticationRequest authenticationRequest)
        {
            var obj = new LoginModel();
            if (authenticationRequest.IsSSOLogin == true)
            {
                //obj = new LoginModel
                //{
                //    SSOToken = string.IsNullOrEmpty(authenticationRequest.SSOToken) ? "" : Core.Common.Decrypt(authenticationRequest.SSOToken),
                //    UserName = "PCMS.TEST",
                //    Password = "R@jS$opcm21#",
                //    IsSSOLogin = authenticationRequest.IsSSOLogin,
                //    IPAddress = authenticationRequest.IPAddress
                //};
                obj = new LoginModel
                {
                    SSOToken = string.IsNullOrEmpty(authenticationRequest.SSOToken) ? "" : Core.Common.Decrypt(authenticationRequest.SSOToken),
                    UserName = "",
                    Password = "",
                    IsSSOLogin = authenticationRequest.IsSSOLogin,
                    IPAddress = authenticationRequest.IPAddress
                };
            }
            else
            {
                obj = new LoginModel
                {
                    SSOToken = authenticationRequest.SSOToken,
                    UserName = authenticationRequest.UserName,
                    Password = authenticationRequest.Password,
                    IsSSOLogin = authenticationRequest.IsSSOLogin,
                    IPAddress = authenticationRequest.IPAddress
                };
            }
            var data = await _unitOfWork.UserLogins.SSOLogin(obj);
            var collection = ((IEnumerable)data.Data).Cast<Authentication.Dto.Shared.LoginDetailsModel>().ToList();
            var authenticationResponse = _jwtTokenHandler.GenerateAuthToken(collection);
            if (authenticationResponse == null) return Unauthorized();
            return authenticationResponse;
        }

        [HttpPost]
        [Route("MobileAppAuth")]
        public async Task<ActionResult<AuthenticationResponseForMobleApp>> AuthenticateForMobleApp([FromBody] AuthenticationRequestForMobleApp authenticationRequest)
        {
            var obj = new LoginModel()
            {
                UserName = authenticationRequest.UserName,
                Password = authenticationRequest.Password,
                IPAddress = authenticationRequest.IPAddress
            };

            var data = await _unitOfWork.UserLogins.SSOLoginForMobleApp(obj);
            var collection = ((IEnumerable)data.Data).Cast<Authentication.Dto.Shared.LoginDetailsModel>().ToList();
            var authenticationResponse = _jwtTokenHandler.GenerateAuthToken(collection);
            //if (authenticationResponse == null) return Unauthorized();
            //return authenticationResponse;
            return new AuthenticationResponseForMobleApp()
            {
                Status = data.Status,
                Message = data.Message,
                AuthenticationResponse = authenticationResponse
            };
        }

        [HttpPost]
        [Route("SSOIDMapped")]
        public async Task<ActionResult<ResponseWithoutPaginationModel>> SSOIDMapped([FromBody] SSOIDMappedModel objModel)
        {
            var data = await _unitOfWork.UserLogins.SSOIDMapped(objModel);
            return data;
        }

        [HttpPost]
        [Route("AuthenticateMapping")]
        public async Task<ActionResult<ResponseWithoutPaginationModel_New>> AuthenticateMapping([FromBody] LoginModel_New objModel)
        {
            var data = await _unitOfWork.UserLogins.AuthenticateMapping(objModel);
            return data;
        }

        [HttpPost]
        [Route("SsoProfileDt")]
        public async Task<ResponseWithoutPaginationModel> SsoProfileDt(SsoProfileDtModel objModel)
        {
            var encryptedPassword = AES.Encrypt(this.Configuration["SSOURL:WSPASSWORD"], this.Configuration["SSOURL:Encryption"]);
            var obj = new SsoProfileDtRequestModel
            {
                SSOID = objModel.SSOID,
                SsoBaseUrl = this.Configuration["SSOURL:GetUserDetailNew"],
                UserName = this.Configuration["SSOURL:WSUSERNAME"],
                Password = this.Configuration["SSOURL:WSPASSWORD"],
                EncryptedPassword = encryptedPassword
            };
            return await _unitOfWork.UserLogins.SsoProfileDt(obj);
        }


    }
}
