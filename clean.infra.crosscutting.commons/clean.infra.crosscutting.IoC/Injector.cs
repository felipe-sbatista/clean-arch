using System;
using clean.application.Interfaces;
using clean.application.Services;
using clean.domain.Interfaces.Repositories;
using clean.domain.Interfaces.Services;
using clean.domain.Services;
using clean.infra.data;
using clean.infra.data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace clean.infra.crosscutting.IoC
{
    public static class Injector
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            RegisterInfraServices(services);
            RegisterDomainServices(services);
            RegisterApplicationServices(services);

            return services;
        }

        private static void RegisterDomainServices(IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }

        private static void RegisterApplicationServices(IServiceCollection services)
        {
            services.AddTransient<IUserAppService, UserAppService>();
        }

        private static void RegisterInfraServices(IServiceCollection services)
        {
            #region repositories
            services.AddTransient<IUserRepository, UserRepository>();
            
            #endregion
        }
    }
}
