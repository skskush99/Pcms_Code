using Common.Dapper;
using Common.Repository;
using Dapper;
using Master.Dto.Masters;
using Master.Dto.Shared;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Data;
using System.Text;


namespace Master.Repository.RajMaster
{
    public class RajMasterRepository : SqlRepository, IRajMaster
    {
        private readonly LogsService _logsService;
        private readonly System.Data.IDbConnection Con;
        private readonly HttpClient _httpClient;
        public RajMasterRepository(IConfiguration configuration, LogsService logsService) : base(configuration)
        {
            _logsService = logsService;
            _httpClient = new HttpClient();
        }

        public async Task<ResponseModel> AddStateRajMaster(RajMasterModel objModel, int MasterDataID)
        {
            if (MasterDataID != 17)
            {
                return new ResponseModel { Status = true, Message = "Invalid State MasterDataID" };
            }

            try
            {
                // 1. Get data using the common function
                DataTable dt = await GetRajMasterData(MasterDataID, nameof(AddStateRajMaster));

                // 2. Database Logic
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "AddStatesRajMaster");
                    parmeters.Add("@IdStates", dt.AsTableValuedParameter("dbo.Mst_States_Raj_Type"));
                    var objData = await Con.QueryAsync<ResponseModel>("spMstRajMaster", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseModel();
                }
            }
            catch (Exception ex)
            {
                // Handle any Exception re-thrown from GetRajMasterData
                // (Logging has already occurred in the common function)
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseModel> AddDivisionRajMaster(RajMasterModel objModel, int MasterDataID)
        {
            if (MasterDataID != 57)
            {
                return new ResponseModel { Status = true, Message = "Invalid Division MasterDataID" };
            }

            try
            {
                // 1. Get data using the common function
                DataTable dt = await GetRajMasterData(MasterDataID, nameof(AddDivisionRajMaster));

                // 2. Database Logic
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "AddDivisionRajMaster");
                    parmeters.Add("@IdDivision", dt.AsTableValuedParameter("dbo.Mst_Division_Raj_Type1"));
                    var objData = await Con.QueryAsync<ResponseModel>("spMstRajMaster", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseModel();
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseModel> AddDistrictRajMaster(RajMasterModel objModel, int MasterDataID)
        {
            if (MasterDataID != 56)
            {
                return new ResponseModel { Status = true, Message = "Invalid District MasterDataID" };
            }

            try
            {
                // 1. Get data using the common function
                DataTable dt = await GetRajMasterData(MasterDataID, nameof(AddDistrictRajMaster));

                // 2. Database Logic
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "AddDistrictRajMaster");
                    parmeters.Add("@IdDistrict", dt.AsTableValuedParameter("dbo.Mst_Districts_Raj_Type"));
                    var objData = await Con.QueryAsync<ResponseModel>("spMstRajMaster", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseModel();
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseModel> AddPoliceRangeRajMaster(RajMasterModel objModel, int MasterDataID)
        {
            if (MasterDataID != 88)
            {
                return new ResponseModel { Status = true, Message = "Invalid Police Range MasterDataID" };
            }

            try
            {
                // 1. Get data using the common function
                DataTable dt = await GetRajMasterData(MasterDataID, nameof(AddPoliceRangeRajMaster));

                // 2. Database Logic
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "AddPoliceRangeRajMaster");
                    parmeters.Add("@IdPoliceRange", dt.AsTableValuedParameter("dbo.Mst_PoliceRange_Raj_Type"));
                    var objData = await Con.QueryAsync<ResponseModel>("spMstRajMaster", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseModel();
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseModel> AddPoliceDistrictRajMaster(RajMasterModel objModel, int MasterDataID)
        {
            if (MasterDataID != 89)
            {
                return new ResponseModel { Status = true, Message = "Invalid Police District MasterDataID" };
            }

            try
            {
                // 1. Get data using the common function
                DataTable dt = await GetRajMasterData(MasterDataID, nameof(AddPoliceDistrictRajMaster));

                // 2. Database Logic
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "AddPoliceDistrictRajMaster");
                    parmeters.Add("@IdPoliceDistrict", dt.AsTableValuedParameter("dbo.Mst_PoliceDistrict_Raj_Type"));
                    var objData = await Con.QueryAsync<ResponseModel>("spMstRajMaster", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseModel();
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }
        
        public async Task<ResponseModel> AddPoliceCircleRajMaster(RajMasterModel objModel, int MasterDataID)
        {
            if (MasterDataID != 90)
            {
                return new ResponseModel { Status = true, Message = "Invalid Police Circle MasterDataID" };
            }

            try
            {
                // 1. Get data using the common function
                DataTable dt = await GetRajMasterData(MasterDataID, nameof(AddPoliceCircleRajMaster));

                // 2. Database Logic
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "AddPoliceCircleRajMaster");
                    parmeters.Add("@IdPoliceCircle", dt.AsTableValuedParameter("dbo.Mst_PoliceCircle_Raj_Type"));
                    var objData = await Con.QueryAsync<ResponseModel>("spMstRajMaster", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseModel();
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<ResponseModel> AddPoliceStationRajMaster(RajMasterModel objModel, int MasterDataID)
        {
            if (MasterDataID != 91)
            {
                return new ResponseModel { Status = true, Message = "Invalid Police Station MasterDataID" };
            }

            try
            {
                // 1. Get data using the common function
                DataTable dt = await GetRajMasterData(MasterDataID, nameof(AddPoliceStationRajMaster));

                // 2. Database Logic
                using (var Con = GetOpenConnection())
                {
                    var parmeters = new DynamicParameters();
                    parmeters.Add("@Action", "AddPoliceStationRajMaster");
                    parmeters.Add("@IdPoliceStation", dt.AsTableValuedParameter("dbo.Mst_PoliceStation_Raj_Type"));
                    var objData = await Con.QueryAsync<ResponseModel>("spMstRajMaster", parmeters, commandType: CommandType.StoredProcedure);
                    var objResut = objData.FirstOrDefault();
                    DisposeCurrentSqlConnection();
                    return objResut != null ? objResut : new ResponseModel();
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel()
                {
                    Status = false,
                    Message = ex.Message,
                };
            }
        }

        private async Task<DataTable> GetRajMasterData(int masterDataID, string callingFunctionName)
        {
            // Common static data
            string username = "Doit";
            string password = "Doit@123";
            string projectCode = "476U1KUGNW";
            string masterDataIdString = masterDataID.ToString();
            string isNew = "True";
            string isActive = "1";
            string modificationDate = "01-01-2015";
            string url = "https://api.sewadwaar.rajasthan.gov.in/app/live/master/getmasterdata/service?client_id=acc7ae33e0fd0d4a77cc675597c284da";

            string json = JsonConvert.SerializeObject(new
            {
                Username = username,
                Password = password,
                ProjectCode = projectCode,
                MasterDataID = masterDataIdString,
                IsNew = isNew,
                IsActive = isActive,
                ModificationDate = modificationDate
            });

            try
            {
                // Send HTTP POST request
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                // Assuming _httpClient is available in your class
                HttpResponseMessage response = await _httpClient.PostAsync(url, content);

                // Call EnsureSuccessStatusCode() here to throw an Exception on HTTP error codes
                response.EnsureSuccessStatusCode();

                string responseString = await response.Content.ReadAsStringAsync();

                // Extract and convert JSON data to DataTable
                // This logic assumes the JSON response contains a string that starts with the JSON array structure '[...]'
                int startIndex = responseString.IndexOf('[');
                if (startIndex == -1)
                {
                    // Throw an appropriate Exception or return null if the expected JSON array is not found
                    throw new Exception("Invalid response format: Cannot find start of JSON array.");
                }

                string output = responseString.Substring(startIndex);

                // Removing the last character ']' (as per your original code)
                output = output.Remove(output.Length - 1);

                DataTable dt = JsonConvert.DeserializeObject<DataTable>(output);
                return dt;
            }
            catch (Exception ex)
            {
                // Log the errors in this common function
                // Assuming _logsService is available in your class
                _logsService.Logs("Error", callingFunctionName, ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/RajMasterRepository/" + callingFunctionName);
                throw; // Re-throw the Exception so the caller can handle it
            }
        }


        //public async Task<ResponseModel> AddCityRajMaster(RajMasterModel objModel, int MasterDataID)
        //{
        //    string username = "Doit";
        //    string password = "Doit@123";
        //    string projectCode = "476U1KUGNW";
        //    string masterDataId = MasterDataID.ToString();
        //    string isNew = "True";
        //    string isActive = "1";
        //    string modificationDate = "01-01-2015";
        //    string url = "https://api.sewadwaar.rajasthan.gov.in/app/live/master/getmasterdata/service?client_id=acc7ae33e0fd0d4a77cc675597c284da";

        //    string json = JsonConvert.SerializeObject(new
        //    {
        //        Username = username,
        //        Password = password,
        //        ProjectCode = projectCode,
        //        MasterDataID = masterDataId,
        //        IsNew = isNew,
        //        IsActive = isActive,
        //        ModificationDate = modificationDate
        //    });

        //    try
        //    {
        //        // Send HTTP POST request
        //        var content = new StringContent(json, Encoding.UTF8, "application/json");
        //        HttpResponseMessage response = await _httpClient.PostAsync(url, content);
        //        response.EnsureSuccessStatusCode();

        //        string responseString = await response.Content.ReadAsStringAsync();

        //        // Extract and convert JSON data to DataTable
        //        string output = responseString.Substring(responseString.IndexOf('['));

        //        output = output.Remove(output.Length - 1);
        //        DataTable dt = JsonConvert.DeserializeObject<DataTable>(output);

        //        // string responseMessage;

        //        if (MasterDataID == 3)
        //        {
        //            //responseMessage = SyncCityMaster(dt);
        //            try
        //            {
        //                using (var Con = GetOpenConnection())
        //                {
        //                    var parmeters = new DynamicParameters();
        //                    parmeters.Add("@Action", "AddCitysRajMaster");
        //                    parmeters.Add("@IdCitys", dt.AsTableValuedParameter("dbo.mst_City_Type"));
        //                    var objData = await Con.QueryAsync<ResponseModel>("spMstRajMaster", parmeters, commandType: CommandType.StoredProcedure);
        //                    var objResut = objData.FirstOrDefault();
        //                    DisposeCurrentSqlConnection();
        //                    return objResut != null ? objResut : new ResponseModel();
        //                }
        //            }
        //            catch (Exception)
        //            {
        //                throw;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logsService.Logs("Error", "AddCityRajMaster", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/RajMasterRepository/AddCityRajMaster");
        //        return new ResponseModel()
        //        {
        //            Status = false,
        //            Message = ex.Message,
        //        };
        //    }
        //    return new ResponseModel { Status = true, Message = "Invalid City MasterDataID" };
        //}
        //public async Task<ResponseModel> AddSubDivisionRajMaster(RajMasterModel objModel, int MasterDataID)
        //{
        //    string username = "Doit";
        //    string password = "Doit@123";
        //    string projectCode = "476U1KUGNW";
        //    string masterDataId = MasterDataID.ToString();
        //    string isNew = "True";
        //    string isActive = "1";
        //    string modificationDate = "01-01-2015";
        //    string url = "https://api.sewadwaar.rajasthan.gov.in/app/live/master/getmasterdata/service?client_id=acc7ae33e0fd0d4a77cc675597c284da";

        //    string json = JsonConvert.SerializeObject(new
        //    {
        //        Username = username,
        //        Password = password,
        //        ProjectCode = projectCode,
        //        MasterDataID = masterDataId,
        //        IsNew = isNew,
        //        IsActive = isActive,
        //        ModificationDate = modificationDate
        //    });

        //    try
        //    {
        //        // Send HTTP POST request
        //        var content = new StringContent(json, Encoding.UTF8, "application/json");
        //        HttpResponseMessage response = await _httpClient.PostAsync(url, content);
        //        response.EnsureSuccessStatusCode();

        //        string responseString = await response.Content.ReadAsStringAsync();

        //        // Extract and convert JSON data to DataTable
        //        string output = responseString.Substring(responseString.IndexOf('['));

        //        output = output.Remove(output.Length - 1);
        //        DataTable dt = JsonConvert.DeserializeObject<DataTable>(output);

        //        // string responseMessage;

        //        if (MasterDataID == 71)
        //        {
        //            //responseMessage = SyncSubDivisionMaster(dt);
        //            try
        //            {
        //                using (var Con = GetOpenConnection())
        //                {
        //                    var parmeters = new DynamicParameters();
        //                    parmeters.Add("@Action", "AddSubDivisionsRajMaster");
        //                    parmeters.Add("@IdSubDivisions", dt.AsTableValuedParameter("dbo.mst_SubDivisions_Type"));
        //                    var objData = await Con.QueryAsync<ResponseModel>("spMstRajMaster", parmeters, commandType: CommandType.StoredProcedure);
        //                    var objResut = objData.FirstOrDefault();
        //                    DisposeCurrentSqlConnection();
        //                    return objResut != null ? objResut : new ResponseModel();
        //                }
        //            }
        //            catch (Exception)
        //            {
        //                throw;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logsService.Logs("Error", "AddSubDivisionRajMaster", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/RajMasterRepository/AddSubDivisionRajMaster");
        //        return new ResponseModel()
        //        {
        //            Status = false,
        //            Message = ex.Message,
        //        };
        //    }
        //    return new ResponseModel { Status = true, Message = "Invalid Sub-Division MasterDataID" };
        //}
        //public async Task<ResponseModel> AddTehsilRajMaster(RajMasterModel objModel, int MasterDataID)
        //{
        //    string username = "Doit";
        //    string password = "Doit@123";
        //    string projectCode = "476U1KUGNW";
        //    string masterDataId = MasterDataID.ToString();
        //    string isNew = "True";
        //    string isActive = "1";
        //    string modificationDate = "01-01-2015";
        //    string url = "https://api.sewadwaar.rajasthan.gov.in/app/live/master/getmasterdata/service?client_id=acc7ae33e0fd0d4a77cc675597c284da";

        //    string json = JsonConvert.SerializeObject(new
        //    {
        //        Username = username,
        //        Password = password,
        //        ProjectCode = projectCode,
        //        MasterDataID = masterDataId,
        //        IsNew = isNew,
        //        IsActive = isActive,
        //        ModificationDate = modificationDate
        //    });

        //    try
        //    {
        //        // Send HTTP POST request
        //        var content = new StringContent(json, Encoding.UTF8, "application/json");
        //        HttpResponseMessage response = await _httpClient.PostAsync(url, content);
        //        response.EnsureSuccessStatusCode();

        //        string responseString = await response.Content.ReadAsStringAsync();

        //        // Extract and convert JSON data to DataTable
        //        string output = responseString.Substring(responseString.IndexOf('['));

        //        output = output.Remove(output.Length - 1);
        //        DataTable dt = JsonConvert.DeserializeObject<DataTable>(output);

        //        // string responseMessage;

        //        if (MasterDataID == 62)
        //        {
        //            //responseMessage = SyncTehsilMaster(dt);
        //            try
        //            {
        //                using (var Con = GetOpenConnection())
        //                {
        //                    var parmeters = new DynamicParameters();
        //                    parmeters.Add("@Action", "AddTehsilsRajMaster");
        //                    parmeters.Add("@IdTehsils", dt.AsTableValuedParameter("dbo.mst_Tehsils_Type"));
        //                    var objData = await Con.QueryAsync<ResponseModel>("spMstRajMaster", parmeters, commandType: CommandType.StoredProcedure);
        //                    var objResut = objData.FirstOrDefault();
        //                    DisposeCurrentSqlConnection();
        //                    return objResut != null ? objResut : new ResponseModel();
        //                }
        //            }
        //            catch (Exception)
        //            {
        //                throw;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logsService.Logs("Error", "AddTehsilRajMaster", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/RajMasterRepository/AddTehsilRajMaster");
        //        return new ResponseModel()
        //        {
        //            Status = false,
        //            Message = ex.Message,
        //        };
        //    }
        //    return new ResponseModel { Status = true, Message = "Invalid Tehsil MasterDataID" };
        //}
        //public async Task<ResponseModel> AddDesignationRajMaster(RajMasterModel objModel, int MasterDataID)
        //{
        //    string username = "Doit";
        //    string password = "Doit@123";
        //    string projectCode = "476U1KUGNW";
        //    string masterDataId = MasterDataID.ToString();
        //    string isNew = "True";
        //    string isActive = "1";
        //    string modificationDate = "01-01-2015";
        //    string url = "https://api.sewadwaar.rajasthan.gov.in/app/live/master/getmasterdata/service?client_id=acc7ae33e0fd0d4a77cc675597c284da";

        //    string json = JsonConvert.SerializeObject(new
        //    {
        //        Username = username,
        //        Password = password,
        //        ProjectCode = projectCode,
        //        MasterDataID = masterDataId,
        //        IsNew = isNew,
        //        IsActive = isActive,
        //        ModificationDate = modificationDate
        //    });

        //    try
        //    {
        //        // Send HTTP POST request
        //        var content = new StringContent(json, Encoding.UTF8, "application/json");
        //        HttpResponseMessage response = await _httpClient.PostAsync(url, content);
        //        response.EnsureSuccessStatusCode();

        //        string responseString = await response.Content.ReadAsStringAsync();

        //        // Extract and convert JSON data to DataTable
        //        string output = responseString.Substring(responseString.IndexOf('['));

        //        output = output.Remove(output.Length - 1);
        //        DataTable dt = JsonConvert.DeserializeObject<DataTable>(output);

        //        // string responseMessage;

        //        if (MasterDataID == 107)
        //        {
        //            //responseMessage = SyncDesignationMaster(dt);
        //            try
        //            {
        //                using (var Con = GetOpenConnection())
        //                {
        //                    var parmeters = new DynamicParameters();
        //                    parmeters.Add("@Action", "AddDesignationRajMaster");
        //                    parmeters.Add("@IdDesignation", dt.AsTableValuedParameter("dbo.mst_DesignationRajmaster_Type"));
        //                    var objData = await Con.QueryAsync<ResponseModel>("spMstRajMaster", parmeters, commandType: CommandType.StoredProcedure);
        //                    var objResut = objData.FirstOrDefault();
        //                    DisposeCurrentSqlConnection();
        //                    return objResut != null ? objResut : new ResponseModel();
        //                }
        //            }
        //            catch (Exception)
        //            {
        //                throw;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logsService.Logs("Error", "AddDesignationRajMaster", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/RajMasterRepository/AddDesignationRajMaster");
        //        return new ResponseModel()
        //        {
        //            Status = false,
        //            Message = ex.Message,
        //        };
        //    }
        //    return new ResponseModel { Status = true, Message = "Invalid Designation MasterDataID" };
        //}
        //public async Task<ResponseModel> AddAdminDepartmentRajMaster(RajMasterModel objModel, int MasterDataID)
        //{
        //    string username = "Doit";
        //    string password = "Doit@123";
        //    string projectCode = "476U1KUGNW";
        //    string masterDataId = MasterDataID.ToString();
        //    string isNew = "True";
        //    string isActive = "1";
        //    string modificationDate = "01-01-2015";
        //    string url = "https://api.sewadwaar.rajasthan.gov.in/app/live/master/getmasterdata/service?client_id=acc7ae33e0fd0d4a77cc675597c284da";

        //    string json = JsonConvert.SerializeObject(new
        //    {
        //        Username = username,
        //        Password = password,
        //        ProjectCode = projectCode,
        //        MasterDataID = masterDataId,
        //        IsNew = isNew,
        //        IsActive = isActive,
        //        ModificationDate = modificationDate
        //    });

        //    try
        //    {
        //        // Send HTTP POST request
        //        var content = new StringContent(json, Encoding.UTF8, "application/json");
        //        HttpResponseMessage response = await _httpClient.PostAsync(url, content);
        //        response.EnsureSuccessStatusCode();

        //        string responseString = await response.Content.ReadAsStringAsync();

        //        // Extract and convert JSON data to DataTable
        //        string output = responseString.Substring(responseString.IndexOf('['));

        //        output = output.Remove(output.Length - 1);
        //        DataTable dt = JsonConvert.DeserializeObject<DataTable>(output);

        //        // string responseMessage;

        //        if (MasterDataID == 81)
        //        {
        //            //responseMessage = SyncAdminDepartmentMaster(dt);
        //            try
        //            {
        //                using (var Con = GetOpenConnection())
        //                {
        //                    var parmeters = new DynamicParameters();
        //                    parmeters.Add("@Action", "AddAdminDepartmentRajMaster");
        //                    parmeters.Add("@IdAdminDepartment", dt.AsTableValuedParameter("dbo.mst_AdminDepartment_Type"));
        //                    var objData = await Con.QueryAsync<ResponseModel>("spMstRajMaster", parmeters, commandType: CommandType.StoredProcedure);
        //                    var objResut = objData.FirstOrDefault();
        //                    DisposeCurrentSqlConnection();
        //                    return objResut != null ? objResut : new ResponseModel();
        //                }
        //            }
        //            catch (Exception)
        //            {
        //                throw;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logsService.Logs("Error", "AddAdminDepartmentRajMaster", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/RajMasterRepository/AddAdminDepartmentRajMaster");
        //        return new ResponseModel()
        //        {
        //            Status = false,
        //            Message = ex.Message,
        //        };
        //    }
        //    return new ResponseModel { Status = true, Message = "Invalid Admin Department MasterDataID" };
        //}
        //public async Task<ResponseModel> AddAdminUnitsDepartmentRajMaster(RajMasterModel objModel, int MasterDataID)
        //{
        //    string username = "Doit";
        //    string password = "Doit@123";
        //    string projectCode = "476U1KUGNW";
        //    string masterDataId = MasterDataID.ToString();
        //    string isNew = "True";
        //    string isActive = "1";
        //    string modificationDate = "01-01-2015";
        //    string url = "https://api.sewadwaar.rajasthan.gov.in/app/live/master/getmasterdata/service?client_id=acc7ae33e0fd0d4a77cc675597c284da";

        //    string json = JsonConvert.SerializeObject(new
        //    {
        //        Username = username,
        //        Password = password,
        //        ProjectCode = projectCode,
        //        MasterDataID = masterDataId,
        //        IsNew = isNew,
        //        IsActive = isActive,
        //        ModificationDate = modificationDate
        //    });

        //    try
        //    {
        //        // Send HTTP POST request
        //        var content = new StringContent(json, Encoding.UTF8, "application/json");
        //        HttpResponseMessage response = await _httpClient.PostAsync(url, content);
        //        response.EnsureSuccessStatusCode();

        //        string responseString = await response.Content.ReadAsStringAsync();

        //        // Extract and convert JSON data to DataTable
        //        string output = responseString.Substring(responseString.IndexOf('['));

        //        output = output.Remove(output.Length - 1);
        //        DataTable dt = JsonConvert.DeserializeObject<DataTable>(output);

        //        // string responseMessage;

        //        if (MasterDataID == 82)
        //        {
        //            //responseMessage = SyncAdminUnitsDepartmentMaster(dt);
        //            try
        //            {
        //                using (var Con = GetOpenConnection())
        //                {
        //                    var parmeters = new DynamicParameters();
        //                    parmeters.Add("@Action", "AddUnitRajMaster");
        //                    parmeters.Add("@IdUnit", dt.AsTableValuedParameter("dbo.mst_Units_Type"));
        //                    var objData = await Con.QueryAsync<ResponseModel>("spMstRajMaster", parmeters, commandType: CommandType.StoredProcedure);
        //                    var objResut = objData.FirstOrDefault();
        //                    DisposeCurrentSqlConnection();
        //                    return objResut != null ? objResut : new ResponseModel();
        //                }
        //            }
        //            catch (Exception)
        //            {
        //                throw;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logsService.Logs("Error", "AddAdminUnitsDepartmentRajMaster", ex.Message, ex.StackTrace, ex.Source, "MasterService/Master.Repository/RajMasterRepository/AddAdminUnitsDepartmentRajMaster");
        //        return new ResponseModel()
        //        {
        //            Status = false,
        //            Message = ex.Message,
        //        };
        //    }
        //    return new ResponseModel { Status = true, Message = "Invalid Admin Units Department MasterDataID" };
        //}


    }
}
