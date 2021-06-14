using AppRegFunctions.Auth;

using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

using System.Net.Http;
using System.Threading.Tasks;

namespace AppRegFunctions
{
    public class Function1
    {
        private readonly IRequestDataAutorizationService _authService;
        private readonly ILogger _logger;

        public Function1(IRequestDataAutorizationService authorizationService, ILogger<Function1> logger)
        {
            this._authService = authorizationService;
            this._logger = logger;
        }

        [Function("Function1")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
            FunctionContext executionContext)
        {
            try
            {
                await this._authService.AuthorizeAsync(req, Constants.Auth.AdminPolicy);
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode != null)
                {
                    return req.CreateResponse(ex.StatusCode.Value);
                }
            }

            HttpResponseData? response = req.CreateResponse();
            return response;
        }
    }
}
