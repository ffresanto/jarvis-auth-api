﻿using JarvisAuth.Application.Services;
using JarvisAuth.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace JarvisAuth.Application.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection ServicesDependencies(this IServiceCollection services)
        {
            services.AddScoped<ITypeService, TypeService>();

            return services;
        }
    }
}
