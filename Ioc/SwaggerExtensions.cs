using Microsoft.OpenApi.Models;

namespace ApiTesteKognit.Ioc;

public static class SwaggerExtensions
{
    public static IServiceCollection AddCustomSwaggerGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Api Kognit",
                Version = "v1",
                Description = "API para apresentação de teste"
            });

        });

        return services;
    }

}

