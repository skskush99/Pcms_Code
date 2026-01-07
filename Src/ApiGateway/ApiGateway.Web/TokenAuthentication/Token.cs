namespace ApiGateway.Web.TokenAuthentication
{
    public class Token
    {
        public String Value { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
