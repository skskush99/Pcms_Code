using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace PcmsApi.Gateway.Controllers
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
        
        public async Task<ActionResult> Index()
        {
            var token = Request.Form["userdetails"];
            //var token = "NnJBbGc3eTQwS3BreUZXa2tLeERNSTh2eVp4TWUvSW45RzFsQTRtaXU2aG93ZzNERDZwditWRXV4R1MvRFhIdmNqSndRSE14SFh4VXVNY21hSXRhT3JDdFF6dFh3d0o2OWhhQ2xtcDJLS3FrNzBkR0hyNUZUUi92WlprUFk1SnBra1FucWdpVnErV0paL0tzWUZyT3NLWkRKM3p6elVhVnRYQWRET1ROQjFMeG15azZGVmZ4MkVLczJndmJIa09h";
            //ViewBag.ReturnUrl = "http://10.70.236.252/LiteAngular/ssologin?token=" + token;
            var url = "http://10.70.234.9/PcmsAngular/login-sso?token=" + token;

            return Redirect(url);
        }
    }
}
