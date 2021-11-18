using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using tippy.cloud.Client;

namespace tippy.cloud.Client
{
 

    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddAuth(this IServiceCollection services)
        {
            services
                .AddAuthorizationCore()
                .AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>()
                .AddScoped<IAuthService, AuthService>()
                .AddScoped<ILocalStorage, LocalStorage>()
                .AddScoped<IMyHttp, MyHttp>();

            return services;
        }
    }
}
