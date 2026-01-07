using Core.Enums.User;
using Core.Insfrastructure;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ApiGateway.Web.Controllers
{
    public class ErrorLogsController : Controller
    {
        // private readonly Context dbContext;
        private IConfiguration Configuration;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IWebHostEnvironment environment;
        public ErrorLogsController(IConfiguration _configuration, IWebHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            Configuration = _configuration;
            this.httpContextAccessor = httpContextAccessor;
            environment = hostEnvironment;
        }
        public IActionResult Index()
        {
            ResponseModel returnStatus = new ResponseModel(Status.Alert);
            string baseURL = this.Configuration["ServiceURL:CFG"];
            string Application = this.Configuration["Application"];
            List<ServiceModel> Services = new List<ServiceModel>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var responseTask = client.GetAsync("Common/GetAllServiceNames?appType=" + Application + "");
                responseTask.Wait();
                var result = responseTask.Result;
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                returnStatus = JsonSerializer.Deserialize<ResponseModel>(readTask.Result, options);

                Services = JsonSerializer.Deserialize<List<ServiceModel>>(returnStatus.CustomObject);
                SelectList DropdownList = new SelectList(Services, "value", "Name");

                ViewBag.items = DropdownList;



            }
            return View();
        }

        public class ServiceModel
        {
            public string Name { get; set; }
            public string value { get; set; }
        }

        public class ErrorLogFile
        {
            public string ServiceName { get; set; }
            public string FolderPath { get; set; }
        }
        public async Task<IActionResult> GetErrorLogFiles(string ServiceName, string folderPath)
        {
            // ServiceName = "UPM";
            StringBuilder str = new StringBuilder();
            ResponseModel returnStatus = new ResponseModel(Status.Alert);
            List<ErrorFiles> filePaths = new List<ErrorFiles>();
            string errorfiles = "";
            StringBuilder files = new StringBuilder();
            try
            {
                string baseURL = this.Configuration["ServiceURL:" + ServiceName];
                // string baseURL = this.Configuration["ServiceURL:UPM"];
                string apiAddress = folderPath + "GetErrorLogFiles";
                //if (ServiceName == "UPM")
                //{
                //    apiAddress = "GetErrorLogFiles";
                //}
                //else if (ServiceName == "CFG")
                //{
                //    apiAddress = "GetErrorLogFiles";
                //}

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseURL);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var responseTask = client.GetAsync(apiAddress + "?ServiceName=" + ServiceName + "&FolderPath=" + folderPath);
                    responseTask.Wait();
                    var result = responseTask.Result;
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    returnStatus = JsonSerializer.Deserialize<ResponseModel>(readTask.Result, options);
                    errorfiles = JsonSerializer.Deserialize<string>(returnStatus.CustomObject, options);
                }
                files.Append(errorfiles);
                files.Append("</tbody></table>");
                ViewBag.HtmlContent = files.ToString();
                return PartialView("ErrorLogs");
            }
            catch (Exception ex)
            {
                ViewBag.HtmlContent = ex.Message;
                return PartialView("ErrorLogs");
            }
        }


        [HttpGet("DownloadFile")]
        public IActionResult DownloadFile(string filePath)
        {
            try
            {
                // Check if the file exists
                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound("File not found.");
                }

                // Read the file content
                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

                // Determine the file's content type based on its extension
                string contentType = "application/octet-stream"; // Default content type
                string fileExtension = Path.GetExtension(filePath).ToLower();

                if (!string.IsNullOrEmpty(fileExtension))
                {
                    switch (fileExtension)
                    {
                        case ".txt":
                            contentType = "text/plain";
                            break;
                    }
                }

                // Return the file as a downloadable attachment
                return File(fileBytes, contentType, Path.GetFileName(filePath));
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

    }
}
