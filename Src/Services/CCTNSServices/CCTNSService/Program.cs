namespace CCTNSService;

using Common.Repository;
using CCTNSService.Middleware;
using CCTNSServiceBus;
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
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "CCTNS NAPIX API", Version = "v1" });

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
                //c.SwaggerEndpoint("/swagger/v1/swagger.json", "CCTNS NAPIX API v1");
                c.SwaggerEndpoint("v1/swagger.json", "CCTNS NAPIX API v1");
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
