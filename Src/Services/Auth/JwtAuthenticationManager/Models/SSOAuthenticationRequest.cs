using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuthenticationManager.Models
{
    public class SSOAuthenticationRequest
    {
        //public string UserDetails { get; set; }
        public string SSOToken { get; set; }
        public string IPAddress { get; set; }
    }

    public class SSORequest
    {
        public string UserDetails { get; set; }
    }



}
