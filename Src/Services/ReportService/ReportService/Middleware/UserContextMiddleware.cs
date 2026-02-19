using Core;
using System.Security.Claims;

namespace ReportService.Middleware
{

    // Define your static session model.  This should be a class that
    // represents the data you want to store in the "session" (though it's
    // not a true session in the traditional sense since it's static).  Make
    // sure it's serializable if you plan to use distributed caching later.
    public static class UserSession
    {
        public static UserData Current { get; set; } = new UserData();
    }

    public class UserData
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
    public class UserContextMiddleware
    {
        private readonly RequestDelegate _next;

        public UserContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            // Check if the user is authenticated
            if (context.User.Identity?.IsAuthenticated == true)
            {
                var token = context.User.FindFirstValue("Token");
                if (!string.IsNullOrEmpty(token))
                {
                    var SessionData = CommonMethods.GetLoginUserDataModelFromToken(token);
                    var userData = new UserData
                    {
                        UserId = SessionData.UserId,
                        RoleId = SessionData.RoleId,
                        DepartmentId = SessionData.DepartmentId,
                        UnitId = SessionData.UnitId,
                        OfficeId = SessionData.OfficeId,
                        OICId = SessionData.OICId,
                        DistrictId = SessionData.DistrictId,
                        LawyerId = SessionData.LawyerId,
                        SSOID = SessionData.SSOID,
                        LoginOn = SessionData.LoginOn,
                        IPAddress = SessionData.IPAddress,
                    };

                    UserSession.Current = userData;
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return;
                }
            }
            else
            {
                // Handle unauthenticated requests.  You might want to clear
                // any existing user data or set default values.  Crucially,
                // you *must* still set UserSession.Current to *something* to
                // avoid NullReferenceExceptions later in your code.

                //UserSession.Current = new UserData(); // Or set to a default anonymous user object
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            await _next(context); // Important: Call the next middleware in the pipeline
        }
    }
}
