using Common.Dapper;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Core.Insfrastructure.Controller
{
    public class BaseApiController : ControllerBase
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        protected readonly IConfiguration configuration;

        protected BaseApiController(IConfiguration Configuration, IHttpContextAccessor _httpContextAccessor)
        {
            this.configuration = Configuration;
            httpContextAccessor = _httpContextAccessor;
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

                //client.DefaultRequestHeaders.Add("UserIPAddress", httpContextAccessor.HttpContext.Request.Headers["Host"].ToString());
                //client.DefaultRequestHeaders.Add("UserBrowser", context.Request.Browser.Browser.ToString() + " " + HttpContext.Current.Request.Browser.Version);
                //client.DefaultRequestHeaders.Add("UserDevice", (context.Current.Request.Browser.IsMobileDevice ? ("Mobile-" + HttpContext.Current.Request.Browser.MobileDeviceModel + " (" + HttpContext.Current.Request.Browser.MobileDeviceManufacturer + ")") : "Desktop"));
                //client.DefaultRequestHeaders.Add("UserUserAgent", httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString());
                //client.DefaultRequestHeaders.Add("UserLoginUrl", httpContextAccessor.HttpContext.Request.Headers["Referer"].ToString());
                //client.DefaultRequestHeaders.Add("Language", httpContextAccessor.HttpContext.Request.Headers["Language"].ToString());
            }
            catch (Exception)
            {
                //throw ex;
            }
        }

        protected async Task<dynamic> CallPostWebAPI<T>(string apiNameWithParameter, T postModel, string baseAddress)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    PrepareHttpClientRequest(client, baseAddress);
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
            catch (Exception)
            {
                throw;
            }
        }
        [NonAction]
        protected async Task<ResponseModel> GetApiConfigModel(Int32 orgId, string apiKey, string endpointClassName = "", string endpointFunctionName = "")
        {
            string baseURL = configuration["ServiceURL:CFG"];
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseURL);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var result = await client.GetAsync("Configuration/GetAPIConfigModelByApiKey?OrgId=" + orgId.ToString() + "&apiKey=" + apiKey + "&endpointClassName=" + endpointClassName + "&endpointFunctionName=" + endpointFunctionName);
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

    public class CustomHeaderSwaggerAttribute : IOperationFilter
    {

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "AuthId",
                In = ParameterLocation.Header,
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = "string"
                }
            });
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "AuthKey",
                In = ParameterLocation.Header,
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = "string"
                }
            });
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "Language",
                In = ParameterLocation.Header,
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = "string"
                }
            });
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "FormURL",
                In = ParameterLocation.Header,
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = "string"
                }
            });
        }

    }
}
