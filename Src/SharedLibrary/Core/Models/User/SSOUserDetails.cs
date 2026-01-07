using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.User
{
#nullable disable
    public class SSOUserDetails
    {
        public string SSOID { get; set; }
        public List<string> roles { get; set; }
        public string aadhaarId { get; set; }
        public string bhamashahId { get; set; }
        public string displayName { get; set; }
        public string bhamashahMemberId { get; set; }
        public string dateOfBirth { get; set; }
        public string gender { get; set; }
        public string mobile { get; set; }
        public string telephoneNumber { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string mailPersonal { get; set; }
        public string postalAddress { get; set; }
        public string postalCode { get; set; }
        public string designation { get; set; }
        public string department { get; set; }
        //public string departmentId { get; set; }
        public string mailOfficial { get; set; }
        public string employeeNumber { get; set; }
        public string firstName { get; set; }
        public string jpegPhoto { get; set; }
        public string lastName { get; set; }
        public string sAMAccountName { get; set; }
        public string oldSSOIDs { get; set; }
        public string janaadhaarId { get; set; }
        public string janaadhaarMemberId { get; set; }
        public string userType { get; set; }
        public string mfa { get; set; }


    }

    public class SSOTokenDetails
    {
        public string sAMAccountName { get; set; }
        public string OldSSOIDs { get; set; }
        public string UserType { get; set; }
        public List<string> Roles { get; set; }
        public string SsoToken { get; set; }

    }
    public class SSoDetails
    {
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string SSOID { get; set; }
    }

    public class SSOIncreaseSessionTime
    {
        public bool valid { get; set; }
    }

    public class EmailPassword
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class CommonSsoLoginModel
    {
        public SSOUserDetails ssoUserDetails { get; set; }
        public SSOTokenDetails ssoTokenDetails { get; set; }
    }

    public class LoginSSoModel
    {
        public Int32 Id { get; set; }
        public string LoginSource { get; set; }
        public string LoginType { get; set; }
        public string LoginId { get; set; }
        //  public string Password { get; set; }
        public string IPAddress { get; set; }
        //  public string MobileNo { get; set; }
        public string SsoId { get; set; }
        //  public string TransactionNumber { get; set; }
        //  public string JanAadharId { get; set; }
        //  public string OTP { get; set; }
    }
    public class SSOLoginWithMobile
    {
        public string AuthId { get; set; }
        public string AuthKey { get; set; }
        public string SsoToken { get; set; }
        public string ComeFrom { get; set; }
        public string RedirectKey { get; set; }
        public string ReactURL { get; set; }
        public string Language { get; set; }
        public int UserType { get; set; }
        public Int64 LoginLogId { get; set; }
    }
}
