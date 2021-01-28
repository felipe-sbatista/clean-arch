using System;
using AutoMapper;
using clean.application.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace clean.api.Configurations
{
    public static class AutoMapperConfiguration
    {
        
        public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DtoProfile));
            Configuration.RegisterConfigs();
            return services;
        }
    }
}
