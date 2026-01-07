using Core.Models.User;
using Core.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using Core;

namespace Authentication.Service.Controllers
{
    public class SsoLoginController : Controller
    {
        private IConfiguration Configuration;
        private readonly IHttpContextAccessor httpContextAccessor;
        public SsoLoginController(IConfiguration _configuration, IHttpContextAccessor httpContextAccessor)
        {
            Configuration = _configuration;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            var token = Request.Form["userdetails"];
            //var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            var ipAddress = GetClientIpAddress(HttpContext);
            if (!string.IsNullOrEmpty(ipAddress))                                                                           
                ipAddress = ipAddress.Trim().Replace(".", "IA");
            var url = this.Configuration["SSOLoginURL"] + "?token=" + token + "&ia=" + ipAddress;

            return Redirect(url);
        }

        private string GetClientIpAddress(HttpContext context)
        {
            // 1️⃣ Try X-Forwarded-For first (used by proxies/load balancers)
            var forwardedFor = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (!string.IsNullOrEmpty(forwardedFor))
            {
                // The header can contain multiple IPs, take the first one
                return forwardedFor.Split(',').FirstOrDefault()?.Trim();
            }

            // 2️⃣ Fallback to connection info
            return context.Connection.RemoteIpAddress?.ToString();
        }
    }
}
