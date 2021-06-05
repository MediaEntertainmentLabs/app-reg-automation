using AppRegPortal.Components;

using MudBlazor;

namespace AppRegPortal.Pages
{
    public partial class UserRegList
    {
        private IDialogService DialogService { get; set; }

        public UserRegList(IDialogService dialogService)
        {
            this.DialogService = dialogService;
        }

        private void OnNewRequestClick()
        {
            this.DialogService.Show<NewAppRegFormDialog>();
        }
    }
}