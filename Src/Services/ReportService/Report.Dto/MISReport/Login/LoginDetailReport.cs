using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Dto.MISReport.Login
{
    public class LoginDetailReport
    {
        public int No { get; set; }
        public Nullable<long> RowNum { get; set; }
        public string AdmDepttName { get; set; }
        public string UnitName { get; set; }
        public string OfficeName { get; set; }
        public string OicName { get; set; }
        public long LoginDetailId { get; set; }
        public Nullable<int> LoginId { get; set; }
        public Nullable<System.DateTime> LoginOn { get; set; }
        public string IpAddress { get; set; }
        public Nullable<System.DateTime> LogoutOn { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string RoleName { get; set; }
    }
   
}
