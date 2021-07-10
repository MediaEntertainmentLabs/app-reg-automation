
using AppRegShared.Utility;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AppRegFunctions.Auth
{
    /// <summary>
    /// Validate correct claims for an out of process C# function
    /// </summary>
    public class RequestAuthorizationService : IRequestAuthorizationService
    {
        private readonly IAuthorizationService _authService;
        private readonly ILogger _logger;

        public RequestAuthorizationService(IAuthorizationService authorizationService, ILogger<RequestAuthorizationService> logger)
        {
            this._logger = Guard.NotNull(logger, nameof(logger));
            this._authService = Guard.NotNull(authorizationService, nameof(authorizationService), logger);
        }
        public Task AuthorizeAsync(HttpRequest req, string policyName)
        {
            return this.AuthorizeAsync(req.HttpContext.User, policyName);
        }

        public async Task AuthorizeAsync(ClaimsPrincipal claimsPrincipal, string policyName)
        {
            ClaimsPrincipal cp;
            //Update the name type and role type in the ClaimsIdentity
            try
            {
                ClaimsIdentity[]? identities = claimsPrincipal.Identities.Select(i => new ClaimsIdentity(i, null, i.AuthenticationType, "name", "roles")).ToArray();
                cp = new ClaimsPrincipal(identities);
            }
            catch (Exception ex)
            {
                string message = "Error wile getting user identity";
                this._logger.LogError(ex, message);

                var msg = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    ReasonPhrase = message
                };

                throw new AuthorizationException(msg);
            }

            AuthorizationResult? authResult = await this._authService.AuthorizeAsync(cp, policyName);

            if (!authResult.Succeeded)
            {
                string? requirements = authResult.Failure != null
                    ? string.Join(Environment.NewLine, authResult.Failure.FailedRequirements.Select(req => req.ToString()))
                    : string.Empty;

                string message = $"Authorization failed for policy {policyName} because following requirements were not met: {requirements}";
                this._logger?.LogError(message);

                var msg = new HttpResponseMessage(HttpStatusCode.Unauthorized);

                throw new AuthorizationException(msg);
            }
        }
    }
}
