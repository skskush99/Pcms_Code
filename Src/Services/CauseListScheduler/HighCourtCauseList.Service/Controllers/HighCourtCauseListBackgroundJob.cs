using HighCourtRajCauseList.Dto.CauseListModel;
using HighCourtRajCauseList.ServiceBus.UnitOfWork;
using Newtonsoft.Json;
using System.Collections;
using System.Net.Http.Headers;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Common.Repository;

namespace HighCourtCauseList.Service.Controllers
{
    public class HighCourtCauseListBackgroundJob : BackgroundService
    {
        private readonly LogsService _logsService;
        private readonly IServiceProvider serviceProvider;
        private readonly IUnitOfWorkService unitOfWorkService;
        public HighCourtCauseListBackgroundJob(IServiceProvider serviceProvider, IUnitOfWorkService unitOfWorkService, LogsService logsService)
        {
            _logsService = logsService;
            this.serviceProvider = serviceProvider;
            this.unitOfWorkService = unitOfWorkService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logsService.Logs("Information", "CauseListService", "Cause list service started on: " + System.DateTime.Now.ToString("dd-MMM-yyyy HH:mm"));
            Console.WriteLine("CauseListService started on: " + System.DateTime.Now.ToString("dd-MMM-yyyy HH:mm"));
            while (!stoppingToken.IsCancellationRequested)
            {
                await ExecuteJobAsync(stoppingToken);

                await ExecuteJobAsyncJustDept(stoppingToken);

                await ExecuteJobAsyncCaseRegistrationHighCourt(stoppingToken);

                // Calculate the time until the next 6:00 AM
                DateTime now = DateTime.Now;
                DateTime nextRun = now.Date.AddDays(1).AddHours(6); // Next day at 6 AM
                if (now.Hour < 6)
                {
                    nextRun = now.Date.AddHours(6); // Today at 6 AM
                }
                TimeSpan delay = nextRun - now;
                _logsService.Logs("Information", "CauseListService", $"CauseListService will run again in {delay.TotalHours} hours at " + System.DateTime.Now.AddHours(delay.TotalHours).ToString("dd-MMM-yyyy HH:mm"));
                Console.WriteLine($"CauseListService will run again in {delay.TotalHours} hours at " + System.DateTime.Now.AddHours(delay.TotalHours).ToString("dd-MMM-yyyy HH:mm"));
                await Task.Delay(delay, stoppingToken);  // Wait until next execution time
            }
            _logsService.Logs("Information", "CauseListService", "Cause list service stop on: " + System.DateTime.Now.ToString("dd-MMM-yyyy HH:mm"));
        }
        private async Task ExecuteJobAsync(CancellationToken stoppingToken)
        {
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var unitOfWork = scope.ServiceProvider.GetRequiredService<HighCourtRajCauseList.ServiceBus.UnitOfWork.IUnitOfWorkService>();
                    ArrayList al = ["JP", "JU"];
                    foreach (var item in al)
                    {
                        for (int i = 0; i <= 7; i++)
                        {
                            string strCauselistDate = DateTime.Now.AddDays(i).ToString("ddMMyyyy");
                            //New CauseList Master API
                            await CauseListMaster(unitOfWork, Convert.ToString(item), strCauselistDate);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetCaseList", ex.Message, ex.StackTrace, ex.Source, "CauseListScheduler/HighCourtCauseList.Service/HighCourtCauseListBackgroundJob/ExecuteJobAsync");
            }
        }

        private async Task ExecuteJobAsyncJustDept(CancellationToken stoppingToken)
        {
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var unitOfWork = scope.ServiceProvider.GetRequiredService<HighCourtRajCauseList.ServiceBus.UnitOfWork.IUnitOfWorkService>();
                    ArrayList al = ["JP", "JU"];
                    foreach (var item in al)
                    {
                        await JustDeptScheduler(unitOfWork, Convert.ToString(item));
                    }
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetCaseList", ex.Message, ex.StackTrace, ex.Source, "CauseListScheduler/HighCourtCauseList.Service/HighCourtCauseListBackgroundJob/ExecuteJobAsync");
            }
        }

