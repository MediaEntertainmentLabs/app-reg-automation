using Microsoft.Azure.Functions.Worker.Http;

using System.Threading.Tasks;

namespace AppRegFunctions.Auth
{
    public interface IRequestDataAutorizationService
    {
        Task AuthorizeAsync(HttpRequestData req, string policyName);
    }
}