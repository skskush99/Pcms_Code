using Core.Enums.User;
using Core.Insfrastructure.Controller;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ApiGateway.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [TypeFilter(typeof(AuthorizationRequiredAttribute))]
    public class GatewayServiceController : BaseApiController
    {
        public GatewayServiceController(IConfiguration Configuration, IHttpContextAccessor _httpContextAccessor) : base(Configuration, _httpContextAccessor)
        {

        }

        [HttpPost("GetList")]
        public async Task<IActionResult> PostGetList(ApiRequestModel apiRequestModel)
        {
            ResponseModel returnStatus = new ResponseModel(Status.Alert);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            //----------------------Log History-----------------------
            try
            {
                //var url = configuration["ServiceURL:CFG"];
                ////ResponseModel LogOutput1 = await CallPostWebAPI<dynamic>(apiRequestModel.Path, HttpContext.Request, url);

                //string AuthId = HttpContext.Request.Headers["AuthId"].ToString();
                //string AuthKey = HttpContext.Request.Headers["AuthKey"].ToString();
                //string Language = HttpContext.Request.Headers["Language"].ToString();
                //string FormURL = HttpContext.Request.Headers["FormURL"].ToString();

                //string strValue = "AuthId:" + AuthId + " AuthKey:" + AuthKey + " Language:" + Language + " FormURL:" + FormURL;
                //ResponseModel LogOutput = await CallPostWebAPIWithoutSession<dynamic>("General/UpdateLogData", strValue, url);



            }
            catch (Exception ex)
            {
                // LogUtility.WriteErrorLog(httpContextAccessor.HttpContext, ex, "", "Gateway_PostAuthAdminService.PostGetList", JsonSerializer.Serialize(1));
            }
            //----------------------Log History-----------------------

            returnStatus = await GetApiConfigModel((Int32)apiRequestModel.OrgId, apiRequestModel.ApiKey, "GatewayServiceController", "PostGetList");
            if (returnStatus.Status == Status.Success)
            {
                APIConfigModel apiConfig = JsonSerializer.Deserialize<APIConfigModel>(returnStatus.CustomObject, options);
                var url = configuration["ServiceURL:" + apiConfig.ModuleName.ToUpper() + ""];
                ResponseModel output = await CallPostWebAPIWithoutSession<dynamic>(apiConfig.FullPath + "?orgId=" + apiRequestModel.OrgId, apiRequestModel.ApiParams, url);
                return Ok(output);
            }
            else
            {
                return Ok();
            }
        }

    }
}
