using Core.Enums.User;
using Core.Insfrastructure.Controller;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ApiGateway.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : BaseApiController
    {
        private IConfiguration Configuration;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IWebHostEnvironment environment;
        public DocumentController(IConfiguration _configuration, IWebHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor) : base(_configuration, httpContextAccessor)
        {
            Configuration = _configuration;
            this.httpContextAccessor = httpContextAccessor;
            environment = hostEnvironment;
        }
        [HttpPost("UploadDocument")]
        public async Task<IActionResult> PostUploadDocument(IFormFile file, long userNo, Int64 EntityId, Int32 EntityTypeId, Int32 DocumentTypeId, string folder)
        {
            ResponseModel returnStatus = new ResponseModel(Status.Alert);
            string saveToPath = "";
            try
            {

                if (file == null)
                {
                    returnStatus.Message = "File is Not Received...";
                    return Ok(returnStatus);
                }
                string baseURL = this.Configuration["ServiceURL:DMS"];
                using (var client = new HttpClient())
                {
                    string dirPath = Path.Combine(environment.ContentRootPath, "File");
                    if (!Directory.Exists(dirPath))
                    {
                        Directory.CreateDirectory(dirPath);
                    }
                    var fileName = Path.GetFileName(file.FileName);
                    saveToPath = Path.Combine(dirPath, fileName);

                    using (var content = new MultipartFormDataContent())
                    {
                        client.BaseAddress = new Uri(baseURL);

                        using (FileStream stream = new FileStream(saveToPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                        var fileStream = System.IO.File.Open(saveToPath, FileMode.Open);
                        content.Add(new StreamContent(fileStream), "file", fileName);
                        var requestUri = client.BaseAddress + "Upload/UploadDocument?userNo=" + userNo + "&EntityTypeId=" + EntityTypeId + "&DocumentTypeId=" + DocumentTypeId + "&folder=" + folder + "&EntityId=" + EntityId;
                        var request = new HttpRequestMessage(HttpMethod.Post, requestUri) { Content = content };
                        var result = await client.SendAsync(request);
                        var readTask = result.Content.ReadAsStringAsync();
                        readTask.Wait();
                        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                        returnStatus = JsonSerializer.Deserialize<ResponseModel>(readTask.Result, options);

                    }
                    if (System.IO.File.Exists(saveToPath))
                    {
                        System.IO.File.Delete(saveToPath);

                    }
                    return Ok(returnStatus);
                }
            }

            catch (Exception ex)
            {
                if (System.IO.File.Exists(saveToPath))
                {
                    System.IO.File.Delete(saveToPath);

                }
                returnStatus.Status = Status.Error;
                returnStatus.Message = "Sorry ! An error occured while processing your request. Please try after some time." + ex.Message;
                return Ok();
            }
        }

        [HttpPost("DownloadDocument")]
        public async Task<IActionResult> PostDownloadDocument(Int64 DocumentId)
        {
            ResponseModel returnStatus = new ResponseModel(Status.Alert);
            try
            {
                string baseURL = this.Configuration["ServiceURL:DMS"];
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseURL);
                    var requestUri = client.BaseAddress + "Upload/DownloadDocument?DocumentId=" + DocumentId;
                    var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
                    var result = await client.SendAsync(request);
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    returnStatus = JsonSerializer.Deserialize<ResponseModel>(readTask.Result, options);
                }
                return Ok(returnStatus);
            }
            catch (Exception ex)
            {
                returnStatus.Status = Status.Error;
                returnStatus.Message = "Sorry ! An error occured while processing your request. " + ex.Message.ToString();
                return Ok(returnStatus);
            }
        }

        [HttpPost("DownloadTemplate")]
        public IActionResult PostDownloadTemplate(string FileName)
        {
            string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Templates", FileName);
            var stream = new FileStream(_filePath, FileMode.Open, FileAccess.Read);
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", FileName);
        }

    }
}
