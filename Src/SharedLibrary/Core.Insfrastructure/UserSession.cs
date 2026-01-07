using Core.Enums;
using Core.Enums.User;
using Core.Models;
using Core.Utils;
using System.Reflection;
using System.Text;

namespace Core.Insfrastructure
{
    public class Session
    {
        public static UserSessionModel UserSession { get; set; }
        public static Language UserLanguge { get; set; }

        public static void SetUserSession(UserSessionModel userSessionData, Language? userLanguage = null)
        {
            UserSession = userSessionData;
            //UserLanguge = UserLanguge;
            UserLanguge = userLanguage.Value;
            //FillAccessValidator(userSessionData.RoleId, userSessionData.FormUrl, -1, UPMDbContext context);
        }

        public static bool HasAccess(Int32 priviledgeId)
        {
            if (!String.IsNullOrEmpty(UserSession.Previleges))
            {
                string sessionPrevileges = UserSession.Previleges;
                if (sessionPrevileges.Contains(Convert.ToString(priviledgeId)))//any
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        //public static void FillAccessValidator(Int32 roleId, string formUrl, Int32 privilegeId)
        //{
        //    try
        //    {
        //        OracleParameter[] param = new OracleParameter[2];
        //        param[0] = new OracleParameter { ParameterName = "V_ROLE_ID", OracleDbType = OracleDbType.Int32, Value = roleId, Direction = ParameterDirection.Input };
        //        param[1] = new OracleParameter { ParameterName = "V_FORM_LINK", OracleDbType = OracleDbType.Int32, Value = formUrl, Direction = ParameterDirection.Output };
        //        param[5] = new OracleParameter { ParameterName = "DETAIL", OracleDbType = OracleDbType.RefCursor, Value = DBNull.Value, Direction = ParameterDirection.Output };
        //        dynamic IsHeadOffice = SPUtility.ExecuteGetValue(context, "CHECK_HEADOFFICE", param);
        //        if (IsHeadOffice == 1)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public static string SeriaizeUserSessionData()
        {
            //Int32 userTypeId = (Int32)UserSession.UserType;
            //Int32 userLanguageId = (Int32)UserSession.UserLanguage;
            return Newtonsoft.Json.JsonConvert.SerializeObject(
                    new UserSessionModel()
                    {
                        Id = 0,
                        ProfileId = UserSession.ProfileId,
                        LoginId = UserSession.LoginId,
                        LoginLogId = UserSession.LoginLogId,
                        LoginStatusId = UserSession.LoginStatusId,
                        MobileNumber = UserSession.MobileNumber,
                        SSOId = UserSession.SSOId,
                        //OrganizationName = UserSession.OrganizationName,
                        //SchemeName = UserSession.SchemeName,
                        //OrgCode = UserSession.OrgCode,
                        // OrgId = UserSession.OrgId,
                        //PersonalDetails = new PersonalDetails()
                        //{
                        //    DOB = (UserSession.PersonalDetails == null ? null : UserSession.PersonalDetails.DOB),
                        //    EmailAddress = (UserSession.PersonalDetails == null ? "" : UserSession.PersonalDetails.EmailAddress),
                        //    MobileNumber = (UserSession.PersonalDetails == null ? "" : UserSession.PersonalDetails.MobileNumber),
                        //    Name = (UserSession.PersonalDetails == null ? "" : UserSession.PersonalDetails.Name)
                        //},
                        // Priveleges = UserSession.Priveleges,
                        //  RoleId = UserSession.RoleId,
                        RoleName = UserSession.RoleName,
                        UserName = UserSession.UserName,
                        UserReferenceNumber = UserSession.UserReferenceNumber,
                        UserType = UserSession.UserType,
                        // HorizonMappingId = UserSession.HorizonMappingId,
                        // ParentOrgId = UserSession.ParentOrgId,
                        // AssociatedOrgId = UserSession.AssociatedOrgId,
                        // SchemeId = UserSession.SchemeId,
                        // HorizonTypeId = UserSession.HorizonTypeId,
                        // HorizonName = UserSession.HorizonName,
                        // PostId = UserSession.PostId,
                        // OfficeId = UserSession.OfficeId,
                        //OfficeName = UserSession.OfficeName,
                        // OfficeTypeId = UserSession.OfficeTypeId,
                        //  OfficeTypeName = UserSession.OfficeTypeName,
                        Language = UserSession.Language,
                        RoleId = UserSession.RoleId,
                        TypeId = UserSession.TypeId,
                        UserId = UserSession.UserId,
                        Previleges = UserSession.Previleges,
                        FormUrl = UserSession.FormUrl
                        //DesignationId = UserSession.DesignationId,
                        //DistrictId = UserSession.DistrictId,
                        //DepartmentId = UserSession.DepartmentId,
                        //OfficeNo = UserSession.OfficeNo
                        // FinYear = UserSession.FinYear
                    });
        }

        public static string GetUserSessionData()
        {
            //Int32 userTypeId = (Int32)UserSession.UserType;
            //Int32 userLanguageId = (Int32)UserSession.UserLanguage;
            return Newtonsoft.Json.JsonConvert.SerializeObject(
                    new UserSessionModel()
                    {
                        ProfileId = UserSession.ProfileId,
                        LoginId = UserSession.LoginId,
                        LoginLogId = UserSession.LoginLogId,
                        LoginStatusId = UserSession.LoginStatusId,
                        SSOId = UserSession.SSOId,
                        // OrganizationName = UserSession.OrganizationName,
                        // OrgCode = UserSession.OrgCode,
                        // OrgId = UserSession.OrgId,
                        //  RoleId = UserSession.RoleId,
                        UserReferenceNumber = UserSession.UserReferenceNumber,
                        UserType = UserSession.UserType,
                        // HorizonMappingId = UserSession.HorizonMappingId,
                        // ParentOrgId = UserSession.ParentOrgId,
                        // AssociatedOrgId = UserSession.AssociatedOrgId,
                        // SchemeId = UserSession.SchemeId,
                        // HorizonTypeId = UserSession.HorizonTypeId,
                        //  PostId = UserSession.PostId,
                        // OfficeId = UserSession.OfficeId,
                        //  OfficeTypeId = UserSession.OfficeTypeId,
                        Language = UserSession.Language
                    }, Newtonsoft.Json.Formatting.None, new Newtonsoft.Json.JsonSerializerSettings
                    {
                        NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
                    });
        }

        public static string GetErrorLogFiles(List<ErrorFiles> files)
        {
            StringBuilder str = new StringBuilder();
            try
            {
                // string serviceurl = ConfigurationManger.AppSetting["ServiceURL:" + ServiceName];
                // string directoryName = "Logs";
                // string servicesDirectory = Path.Combine(directoryName);
                // var directoryPath = "";
                //List<ErrorFiles> files = new List<ErrorFiles>();

                //if (Directory.Exists(servicesDirectory))
                //{
                //    string[] subdirectories = Directory.GetDirectories(servicesDirectory);

                //    if (subdirectories.Length > 0)
                //    {
                //        foreach (string subdirectory in subdirectories)
                //        {
                //            foreach (var file in Directory.GetFiles(subdirectory))
                //            {
                //                ErrorFiles errfile = new ErrorFiles();
                //                errfile.Files = file;
                //                files.Add(errfile);
                //            }

                //        }
                //    }
                //}

                int i = 0;
                str.Append("<table class=\"table table-striped\" width='50%' class='gridview'><tbody><tr><th style='width:5%'>Sr No </th><th>Error File </th><th>File Size </th><th>Last Modified on</th><th style='width:8%'>Download</th></tr>");
                foreach (var filePath in files)
                {
                    string fileName = Path.GetFileName(filePath.Files);
                    FileInfo fileInfo = new FileInfo(filePath.Files);
                    DateTime lastModified = DateTime.Now.Date;

                    DateTime CreatedOn = DateTime.Now.Date;
                    double fileSize = 0;

                    try
                    {
                        //if (fileInfo.Length > 0)
                        {
                            fileSize = Math.Round(fileInfo.Length / 1024.0, 2);
                            lastModified = fileInfo.LastWriteTime;
                            CreatedOn = fileInfo.CreationTime;
                        }
                    }
                    catch (Exception)
                    {

                    }

                    TimeSpan dateDiff = DateTime.Now - CreatedOn;
                    string absolutePath = Path.GetFullPath(filePath.Files);
                    //string downloadLink = Url.Action("DownloadFile", "ErrorLogs", new { filePath = absolutePath });
                    string downloadLink = CommonUtility.DownloadFile(absolutePath);
                    //if (dateDiff.TotalDays <= 10)
                    {
                        i++;
                        str.Append("<tr>");
                        str.Append("<td style='text-align: center;'>" + i + "</td>");
                        str.Append("<td>" + fileName + "</td>");
                        str.Append("<td>" + fileSize + "Kb</td>");
                        str.Append("<td>" + lastModified + "</td>");
                        str.Append("<td style='text-align: center;'><a href='" + downloadLink + "'  download ='" + fileName + "' >Download</a></td>");
                        str.Append("</tr>");
                    }
                }
                //return files;
                return str.ToString();
            }
            catch (Exception ex)
            {
                LogUtility.WriteEventErrorLog(ex, "", MethodBase.GetCurrentMethod().DeclaringType.ToString() + "." + MethodBase.GetCurrentMethod().Name, "");
                throw;
            }
        }
    }

    public class UserSessionModel
    {
        public Int64 Id { get; set; }
        public Int64 ProfileId { get; set; }
        public Int64 UserId { get; set; }
        public Int32 RoleId { get; set; }
        public Int64 DesignationId { get; set; }
        public Int64 DistrictId { get; set; }
        public Int64 DepartmentId { get; set; }
        public Int64 OfficeNo { get; set; }
        public Int32 TypeId { get; set; }
        public string LoginId { get; set; }
        public string UserName { get; set; }
        public string Previleges { get; set; }
        public UserDetails UserDetails { get; set; }
        //public Int32 OrgId { get; set; }
        // public string OrgCode { get; set; }
        // public string OrganizationName { get; set; }
        public Int64 LoginLogId { get; set; }
        public Int64 UserReferenceNumber { get; set; }
        public UserType UserType { get; set; }
        public Int64 UserTypeId { get; set; }
        public Int32 LoginStatusId { get; set; }
        public string RoleName { get; set; }
        // public Int32 HorizonMappingId { get; set; }
        //public Int32 SchemeId { get; set; }
        //public string SchemeName { get; set; }
        // public Int32 ParentOrgId { get; set; }
        // public Int32 AssociatedOrgId { get; set; }
        //  public Int32? HorizonTypeId { get; set; }
        // public string HorizonName { get; set; }
        // public Int32 MappingTypeId { get; set; }
        // public Int32? PostId { get; set; }
        //  public Int32 OfficeId { get; set; }
        //public string OfficeName { get; set; }
        // public Int32 OfficeTypeId { get; set; }
        // public string OfficeTypeName { get; set; }
        // public PersonalDetails PersonalDetails { get; set; }
        public string MobileNumber { get; set; }
        //public List<Int32> Priveleges { get; set; }
        //public bool HasPrivelge(Int32 ActionId)
        //{
        //  return this.Priveleges.Contains(ActionId);
        //}
        public string SSOUrl { get; set; }
        public string SSOId { get; set; }
        public string Language { get; set; }
        // public Int64? FinYear { get; set; }
        public string FormUrl { get; set; }
        public Int32? IsDisplayProfilePage { get; set; }
        public Int32? IsProfileIncomplete { get; set; }
        public List<RoleList> RoleList { get; set; }

        public List<DepartmentModule> DepartmentModule { get; set; }
        public selectedDepAndRole selectedDepAndRole { get; set; }

        public AgentLogin AgentLogin { get; set; }
    }
    public class RoleList
    {
        public Int64 UserId { get; set; }
        public Int64 ProfileId { get; set; }
        public string InternalId { get; set; }
        public Int64 RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleNameHindi { get; set; }
        public string Active { get; set; }
        public Int64 DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentNameHindi { get; set; }

        public string SectionName { get; set; }
        public string SectionHindi { get; set; }
        public Int64 SectionId { get; set; }
        public Int64 LoginLogId { get; set; }
        public string Language { get; set; }
    }
    public class DepartmentModule
    {

        public string DepartmentName { get; set; }
        public string DepartmentNameHindi { get; set; }
        public Int64 DepartmentID { get; set; }

        public List<RoleList> RoleList { get; set; }


    }
    public class selectedDepAndRole
    {
        public Int64 LastRoleId { get; set; }
        public Int64 LastUserId { get; set; }
        public Int64 LastUserTypeId { get; set; }
    }

    public class AgentLogin
    {
        public Int64 AgentId { get; set; }
        public string AgentCode { get; set; }
        public List<CampaignNameChild> Campaign { get; set; }
        public string AgentActType { get; set; }
        public int EnableOnAll { get; set; }
        public string IpAddress { get; set; }
    }
    public class CampaignNameChild
    {
        public Int64 CampaignId { get; set; }
        public string CampaignName { get; set; }
        public string CampaignNameRegional { get; set; }
        public string ModuleId { get; set; }
        public string ModuleName { get; set; }
        public string ModuleNameRegional { get; set; }
    }
    public class ErrorFiles
    {
        public string Files { get; set; }
    }
}
