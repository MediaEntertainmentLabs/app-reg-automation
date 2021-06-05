using AppRegShared.Model;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace AppRegPortal.Components
{
    public partial class AppRegDetailsForm
    {
        [Parameter]
        public AppRegistrationRequest Model { get; set; } = new AppRegistrationRequest();

        private void OnValidSubmit(EditContext context)
        {

        }
    }
}