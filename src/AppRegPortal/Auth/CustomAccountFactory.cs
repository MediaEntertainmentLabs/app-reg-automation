
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

            if (initialUser.Identity.IsAuthenticated)
            {
                var userIdentity = (ClaimsIdentity)initialUser.Identity;

                foreach (string role in account.Roles)
                {
                    userIdentity.AddClaim(new Claim("appRole", role));
                }

                foreach (string wid in account.Wids)
                {
                    userIdentity.AddClaim(new Claim("directoryRole", wid));
                }
            }

            return initialUser;
        }
    }
}