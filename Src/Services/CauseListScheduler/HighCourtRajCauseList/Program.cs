using HighCourtRajCauseList.Dto.CauseListModel;
using HighCourtRajCauseList.ServiceBus.HighCourtRajCauseList;
using HighCourtRajCauseList.ServiceBus.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Collections;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;

namespace HighCourtRajCauseList
{
    public class Program
    {
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                //Register Services for Dependency Injection
                services.AddTransient<IUnitOfWorkService, UnitOfWorkService>();
                services.AddTransient<IHighCourtRajCauseListService, HighCourtRajCauseListService>();
            })
            .Build();
            var configService = host.Services.GetRequiredService<IConfiguration>();
            var data = configService.GetConnectionString("DefaultConnection");
            // 3. Resolve Dependency
            ArrayList al = ["JP", "JU"];

            var unitOfWork = host.Services.GetRequiredService<IUnitOfWorkService>();

            foreach (var item in al)
            {
                for (int i = 0; i <= 7; i++)
                {
                    string strCauselistDate = DateTime.Now.AddDays(i).ToString("ddMMyyyy");
                    //Old API
                    //CauseListJP(unitOfWork, Convert.ToString(item));

                    //New CauseList Master API
                    CauseListMaster(unitOfWork, Convert.ToString(item), strCauselistDate);
                }
            }
        }

        public static void CauseListJP(IUnitOfWorkService unitOfWork, string CourtType)
        {
            //The URL of the WEB API Service
            string url = "https://hcraj.nic.in/litesapi/causelists";
            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromHours(1); //adjust based on your network
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Framework Test Client");

            try
            {
                string todt = DateTime.Now.ToString("ddMMyyyy");

                var credentials = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("uid", "lawdept"),
                new KeyValuePair<string, string>("pwd", "817D84412A3E33AB240F307A5B09EA08"),
                new KeyValuePair<string, string>("estt", CourtType),
                //new KeyValuePair<string, string>("estt", "JP"),
                new KeyValuePair<string, string>("dt", todt)
                });

                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                HttpResponseMessage responseMessage = client.PostAsync(url, credentials).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<CauselistModel>(responseData);

                    List<CauselistResult> list = new List<CauselistResult>(data.result);
                    for (int i = 0; i < list.Count; i = i + 100)
                    {
                        var items = list.Skip(i).Take(100);
                        foreach (var item in items)
                        {
                            var model = new CauseListRequestModel
                            {
                                CourtJuJp = CourtType,
                                CauseListDate = item.Causelist.CauseListDate,
                                CauseListType = item.Causelist.CauseListType,
                                BenchSBDB = Convert.ToInt32(item.Causelist.BenchSBDB),
                                CourtNoCourtName = item.Causelist.CourtNoCourtName,
                                CaseRegTypeName = item.Causelist.CaseRegTypeName,
                                CaseRegNo = Convert.ToInt32(item.Causelist.CaseRegNo),
                                CaseRegyear = item.Causelist.CaseRegyear,
                                CaseAbbreviation = item.Causelist.CaseAbbreviation,
                                JudgeName = item.Causelist.JudgeName,
                                JudgeName2 = item.Causelist.JudgeName2,
                                PetitionerLawyerName = item.Causelist.PetitionerLawyerName,
                                RespondentLawyerName = item.Causelist.RespondentLawyerName,
                                PetitionerName = item.Causelist.PetitionerName,
                                RespondentName = item.Causelist.RespondentName,
                                ForOrders = item.Causelist.ForOrders,
                                MainConnected = item.Causelist.MainConnected,
                                CaseSerialNo = item.Causelist.CaseSerialNo,
                                TimeCauseList = item.Causelist.TimeCauseList,
                                TimeCauseList2 = item.Causelist.TimeCauseList2,
                                Roster = item.Causelist.Roster,
                                Note = item.Causelist.Note,
                                ApplicationDetail = item.Causelist.ApplicationDetail,
                                UpdateDate = Convert.ToDateTime(item.Causelist.UpdateDate),
                                Uniquecasenumberformaincase = item.Causelist.Uniquecasenumberformaincase,
                                Uniquecasenumberforconnectedcase = item.Causelist.Uniquecasenumberforconnectedcase,
                                isfinalized = item.Causelist.isfinalized,
                                Active = true,
                                CreatedDate = DateTime.Now
                            };
                            var obj = unitOfWork.HighCourtRajCauseListService.AddHighCourtRajCauseList(model);
                            Console.WriteLine(Convert.ToInt32(item.Causelist.CaseRegNo).ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("HCJaipur Service", "error", ex.ToString());
                //TempData["Message"] = Application.Message_Error("Error", "Some error occurred");
            }
        }

        public static void CauseListMaster(IUnitOfWorkService unitOfWork, string CourtType, string strCauselistDate = "")
        {
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

                        var obj = unitOfWork.HighCourtRajCauseListService.AddNewHighCourtRajCauseList(model);
                        count++;
                    }
                    Console.WriteLine("CauseListMasterService-" + CourtType, "information", "Success with data: " + count.ToString());
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public static void CauseListJU(IUnitOfWorkService unitOfWork)
        {
            //The URL of the WEB API Service
            string url = "https://hcraj.nic.in/litesapi/causelists";
            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromHours(1); //adjust based on your network
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Framework Test Client");
            try
            {
                string todt = DateTime.Now.ToString("ddMMyyyy");

                var credentials = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("uid", "lawdept"),
                new KeyValuePair<string, string>("pwd", "817D84412A3E33AB240F307A5B09EA08"),
                new KeyValuePair<string, string>("estt", "JU"),
                new KeyValuePair<string, string>("dt", todt)
                });

                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                HttpResponseMessage responseMessage = client.PostAsync(url, credentials).Result;

                //  HttpResponseMessage response = await client.PostAsync("api/Department", credentials); 
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData = responseMessage.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<CauselistModel>(responseData);

                    List<CauselistResult> list = new List<CauselistResult>(data.result);
                    for (int i = 0; i < list.Count; i = i + 100)
                    {
                        var items = list.Skip(i).Take(100);
                        foreach (var item in items)
                        {
                            var model = new CauseListRequestModel
                            {
                                CourtJuJp = "JU",
                                CauseListDate = item.Causelist.CauseListDate,
                                CauseListType = item.Causelist.CauseListType,
                                BenchSBDB = Convert.ToInt32(item.Causelist.BenchSBDB),
                                CourtNoCourtName = item.Causelist.CourtNoCourtName,
                                CaseRegTypeName = item.Causelist.CaseRegTypeName,
                                CaseRegNo = Convert.ToInt32(item.Causelist.CaseRegNo),
                                CaseRegyear = item.Causelist.CaseRegyear,
                                CaseAbbreviation = item.Causelist.CaseAbbreviation,
                                JudgeName = item.Causelist.JudgeName,
                                JudgeName2 = item.Causelist.JudgeName2,
                                PetitionerLawyerName = item.Causelist.PetitionerLawyerName,
                                RespondentLawyerName = item.Causelist.RespondentLawyerName,
                                PetitionerName = item.Causelist.PetitionerName,
                                RespondentName = item.Causelist.RespondentName,
                                ForOrders = item.Causelist.ForOrders,
                                MainConnected = item.Causelist.MainConnected,
                                CaseSerialNo = item.Causelist.CaseSerialNo,
                                TimeCauseList = item.Causelist.TimeCauseList,
                                TimeCauseList2 = item.Causelist.TimeCauseList2,
                                Roster = item.Causelist.Roster,
                                Note = item.Causelist.Note,
                                ApplicationDetail = item.Causelist.ApplicationDetail,
                                UpdateDate = Convert.ToDateTime(item.Causelist.UpdateDate),
                                Uniquecasenumberformaincase = item.Causelist.Uniquecasenumberformaincase,
                                Uniquecasenumberforconnectedcase = item.Causelist.Uniquecasenumberforconnectedcase,
                                isfinalized = item.Causelist.isfinalized,
                                Active = true,
                                CreatedDate = DateTime.Now
                            };
                            var obj = unitOfWork.HighCourtRajCauseListService.AddHighCourtRajCauseList(model);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("HCJaipur Service", "error", ex.ToString());
                //TempData["Message"] = Application.Message_Error("Error", "Some error occurred");
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
