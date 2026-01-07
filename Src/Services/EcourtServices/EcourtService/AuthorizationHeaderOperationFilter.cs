using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.Swagger;

namespace EcourtService
{
    public class CustomHeaderParameter : Swashbuckle.AspNetCore.SwaggerGen.IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "X-Authorization",
                In = ParameterLocation.Header,
                Required = true,
                Description = "Custom header for authentication",
                Schema = new OpenApiSchema
                {
                    Type = "string"
                }
            });
            //operation.Parameters =
            //[
            //    new OpenApiParameter
            //    {
            //        Name = "Authorization",
            //        In = ParameterLocation.Header,
            //        Description = "Custom header for authentication",
            //        Required = true
            //    },
            //];
        }
    }
}
