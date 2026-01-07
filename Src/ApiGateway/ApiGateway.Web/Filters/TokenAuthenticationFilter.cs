using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using ApiGateway.Web.TokenAuthentication;

namespace ApiGateway.Web.Filters
{
    public class TokenAuthenticationFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var tokenManager = context.HttpContext.RequestServices.GetService(typeof(ITokenManager)) as ITokenManager;
            var result = true;
            if (!context.HttpContext.Request.Headers.ContainsKey("Authorization"))
                result = false;
            string token = String.Empty;
            if (result)
                token = context.HttpContext.Request.Headers.First(d => d.Key == "Authorization").Value;
            if (!tokenManager.VerifyToken(token))
                result = false;
            if (!result)
            {
                context.ModelState.AddModelError("Unauthorised", "You are not authorised");
                context.Result = new UnauthorizedObjectResult(context.ModelState);
            }
        }
    }
}
