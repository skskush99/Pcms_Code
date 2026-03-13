using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCTNSDto
{
    public class CCTNSCredentials
    {
        public string ClientId { get; set; } =null!;
        public string Content_Type { get; set; } = null!;
        public string BaseUrl { get; set; } = null!;
        public string ClientSecret { get; set; } = null!;
        

    }

    public class AuthCCTNSCredentials
    {
        public string ClientId { get; set; } = null!;
        public string Content_Type { get; set; } = null!;
        public string urls { get; set; } = null!;
        public string AuthsecretKey { get; set; } = null!;

    }



}
