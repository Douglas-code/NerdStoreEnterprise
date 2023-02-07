using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Services;
using NSE.WebApp.MVC.Services.Handlers;
using Polly;
using System;

namespace NSE.WebApp.MVC.Configuration
{
    public static partial class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();
            services.AddHttpClient<IAutenticacaoService, AutenticacaoService>();

            services.AddHttpClient<ICatalogoService, CatalogoService>().AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>().AddPolicyHandler(PollyExtensions.EsperarTentar()).AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IUser, AspNetUser>();
        }
    }
}
