using Authentication.Dto.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuthenticationManager.Models
{
    public class AuthenticationResponse
    {
        public string UserName { get; set; }
        public long RoleId { get; set; }
        public string AuthToken { get; set; }
        public int ExpiresIn { get; set; }
       // public string LoginLogsToken { get; set; }
        public LoginDetailsModel? LoginUserData { get; set; }
    }
}
