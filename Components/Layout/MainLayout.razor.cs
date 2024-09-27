using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;

namespace ReserGO.WebApp.Components.Layout
{
    public partial class MainLayout : LayoutComponentBase
    {
        [CascadingParameter] private Task<AuthenticationState> Authentication { get; set; }
        [Inject] NavigationManager Navigation { get; set; }
        public bool Render = false;
        public bool IsRedirect = false;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var currentUrl = Navigation.Uri;

                IsRedirect = currentUrl.Count(c => c == '/') > 3;

                if(!IsRedirect)
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
}
