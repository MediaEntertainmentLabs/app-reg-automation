using AppRegFunctions.Auth;

using AppRegShared.Utility;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

using Newtonsoft.Json;

using System.Collections.Generic;
using System.Dynamic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AppRegFunctions
{
    public class AuthInfo
    {
        private readonly ILogger _logger;
        private readonly IRequestAuthorizationService _authService;

        public AuthInfo(IRequestAuthorizationService authService, ILogger<AuthInfo> logger)
        {
            this._logger = Guard.NotNull(logger, nameof(logger));
            this._authService = Guard.NotNull(authService, nameof(authService), this._logger);
        }

        [FunctionName("GetAllClaims")]
        public IActionResult GetAllClaims(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log, ClaimsPrincipal cp)
        {

            var claimsList = new List<dynamic>();

            foreach (Claim? claim in cp.Claims)
            {
                dynamic c = new ExpandoObject();
                c.Type = claim.Type;
                c.Value = claim.Value;
                c.Issuer = claim.Issuer;
                c.OriginalIssuer = claim.OriginalIssuer;
                c.SubjectName = claim.Subject?.Name;
                c.SubjectActorName = claim.Subject?.Actor?.Name;
                c.SubjectNameClaimType = claim.Subject?.NameClaimType;
                c.SubjectRoleClaimType = claim.Subject?.RoleClaimType;
                c.Properties = claim.Properties;

                claimsList.Add(c);
            }

            dynamic identity = new ExpandoObject();
            identity.Name = cp.Identity.Name;
            identity.AuthenticationType = cp.Identity.AuthenticationType;
            identity.IsAuthenticated = cp.Identity.IsAuthenticated;

            identity.Claims = claimsList;

            string responseMessage = JsonConvert.SerializeObject(identity, Formatting.Indented);

            return new OkObjectResult(responseMessage);
        }

        [FunctionName("RequiresUser")]
        public async Task<IActionResult> RequiresUser([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req)
        {
            await this._authService.AuthorizeAsync(req, Constants.Auth.UserPolicy);

            return new OkResult();
        }

        [FunctionName("RequiresAdmin")]
        public async Task<IActionResult> RequiresAdmin([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req)
        {
            await this._authService.AuthorizeAsync(req, Constants.Auth.AdminPolicy);

            return new OkResult();
        }

        [FunctionName("RequiresApprover")]
        public async Task<IActionResult> RequiresApprover([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req)
        {
            await this._authService.AuthorizeAsync(req, Constants.Auth.ApproverPolicy);

            return new OkResult();
        }
    }
}
