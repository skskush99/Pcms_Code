namespace EcourtService;

using Common.Repository;
using EcourtService.Middleware;
using EcourtServiceBus;
using System.Reflection;
using Microsoft.OpenApi.Models;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // --- 1. Services Configuration (DI Container) ---
        builder.Services.AddHttpClient();
        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer(); // Required for Swagger with modern Minimal APIs/Controllers
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "E-Court NAPIX API", Version = "v1" });

            // Fixed XML path logic to be safer
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath))
            {
                c.IncludeXmlComments(xmlPath);
            }
        });

        builder.Services.AddServiceInfrastructure(builder.Configuration);
        builder.Services.AddScoped<LogsService>();

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                // Note: AllowAnyOrigin cannot be used with AllowCredentials. 
                // Fine for public APIs, but be aware for Auth scenarios.
                policy.AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowAnyOrigin();
            });
        });

        // --- 2. Build the App ---
        var app = builder.Build();

        // --- 3. Middleware Pipeline (Order Matters!) ---

        // Always handle Exceptions and HSTS first in production
        if (!app.Environment.IsDevelopment())
        {
            app.UseHsts();
        }

        // Move Swagger outside the Dev check if you explicitly want it in Production
        if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "E-Court NAPIX API v1");
                // c.RoutePrefix = string.Empty; // Uncomment to serve Swagger at the app root
            });
        }

        app.UseHttpsRedirection();

        // UseCors must come AFTER UseRouting (done implicitly by MapControllers) 
        // but BEFORE UseAuthorization
        app.UseCors();

        // If you re-enable your encryption middleware, it usually goes here:
        // app.UseMiddleware<EncryptionDecryptionMiddleware>();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
//namespace EcourtService;

//using Common.Repository;
//using EcourtService.Middleware;
//using EcourtServiceBus;
//using System.Reflection;

//public class Program
//{
//    public static void Main(string[] args)
//    {
//        var builder = WebApplication.CreateBuilder(args);

//        // Add services to the container.
//        builder.Services.AddHttpClient();
//        builder.Services.AddControllers();
//        builder.Services.AddSwaggerGen(c =>
//        {
//            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "E-Court NAPIX API", Version = "v1" });
//            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
//            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
//            c.IncludeXmlComments(xmlPath);
//            //c.OperationFilter<CustomHeaderParameter>();
//        });
//        //builder.Services.AddSwaggerGen();

//        builder.Services.AddServiceInfrastructure(builder.Configuration);
//        builder.Services.AddScoped<LogsService>();

//        // Configure the HTTP request pipeline.   // 

//        //app.UseCors(builder =>
//        //    builder
//        //    .AllowAnyOrigin()//WithOrigins("http://example.com", "http://example2.com")
//        //    .AllowAnyMethod()
//        //    .AllowAnyHeader());


//        builder.Services.AddCors(options =>
//        {
//            options.AddDefaultPolicy(builder =>
//            {
//                //builder.AllowAnyHeader()
//                //    .WithMethods("POST", "GET")
//                //    .WithOrigins("http://10.70.234.9", "http://10.70.234.9:80", "http://localhost:4200");
//                builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
//            });
//        });

//        // Configure the HTTP request pipeline.
//        var app = builder.Build();

//        if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
//        {
//            app.UseSwagger();
//            app.UseSwaggerUI();
//            app.UseHttpsRedirection();

//        }

//        //middleware for encryption
//        //app.UseMiddleware<EncryptionDecryptionMiddleware>();  //open in future if needed Decryption

//        app.UseCors();

//        app.UseAuthorization();

//        app.MapControllers();

//        app.Run();
//    }
//}
