namespace EcourtService;

using EcourtService.Middleware;
using EcourtServiceBus;
using System.Reflection;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddHttpClient();
        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "E-Court NAPIX API", Version = "v1" });
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
            //c.OperationFilter<CustomHeaderParameter>();
        });
        //builder.Services.AddSwaggerGen();

        builder.Services.AddServiceInfrastructure(builder.Configuration);

        var app = builder.Build();

        app.UseCors(builder =>
            builder
            .AllowAnyOrigin()//WithOrigins("http://example.com", "http://example2.com")
            .AllowAnyMethod()
            .AllowAnyHeader());

        // Configure the HTTP request pipeline.

        if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();

        }

        //middleware for encryption
        app.UseMiddleware<EncryptionDecryptionMiddleware>();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
