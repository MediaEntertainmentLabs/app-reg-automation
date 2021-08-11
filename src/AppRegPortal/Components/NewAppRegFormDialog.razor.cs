using Microsoft.AspNetCore.Components;

using MudBlazor;

namespace AppRegPortal.Components
{
    public partial class NewAppRegFormDialog
    {
        [CascadingParameter]
        private MudDialogInstance? MudDialog { get; set; }

    }
}