
using AppRegPortal.Services;
using AppRegPortal.Utilities;

using AppRegShared.Utility;

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

using MudBlazor;

using System;
using System.Threading.Tasks;

namespace AppRegPortal.Pages
{
    public partial class UserRegList : ComponentBase
    {
        private readonly ILogger<UserRegList> _logger;
        private readonly IAppRegistrationService _userService;
        private readonly INavigator _navigator;

        public UserRegList(IDialogService dialogService, IAppRegistrationService userService, INavigator navigator, ILogger<UserRegList> logger)
        {
            this._logger = Guard.NotNull(logger, nameof(logger));
            this._userService = Guard.NotNull(userService, nameof(userService), this._logger);
            this._navigator = Guard.NotNull(navigator, nameof(navigator));
        }

        public void OnNewRequestClick()
        {
            try
            {
                this._logger.LogInformation("Navigating to new request dialog");
                this._navigator.NavigateToNewAppRegRequestDialog();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex);
            }
        }
    }
}