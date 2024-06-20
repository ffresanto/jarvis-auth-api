using Microsoft.OpenApi.Models;

namespace JarvisAuth.API.Configurations
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "JarvisAuth.API",
                    Description = "Login and access permission management API for multiple applications.",
                    Contact = new OpenApiContact 
                    { 
                        Url = new Uri("https://www.linkedin.com/in/franccesco-felipe-rodrigues/"),
                        Name = "Franccesco Felipe", 
                        Email = "ffresanto@outlook.com"
                    },Version = "v1"
                });

                // Configuração da autenticação JWT no Swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });

            return services;
        }
    }
}
