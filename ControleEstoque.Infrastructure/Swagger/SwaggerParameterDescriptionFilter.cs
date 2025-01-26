using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class SwaggerParameterDescriptionFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters != null)
        {
            foreach (var param in operation.Parameters)
            {
                if (param.Name == "id")
                {
                    param.Description = "Informe um valor inteiro válido para o ID do produto.";
                    param.Required = true; // Define como obrigatório
                }
            }
        }
    }
}
