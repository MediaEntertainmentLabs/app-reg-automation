using AppRegShared.Utility;

using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace AppRegFunctions.Auth
{
    public class DevRequestDataAuthorizationService : IRequestDataAutorizationService
    {
        private readonly ILogger _logger;

        public DevRequestDataAuthorizationService(ILogger<DevRequestDataAuthorizationService> logger)
        {
            this._logger = Guard.NotNull(logger, nameof(logger));
        }

        public Task AuthorizeAsync(HttpRequestData req, string policyName)
        {
            //Make sure we are in an environment where there is no identity
            //information available, to prevent accidental use in Azure
            try
            {
                var identities = req.Identities.ToList();
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
            this._logger.LogCritical(message);
            throw new AuthorizationException(message);
        }
    }
}
