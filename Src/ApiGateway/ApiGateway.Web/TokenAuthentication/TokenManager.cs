namespace ApiGateway.Web.TokenAuthentication
{
    public class TokenManager : ITokenManager
    {
        private List<Token> _listToken;
        public TokenManager()
        {
            _listToken = new List<Token>();
        }
        public bool Authenticate(string userName, string password)
        {
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                return true;
            }
            else
                return false;
        }
        public Token NewToken()
        {
            Token token = new Token()
            {
                Value = Guid.NewGuid().ToString(),
                ExpiryDate = DateTime.UtcNow.AddHours(1)
            };
            _listToken.Add(token);
            return token;
        }
        public bool VerifyToken(string token)
        {
            if (_listToken.Any(d => d.Value == token && d.ExpiryDate > DateTime.UtcNow))
                return true;
            else
                return false;
        }
    }
}
