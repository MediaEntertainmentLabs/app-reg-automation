using AppRegShared.Utility;

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
    public class DevRequestAuthorizationService : IRequestAuthorizationService
    {
        private readonly ILogger _logger;

        public DevRequestAuthorizationService(ILogger<DevRequestAuthorizationService> logger)
        {
            this._logger = Guard.NotNull(logger, nameof(logger));
        }
        public Task AuthorizeAsync(HttpRequest req, string policyName)
        {
            return this.AuthorizeAsync(req.HttpContext.User, policyName);
        }
        public Task AuthorizeAsync(ClaimsPrincipal claimsPrincipal, string policyName)
        {
            //Make sure we are in an environment where there is no identity
            //information available, to prevent accidental use in Azure
            try
            {
                var identities = claimsPrincipal.Identities.ToList();
                if (identities.Count == 0)
                {
                    return Task.CompletedTask;
                }
            }
            catch (NullReferenceException)
            {
                //If there are no identities probably OK
                return Task.CompletedTask;
            }

            string message = "Looks like there are identities associated with this HttpRequestData, don't use DevRequestDataAuthorizationService";

            var msg = new HttpResponseMessage(HttpStatusCode.Unauthorized)
            {
                ReasonPhrase = message
            };

            this._logger.LogCritical(message);

            throw new AuthorizationException(msg);
        }
    }
}
