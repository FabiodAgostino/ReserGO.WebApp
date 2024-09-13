using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Authentication;

namespace ReserGO.WebApp.Components.Layout
{
    public partial class MainLayout : LayoutComponentBase
    {
        private readonly IAuthenticationService _authService;

        [CascadingParameter] private Task<AuthenticationState> Authentication { get; set; }
        public bool Render { get; set; } = false;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var authstate = await Authentication;
                if(authstate != null)
                {
                    Render = true;
                }
            }
        }
    }
}
