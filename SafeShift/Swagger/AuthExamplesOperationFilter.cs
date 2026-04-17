using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SafeShift.Swagger;

public class AuthExamplesOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var routeValues = context.ApiDescription.ActionDescriptor.RouteValues;
        var controllerName = routeValues["controller"];
        var actionName = routeValues["action"];

        if (!string.Equals(controllerName, "Auth", StringComparison.OrdinalIgnoreCase) ||
            operation.RequestBody is null)
        {
            return;
        }

        foreach (var content in operation.RequestBody.Content.Values)
        {
            if (string.Equals(actionName, "Register", StringComparison.OrdinalIgnoreCase))
            {
                content.Example = new OpenApiObject
                {
                    ["name"] = new OpenApiString("Jane Safety"),
                    ["email"] = new OpenApiString("jane.safety@company.com"),
                    ["password"] = new OpenApiString("StrongPass123"),
                    ["role"] = new OpenApiString("User")
                };
            }

            if (string.Equals(actionName, "Login", StringComparison.OrdinalIgnoreCase))
            {
                content.Example = new OpenApiObject
                {
                    ["email"] = new OpenApiString("jane.safety@company.com"),
                    ["password"] = new OpenApiString("StrongPass123")
                };
            }
        }
    }
}
