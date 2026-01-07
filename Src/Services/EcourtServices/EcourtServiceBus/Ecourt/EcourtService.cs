using EcourtDto;
using EcourtDto.Shared;
using static System.Formats.Asn1.AsnWriter;
using System;
using System.Text;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Security.Cryptography;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Newtonsoft.Json.Linq;
using System.Text.Json.Serialization;
using System.Dynamic;

namespace EcourtServiceBus.Ecourt
{
    public class EcourtService : IEcourtService
    {
        public async Task<ResponseWithoutPaginationModel> GetAuthToken(EcourtCredentials data)
        {
            try
            {
                ResponseWithoutPaginationModel objResut = new();
                string combineKey = $"{data.Username}:{data.Password}";
                string basicAuthValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(combineKey));
                var credentials = new Dictionary<string, string>
                {
                    { "grant_type", data.grant_type },
                    { "scope", data.Scope },
                    { "username", data.Username },
                    { "password", data.Password },
                    { "authorization", basicAuthValue },
                    { "url", data.BaseUrl }
                };

                var responseString = await GetToken(credentials); // Await the asynchronous task
                if (!responseString.Contains("Error"))
                {
                    var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ExpandoObject>(responseString);
                    objResut = new()
                    {
                        Status = true,
                        Message = "Success",
                        Data = result
                    };
                }
                else
                {
                    dynamic result = new System.Dynamic.ExpandoObject();
                    result.Error = responseString.Split("Error:")[1].Trim();
                    objResut = new()
                    {
                        Status = true,
                        Message = "Success",
                        Data = result
                    };
                }
                return objResut;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it differently
                Console.WriteLine($"Error occurred: {ex.Message}");
                throw;
            }

        }
        private async Task<string> GetToken(Dictionary<string, string> credentials)
        {
            string grantType = credentials["grant_type"];
            string scope = credentials["scope"];
            string authorization = credentials["authorization"];
            string url = credentials["url"] + "oauth2/token";
            string postFields = $"grant_type={grantType}&scope={scope}";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authorization);
                var content = new StringContent(postFields, Encoding.UTF8, "application/x-www-form-urlencoded");
                var response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    return responseString;
                }
                else
                {
                    // Handle error cases (e.g., return an empty string or throw an exception)
                    return "";
                }
            }
        }

        public async Task<ResponseWithoutPaginationModel> GetDetailByCNR(string Cinno, string accessToken, EcourtCredentials data)
        {
            try
            {
                ResponseWithoutPaginationModel objResut = new();
                string requestStr = $"cino={Cinno}";
                string requestToken = HashHMAC("15081947", requestStr);
                requestStr = Encrypt(requestStr, data.AuthenticationKey, data.Iv);
                requestStr = Uri.EscapeDataString(requestStr);

                var credentials = new Dictionary<string, string>
                {
                    { "accessToken", accessToken },
                    { "AuthenticationKey", data.AuthenticationKey },
                    { "Iv", data.Iv },
                    { "DeptId", data.DeptId },
                    { "requestStr", requestStr},
                    { "requestToken", requestToken },
                    { "url", data.BaseUrl},
                    { "version", data.version}
                };
                var responseString = await GetCnrDetail(credentials);
                if (!responseString.Contains("Error"))
                {
                    var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ExpandoObject>(responseString);
                    objResut = new()
                    {
                        Status = true,
                        Message = "Success",
                        Data = result
                    };
                }
                else
                {
                    dynamic result = new System.Dynamic.ExpandoObject();
                    result.Error = responseString.Split("Error:")[1].Trim();
                    objResut = new()
                    {
                        Status = true,
                        Message = "Success",
                        Data = result
                    };
                }

                return objResut;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it differently
                Console.WriteLine($"Error occurred: {ex.Message}");
                throw;
            }
        }

        public async Task<string> GetCnrDetail(Dictionary<string, string> credentials)
        {
            string accessToken = credentials["accessToken"];
            string AuthenticationKey = credentials["AuthenticationKey"];
            string Iv = credentials["Iv"];
            string DeptId = credentials["DeptId"];
            string requestStr = credentials["requestStr"];
            string requestToken = credentials["requestToken"];
            string version = credentials["version"];
            string url = $"{credentials["url"]}/dc-cnr-api/cnr?dept_id={DeptId}&request_str={requestStr}&request_token={requestToken}&version={version}";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var resultApi = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseBody);

                    if (resultApi.ContainsKey("response_str"))
                    {
                        string responseStr = resultApi["response_str"];
                        byte[] payload = Convert.FromBase64String(responseStr);
                        string decrypt = Decrypt(payload, AuthenticationKey, Iv);
                        return decrypt;
                    }
                    else if (resultApi.ContainsKey("status"))
                    {
                        return $"Error: {resultApi["status"]}";
                    }
                    else
                    {
                        // Handle unexpected response format
                        return "Unexpected response format";
                    }
                }
                else
                {
                    return $"Error: {response.StatusCode} - {response.ReasonPhrase}";
                }
            }
        }
        public async Task<ResponseWithoutPaginationModel> GetDetailByCNRBulk(string Cinno, string accessToken, EcourtCredentials data)
        {
            try
            {
                ResponseWithoutPaginationModel objResut = new();
                string requestStr = $"cino={Cinno}";
                string requestToken = HashHMAC("15081947", requestStr);
                requestStr = Encrypt(requestStr, data.AuthenticationKey, data.Iv);
                requestStr = Uri.EscapeDataString(requestStr);

                var credentials = new Dictionary<string, string>
                {
                    { "accessToken", accessToken },
                    { "AuthenticationKey", data.AuthenticationKey },
                    { "Iv", data.Iv },
                    { "DeptId", data.DeptId },
                    { "requestStr", requestStr},
                    { "requestToken", requestToken },
                    { "url", data.BaseUrl},
                    { "version", data.version}
                };
                var responseString = await GetCnrBulkDetail(credentials);
                if (!responseString.Contains("Error"))
                {
                    var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ExpandoObject>(responseString);
                    objResut = new()
                    {
                        Status = true,
                        Message = "Success",
                        Data = result
                    };
                }
                else
                {
                    dynamic result = new System.Dynamic.ExpandoObject();
                    result.Error = responseString.Split("Error:")[1].Trim();
                    objResut = new()
                    {
                        Status = true,
                        Message = "Success",
                        Data = result
                    };
                }

                return objResut;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it differently
                Console.WriteLine($"Error occurred: {ex.Message}");
                throw;
            }
        }

        public async Task<string> GetCnrBulkDetail(Dictionary<string, string> credentials)
        {
            string accessToken = credentials["accessToken"];
            string AuthenticationKey = credentials["AuthenticationKey"];
            string Iv = credentials["Iv"];
            string DeptId = credentials["DeptId"];
            string requestStr = credentials["requestStr"];
            string requestToken = credentials["requestToken"];
            string version = credentials["version"];
            string url = $"{credentials["url"]}/dc-current-status-api/currentStatus?dept_id={DeptId}&request_str={requestStr}&request_token={requestToken}&version={version}";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var resultApi = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseBody);

                    if (resultApi.ContainsKey("response_str"))
                    {
                        string responseStr = resultApi["response_str"];
                        byte[] payload = Convert.FromBase64String(responseStr);
                        string decrypt = Decrypt(payload, AuthenticationKey, Iv);
                        return decrypt;
                    }
                    else if (resultApi.ContainsKey("status"))
                    {
                        return $"Error: {resultApi["status"]}";
                    }
                    else
                    {
                        // Handle unexpected response format
                        return "Unexpected response format";
                    }
                }
                else
                {
                    return $"Error: {response.StatusCode} - {response.ReasonPhrase}";
                }
            }
        }
        public async Task<ResponseWithoutPaginationModel> GetDetailCaseType(string est_code, string accessToken, EcourtCredentials data)
        {
            try
            {
                ResponseWithoutPaginationModel objResut = new();
                string requestStr = $"est_code={est_code}";
                string requestToken = HashHMAC("15081947", requestStr);
                requestStr = Encrypt(requestStr, data.AuthenticationKey, data.Iv);
                requestStr = Uri.EscapeDataString(requestStr);

                var credentials = new Dictionary<string, string>
                {
                    { "accessToken", accessToken },
                    { "AuthenticationKey", data.AuthenticationKey },
                    { "Iv", data.Iv },
                    { "DeptId", data.DeptId },
                    { "requestStr", requestStr},
                    { "requestToken", requestToken },
                    { "url", data.BaseUrl},
                    { "version", data.version}
                };
                var responseString = await GetCaseTypeDetail(credentials);
                if (!responseString.Contains("Error"))
                {
                    var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ExpandoObject>(responseString);
                    objResut = new()
                    {
                        Status = true,
                        Message = "Success",
                        Data = result
                    };
                }
                else
                {
                    dynamic result = new System.Dynamic.ExpandoObject();
                    result.Error = responseString.Split("Error:")[1].Trim();
                    objResut = new()
                    {
                        Status = true,
                        Message = "Success",
                        Data = result
                    };
                }

                return objResut;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it differently
                Console.WriteLine($"Error occurred: {ex.Message}");
                throw;
            }
        }
        public async Task<string> GetCaseTypeDetail(Dictionary<string, string> credentials)
        {
            string accessToken = credentials["accessToken"];
            string AuthenticationKey = credentials["AuthenticationKey"];
            string Iv = credentials["Iv"];
            string DeptId = credentials["DeptId"];
            string requestStr = credentials["requestStr"];
            string requestToken = credentials["requestToken"];
            string version = credentials["version"];
            string url = $"{credentials["url"]}/dc-casetype-master-api/casetypeMaster?dept_id={DeptId}&request_str={requestStr}&request_token={requestToken}&version={version}";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var resultApi = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseBody);

                    if (resultApi.ContainsKey("response_str"))
                    {
                        string responseStr = resultApi["response_str"];
                        byte[] payload = Convert.FromBase64String(responseStr);
                        string decrypt = Decrypt(payload, AuthenticationKey, Iv);
                        return decrypt;
                    }
                    else if (resultApi.ContainsKey("status"))
                    {
                        return $"Error: {resultApi["status"]}";
                    }
                    else
                    {
                        // Handle unexpected response format
                        return "Unexpected response format";
                    }
                }
                else
                {
                    return $"Error: {response.StatusCode} - {response.ReasonPhrase}";
                }
            }
        }
        public async Task<ResponseWithoutPaginationModel> GetDetailByCaseNumber(string est_code, string case_type, string reg_no, string reg_year, string accessToken, EcourtCredentials data)
        {
            try
            {
                ResponseWithoutPaginationModel objResut = new();
                string requestStr = $"est_code={est_code}|case_type={case_type}|reg_no={reg_no}|reg_year={reg_year}";
                string requestToken = HashHMAC("15081947", requestStr);
                requestStr = Encrypt(requestStr, data.AuthenticationKey, data.Iv);
                requestStr = Uri.EscapeDataString(requestStr);

                var credentials = new Dictionary<string, string>
                {
                    { "accessToken", accessToken },
                    { "AuthenticationKey", data.AuthenticationKey },
                    { "Iv", data.Iv },
                    { "DeptId", data.DeptId },
                    { "requestStr", requestStr},
                    { "requestToken", requestToken },
                    { "url", data.BaseUrl},
                    { "version", data.version}
                };
                var responseString = await GetDetailByCaseNumber(credentials);
                if (!responseString.Contains("Error"))
                {
                    var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ExpandoObject>(responseString);
                    objResut = new()
                    {
                        Status = true,
                        Message = "Success",
                        Data = result
                    };
                }
                else
                {
                    dynamic result = new System.Dynamic.ExpandoObject();
                    result.Error = responseString.Split("Error:")[1].Trim();
                    objResut = new()
                    {
                        Status = true,
                        Message = "Success",
                        Data = result
                    };
                }

                return objResut;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it differently
                Console.WriteLine($"Error occurred: {ex.Message}");
                throw;
            }
        }
        public async Task<string> GetDetailByCaseNumber(Dictionary<string, string> credentials)
        {
            string accessToken = credentials["accessToken"];
            string AuthenticationKey = credentials["AuthenticationKey"];
            string Iv = credentials["Iv"];
            string DeptId = credentials["DeptId"];
            string requestStr = credentials["requestStr"];
            string requestToken = credentials["requestToken"];
            string version = credentials["version"];
            string url = $"{credentials["url"]}/dc-case-number-api/caseSearch?dept_id={DeptId}&request_str={requestStr}&request_token={requestToken}&version={version}";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var resultApi = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseBody);

                    if (resultApi.ContainsKey("response_str"))
                    {
                        string responseStr = resultApi["response_str"];
                        byte[] payload = Convert.FromBase64String(responseStr);
                        string decrypt = Decrypt(payload, AuthenticationKey, Iv);
                        return decrypt;
                    }
                    else if (resultApi.ContainsKey("status"))
                    {
                        return $"Error: {resultApi["status"]}";
                    }
                    else
                    {
                        // Handle unexpected response format
                        return "Unexpected response format";
                    }
                }
                else
                {
                    return $"Error: {response.StatusCode} - {response.ReasonPhrase}";
                }
            }
        }
        public async Task<ResponseWithoutPaginationModel> GetDetailByPartyName(string est_code, string pend_disp, string litigant_name, string reg_year, string accessToken, EcourtCredentials data)
        {
            try
            {
                ResponseWithoutPaginationModel objResut = new();
                string requestStr = $"est_code={est_code}|pend_disp={pend_disp}|litigant_name={litigant_name}|reg_year={reg_year}";
                string requestToken = HashHMAC("15081947", requestStr);
                requestStr = Encrypt(requestStr, data.AuthenticationKey, data.Iv);
                requestStr = Uri.EscapeDataString(requestStr);

                var credentials = new Dictionary<string, string>
                {
                    { "accessToken", accessToken },
                    { "AuthenticationKey", data.AuthenticationKey },
                    { "Iv", data.Iv },
                    { "DeptId", data.DeptId },
                    { "requestStr", requestStr},
                    { "requestToken", requestToken },
                    { "url", data.BaseUrl},
                    { "version", data.version}
                };
                var responseString = await GetDetailByPartyName(credentials);
                if (!responseString.Contains("Error"))
                {
                    var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ExpandoObject>(responseString);
                    objResut = new()
                    {
                        Status = true,
                        Message = "Success",
                        Data = result
                    };
                }
                else
                {
                    dynamic result = new System.Dynamic.ExpandoObject();
                    result.Error = responseString.Split("Error:")[1].Trim();
                    objResut = new()
                    {
                        Status = true,
                        Message = "Success",
                        Data = result
                    };
                }

                return objResut;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it differently
                Console.WriteLine($"Error occurred: {ex.Message}");
                throw;
            }
        }
        public async Task<string> GetDetailByPartyName(Dictionary<string, string> credentials)
        {
            string accessToken = credentials["accessToken"];
            string AuthenticationKey = credentials["AuthenticationKey"];
            string Iv = credentials["Iv"];
            string DeptId = credentials["DeptId"];
            string requestStr = credentials["requestStr"];
            string requestToken = credentials["requestToken"];
            string version = credentials["version"];
            string url = $"{credentials["url"]}/dc-party-name-api/partyName?dept_id={DeptId}&request_str={requestStr}&request_token={requestToken}&version={version}";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var resultApi = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseBody);

                    if (resultApi.ContainsKey("response_str"))
                    {
                        string responseStr = resultApi["response_str"];
                        byte[] payload = Convert.FromBase64String(responseStr);
                        string decrypt = Decrypt(payload, AuthenticationKey, Iv);
                        return decrypt;
                    }
                    else if (resultApi.ContainsKey("status"))
                    {
                        return $"Error: {resultApi["status"]}";
                    }
                    else
                    {
                        // Handle unexpected response format
                        return "Unexpected response format";
                    }
                }
                else
                {
                    return $"Error: {response.StatusCode} - {response.ReasonPhrase}";
                }
            }
        }
        public async Task<ResponseWithoutPaginationModel> GetDistrictDetail(string state_code, string accessToken, EcourtCredentials data)
        {
            try
            {
                ResponseWithoutPaginationModel objResut = new();
                string requestStr = $"state_code={state_code}";
                string requestToken = HashHMAC("15081947", requestStr);
                requestStr = Encrypt(requestStr, data.AuthenticationKey, data.Iv);
                requestStr = Uri.EscapeDataString(requestStr);

                var credentials = new Dictionary<string, string>
                {
                    { "accessToken", accessToken },
                    { "AuthenticationKey", data.AuthenticationKey },
                    { "Iv", data.Iv },
                    { "DeptId", data.DeptId },
                    { "requestStr", requestStr},
                    { "requestToken", requestToken },
                    { "url", data.BaseUrl},
                    { "version", data.version}
                };
                var responseString = await GetDistrictDetail(credentials);
                if (!responseString.Contains("Error"))
                {
                    var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ExpandoObject>(responseString);
                    objResut = new()
                    {
                        Status = true,
                        Message = "Success",
                        Data = result
                    };
                }
                else
                {
                    dynamic result = new System.Dynamic.ExpandoObject();
                    result.Error = responseString.Split("Error:")[1].Trim();
                    objResut = new()
                    {
                        Status = true,
                        Message = "Success",
                        Data = result
                    };
                }

                return objResut;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it differently
                Console.WriteLine($"Error occurred: {ex.Message}");
                throw;
            }
        }
        public async Task<string> GetDistrictDetail(Dictionary<string, string> credentials)
        {
            string accessToken = credentials["accessToken"];
            string AuthenticationKey = credentials["AuthenticationKey"];
            string Iv = credentials["Iv"];
            string DeptId = credentials["DeptId"];
            string requestStr = credentials["requestStr"];
            string requestToken = credentials["requestToken"];
            string version = credentials["version"];
            string url = $"{credentials["url"]}/dc-district-api/district?dept_id={DeptId}&request_str={requestStr}&request_token={requestToken}&version={version}";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var resultApi = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseBody);

                    if (resultApi.ContainsKey("response_str"))
                    {
                        string responseStr = resultApi["response_str"];
                        byte[] payload = Convert.FromBase64String(responseStr);
                        string decrypt = Decrypt(payload, AuthenticationKey, Iv);
                        return decrypt;
                    }
                    else if (resultApi.ContainsKey("status"))
                    {
                        return $"Error: {resultApi["status"]}";
                    }
                    else
                    {
                        // Handle unexpected response format
                        return "Unexpected response format";
                    }
                }
                else
                {
                    return $"Error: {response.StatusCode} - {response.ReasonPhrase}";
                }
            }
        }
        public async Task<ResponseWithoutPaginationModel> GetECourtStateDetail(string accessToken, EcourtCredentials data)
        {
            try
            {
                ResponseWithoutPaginationModel objResut = new();
                string requestStr = $"";
                string requestToken = HashHMAC("15081947", requestStr);
                requestStr = Encrypt(requestStr, data.AuthenticationKey, data.Iv);
                requestStr = Uri.EscapeDataString(requestStr);

                var credentials = new Dictionary<string, string>
                {
                    { "accessToken", accessToken },
                    { "AuthenticationKey", data.AuthenticationKey },
                    { "Iv", data.Iv },
                    { "DeptId", data.DeptId },
                    { "requestStr", requestStr},
                    { "requestToken", requestToken },
                    { "url", data.BaseUrl},
                    { "version", data.version}
                };
                var responseString = await GetECourtStateDetail(credentials);
                if (!responseString.Contains("Error"))
                {
                    var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ExpandoObject>(responseString);
                    objResut = new()
                    {
                        Status = true,
                        Message = "Success",
                        Data = result
                    };
                }
                else
                {
                    dynamic result = new System.Dynamic.ExpandoObject();
                    result.Error = responseString.Split("Error:")[1].Trim();
                    objResut = new()
                    {
                        Status = true,
                        Message = "Success",
                        Data = result
                    };
                }

                return objResut;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it differently
                Console.WriteLine($"Error occurred: {ex.Message}");
                throw;
            }
        }
        public async Task<string> GetECourtStateDetail(Dictionary<string, string> credentials)
        {
            string accessToken = credentials["accessToken"];
            string AuthenticationKey = credentials["AuthenticationKey"];
            string Iv = credentials["Iv"];
            string DeptId = credentials["DeptId"];
            string requestStr = credentials["requestStr"];
            string requestToken = credentials["requestToken"];
            string version = credentials["version"];
            string url = $"{credentials["url"]}/dc-state-api/state?dept_id={DeptId}&request_str={requestStr}&request_token={requestToken}&version={version}";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var resultApi = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseBody);

                    if (resultApi.ContainsKey("response_str"))
                    {
                        string responseStr = resultApi["response_str"];
                        byte[] payload = Convert.FromBase64String(responseStr);
                        string decrypt = Decrypt(payload, AuthenticationKey, Iv);
                        return decrypt;
                    }
                    else if (resultApi.ContainsKey("status"))
                    {
                        return $"Error: {resultApi["status"]}";
                    }
                    else
                    {
                        // Handle unexpected response format
                        return "Unexpected response format";
                    }
                }
                else
                {
                    return $"Error: {response.StatusCode} - {response.ReasonPhrase}";
                }
            }
        }
        public async Task<ResponseWithoutPaginationModel> GetCourtComplexDetail(string state_code,string dist_code, string accessToken, EcourtCredentials data)
        {
            try
            {
                ResponseWithoutPaginationModel objResut = new();
                string requestStr = $"state_code={state_code}|dist_code={dist_code}";
                string requestToken = HashHMAC("15081947", requestStr);
                requestStr = Encrypt(requestStr, data.AuthenticationKey, data.Iv);
                requestStr = Uri.EscapeDataString(requestStr);

                var credentials = new Dictionary<string, string>
                {
                    { "accessToken", accessToken },
                    { "AuthenticationKey", data.AuthenticationKey },
                    { "Iv", data.Iv },
                    { "DeptId", data.DeptId },
                    { "requestStr", requestStr},
                    { "requestToken", requestToken },
                    { "url", data.BaseUrl},
                    { "version", data.version}
                };
                var responseString = await GetCourtComplexDetail(credentials);
                if (!responseString.Contains("Error"))
                {
                    var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ExpandoObject>(responseString);
                    objResut = new()
                    {
                        Status = true,
                        Message = "Success",
                        Data = result
                    };
                }
                else
                {
                    dynamic result = new System.Dynamic.ExpandoObject();
                    result.Error = responseString.Split("Error:")[1].Trim();
                    objResut = new()
                    {
                        Status = true,
                        Message = "Success",
                        Data = result
                    };
                }

                return objResut;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it differently
                Console.WriteLine($"Error occurred: {ex.Message}");
                throw;
            }
        }
        public async Task<string> GetCourtComplexDetail(Dictionary<string, string> credentials)
        {
            string accessToken = credentials["accessToken"];
            string AuthenticationKey = credentials["AuthenticationKey"];
            string Iv = credentials["Iv"];
            string DeptId = credentials["DeptId"];
            string requestStr = credentials["requestStr"];
            string requestToken = credentials["requestToken"];
            string version = credentials["version"];
            string url = $"{credentials["url"]}/dc-court-complex-api/courtComplex?dept_id={DeptId}&request_str={requestStr}&request_token={requestToken}&version={version}";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var resultApi = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseBody);

                    if (resultApi.ContainsKey("response_str"))
                    {
                        string responseStr = resultApi["response_str"];
                        byte[] payload = Convert.FromBase64String(responseStr);
                        string decrypt = Decrypt(payload, AuthenticationKey, Iv);
                        return decrypt;
                    }
                    else if (resultApi.ContainsKey("status"))
                    {
                        return $"Error: {resultApi["status"]}";
                    }
                    else
                    {
                        // Handle unexpected response format
                        return "Unexpected response format";
                    }
                }
                else
                {
                    return $"Error: {response.StatusCode} - {response.ReasonPhrase}";
                }
            }
        }
        public async Task<ResponseWithoutPaginationModel> GetCauseListDetail(string est_code, string court_no, string causelist_date, string ci_cri, string accessToken, EcourtCredentials data)
        {
            try
            {
                ResponseWithoutPaginationModel objResut = new();
                string requestStr = $"est_code={est_code}|court_no={court_no}|causelist_date={causelist_date}|ci_cri={ci_cri}";
                string requestToken = HashHMAC("15081947", requestStr);
                requestStr = Encrypt(requestStr, data.AuthenticationKey, data.Iv);
                requestStr = Uri.EscapeDataString(requestStr);

                var credentials = new Dictionary<string, string>
                {
                    { "accessToken", accessToken },
                    { "AuthenticationKey", data.AuthenticationKey },
                    { "Iv", data.Iv },
                    { "DeptId", data.DeptId },
                    { "requestStr", requestStr},
                    { "requestToken", requestToken },
                    { "url", data.BaseUrl},
                    { "version", data.version}
                };
                var responseString = await GetCauseListDetail(credentials);
                if (!responseString.Contains("Error"))
                {
                    var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ExpandoObject>(responseString);
                    objResut = new()
                    {
                        Status = true,
                        Message = "Success",
                        Data = result
                    };
                }
                else
                {
                    dynamic result = new System.Dynamic.ExpandoObject();
                    result.Error = responseString.Split("Error:")[1].Trim();
                    objResut = new()
                    {
                        Status = true,
                        Message = "Success",
                        Data = result
                    };
                }

                return objResut;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it differently
                Console.WriteLine($"Error occurred: {ex.Message}");
                throw;
            }
        }
        public async Task<string> GetCauseListDetail(Dictionary<string, string> credentials)
        {
            string accessToken = credentials["accessToken"];
            string AuthenticationKey = credentials["AuthenticationKey"];
            string Iv = credentials["Iv"];
            string DeptId = credentials["DeptId"];
            string requestStr = credentials["requestStr"];
            string requestToken = credentials["requestToken"];
            string version = credentials["version"];
            string url = $"{credentials["url"]}/dc-causelist-api/causelist?dept_id={DeptId}&request_str={requestStr}&request_token={requestToken}&version={version}";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var resultApi = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseBody);

                    if (resultApi.ContainsKey("response_str"))
                    {
                        string responseStr = resultApi["response_str"];
                        byte[] payload = Convert.FromBase64String(responseStr);
                        string decrypt = Decrypt(payload, AuthenticationKey, Iv);
                        return decrypt;
                    }
                    else if (resultApi.ContainsKey("status"))
                    {
                        return $"Error: {resultApi["status"]}";
                    }
                    else
                    {
                        // Handle unexpected response format
                        return "Unexpected response format";
                    }
                }
                else
                {
                    return $"Error: {response.StatusCode} - {response.ReasonPhrase}";
                }
            }
        }
        private static string HashHMAC(string key, string data)
        {
            // Convert the secret key to a byte array
            byte[] keyBytes = Encoding.ASCII.GetBytes(key);
            using (HMACSHA256 hmac = new HMACSHA256(keyBytes))
            {
                // Convert the request string to a byte array
                byte[] dataBytes = Encoding.UTF8.GetBytes(data);
                // Compute the HMAC-SHA256 hash
                byte[] hashBytes = hmac.ComputeHash(dataBytes);
                // Convert the hash to a hexadecimal string
                string stoken = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                return stoken;
            }
        }
        private static string Encrypt(string inputStr, string key, string iv)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = Encoding.ASCII.GetBytes(key);
                aes.IV = Encoding.ASCII.GetBytes(iv); ;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;
                ICryptoTransform encryptor = aes.CreateEncryptor();
                byte[] encryptedBytes;
                using (var msEncrypt = new System.IO.MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt,
                   encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new
                       System.IO.StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(inputStr);
                        }
                    }
                    encryptedBytes = msEncrypt.ToArray();
                }
                string requestStr = Convert.ToBase64String(encryptedBytes);
                return requestStr;
            }
        }
        private static string Decrypt(byte[] inputBytes, string key, string iv)
        {
            byte[] ivByte = Encoding.ASCII.GetBytes(iv);
            // Convert Base64-encoded ciphertext to bytes
            byte[] encryptedBytes = inputBytes;
            // Create AES key and IV
            byte[] keyBytes = Encoding.ASCII.GetBytes(key);
            // Ensure the key and IV have the correct lengths
            Array.Resize(ref keyBytes, 16); // For AES-128
            Array.Resize(ref ivByte, 16);
            using (var aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.IV = ivByte;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                ICryptoTransform decryptor = aes.CreateDecryptor();
                byte[] decryptedBytes;
                using (var msDecrypt = new
                System.IO.MemoryStream(encryptedBytes))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new System.IO.StreamReader(csDecrypt))
                        {
                            string decryptedText = srDecrypt.ReadToEnd();
                            return decryptedText;
                        }
                    }
                }
            }
        }
    }


}
