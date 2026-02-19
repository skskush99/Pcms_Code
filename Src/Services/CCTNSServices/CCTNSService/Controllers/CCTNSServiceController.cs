using Common.Repository;
using Core.Enums.User;
using CCTNSDto;
using CCTNSDto.CCTNS;
using CCTNSDto.Shared;
using CCTNSServiceBus.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CCTNSService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CCTNSServiceController : ControllerBase
    {
        private readonly IUnitOfWorkService unitOfWork;
        private IConfiguration _Configuration;
        private const int MaxRetries = 3;  // Max retry attempts
        private const int RetryDelayMilliseconds = 1000;  // Delay between retries
        private readonly LogsService _logsService;

        public CCTNSServiceController(IUnitOfWorkService unitOfWorkService, IConfiguration Configuration, LogsService logsService)
        {
            unitOfWork = unitOfWorkService;
            _Configuration = Configuration;
            _logsService = logsService;

        }
        [HttpGet]
        [Route("Token")]
        public async Task<ResponseWithoutPaginationModel> GetAuthToken()
        {
            try
            {
                var authUser = new TokenAuthModel();
                var data = _Configuration.GetSection("Credentials:CCTNS").Get<CCTNSCredentials>();
                return await unitOfWork.CCTNSService.GetAuthToken(data);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetAuthToken", ex.Message, ex.StackTrace, ex.Source, "CCTNSService/CCTNSServiceController/GetAuthToken");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        [HttpGet]
        [Route("District")]
        public async Task<ResponseWithoutPaginationModel> GetDistrictDetail(string StateCode)
        {
            try
            {
                var result = new ResponseWithoutPaginationModel();
                try
                {
                    int attempt = 0;
                    while (attempt < MaxRetries)
                    {
                        attempt++;
                        // Attempt the API call
                        var data = _Configuration.GetSection("Credentials:CCTNS").Get<CCTNSCredentials>();
                        var tokenobject = await unitOfWork.CCTNSService.GetAuthToken(data);
                        if (!string.IsNullOrEmpty(Convert.ToString(tokenobject?.Data?.access_token)))
                        {
                            var auth = tokenobject.Data.access_token;
                            return await unitOfWork.CCTNSService.GetDistrictDetail(StateCode, auth, data);
                        }
                        else
                        {
                            result = new ResponseWithoutPaginationModel()
                            {
                                Status = true,
                                Message = tokenobject?.Data?.Error,
                                Data = tokenobject?.Data
                            };
                            return result;
                        }
                    }
                    result = new ResponseWithoutPaginationModel()
                    {
                        Status = true,
                        Message = "You have Reached Limit Please try again",
                        Data = null
                    };
                    return result;
                }
                catch (Exception ex)
                {
                    result = new ResponseWithoutPaginationModel()
                    {
                        Status = true,
                        Message = ex.Message,
                        Data = null
                    };
                    return result;
                    throw;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetDistrictDetail", ex.Message, ex.StackTrace, ex.Source, "CCTNSService/CCTNSServiceController/GetDistrictDetail");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

    }
}
