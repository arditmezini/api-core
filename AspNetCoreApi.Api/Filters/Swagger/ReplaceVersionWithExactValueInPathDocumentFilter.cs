using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace AspNetCoreApi.Api.Filters.Swagger
{
    public class ReplaceVersionWithExactValueInPathDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument document, DocumentFilterContext context)
        {
            if (document == null)
                throw new ArgumentNullException(nameof(document));

            var replacements = new OpenApiPaths();

            foreach (var (key, value) in document.Paths)
            {
                replacements.Add(key.Replace("{version}", document.Info.Version,
                        StringComparison.InvariantCulture), value);
            }

            document.Paths = replacements;
        }
    }
}