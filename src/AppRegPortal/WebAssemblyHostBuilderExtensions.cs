using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;

using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AppRegPortal
{
    public static class WebAssemblyHostBuilderExtensions
    {
        /// <summary>
        /// Download and add additional setting.json formatted files to config
        /// </summary>
        /// <param name="builder">Host builder</param>
        /// <param name="fileName">Name of the file relative to base URL</param>
        /// <param name="optional">If the file is required or optional</param>
        /// <returns></returns>
        public static async Task AddJsonConfiguration(this WebAssemblyHostBuilder builder, string fileName, bool optional)
        {
            var http = new HttpClient()
            {
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            };

            using HttpResponseMessage response = await http.GetAsync(fileName);

            if (response.IsSuccessStatusCode)
            {
                using System.IO.Stream stream = await response.Content.ReadAsStreamAsync();
                builder.Configuration.AddJsonStream(stream);
            }
            else
            {
                if (!optional)
                {
                    throw new HttpRequestException(response.ReasonPhrase, null, response.StatusCode);
                }
            }
        }
    }
}
