
using AppRegShared.Utility;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AppRegFunctions.Auth
{
    /// <summary>
    /// Validate correct claims for an out of process C# function
    /// </summary>
    public class RequestDataAuthorizationService : IRequestDataAutorizationService
    {
        private readonly IAuthorizationService _authService;
        private readonly ILogger _logger;

        public RequestDataAuthorizationService(IAuthorizationService authorizationService, ILogger<RequestDataAuthorizationService> logger)
        {
            this._logger = Guard.NotNull(logger, nameof(logger));
            this._authService = Guard.NotNull(authorizationService, nameof(authorizationService), logger);
        }

        public async Task AuthorizeAsync(HttpRequestData req, string policyName)
        {
            ClaimsPrincipal cp;
            //Update the name type and role type in the ClaimsIdentity
            try
            {
                ClaimsIdentity[]? identities = req.Identities.Select(i => new ClaimsIdentity(i, null, i.AuthenticationType, "name", "roles")).ToArray();
                cp = new ClaimsPrincipal(identities);
            }
            catch (Exception ex)
            {
                string message = "Error wile getting user identity";
                this._logger.LogError(ex, message);

                throw new AuthorizationException(message, ex);
            }

            AuthorizationResult? authResult = await this._authService.AuthorizeAsync(cp, policyName);

            if (!authResult.Succeeded)
            {
                string? requirements = authResult.Failure != null
                    ? string.Join(Environment.NewLine, authResult.Failure.FailedRequirements.Select(req => req.ToString()))
                    : string.Empty;

                string message = $"Authorization failed for policy {policyName} because following requirements were not met: {requirements}";
                this._logger?.LogError(message);

                throw new AuthorizationException(message);
            }
        }
    }
}
