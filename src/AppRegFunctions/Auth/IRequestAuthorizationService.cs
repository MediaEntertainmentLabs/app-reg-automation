using Microsoft.AspNetCore.Http;

using System.Security.Claims;
using System.Threading.Tasks;

namespace AppRegFunctions.Auth
{
    public interface IRequestAuthorizationService
    {
        Task AuthorizeAsync(HttpRequest req, string policyName);
        Task AuthorizeAsync(ClaimsPrincipal claimsPrincipal, string policyName);
    }
}