
using AppRegPortal.Services;

using MudBlazor;

using System;
using System.Net.Http;

namespace AppRegPortal.Pages
{
    public partial class UserRegList
    {
        private IDialogService DialogService { get; set; }
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IAppRegistrationService _userService;

        private string? Message { get; set; }
        private string? Result { get; set; }

        public UserRegList(IDialogService dialogService, HttpClient hc, IHttpClientFactory factory, IAppRegistrationService userService)
        {
            this.DialogService = dialogService;
            this._httpClient = hc;
            this._clientFactory = factory;
            this._userService = userService;
        }

        private async void OnNewRequestClick()
        {
            try
            {
                //this.Message += "Getting client";

                //this._httpClient = this._clientFactory.CreateClient("spn-user-bff");

                //this.Message += "  Getting data";
                //HttpResponseMessage? dataRequest = await this._httpClient.GetAsync("api/Test?");

                //this.Message += " Checking status code";
                //if (dataRequest.IsSuccessStatusCode)
                //{
                //    this.Message += "  success";
                //    this.Result = await dataRequest.Content.ReadAsStringAsync();
                //}
                //else
                //{
                //    this.Message += "  datarequest failed";
                //    this.Result = dataRequest?.ReasonPhrase!;
                //}
                this.Message += await this._userService.Test();

            }
            catch (Exception ex)
            {
                this.Message += "  Exception " + ex.GetType().ToString();
                this.Result = ex.Message;
            }
            this.StateHasChanged();
        }
    }
}