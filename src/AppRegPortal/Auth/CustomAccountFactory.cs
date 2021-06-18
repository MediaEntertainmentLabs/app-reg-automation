
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using Microsoft.Extensions.Logging;

using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AppRegPortal.Auth
{


    public class CustomAccountFactory : AccountClaimsPrincipalFactory<CustomUserAccount>
    {
        private readonly ILogger<CustomAccountFactory> _logger;
        private readonly IServiceProvider _serviceProvider;

        public CustomAccountFactory(IAccessTokenProviderAccessor accessor,
            IServiceProvider serviceProvider,
            ILogger<CustomAccountFactory> logger)
            : base(accessor)
        {
            this._serviceProvider = serviceProvider;
            this._logger = logger;
        }
        public async override ValueTask<ClaimsPrincipal> CreateUserAsync(
            CustomUserAccount account,
            RemoteAuthenticationUserOptions options)
        {
            ClaimsPrincipal initialUser = await base.CreateUserAsync(account, options);

            if (initialUser?.Identity?.IsAuthenticated == true)
            {
                this._logger.LogInformation("User is authenticated");
                var userIdentity = (ClaimsIdentity)initialUser.Identity;

                foreach (string role in account.Roles)
                {
                    this._logger.LogInformation("Found AppRole {role}", role);
                    userIdentity.AddClaim(new Claim("appRole", role));
                }

                foreach (string wid in account.Wids)
                {
                    this._logger.LogInformation("Found directory role {role}", wid);
                    userIdentity.AddClaim(new Claim("directoryRole", wid));
                }
            }
            else
            {
                this._logger.LogInformation("User was not authenticated");
            }

            return initialUser!;
        }
    }
}