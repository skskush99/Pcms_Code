using Authentication.Dto.Shared;
using Common.Dapper;
using Dapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Data;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using Core.Models.User;
using Core.Utils;
using System.Text.Json;


namespace Authentication.Repository.UserToken;

internal class UserLoginRepository : SqlRepository, IUserLogin
{
    private IConfiguration _Configuration;
    private readonly System.Data.IDbConnection Con;
    public UserLoginRepository(IConfiguration Configuration) : base(Configuration)
    {
        _Configuration = Configuration;
    }

    public async Task<ResponseUserMappingModel> SSOLogin(LoginModel objModel)
    {
        var objResult = new ResponseUserMappingModel();

        try
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Action", "Login");
            parameters.Add("@UserName", objModel.UserName);
            parameters.Add("@Password", objModel.Password);
            parameters.Add("@IPAddress", objModel.IPAddress);
            parameters.Add("@IsSSOLogin", objModel.IsSSOLogin ? 1 : 0);

            string SSOId = string.Empty;

            // ===== SSO TOKEN CHECK =====
            if (objModel.IsSSOLogin)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_Configuration["SSOLoginURLForWeb"]);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var res = await client.GetAsync("SSOREST/GetTokenDetailJSON/" + objModel.SSOToken);

                    if (!res.IsSuccessStatusCode)
                    {
                        objResult.Status = false;
                        objResult.Message = "Invalid SSO token.";
                        objResult.Data = new List<object>();
                        return objResult;
                    }

