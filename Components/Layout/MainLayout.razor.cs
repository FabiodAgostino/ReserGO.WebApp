using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using ReserGO.Service.Service.Authentication;
using ReserGO.Service.Interface.Authentication;

namespace ReserGO.WebApp.Components.Layout
{
    public partial class MainLayout : LayoutComponentBase
    {
        [CascadingParameter] private Task<AuthenticationState> Authentication { get; set; }
        [Inject] NavigationManager Navigation { get; set; }
        [Inject] IJwtAuthenticationStateProvider provider { get; set; }
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
                        await provider.GetAuthenticationStateAsync();
                        Render = true;
                        StateHasChanged();
                    }
                }
            }
        }

    }
}
