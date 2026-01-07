using EcourtDto.Shared;
using Microsoft.AspNetCore.Mvc;
using EcourtServiceBus.UnitOfWork;
using EcourtDto;
using EcourtDto.Ecourt;
using System.ComponentModel.DataAnnotations;
using Common.Repository;

namespace EcourtService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EcourtServiceController : ControllerBase
    {
        private readonly IUnitOfWorkService unitOfWork;
        private IConfiguration _Configuration;
        private const int MaxRetries = 3;  // Max retry attempts
        private const int RetryDelayMilliseconds = 1000;  // Delay between retries
        private readonly LogsService _logsService;
        public EcourtServiceController(IUnitOfWorkService unitOfWorkService, IConfiguration Configuration, LogsService logsService)
        {
            unitOfWork = unitOfWorkService;
            _Configuration = Configuration;
            _logsService = logsService;
        }

        /// <summary>
        /// Creates an Token To Access Other API's.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/EcourtService/GetAuthToken/Token
        ///     
        ///  this API provide Authorization token to Access Other API's
        /// </remarks>
        [HttpGet]
        [Route("Token")]
        public async Task<ResponseWithoutPaginationModel> GetAuthToken()
        {
            try
            {
                var authUser = new TokenAuthModel();
                var data = _Configuration.GetSection("Credentials:Ecourt").Get<EcourtCredentials>();
                return await unitOfWork.EcourtService.GetAuthToken(data);
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetAuthToken", ex.Message, ex.StackTrace, ex.Source, "EcourtService/EcourtServiceController/GetAuthToken");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        /// <summary>
        /// API provides complete history of the case including, case details, party names, current status, 
        /// daily proceedings, orders, judgments, IA details and process issued information based on 
        /// CNR number of the case.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/EcourtService/GetDetailByCNR/SearchByCnr?CinNo=RJAJ1A0000862019
        ///     
        ///  this API provides Case Detail by Providing CNR Number 
        /// </remarks>
        [HttpGet]
        [Route("SearchByCnr")]
        public async Task<ResponseWithoutPaginationModel> GetDetailByCNR(string CinNo)
        {
            try
            {
                var result = new ResponseWithoutPaginationModel();
                int attempt = 0;
                const int MaxRetries = 3; // Max retries
                const int RetryDelayMilliseconds = 1000; // Delay between retries

                try
                {
                    while (attempt < MaxRetries)
                    {
                        attempt++;
                        try
                        {
                            // Get Ecourt credentials
                            var data = _Configuration.GetSection("Credentials:Ecourt").Get<EcourtCredentials>();

                            // Get authentication token
                            var tokenObject = await unitOfWork.EcourtService.GetAuthToken(data);

                            // If token is valid
                            if (!string.IsNullOrEmpty(Convert.ToString(tokenObject?.Data?.access_token)))
                            {
                                var authToken = tokenObject.Data.access_token;

                                // Make the actual call to get details by CNR
                                var apiResponse = await unitOfWork.EcourtService.GetDetailByCNR(CinNo, authToken, data);

                                if (apiResponse != null)
                                {
                                    // Success response
                                    result.Status = true;
                                    result.Message = "Success";
                                    result.Data = apiResponse;
                                    return result;
                                }
                                else
                                {
                                    // If the API response is null, handle this case
                                    result.Status = false;
                                    result.Message = "Failed to retrieve details.";
                                    result.Data = null;
                                    return result;
                                }
                            }
                            else
                            {
                                // If access token is not available
                                result.Status = false;
                                result.Message = tokenObject?.Data?.Error ?? "Failed to get access token.";
                                result.Data = tokenObject?.Data;
                                return result;
                            }
                        }
                        catch (Exception ex)
                        {
                            await Task.Delay(RetryDelayMilliseconds); // Wait before retrying
                        }
                    }

                    // After all retries are exhausted, return a failure message
                    result.Status = false;
                    result.Message = "You have reached the retry limit. Please try again later.";
                    result.Data = null;
                    return result;
                }
                catch (Exception ex)
                {
                    // Handle unexpected exceptions
                    result.Status = false;
                    result.Message = $"An unexpected error occurred: {ex.Message}";
                    result.Data = null;
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetDetailByCNR", ex.Message, ex.StackTrace, ex.Source, "EcourtService/EcourtServiceController/GetDetailByCNR");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }

        }

        //[HttpGet]
        //[Route("SearchByCnr")]
        ////[Consumes("application/x-www-form-urlencoded")]
        //public async Task<ResponseWithoutPaginationModel> GetDetailByCNR(string CinNo)
        //{
        //    var result = new ResponseWithoutPaginationModel();
        //    try
        //    {
        //        int attempt = 0;
        //        while (attempt < MaxRetries)
        //        {
        //            attempt++;
        //            // Attempt the API call
        //            var data = _Configuration.GetSection("Credentials:Ecourt").Get<EcourtCredentials>();
        //            var tokenobject =await unitOfWork.EcourtService.GetAuthToken(data);
        //            if (!string.IsNullOrEmpty(Convert.ToString(tokenobject?.Data?.access_token)))
        //            {
        //                var auth = tokenobject.Data.access_token;
        //                return await unitOfWork.EcourtService.GetDetailByCNR(CinNo, auth, data);
        //            }
        //            else
        //            {
        //                result = new ResponseWithoutPaginationModel()
        //                {
        //                    Status = true,
        //                    Message = tokenobject?.Data?.Error,
        //                    Data = tokenobject?.Data
        //                };
        //                return result;
        //            }
        //        }
        //        result = new ResponseWithoutPaginationModel()
        //        {
        //            Status = true,
        //            Message = "You have Reached Limit Please try again",
        //            Data = null
        //        };
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        result = new ResponseWithoutPaginationModel()
        //        {
        //            Status = true,
        //            Message = ex.Message,
        //            Data = null
        //        };
        //        return result;
        //        throw;
        //    }
        //}
        /// <summary>
        /// API provides only the current status like next date, stage or decision date 
        /// of the case based on CNR number. Instead of getting the complete history of 
        /// case which is more elaborate, this API provides the current status of the 
        /// case in short. This API is used to update the current status of the case and 
        /// may be used in batch processing mode periodically. Single CNR or Multiple 
        /// CNRs can be sent as request parameter to get the current status of the Case(s).
        /// </summary>
        /// <remarks>
        /// Search by CNR (Current Case Status) API
        /// Sample request:
        ///
        ///     GET /api/EcourtService/GetDetailByCNRBulk/SearchByCnr?CinNo=RJAJ1A0000862019
        ///  
        /// </remarks>
        [HttpPost]
        [Route("SearchByCnrBulk")]
        public async Task<ResponseWithoutPaginationModel> GetDetailByCNRBulk([FromForm, Required] GetDetailByCNR obj)
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
                        var data = _Configuration.GetSection("Credentials:Ecourt").Get<EcourtCredentials>();
                        var tokenobject = await unitOfWork.EcourtService.GetAuthToken(data);
                        if (!string.IsNullOrEmpty(Convert.ToString(tokenobject?.Data?.access_token)))
                        {
                            var auth = tokenobject.Data.access_token;
                            return await unitOfWork.EcourtService.GetDetailByCNRBulk(obj.CinNo, auth, data);
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
                _logsService.Logs("Error", "GetDetailByCNRBulk", ex.Message, ex.StackTrace, ex.Source, "EcourtService/EcourtServiceController/GetDetailByCNRBulk");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        /// <summary>
        /// API provides list of the Case Types (referred as type_name) and relevant Case Type Codes 
        /// (referred as case_type) used in the establishment for further accessing relevant APIs.
        /// Input for this API is Establishment Code and other details.Type Name and Case Type retrieved 
        /// using this API can be used to access the case data using various Case Search APIs like Search 
        /// by Case Registration Number, Filing Number etc.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/EcourtService/GetDetailCaseType/CaseTypeMaster?EstCode=MHAU01
        ///  
        /// </remarks>
        [HttpGet]
        [Route("CaseTypeMaster")]
        public async Task<ResponseWithoutPaginationModel> GetDetailCaseType(string EstCode)
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
                        var data = _Configuration.GetSection("Credentials:Ecourt").Get<EcourtCredentials>();
                        var tokenobject = await unitOfWork.EcourtService.GetAuthToken(data);
                        if (!string.IsNullOrEmpty(Convert.ToString(tokenobject?.Data?.access_token)))
                        {
                            var auth = tokenobject.Data.access_token;
                            return await unitOfWork.EcourtService.GetDetailCaseType(EstCode, auth, data);
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
                _logsService.Logs("Error", "GetDetailCaseType", ex.Message, ex.StackTrace, ex.Source, "EcourtService/EcourtServiceController/GetDetailCaseType");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }

        }
        /// <summary>
        ///  API provides list of the Case Types (referred as type_name) and relevant Case Type Codes 
        ///  (referred as case_type) used in the establishment for further accessing relevant APIs. Input for 
        ///  this API is Establishment Code and other details. Type Name and Case Type retrieved using this 
        ///  API can be used to access the case data using various Case Search APIs like Search by Case 
        ///  Registration Number, Filing Number etc.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/EcourtService/GetDetailByCaseNumber/SearchByCaseNumber?EstCode=RJHC02&amp;CaseType=48&amp;CaseNumber=133&amp;RegYear=2013
        ///     
        /// Required Params :
        /// 
        ///     {
        ///        EstCode : '6 characters alphanumeric est_code (ex.MHAU01) received using Court Complex API.',
        ///        CaseType : 'Integer value (ex. 15) of the Case Type. This value case_type can be obtained using Case Type Master API.',
        ///        CaseNumber : 'Integer value with max length 7 digits (ex. 234) i.e. Registration number of case. If Case Number is not provided, all the cases registered for the given Case type in the given registration year will be shown. ',
        ///        RegYear : '4 digit integer value (ex 2019). i.e. Registration Year of the case.'
        ///     }
        ///   
        /// </remarks>
        [HttpPost]
        [Route("SearchByCaseNumber")]
        public async Task<ResponseWithoutPaginationModel> GetDetailByCaseNumber(string EstCode, string CaseType, string CaseNumber, string RegYear)
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
                        var data = _Configuration.GetSection("Credentials:Ecourt").Get<EcourtCredentials>();
                        var tokenobject = await unitOfWork.EcourtService.GetAuthToken(data);
                        if (!string.IsNullOrEmpty(Convert.ToString(tokenobject?.Data?.access_token)))
                        {
                            var auth = tokenobject.Data.access_token;
                            return await unitOfWork.EcourtService.GetDetailByCaseNumber(EstCode, CaseType, CaseNumber, RegYear, auth, data);
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
                _logsService.Logs("Error", "GetDetailByCaseNumber", ex.Message, ex.StackTrace, ex.Source, "EcourtService/EcourtServiceController/GetDetailByCaseNumber");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        /// <summary>
        ///  API provides list of cases from a High court establishment matching name of the party. Input for this
        ///  API is Establishment Code, Litigants Name, Case Registration Year, Status of the Case(Pending or
        ///  Disposed) and other details.The litigants name may be petitioner’s name or respondent’s name or an
        ///  extra party name.The search string for litigant name may consist of full name of the party or even few
        ///  characters(minimum 3) for searching.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/EcourtService/GetDetailByPartyName/SearchByPartyName?EstCode=RJHC02&amp;PendDisp=D&amp;LitigantName=MANANGING%20DIRECTOR%20RIICO%20LTD%20AND%20OTHERS&amp;RegYear=2013
        ///     
        /// Required Params :
        /// 
        ///     {
        ///        EstCode : '6 characters alphanumeric est_code (ex. RJHC02) retrieved using Bench API.',
        ///        PendDisp : 'Character value ‘P’ or ‘D’. P for pending cases and D for disposed cases.',
        ///        LitigantName : 'Character string with maximum length 99(ex. MANANGING DIRECTOR RIICO LTD AND OTHERS). Minimum length of 3 characters is mandatory.',
        ///        RegYear : '4 digit integer value (ex 2013)i.e. Registration Year of the case.'
        ///     }
        ///   
        /// </remarks>
        [HttpPost]
        [Route("SearchByPartyName")]
        public async Task<ResponseWithoutPaginationModel> GetDetailByPartyName(string EstCode, string PendDisp, string LitigantName, string RegYear)
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
                        var data = _Configuration.GetSection("Credentials:Ecourt").Get<EcourtCredentials>();
                        var tokenobject = await unitOfWork.EcourtService.GetAuthToken(data);
                        if (!string.IsNullOrEmpty(Convert.ToString(tokenobject?.Data?.access_token)))
                        {
                            var auth = tokenobject.Data.access_token;
                            return await unitOfWork.EcourtService.GetDetailByPartyName(EstCode, PendDisp, LitigantName, RegYear, auth, data);
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
                _logsService.Logs("Error", "GetDetailByPartyName", ex.Message, ex.StackTrace, ex.Source, "EcourtService/EcourtServiceController/GetDetailByPartyName");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        /// <summary>
        /// API provides list of High Courts and states relevant state codes used in the eCourts project 
        /// for further accessing relevant APIs. This API doesn’t require any input string and can just be 
        /// connected using dept_id,request_str,token &amp; version to get the list of the states.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/EcourtService/GetECourtStateDetail/ECourtState
        ///     
        /// </remarks>
        [HttpGet]
        [Route("ECourtState")]
        public async Task<ResponseWithoutPaginationModel> GetECourtStateDetail()
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
                        var data = _Configuration.GetSection("Credentials:Ecourt").Get<EcourtCredentials>();
                        var tokenobject = await unitOfWork.EcourtService.GetAuthToken(data);
                        if (!string.IsNullOrEmpty(Convert.ToString(tokenobject?.Data?.access_token)))
                        {
                            var auth = tokenobject.Data.access_token;
                            return await unitOfWork.EcourtService.GetECourtStateDetail(auth, data);
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
                _logsService.Logs("Error", "GetECourtStateDetail", ex.Message, ex.StackTrace, ex.Source, "EcourtService/EcourtServiceController/GetECourtStateDetail");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        /// <summary>
        /// API provides list of the Districts and relevant District codes pertaining to the given state 
        /// used in the eCourts project for further accessing relevant APIs. Input for this API is State 
        /// Code and other details. 
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/EcourtService/GetDistrictDetail/District?StateCode=9
        ///  
        /// Required Params :
        /// 
        ///     {
        ///        StateCode : '2 digit state code(ex. 9) retrieved from State Master.'
        ///     }
        ///     
        /// </remarks>
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
                        var data = _Configuration.GetSection("Credentials:Ecourt").Get<EcourtCredentials>();
                        var tokenobject = await unitOfWork.EcourtService.GetAuthToken(data);
                        if (!string.IsNullOrEmpty(Convert.ToString(tokenobject?.Data?.access_token)))
                        {
                            var auth = tokenobject.Data.access_token;
                            return await unitOfWork.EcourtService.GetDistrictDetail(StateCode, auth, data);
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
                _logsService.Logs("Error", "GetDistrictDetail", ex.Message, ex.StackTrace, ex.Source, "EcourtService/EcourtServiceController/GetDistrictDetail");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        /// <summary>
        /// API provides list of the Districts and relevant District codes pertaining to the given state 
        /// used in the eCourts project for further accessing relevant APIs. Input for this API is State 
        /// Code and other details. 
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/EcourtService/GetDistrictDetail/District?StateCode=9
        ///  
        /// Required Params :
        /// 
        ///     {
        ///        StateCode : '2 digit state code(ex. 9) retrieved from State Master.'
        ///     }
        ///     
        /// </remarks>
        [HttpGet]
        [Route("CourtComplex")]
        public async Task<ResponseWithoutPaginationModel> GetCourtComplexDetail(string StateCode, string DistCode)
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
                        var data = _Configuration.GetSection("Credentials:Ecourt").Get<EcourtCredentials>();
                        var tokenobject = await unitOfWork.EcourtService.GetAuthToken(data);
                        if (!string.IsNullOrEmpty(Convert.ToString(tokenobject?.Data?.access_token)))
                        {
                            var auth = tokenobject.Data.access_token;
                            return await unitOfWork.EcourtService.GetCourtComplexDetail(StateCode, DistCode, auth, data);
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
                _logsService.Logs("Error", "GetCourtComplexDetail", ex.Message, ex.StackTrace, ex.Source, "EcourtService/EcourtServiceController/GetCourtComplexDetail");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        /// <summary>
        ///  API provides list of cases from a High court establishment matching name of the party. Input for this
        ///  API is Establishment Code, Litigants Name, Case Registration Year, Status of the Case(Pending or
        ///  Disposed) and other details.The litigants name may be petitioner’s name or respondent’s name or an
        ///  extra party name.The search string for litigant name may consist of full name of the party or even few
        ///  characters(minimum 3) for searching.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/EcourtService/GetDetailByPartyName/SearchByPartyName?EstCode=RJHC02&amp;PendDisp=D&amp;LitigantName=MANANGING%20DIRECTOR%20RIICO%20LTD%20AND%20OTHERS&amp;RegYear=2013
        ///     
        /// Required Params :
        /// 
        ///     {
        ///        EstCode : '6 characters alphanumeric est_code (ex.MHAU01) received using Court Complex API.',
        ///        Courtno : '2 digit court number for which the cause list is to be generated.',
        ///        Causelistdate : 'Cause list date in yyyy-mm-dd format.',
        ///        Cicri : '2 for civil and 3 for criminal.'
        ///     }
        ///   
        /// </remarks>
        [HttpPost]
        [Route("CauseList")]
        public async Task<ResponseWithoutPaginationModel> GetCauseListDetail(string EstCode, string Courtno, string Causelistdate, string Cicri)
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
                        var data = _Configuration.GetSection("Credentials:Ecourt").Get<EcourtCredentials>();
                        var tokenobject = await unitOfWork.EcourtService.GetAuthToken(data);
                        if (!string.IsNullOrEmpty(Convert.ToString(tokenobject?.Data?.access_token)))
                        {
                            var auth = tokenobject.Data.access_token;
                            return await unitOfWork.EcourtService.GetCauseListDetail(EstCode, Courtno, Causelistdate, Cicri, auth, data);
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
                _logsService.Logs("Error", "GetCauseListDetail", ex.Message, ex.StackTrace, ex.Source, "EcourtService/EcourtServiceController/GetCauseListDetail");
                return new ResponseWithoutPaginationModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

    }
}
