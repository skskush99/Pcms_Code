using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LitesApi.Gateway.Middleware;

public class SwaggerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _ocelotConfigPath;

    public SwaggerMiddleware(RequestDelegate next, string ocelotConfigPath)
    {
        _next = next;
        _ocelotConfigPath = ocelotConfigPath;
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Path == "/swagger/v1/swagger.json") // Customize path as needed
        {
            var ocelotConfig = JObject.Parse(File.ReadAllText(_ocelotConfigPath));
            var swagger = new  swagger.Info = new Info { Title = "API Gateway", Version = "v1" }; // Customize

            // Iterate through Ocelot routes
            foreach (var route in ocelotConfig["ReRoutes"])
            {
                var routePath = route["DownstreamPathTemplate"].ToString().Replace("{", "").Replace("}", ""); // Clean path
                var upstreamPath = route["UpstreamPathTemplate"].ToString();
                var methods = route["UpstreamHttpMethod"].ToObject<List<string>>();

                foreach (var method in methods)
                {
                    var pathItem = new PathItem();
                    var operation = new Operation();

                    // Basic operation details - customize as needed
                    operation.Summary = $"API endpoint for {upstreamPath}";
                    operation.Produces = new List<string> { "application/json" }; // Adjust as needed

                    // Add parameters (you'll need to extract these from DownstreamPathTemplate)
                    operation.Parameters = ExtractParameters(route["DownstreamPathTemplate"].ToString());


                    switch (method.ToLower()) // Set appropriate operation type
                    {
                        case "get": pathItem.Get = operation; break;
                        case "post": pathItem.Post = operation; break;
                        case "put": pathItem.Put = operation; break;
                        case "delete": pathItem.Delete = operation; break;
                            // ... other methods
                    }

                    swagger.Paths.Add(upstreamPath, pathItem);
                }
            }

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(swagger));
            return;
        }

        await _next(context);
    }

    private List<Parameter> ExtractParameters(string path)
    {
        var parameters = new List<Parameter>();
        var matches = System.Text.RegularExpressions.Regex.Matches(path, @"\{(.*?)\}");
        foreach (var match in matches)
        {
            parameters.Add(new Parameter
            {
                Name = match.ToString().Trim('{', '}'),
                In = "path", // Usually path parameters
                Required = true,
                Type = "string" // Infer type if possible, otherwise default to string
            });
        }
        return parameters;
    }
}
