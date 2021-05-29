using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;

using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AppRegPortal
{

    public static class WebAssemblyHostBuilderExtensions
    {
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
