using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using ReserGO.Service.Service.Authentication;
using ReserGO.Service.Interface.Authentication;
using ReserGO.Utils.Event;
using ReserGO.Miscellaneous.Message;
using ReserGO.Miscellaneous.Model;
using ReserGO.ViewModel.ViewModel.Utils;
using ReserGO.Service.Service.Utils;
using ReserGO.DTO;
using ReserGO.Service.Interface.Service;

namespace ReserGO.WebApp.Components.Layout
{
    public partial class MainLayout : LayoutComponentBase
    {
        [CascadingParameter] private Task<AuthenticationState> Authentication { get; set; }
        [Inject] NavigationManager Navigation { get; set; }
        [Inject] IJwtAuthenticationStateProvider provider { get; set; }
        [Inject] IAuthenticationService authenticationService { get; set; }
        [Inject] IEvent eventAggregator { get;set; }
        [Inject] ITranslateService translateService { get; set; }

        public bool Render = false;
        public bool IsRedirect = false;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                eventAggregator.Publish<LoadingSpinner, ObjectMessage<LoadingSpinner>>(new ObjectMessage<LoadingSpinner>(new LoadingSpinner(true, "Loading...", true)), typeof(LoadingSpinnerViewModel));
                await translateService.Initialize(false);

                var currentUrl = Navigation.Uri;
                IsRedirect = currentUrl.Count(c => c == '/') > 3;

                if (!IsRedirect)
                {
                    var authstate = await Authentication;
                    if (authstate != null)
                    {
                        Render = true;
                        await provider.GetAuthenticationStateAsync();
                        StateHasChanged();
                    }
                }
            }
        }

        protected override async Task OnInitializedAsync()
        {
        }

    }
}
