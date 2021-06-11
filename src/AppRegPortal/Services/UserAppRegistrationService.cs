using AppRegShared.Utility;

using Microsoft.Extensions.Logging;

using System.Net.Http;
using System.Threading.Tasks;

namespace AppRegPortal.Services
{
    public class UserAppRegistrationService : IUserAppRegistrationService
    {
        private readonly HttpClient _client;
        private readonly ILogger _logger;
        public UserAppRegistrationService(HttpClient httpClient, ILogger<UserAppRegistrationService> logger)
        {
            this._logger = Guard.NotNull(logger, nameof(logger));
            this._client = Guard.NotNull(httpClient, nameof(httpClient));

        }

        public async Task<string> Test()
        {
            string result = await this._client.GetStringAsync("api/Test?");
            return result;
        }
    }
}
