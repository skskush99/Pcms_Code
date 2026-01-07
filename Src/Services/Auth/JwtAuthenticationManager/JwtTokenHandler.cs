using JwtAuthenticationManager.Models;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Authentication.Dto.Shared;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthenticationManager
{
    public class JwtTokenHandler
    {
        public const string JWT_SECURITY_KEY = "e2f355431cc6264563adfcc5f05aeb11051f9bd8672ee4ee5799fc009ca26dc05fbf7dfaaafb1181011fe40924e71f85ac9bce92807bb704635016ae3b535b9488443208b48b8d5de61bba8451fdc9b7bb430de03859b0c5d5e93a26b271eccc052e29ab33edb7b0272f0c520de00dbfb85223f9b0d6cf7765f690048f2b4d03ebccc09902d126194c9d4eefe973d7c4618cfc797d1414930e22e00cb1812732697eaf3068c262074e414fbd3b5cc76afdaf9d781c3504d2de2e9963f3e905776c1826cac8fdc1e64885ecdf005c694fffd97a167d3d277bfe24b0afffa3a117c2ee7ac652a31fc2875381049d06150cc820b84ebae53f7611a5cb7178f945a3";
        public const int JWT_TOKEN_VALIDITY_MIN = 1440;
        private readonly List<UserAccount> _userAccountList;

        public JwtTokenHandler()
        {
            _userAccountList = new List<UserAccount>
            {
                new UserAccount{ UserName="admin", Password="admin@123", Role="Administrator", Scopes = new string[] { "admin.Read" }},
                new UserAccount{ UserName="user", Password="user@123", Role="User", Scopes = new string[] { "users.Read" }},
            };
        }

        public AuthenticationResponse? GenerateAuthToken(AuthenticationRequest authenticationRequest)
        {
            if (string.IsNullOrWhiteSpace(authenticationRequest.UserName) || string.IsNullOrWhiteSpace(authenticationRequest.Password))
                return null;

            /*Validate here from database*/
            var userAccount = _userAccountList.Where(x => x.UserName == authenticationRequest.UserName && x.Password == authenticationRequest.Password).FirstOrDefault();
            if (userAccount == null) return null;

            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MIN);
            var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);

            //var claimsIdentity = new ClaimsIdentity(new List<Claim>
            //{
            //    new Claim(JwtRegisteredClaimNames.Name, authenticationRequest.UserName),
            //    new Claim(ClaimTypes.Role, userAccount.Role),
            //    new Claim("scope", string.Join(" ", userAccount.Scopes))
            //});

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature);

            //var securityTokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = claimsIdentity,
            //    Expires = tokenExpiryTimeStamp,
            //    SigningCredentials = signingCredentials
            //};

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            //var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name, userAccount.UserName),
                new Claim("Role", userAccount.Role),
                new Claim("scope", string.Join(" ", userAccount.Scopes))
            };

            var tokenOptions = new JwtSecurityToken(
                issuer: "https://localhost:5002",
                claims: claims,
                expires: tokenExpiryTimeStamp,
                signingCredentials: signingCredentials
            );


            var token = jwtSecurityTokenHandler.WriteToken(tokenOptions);

            return new AuthenticationResponse
            {
                UserName = authenticationRequest.UserName,
                ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds,
                AuthToken = token,
            };
        }

        public List<AuthenticationResponse>? GenerateAuthToken(List<LoginDetailsModel> authenticationRequest)
        {
            if (authenticationRequest.Count() == 0)
                return null;


            var obj = new List<AuthenticationResponse>();
            /*Validate here from database*/
            foreach (var loginDetails in authenticationRequest)
            {
                var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MIN);
                var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);

                var signingCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature);

                var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Name, loginDetails.UserName),
                    new Claim("UserId", Convert.ToString(loginDetails.UserId)),
                    new Claim("UserRole", loginDetails.RoleName),
                    new Claim("RoleId", Convert.ToString(loginDetails.RoleId)),
                    new Claim("Token", loginDetails.Token),
                };

                var tokenOptions = new JwtSecurityToken(
                    issuer: "https://localhost:5002",
                    claims: claims,
                    expires: tokenExpiryTimeStamp,
                    signingCredentials: signingCredentials
                );


                var token = jwtSecurityTokenHandler.WriteToken(tokenOptions);

                obj.Add(new AuthenticationResponse
                {
                    UserName = loginDetails.UserName,
                    RoleId = loginDetails.RoleId,
                    //LoginLogsToken = loginDetails.Token,
                    ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds,
                    AuthToken = token,
                    LoginUserData = loginDetails
                });
            }

            return obj;
        }

        public ResponseWithoutPaginationModel GenerateAuthTokenWithNewFeature(ResponseWithoutPaginationModel authenticationRequest)
        {
            if (authenticationRequest?.Data == null && authenticationRequest?.Data?.Count() == 0)
                return null;

            var objdata = authenticationRequest?.Data?.Cast<LoginDetailsModel>().ToList();
            var obj = new List<AuthenticationResponse>();
            foreach (var loginDetails in objdata)
            {
                var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MIN);
                var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);

                var signingCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature);

                var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Name, loginDetails.UserName),
                    new Claim("UserId", Convert.ToString(loginDetails.UserId)),
                    new Claim("UserRole", loginDetails.RoleName),
                    new Claim("RoleId", Convert.ToString(loginDetails.RoleId)),
                    new Claim("Token", loginDetails.Token),
                };

                var tokenOptions = new JwtSecurityToken(
                    issuer: "https://localhost:5002",
                    claims: claims,
                    expires: tokenExpiryTimeStamp,
                    signingCredentials: signingCredentials
                );


                var token = jwtSecurityTokenHandler.WriteToken(tokenOptions);

                obj.Add(new AuthenticationResponse
                {
                    UserName = loginDetails.UserName,
                    RoleId = loginDetails.RoleId,
                    //LoginLogsToken = loginDetails.Token,
                    ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds,
                    AuthToken = token,
                    LoginUserData = loginDetails
                });
            }
            authenticationRequest.Data = obj;
            return authenticationRequest;
        }
    }
}
