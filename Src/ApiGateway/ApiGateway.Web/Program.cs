using ApiGateway.Web.TokenAuthentication;
using AspNetCoreRateLimit;
using Core.Insfrastructure.Controller;
using System.Reflection;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
string basePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
builder.Configuration.SetBasePath(basePath);
builder.Configuration
    .AddJsonFile($"Config/sharedAppsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"Config/sharedAppsettings.{env}.json", optional: true, reloadOnChange: true);
// Add services to the container.

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<CustomHeaderSwaggerAttribute>();
});
builder.Services.AddMvc();
builder.Services.AddSingleton<ITokenManager, TokenManager>();
builder.Services.AddHttpClient();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });
});
// AspNetCoreRateLimit
//builder.Services.Configure<IpRateLimitOptions>(options =>
//{
//    options.EnableEndpointRateLimiting = true;
//    options.StackBlockedRequests = true;
//    options.RealIpHeader = "X-Forwarded-For";
//    options.ClientIdHeader = "X-ClientId";
//    options.HttpStatusCode = 429;
//    options.QuotaExceededResponse = new QuotaExceededResponse
//    {
//        ContentType = "application/json",
//        StatusCode = 429,
//        Content = JsonConvert.SerializeObject(new { message = "Too many requests, please try again later." })
//    };
//});
//builder.Services.Configure<IpRateLimitPolicies>(options =>
//{
//    options.IpRules = new List<IpRateLimitPolicy> {
//        new IpRateLimitPolicy {
//             Ip = "0.0.0.0/0" ,
//             Rules = new List<RateLimitRule>
//             {
//                 new RateLimitRule
//                 {
//                    Endpoint = "*",
//                    Limit = 300,
//                    Period = "1m"
//                 }
//             }
//        }
//    };
//});
//**********************SSL Certificate ****************************************
//var CertificatePath = builder.Configuration.GetValue<string>("CertificatePath");
//var Certipassword = builder.Configuration.GetValue<string>("CertificatePass");
//builder.WebHost.UseKestrel(options =>
//{
//    options.ConfigureHttpsDefaults(httpsOptions =>
//    {
//        // Load the SSL certificate
//        var certificate = new X509Certificate2(CertificatePath, Certipassword);
//        httpsOptions.ServerCertificate = certificate;
//        httpsOptions.SslProtocols = System.Security.Authentication.SslProtocols.Tls12 | System.Security.Authentication.SslProtocols.Tls13;
//    });
//});
//******************************** End *****************************************

builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
builder.Services.AddInMemoryRateLimiting();
var app = builder.Build();
app.UseStaticFiles(); // This serves static files from wwwroot

//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Image")),
//    RequestPath = "/Image"
//});
//app.Use(async (context, next) =>
//{
//    if (context.Request.Method == "OPTIONS")
//    {
//        context.Response.StatusCode = 405;
//        await context.Response.CompleteAsync();
//    }
//    else if (context.Request.Method == "OPTIONS" && context.Request.Path.Value.EndsWith(".js"))
//    {
//        context.Response.StatusCode = 405;
//        await context.Response.CompleteAsync();
//    }
//    else if (context.Request.Headers.TryGetValue("X-Forwarded-Host", out var values))
//    {
//        var forwardedHost = values.FirstOrDefault();
//        // Verify the forwarded host value here
//        if (forwardedHost != null && forwardedHost.Contains(configuration["AllowedHosts"]))
//        {
//            await next();
//        }
//        else
//        {
//            context.Response.StatusCode = 400;
//            await context.Response.CompleteAsync();
//        }
//    }
//    else
//    {
//        await next();
//    }
//});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseIpRateLimiting();
//app.UseHttpsRedirection();
//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider(
//           Path.Combine(builder.Environment.ContentRootPath, "File")),
//    RequestPath = "/StaticFiles"
//});
app.UseHostFiltering();
app.UseCors();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=SSOIntegration}/{action=SSOLogin}");


app.Run();
