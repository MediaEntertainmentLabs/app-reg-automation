using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using AppRegPortal;
using AppRegPortal.Shared;
using MudBlazor;
using Microsoft.Extensions.Logging;
using AppRegPortal.Services;
using AppRegShared.Utility;

namespace AppRegPortal.Pages
{
    public partial class NewAppRegRequest
    {
        private readonly ILogger<NewAppRegRequest> _logger;
        private readonly IAppRegistrationService _appregService;

        public NewAppRegRequest(IAppRegistrationService appregService, ILogger<NewAppRegRequest> logger)
        {
            this._logger = Guard.NotNull(logger, nameof(logger));
            this._appregService = Guard.NotNull(appregService, nameof(appregService), logger);
        }
    }
}