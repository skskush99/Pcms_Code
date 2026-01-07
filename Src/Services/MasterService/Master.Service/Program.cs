using Common.Repository;
using JwtAuthenticationManager;
using Master.Repository;
using Master.Service.Extension;
using Master.ServiceBus;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;
using System.Drawing;

//Log.Information("Starting web host");
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddJwtAuthentication();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<LogsService>();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement {
    {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                    }
                },
                new string[] {}
    }
    });
});

builder.Services.AddHttpContextAccessor(); // Important: Add HttpContextAccessor
//builder.Services.AddHttpContextAccessor(); // Important: Add HttpContextAccessor

builder.Services.AddRepositoryInfrastructure(builder.Configuration);
builder.Services.AddServiceInfrastructure(builder.Configuration);

// Configure the HTTP request pipeline.
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

var app = builder.Build();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "Uploads")),
    RequestPath = "/Uploads"
});

app.UseHttpsRedirection();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
}
//middleware for encryption
//app.UseMiddleware<Master.Service.Middleware.EncryptionDecryptionMiddleware>(); //open in future if needed Decryption

app.UseUserContext();
app.UseCors();

app.UseAuthorization();
app.MapControllers();
app.Run();

