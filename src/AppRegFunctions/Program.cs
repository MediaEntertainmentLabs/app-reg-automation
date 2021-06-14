using AppRegFunctions.Auth;

using AppRegShared;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AppRegFunctions
{
    public class Program
    {
        public static void Main()
        {
            IHost host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults(workerApplication =>
                {
                })
                .ConfigureServices((builderContext, serviceCollection) =>
                {
                    if (builderContext.HostingEnvironment.IsDevelopment())
                    {
                        serviceCollection.AddSingleton<IRequestDataAutorizationService, DevRequestDataAuthorizationService>();
                    }
                    else
                    {
                        serviceCollection.AddSingleton<IRequestDataAutorizationService, RequestDataAuthorizationService>();
                    }

                    serviceCollection.AddLogging();

                    serviceCollection.AddAuthorizationCore(options =>
                      {
                          options.AddPolicy(Constants.Auth.UserPolicy, policy =>
                              policy.RequireAuthenticatedUser()
                              .RequireClaim("http://schemas.microsoft.com/identity/claims/scope", Constants.Auth.RequiredScopes)
                              );

                          options.AddPolicy(Constants.Auth.AdminPolicy, policy =>
                              policy.RequireRole(ApplicationRoles.AdminRole)
                              .RequireClaim("http://schemas.microsoft.com/identity/claims/scope", Constants.Auth.RequiredScopes)
                              );

                          options.AddPolicy(Constants.Auth.ApproverPolicy, policy =>
                              policy.RequireRole(ApplicationRoles.ApproverRole)
                              .RequireClaim("http://schemas.microsoft.com/identity/claims/scope", Constants.Auth.RequiredScopes)
                              );
                      });
                })
                .Build();

            host.Run();
        }
    }
}