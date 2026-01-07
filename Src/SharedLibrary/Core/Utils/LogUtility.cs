using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Core.Models;

namespace Core.Utils
{
    public class LogUtility
    {
        public enum LogType
        {
            ErrorLog = 1,
            EventLog = 2
        }
        private delegate void DelegateSaveErrorLog(LogType logType, HttpContext httpContext, Exception ex, string loginId, string errorSource, string apiParameters);
        private static void SaveErrorLogInTextFile(LogType logType, HttpContext httpContext, Exception ex, string loginId, string errorSource, string apiParameters)
        {
            try
            {
                if (logType == LogType.ErrorLog)
                {

                    string folderName = "Logs/ErrorLog/";
                    if (System.IO.Directory.Exists(folderName) == false)
                        System.IO.Directory.CreateDirectory(folderName);
                    string flName = folderName + DateTime.Now.Date.ToString("yyyy-MM-dd") + ".txt";
                    if (System.IO.File.Exists(flName) == false)
                    {
                        System.IO.File.Create(flName);
                    }
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("Date Time : \t" + DateTime.Now.ToString());
                    sb.AppendLine("User Name : \t" + loginId);
                    sb.AppendLine("Requested URL : \t" + httpContext.Request.Host.ToString() + httpContext.Request.Path.ToString());
                    sb.AppendLine("Referral URL : \t" + httpContext.Request.Headers["Referer"]);
                    sb.AppendLine("IP Address : \t" + httpContext.Connection.RemoteIpAddress.ToString() + " --> " + System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList[0].ToString());
                    sb.AppendLine("Browser : \t" + httpContext.Request.Headers["sec-ch-ua-mobile"].ToString());
                    sb.AppendLine("Device : \t" + httpContext.Request.Headers["sec-ch-ua-platform"].ToString());
                    sb.AppendLine("User Agent : \t" + httpContext.Request.Headers["User-Agent"].ToString() + ";" + httpContext.Request.Headers["sec-ch-ua"].ToString() + ";" + httpContext.Request.Headers["sec-ch-ua-mobile"].ToString());
                    sb.AppendLine("Error Source : \t" + errorSource);
                    sb.AppendLine("Parameters : \t" + apiParameters);
                    sb.AppendLine("Error Message : \t" + ex.Message);
                    sb.AppendLine("Stack Trace : \t" + ex.StackTrace.ToString());
                    sb.AppendLine("Error : \t" + ex.ToString());
                    using (System.IO.StreamWriter writer = new StreamWriter(flName, true))
                    {
                        writer.WriteLine(sb.ToString()); // Write the text
                    }
                }
                else
                {
                    if (logType == LogType.EventLog)
                    {
                        string folderName = "Logs/ErrorLog/";
                        if (System.IO.Directory.Exists(folderName) == false)
                            System.IO.Directory.CreateDirectory(folderName);
                        string flName = folderName + DateTime.Now.Date.ToString("yyyy-MM-dd") + ".txt";
                        if (System.IO.File.Exists(flName) == false)
                        {
                            System.IO.File.Create(flName);
                        }
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("Date Time : \t" + DateTime.Now.ToString());
                        sb.AppendLine("User Name : \t" + loginId);
                        sb.AppendLine("Error Source : \t" + errorSource);
                        sb.AppendLine("Parameters : \t" + apiParameters);
                        sb.AppendLine("Error Message : \t" + ex.Message);
                        sb.AppendLine("Stack Trace : \t" + ex.StackTrace.ToString());
                        sb.AppendLine("Error : \t" + ex.ToString());
                        using (System.IO.StreamWriter writer = new StreamWriter(flName, true))
                        {
                            writer.WriteLine(sb.ToString()); // Write the text
                        }

                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public static void WriteErrorLog(HttpContext httpContext, Exception ex, string loginId, string errorSource, string apiParameters)
        {
            try
            {
                DelegateSaveErrorLog dlgt = new DelegateSaveErrorLog(SaveErrorLogInTextFile);
                dlgt(LogType.ErrorLog, httpContext, ex, loginId, errorSource, apiParameters);
            }
            catch (Exception)
            {
            }
        }
        public static void WriteEventErrorLog(Exception ex, string loginId, string errorSource, string apiParameters)
        {
            try
            {
                DelegateSaveErrorLog dlgt = new DelegateSaveErrorLog(SaveErrorLogInTextFile);
                dlgt(LogType.EventLog, null, ex, loginId, errorSource, apiParameters);
            }
            catch (Exception)
            {
            }
        }

        public static ResponseModel GetDynamicMessage(ApiFilterModel apiRequestMode, string baseURL)
        {
            // apiRequestMode.Module = "CFG";
            ResponseModel returnStatus = new ResponseModel(Core.Enums.User.Status.Alert);
            // string baseURL = baseURL;
            //ResponseModel res= 
            string Path = "DynamicMessage/GetDynamicMessageList";
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseURL);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var result = client.PostAsJsonAsync(Path, apiRequestMode);
                    result.Wait();
                    var resultSend = result.Result;
                    var responseSend = resultSend.Content.ReadAsStringAsync();
                    responseSend.Wait();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    returnStatus = JsonSerializer.Deserialize<ResponseModel>(responseSend.Result, options);
                }
            }
            catch (Exception)
            {

                //throw;
            }

            // returnStatus.CustomObject = GetDynamicMessageNew(apiRequestMode, baseURL);
            return returnStatus;
        }

        protected static async Task<dynamic> CallPostWebAPIWithoutSession<T>(string apiNameWithParameter, T postModel, string baseAddress)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    //PrepareHttpClientRequest(client, baseAddress, false);
                    client.BaseAddress = new Uri(baseAddress);
                    //baseAddress = client.BaseAddress.ToString();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var result = await client.PostAsJsonAsync<T>(apiNameWithParameter, postModel);
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var responseString = await result.Content.ReadAsStringAsync();
                    ResponseModel responseModel = JsonSerializer.Deserialize<ResponseModel>(responseString, options);
                    return responseModel;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
