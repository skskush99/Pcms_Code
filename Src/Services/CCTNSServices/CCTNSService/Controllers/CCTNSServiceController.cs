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
        [HttpPost]
        [Route("ClientAppToken")]
        public async Task<ResponseWithoutPaginationModel> GetClientAppToken()
        {
            try
            {
                var data = _Configuration.GetSection("Credentials:CCTNS").Get<CCTNSCredentials>();

                return await unitOfWork.CCTNSService.GetClientAppToken(data);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetClientAppToken", ex.Message, ex.StackTrace, ex.Source, "CCTNSService/CCTNSServiceController/GetClientAppToken");

                return new ResponseWithoutPaginationModel
                {
                    Status = false,
                    Message = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("FIRDetails")]
        public async Task<ResponseWithoutPaginationModel> GetFIRDetails(string PSCode, string FIRNum, string FIRYear)
        {
            try
            {
                var data = _Configuration.GetSection("Credentials1:AuthCCTNS").Get<AuthCCTNSCredentials>();
                // Ensure FIRNum is 4 digits (add leading zero)
                var paddedFIRNum = FIRNum.PadLeft(4, '0');

                // Get last 2 digits of year
                var yearLastTwo = FIRYear.Substring(FIRYear.Length - 2);

                // Create firNum
                var firNum = PSCode + yearLastTwo + paddedFIRNum;

                return await unitOfWork.CCTNSService.GetFIRDetails(data, firNum);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetFIRDetails", ex.Message, ex.StackTrace, ex.Source, "CCTNSService/CCTNSServiceController/GetFIRDetails");

                return new ResponseWithoutPaginationModel
                {
                    Status = false,
                    Message = ex.Message
                };
            }
        }
    }
}
