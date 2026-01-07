using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuthenticationManager.Models
{
    public class AuthenticationRequest
    {
        public string? UserDetails { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? IPAddress { get; set; }
        public string? SSOToken { get; set; }
        public bool IsSSOLogin { get; set; }
    }

    public class AuthenticationRequestForMobleApp
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public string? IPAddress { get; set; }
    }

    public class AuthenticationResponseForMobleApp
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public List<AuthenticationResponse>? AuthenticationResponse { get; set; }
    }
}
