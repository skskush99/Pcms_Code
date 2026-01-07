namespace Sms.Dto.SmsModel
{
    public class SmsListModel
    {
        public string Message { get; set; }
        public string MobileNo { get; set; }
        public int OICId { get; set; }
        public string RecieverName { get; set; }
        public string ShortDescription { get; set; }
        public string EmailId { get; set; }
        public string RoleId { get; set; }
        public string TemplateID { get; set; }
    }

    public class SmsRequestModel
    {
        public DateTime Date { get; set; }
    }

    public class ExternalsmsApiInfo
    {
        public string UniqueID { get; set; }
        public string serviceName { get; set; }
        public string language { get; set; }
        public string message { get; set; }
        public string TemplateID { get; set; }

        public List<string> mobileNo { get; set; }
    }
}
