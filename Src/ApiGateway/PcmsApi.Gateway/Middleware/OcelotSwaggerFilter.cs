using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json;

namespace LitesApi.Gateway.Middleware;

public class OcelotSwaggerFilter : Swashbuckle.AspNetCore.SwaggerGen.IDocumentFilter
{
    private readonly IConfiguration _configuration;

    public OcelotSwaggerFilter(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var ocelotJsonPath = _configuration.GetValue<string>("Ocelot:ConfigurationPath") ?? "ocelot.json"; // Default path
        var ocelotConfig = GetOcelotConfiguration(ocelotJsonPath);

        if (ocelotConfig == null) return;

        swaggerDoc.Servers.Clear(); // Clear default servers

        foreach (var route in ocelotConfig.ReRoutes)
        {
            foreach (var hostAndPort in route.DownstreamHostAndPorts)
            {
                var url = $"{route.DownstreamScheme}://{hostAndPort.Host}:{hostAndPort.Port}";
                swaggerDoc.Servers.Add(new OpenApiServer { Url = url });
            }
        }
    }


    private OcelotConfiguration GetOcelotConfiguration(string path)
    {
        try
        {
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            using (StreamReader reader = new StreamReader(fileStream))
            {
                string json = reader.ReadToEnd();
                return JsonSerializer.Deserialize<OcelotConfiguration>(json);
            }
        }
        catch (System.Exception ex)
        {
            Console.WriteLine($"Error reading Ocelot configuration: {ex.Message}");
            return null;
        }
    }

    public class OcelotConfiguration
    {
        public List<Route> ReRoutes { get; set; }
    }

    public class Route
    {
        public string DownstreamPathTemplate { get; set; }
        public string DownstreamScheme { get; set; }
        public List<HostAndPort> DownstreamHostAndPorts { get; set; }
        public string UpstreamPathTemplate { get; set; }
        public List<string> UpstreamHttpMethod { get; set; }
    }

    public class HostAndPort
    {
        public string Host { get; set; }
        public int Port { get; set; }
    }
}