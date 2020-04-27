using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreApi.Api.Filters
{
    public class UnauthorizedResponsesOperationFilter : IOperationFilter
    {
        private readonly OpenApiSecurityScheme schemeName;

        public UnauthorizedResponsesOperationFilter(OpenApiSecurityScheme schemeName)
        {
            this.schemeName = schemeName;
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var hasAuthorizeControllerOrAction =
                context.MethodInfo.DeclaringType.GetCustomAttributes(true)
                    .Union(context.MethodInfo.GetCustomAttributes(true))
                    .OfType<AuthorizeAttribute>().Any() ||
                context.ApiDescription.ActionDescriptor.FilterDescriptors
                    .OfType<AuthorizeAttribute>().Any();

            var hasAnonymousControllerOrAction =
                context.MethodInfo.DeclaringType.GetCustomAttributes(true)
                    .Union(context.MethodInfo.GetCustomAttributes(true))
                    .OfType<AllowAnonymousAttribute>().Any() ||
                context.ApiDescription.ActionDescriptor.FilterDescriptors
                    .OfType<AllowAnonymousAttribute>().Any();

            if (hasAnonymousControllerOrAction) return;
            if (!hasAuthorizeControllerOrAction) return;

            operation.Security = new List<OpenApiSecurityRequirement>
            {
                new OpenApiSecurityRequirement {
                    {
                      schemeName , new List<string>()
                    }
                }
            };
        }
    }
}