using Master.Service.Middleware;

namespace Master.Service.Extension
{
    // Extension method for easy registration in Startup.cs (Program.cs in .NET 6+)
    public static class UserContextMiddlewareExtensions
    {
        public static IApplicationBuilder UseUserContext(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UserContextMiddleware>();
        }
    }
}
