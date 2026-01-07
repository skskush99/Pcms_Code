using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class TokenAuthModel
    {
        public string? Token { get; set; }
        public bool Status { get; set; }
        public string? Message { get; set; }
        public long UserId { get; set; }
        public long RoleId { get; set; }
        public string? LoginOn { get; set; }
        public string? IPAddress { get; set; }
    }

    public class LoginUserDataModel
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public int DepartmentId { get; set; }
        public int UnitId { get; set; }
        public int OfficeId { get; set; }
        public int OICId { get; set; }
        public int DistrictId { get; set; }
        public int LawyerId { get; set; }
        public string? SSOID { get; set; }
        public string? LoginOn { get; set; }
        public string? IPAddress { get; set; }
    }
}
