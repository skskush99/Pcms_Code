namespace EcourtService;

using Common.Repository;
//using EcourtService.Middleware;
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
        builder.Services.AddScoped<LogsService>();

        // Configure the HTTP request pipeline.   // 

        //app.UseCors(builder =>
        //    builder
        //    .AllowAnyOrigin()//WithOrigins("http://example.com", "http://example2.com")
        //    .AllowAnyMethod()
        //    .AllowAnyHeader());


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

        // Configure the HTTP request pipeline.
        var app = builder.Build();

        if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();

        }

        //middleware for encryption
        //app.UseMiddleware<EcourtService.Middleware.EncryptionDecryptionMiddleware>();  //open in future if needed Decryption

        app.UseCors();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
