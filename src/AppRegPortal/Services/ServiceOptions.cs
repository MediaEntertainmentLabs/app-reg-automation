using System;

namespace AppRegPortal.Services
{
    public class ServiceOptions
    {
        public string? BaseUrl { get; set; }
        public string[]? Scopes { get; set; }
        public string[] AuthorizedUrls => this.BaseUrl == null ? Array.Empty<string>() : new string[] { this.BaseUrl };
    }
}