        private async Task ExecuteJobAsyncCaseRegistrationHighCourt(CancellationToken stoppingToken)
        {
            try
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var unitOfWork = scope.ServiceProvider.GetRequiredService<HighCourtRajCauseList.ServiceBus.UnitOfWork.IUnitOfWorkService>();
                    ArrayList al = ["JP", "JU"];
                    foreach (var item in al)
                    {
                        await CaseRegistrationHighCourtScheduler(unitOfWork, Convert.ToString(item), DateTime.Now.AddDays(-1).ToString("yyyyMMdd"), DateTime.Now.ToString("yyyyMMdd"));
                    }
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetCaseList", ex.Message, ex.StackTrace, ex.Source, "CauseListScheduler/HighCourtCauseList.Service/HighCourtCauseListBackgroundJob/ExecuteJobAsync");
            }
        }

        public async Task CauseListMaster(IUnitOfWorkService unitOfWork, string CourtType, string strCauselistDate = "")
        {
            _logsService.Logs("Information", "CauseListService-" + CourtType+"-"+ strCauselistDate, "Cause list service started on: " + System.DateTime.Now.ToString("dd-MMM-yyyy HH:mm"));
            Console.WriteLine("CauseListService-" + CourtType + "-" + strCauselistDate + " started on: " + System.DateTime.Now.ToString("dd-MMM-yyyy HH:mm"));
            //The URL of the WEB API Service
            string url = "https://hcraj.nic.in/litesapi/causelist_api.php";
            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromHours(1); //adjust based on your network
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Framework Test Client");
            try
            {
                var objData = new
                {
                    Estt = CourtType,
                    TokSe = "9EDG054C3Z26584255B0B0D6B874XQ",
                    cdate = strCauselistDate
                };
                string jsonData = Encrypt(JsonConvert.SerializeObject(objData));

                var credentials = new FormUrlEncodedContent(new[] {
                    new KeyValuePair<string, string>("data", jsonData)
                });

                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                //ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                HttpResponseMessage responseMessage = client.PostAsync(url, credentials).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var responseDataDecrypt = Decrypt(responseData);
                    var data = JsonConvert.DeserializeObject<NewCauselistModel>(responseDataDecrypt);
                    int count = 0;
                    foreach (var item in data.result)
                    {
                        var model = new NewCauseListRequestModel
                        {
                            Estt = CourtType,
                            cdate = DateTime.Now,
                            AddedOn = DateTime.Now,
                            sno = item.sno,
                            courtno = item.courtno,
                            causelistdate = item.causelistdate,
                            causelisttype = item.causelisttype,
                            ctype = item.ctype,
                            cno = item.cno,
                            cyear = item.cyear,
                            pet = item.pet,
                            res = item.res,
                            law1 = item.law1,
                            law2 = item.law2,
                            stg = item.stg,
                            judname = item.judname,
                            judname2 = item.judname2,
                            padv = item.padv,
                            radv = item.radv,
                            case_no = item.case_no,
                            pet_org_name = item.pet_org_name,
                            res_org_name = item.res_org_name,
                            div_ben = item.div_ben,
                            croom = Convert.ToInt32(item.croom_numeric),
                            croom_ju = item.croom,
                            cino = item.cino
                        };
                        if (model.croom == 0)
                            model.croom = null;

                        var obj = await unitOfWork.HighCourtRajCauseListService.AddNewHighCourtRajCauseList(model);
                        count++;
                    }
                    _logsService.Logs("Information", "CauseListService-" + CourtType + "-" + strCauselistDate, "Success with data: " + count.ToString());
                    Console.WriteLine("CauseListService-" + CourtType + "-" + strCauselistDate + " completed on: " + System.DateTime.Now.ToString("dd-MMM-yyyy HH:mm") + " with data: " + count.ToString());
                }
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "GetCaseListService", ex.Message, ex.StackTrace, ex.Source, "CauseListScheduler/HighCourtCauseList.Service/HighCourtCauseListBackgroundJob/CauseListMaster");
            }
        }

        public async Task JustDeptScheduler(IUnitOfWorkService unitOfWork, string CourtType)
        {
            _logsService.Logs("Information", "JustDeptScheduler", "JustDeptScheduler-" + CourtType + " started on: " + System.DateTime.Now.ToString("dd-MMM-yyyy HH:mm"));
            Console.WriteLine("JustDeptScheduler-" + CourtType +" started on: " + System.DateTime.Now.ToString("dd-MMM-yyyy HH:mm"));
            //The URL of the WEB API Service
            string url = "https://hcraj.nic.in/litesapi/departments";
            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromHours(1); //adjust based on your network
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Framework Test Client");
            try
            {
                var credentials = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("uid", "lawdept"),
                new KeyValuePair<string, string>("pwd", "817D84412A3E33AB240F307A5B09EA08"),
                new KeyValuePair<string, string>("estt", CourtType)
                });

                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                HttpResponseMessage responseMessage = await client.PostAsync(url, credentials);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                    var data = JsonConvert.DeserializeObject<DeptHC>(responseData);
                    List<DeptResult> list = new List<DeptResult>(data.result);

                    var listToSend = list.Where(item => item.Department.orgtype == 2)
                    .Select(item => new
                    {
                        code = item.Department.orgid,
                        State = "S",
                        Description = item.Department.orgname,
                        CourtType = CourtType == "JU" ? 1 : 2
                    }).ToList();

                    string jsonData = JsonConvert.SerializeObject(listToSend);

                    var obj = await unitOfWork.HighCourtRajCauseListService.JustDeptScheduler(jsonData, CourtType);
                }
                _logsService.Logs("Information", "JustDeptScheduler-" + CourtType, "Successfully completed.");
                Console.WriteLine("JustDeptScheduler-" + CourtType + " completed on: " + System.DateTime.Now.ToString("dd-MMM-yyyy HH:mm"));
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "JustDeptScheduler", ex.Message, ex.StackTrace, ex.Source, "CauseListScheduler/HighCourtCauseList.Service/HighCourtCauseListBackgroundJob/JustDeptScheduler");
            }
        }

        public async Task CaseRegistrationHighCourtScheduler(IUnitOfWorkService unitOfWork, string CourtType, string fromdt, string todt)
        {
            _logsService.Logs("Information", "CaseRegistrationHighCourtScheduler", "CaseRegistrationHighCourtScheduler-" + CourtType + " started on: " + System.DateTime.Now.ToString("dd-MMM-yyyy HH:mm"));
            Console.WriteLine("CaseRegistrationHighCourtScheduler-" + CourtType +" started on: " + System.DateTime.Now.ToString("dd-MMM-yyyy HH:mm"));
            //The URL of the WEB API Service
            string url = "https://hcraj.nic.in/litesapi/case_details/pending";
            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromHours(1); //adjust based on your network
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Framework Test Client");
            try
            {
                if (String.IsNullOrEmpty(fromdt))
                {
                    fromdt = DateTime.Now.AddDays(-7).ToString("yyyyMMdd");
                }
                if (String.IsNullOrEmpty(todt))
                {
                    todt = DateTime.Now.ToString("yyyyMMdd");
                }
                var credentials = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("uid", "lawdept"),
                new KeyValuePair<string, string>("pwd", "817D84412A3E33AB240F307A5B09EA08"),
                new KeyValuePair<string, string>("estt", CourtType),
                new KeyValuePair<string, string>("fdt", fromdt),
                new KeyValuePair<string, string>("tdt", todt),
                new KeyValuePair<string, string>("searchby", "R")
                });

                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                HttpResponseMessage responseMessage = await client.PostAsync(url, credentials);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<CaseDataHC>(responseData);
                    List<CaseDataResult> list = new List<CaseDataResult>(data.result);

                    var listToSend = list.Where(item => item.CaseDetail.RegistrationDate != null)
                    .Select(item => new
                    {
                        CNR = item.CaseDetail.CNR,
                        HAID = item.CaseDetail.DepartmentId,
                        CaseNo = item.CaseDetail.RegNo,
                        CaseStageCode = item.CaseDetail.purpose_next,
                        CourtType = CourtType == "JU" ? 1 : 2,
                        DataCaseNo = item.CaseDetail.case_no,
                        CaseYear = item.CaseDetail.RegYear,
                        cino = item.CaseDetail.cino,
                        DepartmentType = "S",
                        CaseType = (!String.IsNullOrEmpty(item.CaseDetail.CaseRegTypeName) ? item.CaseDetail.CaseRegTypeName : null),
                        DisposalDate = (!String.IsNullOrEmpty(item.CaseDetail.CaseDecisionDate) ? item.CaseDetail.CaseDecisionDate : null),
                        NextHearingDate = (!String.IsNullOrEmpty(item.CaseDetail.NextHearingDate) ? item.CaseDetail.NextHearingDate : null),
                        CaseRegistrationDate = (!String.IsNullOrEmpty(item.CaseDetail.RegistrationDate) ? item.CaseDetail.RegistrationDate : null),
                        Petitioner = (!String.IsNullOrEmpty(item.CaseDetail.PetitionerName) ? item.CaseDetail.PetitionerName : null),
                        PetitionerAdvocate = (!String.IsNullOrEmpty(item.CaseDetail.PetitionerAdvocateName) ? item.CaseDetail.PetitionerAdvocateName : null),
                        Respondent = (!String.IsNullOrEmpty(item.CaseDetail.RespondentName) ? item.CaseDetail.RespondentName : null),
                        RespondentAdvocate = (!String.IsNullOrEmpty(item.CaseDetail.RespondentAdvocateName) ? item.CaseDetail.RespondentAdvocateName : null),
                        Active = true,
                        CreatedDate = DateTime.Now
                    }).ToList();

                    string jsonData = JsonConvert.SerializeObject(listToSend);

                    var obj = await unitOfWork.HighCourtRajCauseListService.CaseRegistrationHighCourtScheduler(jsonData, CourtType);
                }
                _logsService.Logs("Information", "CaseRegistrationHighCourtScheduler-" + CourtType, "Successfully completed.");
                Console.WriteLine("CaseRegistrationHighCourtScheduler-" + CourtType + " completed on: " + System.DateTime.Now.ToString("dd-MMM-yyyy HH:mm"));
            }
            catch (Exception ex)
            {
                _logsService.Logs("Error", "CaseRegistrationHighCourtScheduler", ex.Message, ex.StackTrace, ex.Source, "CauseListScheduler/HighCourtCauseList.Service/HighCourtCauseListBackgroundJob/CaseRegistrationHighCourtScheduler");
            }
        }

        private static readonly byte[] Key = Encoding.UTF8.GetBytes("084s@yb3z0j2l2#X"); // 16 bytes for AES-128
        private static readonly byte[] IV = Encoding.UTF8.GetBytes("084s@yb3z0j2l2#X"); // 16 bytes for IV
        public static string Encrypt(string plainText)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;
                aes.Mode = CipherMode.CBC;

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                using (var ms = new MemoryStream())
                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                    cs.Write(plainBytes, 0, plainBytes.Length);
                    cs.FlushFinalBlock();
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }
        public static string Decrypt(string cipherText)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;
                aes.Mode = CipherMode.CBC;

                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                using (var ms = new MemoryStream(Convert.FromBase64String(cipherText)))
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (var sr = new StreamReader(cs))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }
}
