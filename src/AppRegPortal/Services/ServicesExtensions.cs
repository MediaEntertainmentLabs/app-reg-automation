using AppRegShared.Utility;

using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;

namespace AppRegPortal.Services
{
    public static class ServicesExtensions
    {
        public static void ConfigureHttpService<TService, TImplementation>(this IServiceCollection services, string key)
            where TService : class
            where TImplementation : class, TService
        {
            IHttpClientBuilder? clientBuilder = services.AddHttpClient<TService, TImplementation>((services, httpClient) =>
            {
                IConfiguration config = services.GetRequiredService<IConfiguration>();
                var options = new ServiceOptions();
                config.Bind(key, options);

                Guard.NotNull(options.BaseUrl, nameof(options.BaseUrl));
                httpClient.BaseAddress = new Uri(options.BaseUrl);
            });

            clientBuilder.AddHttpMessageHandler(serviceProvider =>
            {
                AuthorizationMessageHandler handler = serviceProvider.GetRequiredService<AuthorizationMessageHandler>();

                IConfiguration config = serviceProvider.GetRequiredService<IConfiguration>();
                var options = new ServiceOptions();
                config.Bind(key, options);

                handler.ConfigureHandler(authorizedUrls: options.AuthorizedUrls, scopes: options.Scopes);
                return handler;
            });
        }
    }
}
