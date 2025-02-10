using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CadastroCliente.API.Filters
{
    public class RemoveDomainSchemasFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var schemasToRemove = new List<string> { "Client", "Address" };

            foreach (var schema in schemasToRemove)
            {
                swaggerDoc.Components.Schemas.Remove(schema);
            }
        }
    }
}
