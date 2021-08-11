using AppRegFunctions.Auth;

using AppRegShared;

using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(AppRegFunctions.Startup))]

namespace AppRegFunctions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {

            FunctionsHostBuilderContext? context = builder.GetContext();
            if (string.Compare(context.EnvironmentName, "Development", true) == 0)
            {
                builder.Services.AddSingleton<IRequestAuthorizationService, DevRequestAuthorizationService>();
            }
            else
            {
                builder.Services.AddSingleton<IRequestAuthorizationService, RequestAuthorizationService>();
            }

            builder.Services.AddSingleton<IRequestAuthorizationService, RequestAuthorizationService>();

            builder.Services.AddLogging();

            builder.Services.AddAuthorizationCore(options =>
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
        }
    }
}
