using Authentication.Repository;
//using Authentication.Service.Middleware;   open in future if needed Decryption
using Authentication.ServiceBus;
using JwtAuthenticationManager;
using Common.Repository;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddMvc();
builder.Services.AddHttpClient();

builder.Services.AddSingleton<JwtTokenHandler>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<LogsService>();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor(); // Required for HttpContext access in middleware


builder.Services.AddRepositoryInfrastructure(builder.Configuration);
builder.Services.AddServiceInfrastructure(builder.Configuration);
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseMiddleware<DecryptionMiddleware>(); open in future if needed Decryption
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapControllers();
//app.Use(async (context, next) =>
//{
//    context.Response.Headers.Remove("X-Powered-By");
//    context.Response.Headers.Remove("Server");
//    context.Response.Headers.Remove("X-AspNet-Version");
//    context.Response.Headers.Remove("X-AspNetMvc-Version");
//    await next();
//});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=SsoLogin}/{action=Index}");
app.UseCors();

app.Run();


