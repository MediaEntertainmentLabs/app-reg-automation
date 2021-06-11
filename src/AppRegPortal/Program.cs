using AppRegPortal.Auth;
using AppRegPortal.Services;
using AppRegPortal.Utilities;

using AppRegShared;

using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MudBlazor.Services;

using System.Threading.Tasks;

namespace AppRegPortal
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            //For settings that need to be chaned per deployment, force the user to update.
            await builder.AddJsonConfiguration("local.settings.json", true);

            builder.RootComponents.Add<App>("#app");

            Program.ConfigureAuth(builder);

            builder.Services.AddLogging();
            builder.Services.AddMudServices();

            builder.Services.ConfigureHttpService<IUserAppRegistrationService, UserAppRegistrationService>("API");

            builder.Services.UseDIComponentActivator();
            builder.Services.RegisterAllComponentsInImplementingAssembly<Program>();


            await builder.Build().RunAsync();
        }

        private static void ConfigureAuth(WebAssemblyHostBuilder builder)
        {
            builder.Services.AddMsalAuthentication<RemoteAuthenticationState, CustomUserAccount>(options =>
            {
                builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
                options.UserOptions.RoleClaim = "appRole";

            }).AddAccountClaimsPrincipalFactory<RemoteAuthenticationState, CustomUserAccount, CustomAccountFactory>();

            builder.Services.AddAuthorizationCore(options =>
            {
                options.AddPolicy(Constants.Auth.UserPolicy, policy =>
                    policy.RequireAuthenticatedUser());

                options.AddPolicy(Constants.Auth.AdminPolicy, policy =>
                    policy.RequireRole(ApplicationRoles.AdminRole));

                options.AddPolicy(Constants.Auth.ApproverPolicy, policy =>
                    policy.RequireRole(ApplicationRoles.ApproverRole));

                options.AddPolicy(Constants.Auth.DisplayNavigation, policy =>
                    policy.RequireRole(ApplicationRoles.AdminRole, ApplicationRoles.ApproverRole));
            });
        }
    }
}
