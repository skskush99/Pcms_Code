using JwtAuthenticationManager;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Report.Repository;
using Report.ServiceBus;
using ReportService.Middleware;
using Common.Repository;

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

builder.Services.AddRepositoryInfrastructure(builder.Configuration);
builder.Services.AddServiceInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(builder =>
    builder
    .AllowAnyOrigin()//WithOrigins("http://example.com", "http://example2.com")
    .AllowAnyMethod()
    .AllowAnyHeader());


app.UseHttpsRedirection();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment()|| app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
//middleware for encryption
//app.UseMiddleware<EncryptionDecryptionMiddleware>();
app.UseUserContext();

app.MapControllers();

app.Run();
