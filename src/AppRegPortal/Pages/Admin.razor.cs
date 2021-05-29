using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AppRegPortal.Pages
{
    public partial class Admin
    {
        private string policyMessage = "Check hasn't been made yet.";
        private string userP = "";
        private string adminP = "";
        private string approverP = "";
        private string navP = "";
        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }

        private string Message { get; set; }
        private string UserId { get; set; }
        private IEnumerable<Claim> Claims { get; set; } = Enumerable.Empty<Claim>();

        protected override async Task OnParametersSetAsync()
        {
            await this.GetClaimsPrincipalData();
            await base.OnParametersSetAsync();
        }

        private async Task GetClaimsPrincipalData()
        {
            ClaimsPrincipal user = (await this.authenticationStateTask).User;

            if (user.Identity.IsAuthenticated)
            {
                this.Message = $"{user.Identity.Name} is authenticated.";
                this.Claims = user.Claims;
                this.UserId = $"User Id: {user.FindFirst(c => c.Type == "sub")?.Value}";
            }
            else
            {
                this.Message = "The user is NOT authenticated.";
            }
        }
        private async void CheckPolicy()
        {
            try
            {
                ClaimsPrincipal user = (await this.authenticationStateTask).User;

                this.userP = (await this.AuthorizationService.AuthorizeAsync(user, Constants.Auth.UserPolicy)).Succeeded.ToString();
                this.adminP = (await this.AuthorizationService.AuthorizeAsync(user, Constants.Auth.AdminPolicy)).Succeeded.ToString();
                this.approverP = (await this.AuthorizationService.AuthorizeAsync(user, Constants.Auth.ApproverPolicy)).Succeeded.ToString();
                this.navP = (await this.AuthorizationService.AuthorizeAsync(user, Constants.Auth.DisplayNavigation)).Succeeded.ToString();
            }
            catch (Exception ex)
            {
                this.policyMessage = ex.Message;
            }
        }
    }
}