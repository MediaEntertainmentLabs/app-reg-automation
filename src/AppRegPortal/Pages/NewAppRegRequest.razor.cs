using AppRegPortal.Services;
using AppRegPortal.Utilities;

using AppRegShared.Model;
using AppRegShared.Utility;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging;

using System;
using System.Threading.Tasks;

namespace AppRegPortal.Pages
{
    public partial class NewAppRegRequest
    {
        private readonly ILogger<NewAppRegRequest> _logger;
        private readonly IAppRegistrationService _appregService;
        private readonly INavigator _navigator;
        private readonly AuthenticationStateProvider _authStateProvider;

        public AppRegistrationRequest Model { get; private set; } = new AppRegistrationRequest();
        public EditContext? Context { get; private set; }

        public NewAppRegRequest(IAppRegistrationService appregService, INavigator navigator, AuthenticationStateProvider authenticationStateProvider, ILogger<NewAppRegRequest> logger)
        {
            this._logger = Guard.NotNull(logger, nameof(logger));
            this._appregService = Guard.NotNull(appregService, nameof(appregService), logger);
            this._navigator = Guard.NotNull(navigator, nameof(navigator), logger);
            this._authStateProvider = Guard.NotNull(authenticationStateProvider, nameof(authenticationStateProvider), logger);
        }

        protected override async Task OnInitializedAsync()
        {
            await this.ResetForm();
            await base.OnInitializedAsync();
        }

        public async Task OnValidSubmit()
        {
            await this.ResetForm().ConfigureAwait(false);
        }

        public async Task OnReset(MouseEventArgs args)
        {
            await this.ResetForm().ConfigureAwait(false);
        }

        public void OnCancel(MouseEventArgs args)
        {
            this._navigator.NavigateToHome();
        }
        private async Task<(AppRegistrationRequest, EditContext)> ResetForm()
        {
            this.Model = new AppRegistrationRequest();
            this.Context = new EditContext(this.Model);
            this.Context.AddDataAnnotationsValidation();

            this.Model.RequestId = Guid.NewGuid();

            AuthenticationState authState = await this._authStateProvider.GetAuthenticationStateAsync();

            if (authState.User != null)
            {
                this.Model.RequestorUserName = authState.User?.Identity?.Name;
                this.Model.RequestorUserId = authState.User?.FindFirst(c => c.Type == "preferred_username")?.Value;
                this.Model.RequestorEmailAddress = authState.User?.FindFirst(c => c.Type == "verified_primary_email")?.Value ?? authState.User?.FindFirst(c => c.Type == "email")?.Value;
            }

            return (this.Model, this.Context);
        }
    }
}