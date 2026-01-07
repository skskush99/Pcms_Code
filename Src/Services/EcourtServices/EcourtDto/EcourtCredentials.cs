using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcourtDto
{
    public class HcourtCredentials
    {
        [JsonProperty("Username")]
        public string Username { get; set; }
        [JsonProperty("Password")]
        public string Password { get; set; }
        [JsonProperty("grant_type")]
        public string grant_type { get; set; }
        [JsonProperty("scope")]
        public string Scope { get; set; }
        [JsonProperty("BaseUrl")]
        public string BaseUrl { get; set; }
        [JsonProperty("AuthenticationKey")]
        public string AuthenticationKey { get; set; }
        [JsonProperty("Iv")]
        public string Iv { get; set; }
        [JsonProperty("DeptId")]
        public string DeptId { get; set; }
        [JsonProperty("version")]
        public string version { get; set; }
    }

    public class EcourtCredentials
    {
        [JsonProperty("Username")]
        public string Username { get; set; }
        [JsonProperty("Password")]
        public string Password { get; set; }
        [JsonProperty("grant_type")]
        public string grant_type { get; set; }
        [JsonProperty("scope")]
        public string Scope { get; set; } 
        [JsonProperty("AuthKey")]
        public string AuthKey { get; set; }
        [JsonProperty("BaseUrl")]
        public string BaseUrl { get; set; }
        [JsonProperty("AuthenticationKey")]
        public string AuthenticationKey { get; set; }
        [JsonProperty("Iv")]
        public string Iv { get; set; }
        [JsonProperty("DeptId")]
        public string DeptId { get; set; }
        [JsonProperty("version")]
        public string version { get; set; }
    }
}
