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
        public string ClientId { get; set; }
        public string Content_Type { get; set; }
        public string BaseUrl { get; set; }
        public string ClientSecret { get; set; }
        public string Token { get; set; }
        public string V1 { get; set; }
        public string V2 { get; set; }

    }
}
