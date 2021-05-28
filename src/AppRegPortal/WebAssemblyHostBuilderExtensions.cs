using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
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

            using var response = await http.GetAsync(fileName);

            if(response.IsSuccessStatusCode)
            {
                using var stream = await response.Content.ReadAsStreamAsync();
                builder.Configuration.AddJsonStream(stream);
            }
            else
            {
                if(!optional)
                {
                    throw new HttpRequestException(response.ReasonPhrase, null, response.StatusCode);
                }
            }
        }
    }
}
