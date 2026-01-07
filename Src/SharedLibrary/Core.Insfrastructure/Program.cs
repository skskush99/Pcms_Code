using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");


builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
string basePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
builder.Configuration.SetBasePath(basePath); builder.Configuration
    .AddJsonFile($"Config/appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"Config/appsettings.{env}.json", optional: true, reloadOnChange: true);
var app = builder.Build();

app.Run();
