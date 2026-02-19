using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Dto.SummaryReports.User
{
    public class UserRegistrationReport
    {
        public Nullable<long> RowNum { get; set; }
        public string AdmDepttName { get; set; }
        public string UnitName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string OfficeName { get; set; }
        public string RoleName { get; set; }
    }
}
