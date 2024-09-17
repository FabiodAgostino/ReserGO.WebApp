using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;

namespace ReserGO.WebApp.Components.Layout
{
    public partial class MainLayout : LayoutComponentBase
    {
        [CascadingParameter] private Task<AuthenticationState> Authentication { get; set; }
        public bool Render = false;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var authstate = await Authentication;
                if (authstate != null)
                {
                    Render = true;
                    StateHasChanged();
                }
            }
        }

    }
}
