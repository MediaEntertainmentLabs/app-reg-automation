using AppRegShared.Utility;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace AppRegPortal.Utilities
{
    public class Navigator : INavigator
    {
        private readonly ILogger<Navigator> _logger;
        private readonly NavigationManager _navigationManager;

        public Navigator(NavigationManager navigationManager, ILogger<Navigator> logger)
        {
            this._logger = Guard.NotNull(logger, nameof(logger));
            this._navigationManager = Guard.NotNull(navigationManager, nameof(navigationManager), this._logger);
        }

        public void NavigateToHome()
        {
            this._navigationManager.NavigateTo("/");
        }

        public void NavigateToNewAppRegRequestDialog()
        {
            this._navigationManager.NavigateTo(Constants.Navigation.NewAppRegRequestRoute);
        }
    }
}
