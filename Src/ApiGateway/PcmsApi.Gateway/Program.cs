using JwtAuthenticationManager;
using Microsoft.Extensions.Options;
//using PcmsApi.Gateway.Middleware;
using Microsoft.OpenApi.Models;
using MMLib.SwaggerForOcelot.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Values;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
//builder.Services.AddOcelot();
// Add Ocelot
builder.Services.AddOcelot(builder.Configuration);
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Gateway", Version = "v1" });
//    // Add other Swagger configuration options as needed.
//});

//builder.Services.ConfigureSwaggerGen(options =>
//{
//    options.DocumentFilter<OcelotSwaggerFilter>(); // Add custom filter
//});
// Add services to the container.
builder.Services.AddHttpContextAccessor();
//builder.Services.AddControllers().AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.PropertyNamingPolicy = null;
//});
builder.Services.AddControllers();
builder.Services.AddMvc();
builder.Services.AddHttpClient();

//builder.Services.AddControllers();
builder.Services.AddJwtAuthentication();

// Add Swagger Generation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        //builder.AllowAnyHeader()
        //    .WithMethods("POST", "GET")
        //    .WithOrigins("http://10.70.234.9", "http://10.70.234.9:80", "http://localhost:4200");
        builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

// Add Swagger for Ocelot
builder.Services.AddSwaggerForOcelot(builder.Configuration)
    .AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "API Gateway",
            Version = "v1"
        });
    });
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseRouting();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        if (app.Environment.IsDevelopment())
        {
            // For Debug in Kestrel
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API V1");
        }
        else
        {
            options.SwaggerEndpoint("/PcmsGatewayService/swagger/v1/swagger.json", "Web API V1");
        }
    });
    app.UseHttpsRedirection();
}
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=SsoLogin}/{action=Index}");
app.UseCors("CorsPolicy");
app.UseCors();
await app.UseOcelot();
app.Run();