                    var json = await res.Content.ReadAsStringAsync();
                    var ssoInfo = JsonConvert.DeserializeObject<SSOServiceResponse>(json);
                    SSOId = ssoInfo?.sAMAccountName;
                }

                if (string.IsNullOrEmpty(SSOId))
                {
                    objResult.Status = false;
                    objResult.Message = "Invalid SSO details.";
                    objResult.Data = new List<object>();
                    return objResult;
                }

                parameters.Add("@SSOID", SSOId);
            }
            else
            {
                parameters.Add("@SSOID", "");
            }

            using (var con = GetOpenConnection())
            {
                // ===== REAL LOGIN =====
                var loginData = await con.QueryAsync<LoginDetailsModel>("spUsr_UserLogin", parameters, commandType: CommandType.StoredProcedure);

                if (loginData.Any())
                {
                    foreach (var item in loginData)
                    {
                        item.Token = Core.Common.Encrypt(item.Token);
                    }

                    objResult.Status = true;
                    objResult.UserMappingReq = false;
                    objResult.Message = "Login successful.";
                    objResult.Data = loginData;
                    return objResult;
                }

                // ===== LOGINCHK (SSO MAPPING CHECK) =====
                if (objModel.IsSSOLogin)
                {
                    var chkParams = new DynamicParameters(parameters);
                    //chkParams.Remove("@Action");
                    chkParams.Add("@Action", "Loginchk");

                    var chkData = await con.QueryAsync<LoginDetails2Model>("spUsr_UserLogin", chkParams, commandType: CommandType.StoredProcedure);

                    if (chkData.Any())
                    {
                        objResult.Status = false;
                        objResult.UserMappingReq = true;
                        objResult.Message = "SSOId mapped but user not active.";
                        objResult.Data = chkData;   // 👈 mapping info
                        return objResult;
                    }
                    else
                    {
                        objResult.Status = false;
                        objResult.UserMappingReq = false;
                        objResult.Message = $"SSOId ({SSOId}) is not mapped, please contact to Prosecution Department.";
                        objResult.Data = new List<LoginDetails1Model>
                        {
                        new LoginDetails1Model { SSOID = SSOId }
                        };
                        return objResult;
                    }
                }

                // ===== NORMAL LOGIN FAIL =====
                objResult.Status = false;
                objResult.UserMappingReq = false;
                objResult.Message = "Invalid username or password.";
                objResult.Data = new List<object>();
            }
        }
        catch (Exception)
        {
            throw;
        }

        return objResult;
    }


    //public async Task<ResponseWithoutPaginationModel> SSOLogin(LoginModel objModel)
    //{
    //    try
    //    {
    //        var parmeters = new DynamicParameters();
    //        parmeters.Add("@Action", "Login");
    //        parmeters.Add("@UserName", objModel.UserName);
    //        parmeters.Add("@Password", objModel.Password);
    //        parmeters.Add("@IPAddress", objModel.IPAddress);
    //        parmeters.Add("@IsSSOLogin", objModel.IsSSOLogin == true ? 1 : 0);
    //        //parmeters.Add("@SSOToken", objModel.SSOToken); // Adding SSOToken parameter

    //        // Create a response object to return
    //        ResponseWithoutPaginationModel objResult = new ResponseWithoutPaginationModel();

    //        // Perform SSO lookup if IsSSOLogin is true
    //        if (objModel.IsSSOLogin)
    //        {
    //            SSOServiceResponse ssoInfo = new SSOServiceResponse();
    //            string SSOId = string.Empty;
    //            using (var client = new HttpClient())
    //            {
    //                client.BaseAddress = new Uri(this._Configuration["SSOLoginURLForWeb"]); // Add the appropriate base URL here
    //                client.DefaultRequestHeaders.Clear();
    //                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

    //                // Sending request to get token details
    //                HttpResponseMessage Res = await client.GetAsync("SSOREST/GetTokenDetailJSON/" + objModel.SSOToken);

    //                if (Res.IsSuccessStatusCode)
    //                {
    //                    var ssoResponse = await Res.Content.ReadAsStringAsync();
    //                    ssoInfo = JsonConvert.DeserializeObject<SSOServiceResponse>(ssoResponse);
    //                    SSOId = ssoInfo?.sAMAccountName;
    //                    //SSOId = "PCMS.TEST";
    //                    //string SSOId1 = SSOId;
    //                    if (!string.IsNullOrEmpty(SSOId))
    //                    {
    //                        parmeters.Add("@SSOID", SSOId);
    //                        using (var Con = GetOpenConnection())
    //                        {
    //                            var objData = await Con.QueryAsync<LoginDetailsModel>("spUsr_UserLogin", parmeters, commandType: CommandType.StoredProcedure);
    //                            if (objData.Any())
    //                            {
    //                                // Encrypt token for each item in objData
    //                                foreach (var item in objData)
    //                                {
    //                                    item.Token = Core.Common.Encrypt(item.Token);
    //                                }

    //                                objResult.Status = true;
    //                                objResult.Message = $"SSOId ({SSOId}) is mapped.";
    //                                objResult.Data = objData;
    //                            }
    //                            else
    //                            {
    //                                if (!string.IsNullOrEmpty(SSOId))
    //                                    parmeters.Add("@Action", "Loginchk");
    //                                    parmeters.Add("@SSOID", SSOId);
    //                                {
    //                                    var objData1 = await Con.QueryAsync<LoginDetails2Model>("spUsr_UserLogin", parmeters, commandType: CommandType.StoredProcedure);
    //                                    if (objData1.Any())
    //                                    {
    //                                        //// Encrypt token for each item in objData
    //                                        //foreach (var item in objData1)
    //                                        //{
    //                                        //    item.Token = Core.Common.Encrypt(item.Token);
    //                                        //}

    //                                        objResult.Status = true;
    //                                        objResult.Message = $"SSOId ({SSOId}) is mapped.";
    //                                        objResult.Data = objData1;
    //                                    }
    //                                    else
    //                                    {
    //                                        objResult.Status = false;
    //                                        objResult.Message = $"SSOId ({SSOId}) is not mapped, please contact to PSMS Department.";
    //                                        objResult.Data = new List<LoginDetails1Model>
    //                                        {
    //                                        new LoginDetails1Model
    //                                        {
    //                                            SSOID = SSOId
    //                                        }
    //                                        };
    //                                    }
    //                                }
    //                            }
    //                            DisposeCurrentSqlConnection();
    //                        }
    //                    }
    //                    else
    //                    {
    //                        objResult.Status = false;
    //                        objResult.Message = "Please provide valid login details.";
    //                        objResult.Data = new List<LoginDetailsModel>();
    //                    }
    //                }
    //                else
    //                {
    //                    objResult.Status = false;
    //                    objResult.Message = "Please provide valid login details.";
    //                    objResult.Data = new List<LoginDetailsModel>();
    //                }
    //            }
    //        }
    //        else
    //        {
    //            parmeters.Add("@SSOID", "");
    //            using (var Con = GetOpenConnection())
    //            {
    //                var objData = await Con.QueryAsync<LoginDetailsModel>("spUsr_UserLogin", parmeters, commandType: CommandType.StoredProcedure);

    //                if (objData.Any())
    //                {
    //                    foreach (var item in objData)
    //                    {
    //                        item.Token = Core.Common.Encrypt(item.Token);
    //                    }

    //                    objResult.Status = true;
    //                    objResult.Message = "";
    //                    objResult.Data = objData;
    //                }
    //                else
    //                {
    //                    objResult.Status = false;
    //                    objResult.Message = "Please provide valid login details.";
    //                    objResult.Data = new List<LoginDetailsModel>();
    //                }
    //            }
    //            DisposeCurrentSqlConnection();
    //        }

    //        return objResult;
    //    }
    //    catch (Exception)
    //    {
    //        throw;
    //    }
    //}

    public async Task<ResponseWithoutPaginationModel> SSOLoginBypass(LoginModel objModel)
    {
        try
        {
            var parmeters = new DynamicParameters();
            parmeters.Add("@Action", "Login");
            parmeters.Add("@UserName", objModel.UserName);
            parmeters.Add("@Password", objModel.Password);
            parmeters.Add("@IPAddress", objModel.IPAddress);
            parmeters.Add("@IsSSOLogin", objModel.IsSSOLogin == true ? 1 : 0);

            // Create a response object to return
            ResponseWithoutPaginationModel objResult = new ResponseWithoutPaginationModel();

            parmeters.Add("@SSOID", "");
            using (var Con = GetOpenConnection())
            {
                var objData = await Con.QueryAsync<LoginDetailsModel>("spUsr_UserLogin", parmeters, commandType: CommandType.StoredProcedure);

                if (objData.Any())
                {
                    foreach (var item in objData)
                    {
                        item.Token = Core.Common.Encrypt(item.Token);
                    }

                    objResult.Status = true;
                    objResult.Message = "";
                    objResult.Data = objData;
                }
                else
                {
                    objResult.Status = false;
                    objResult.Message = "Please provide valid login details.";
                }
            }
            DisposeCurrentSqlConnection();

            return objResult;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<ResponseWithoutPaginationModel> SSOLoginForMobleApp(LoginModel objModel)
    {
        ResponseWithoutPaginationModel objResult = new ResponseWithoutPaginationModel();
        try
        {
            objModel.Password = PasswordEncrypt(objModel.Password);
            string Url = this._Configuration["SSOLoginURLForMobleApp"];
            var bodyParams = new
            {
                Application = this._Configuration["SSOLoginApplicationLForMobleApp"],
                UserName = objModel.UserName,
                Password = objModel.Password
            };
            string jsonData = JsonConvert.SerializeObject(bodyParams);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = jsonData.Length;
            StreamWriter requestWriter = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.ASCII);
            requestWriter.Write(jsonData);
            requestWriter.Close();
            WebResponse webResponse = request.GetResponse();
            Stream webStream = webResponse.GetResponseStream();
            StreamReader responseReader = new StreamReader(webStream);
            string response = responseReader.ReadToEnd();
            responseReader.Close();
            dynamic returnData = JsonConvert.DeserializeObject(response);
            if (returnData != null && Convert.ToBoolean(returnData.valid))
            {
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "Login");
                    parmeters.Add("@SSOID", objModel.UserName);
                    parmeters.Add("@IPAddress", objModel.IPAddress);
                    parmeters.Add("@IsSSOLogin", 1);
                    var objData = await Con.QueryAsync<LoginDetailsModel>("spUsr_UserLogin", parmeters, commandType: CommandType.StoredProcedure);
                    if (objData.Any())
                    {
                        foreach (var item in objData)
                        {
                            item.Token = Core.Common.Encrypt(item.Token);
                        }
                        objResult.Status = true;
                        objResult.Message = "";
                        objResult.Data = objData;
                    }
                    else
                    {
                        objResult.Status = false;
                        objResult.Message = "SSOId is not mapped, please contact to Justice Department.";
                        objResult.Data = new List<LoginDetailsModel>();
                    }
                    DisposeCurrentSqlConnection();
                }
            }
            else
            {
                objResult.Status = false;
                objResult.Message = "Please provide valid login details.";
                objResult.Data = new List<LoginDetailsModel>();
            }
        }
        catch (Exception ex)
        {
            objResult.Status = false;
            objResult.Message = "Some error occure, please try after some time.";
            objResult.Data = new List<LoginDetailsModel>();
        }
        return objResult;
    }

    public async Task<ResponseWithoutPaginationModel> SSOIDMapped(SSOIDMappedModel objModel)
    {
        try
        {
            using (var Con = GetOpenConnection())
            {
                var parmeters = new DynamicParameters();
                parmeters.Add("@Action", "SSOIDMapped");
                parmeters.Add("@SSOID", objModel.SSOID);
                parmeters.Add("@UserName", objModel.UserName);
                parmeters.Add("@Password", objModel.Password);

                var objData = await Con.QueryAsync<ResponseWithoutPaginationModel>("spUsr_UserLogin", parmeters, commandType: CommandType.StoredProcedure);
                var objResult = objData.FirstOrDefault();
                DisposeCurrentSqlConnection();
                return objResult;
            }
        }
        catch (Exception ex)
        {
            return new ResponseWithoutPaginationModel()
            {
                Status = false,
                Message = ex.Message,
            };
        }
    }

    public async Task<ResponseWithoutPaginationModel_New> AuthenticateMapping(LoginModel_New objModel)
    {
        var objResult = new ResponseWithoutPaginationModel_New();

        try
        {
            using (var con = GetOpenConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@IPAddress", objModel.IPAddress);
                parameters.Add("@SSOToken", objModel.SSOToken);
                parameters.Add("@IsSSOLogin", objModel.IsSSOLogin);

                if (objModel.IsSSOLogin)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(_Configuration["SSOLoginURLForWeb"]);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        HttpResponseMessage res = await client.GetAsync("SSOREST/GetTokenDetailJSON/" + objModel.SSOToken);

                        if (res.IsSuccessStatusCode)
                        {
                            var ssoResponse = await res.Content.ReadAsStringAsync();
                            var ssoInfo = JsonConvert.DeserializeObject<SSOServiceResponse>(ssoResponse);

                            string ssoId = ssoInfo?.sAMAccountName;

                            if (!string.IsNullOrEmpty(ssoId))
                            {
                                parameters.Add("@SSOID", ssoId);
                                objResult.SSOID = ssoId;
                                var loginData = objResult.SSOID;

                                if (loginData == null || !loginData.Any())
                                {
                                    objResult.Status = false;
                                    objResult.Message = "SSOID is not mapped, please contact to Justice Department.";
                                    objResult.Data = new List<ResponseWithoutPaginationModel_New>();
                                    return objResult;
                                }
                                objResult.Status = true;
                                objResult.Message = "SSO login successful.";
                                objResult.Data = new List<ResponseWithoutPaginationModel_New>();
                                return objResult;
                            }
                        }

                        // If SSO API fails or SSOID is null
                        objResult.Status = false;
                        objResult.Message = "Invalid SSO token or SSOID not found.";
                        objResult.Data = new List<ResponseWithoutPaginationModel_New>();
                        return objResult;
                    }
                }
                else
                {
                    objResult.Status = false;
                    objResult.Message = "Invalid login credentials.";
                    objResult.Data = new List<ResponseWithoutPaginationModel_New>();
                    return objResult;
                }
            }
        }
        catch (Exception ex)
        {
            return new ResponseWithoutPaginationModel_New()
            {
                Status = false,
                Message = "Exception: " + ex.Message,
                Data = new List<LoginDetailsModel>()
            };
        }
    }

    public async Task<ResponseWithoutPaginationModel> SsoProfileDt(SsoProfileDtRequestModel objModel)
    {
        try
        {
            var parmeters = new DynamicParameters();
            ResponseWithoutPaginationModel objResult = new ResponseWithoutPaginationModel();

            if (!string.IsNullOrEmpty(objModel.SSOID))
            {
                SSOServiceResponse ssoInfo = new SSOServiceResponse();
                IEnumerable<SSOUserDetails> data = new List<SSOUserDetails>();
                SSOUserDetails userdetails = new SSOUserDetails();
                using (var client = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(HttpMethod.Post, objModel.SsoBaseUrl))
                    {
                        var collection = new List<KeyValuePair<string, string>>
                            {
                                new("SSOID", objModel.SSOID),
                                new("WSUSERNAME", objModel.UserName),
                                new("WSPASSWORD", objModel.EncryptedPassword)
                            };

                        var content = new FormUrlEncodedContent(collection);
                        request.Content = content;
                        using (var responseSsoUser = client.Send(request))
                        {
                            if (responseSsoUser.IsSuccessStatusCode)
                            {
                                var optionsSso = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                                var stateInfoSso = responseSsoUser.Content.ReadAsStringAsync().Result;
                                dynamic resultSso = System.Text.Json.JsonSerializer.Deserialize<Object>(stateInfoSso, optionsSso);
                                userdetails = ConversionUtility.ConvertFromDynamicObject<SSOUserDetails>(resultSso);
                                data = data.Append(userdetails);
                                if (userdetails != null)
                                {

                                    objResult.Status = true;
                                    objResult.Message = "";
                                    objResult.Data = data;
                                }
                                else
                                {
                                    objResult.Status = false;
                                    objResult.Message = "Invalid SSO Id.";
                                }
                            }
                            else
                            {
                                objResult.Status = false;
                                objResult.Message = "Invalid SSO Id.";
                            }
                        }
                    }
                }
            }
            else
            {
                objResult.Status = false;
                objResult.Message = "SSO Id not provided.";
            }

            return objResult;
        }
        catch (Exception ex)
        {
            return new ResponseWithoutPaginationModel()
            {
                Status = false,
                Message = "Exception: " + ex.Message,
                Data = new List<SsoProfileDtRequestModel>()
            };
        }
    }


    private string PasswordEncrypt(string plainText)
    {
        string encryptionKey = this._Configuration["SSOLoginEncryptionKeyLForMobleApp"];
        if (!string.IsNullOrEmpty(plainText))
        {
            var plainBytes = Encoding.UTF8.GetBytes(plainText);
            var cipher = GetRijndaelManaged(encryptionKey);
            return
           Convert.ToBase64String(cipher.CreateEncryptor().TransformFinalBlock(plainBytes, 0, plainBytes.Length));
        }
        else
        {
            return string.Empty;
        }
    }

    private RijndaelManaged GetRijndaelManaged(string secretKey)
    {
        var keyBytes = new byte[16];
        var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
        Array.Copy(secretKeyBytes, keyBytes, Math.Min(keyBytes.Length, secretKeyBytes.Length));
        return new RijndaelManaged
        {
            Mode = CipherMode.CBC,
            Padding = PaddingMode.PKCS7,
            KeySize = 128,
            BlockSize = 128,
            Key = keyBytes,
            IV = keyBytes
        };
    }
}