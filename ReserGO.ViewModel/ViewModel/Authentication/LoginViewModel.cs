using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using ReserGO.DTO;
using ReserGO.Miscellaneous.Enum;
using ReserGO.Miscellaneous.Message;
using ReserGO.Miscellaneous.Model;
using ReserGO.Service.Interface.Authentication;
using ReserGO.Utils.Event;
using ReserGO.ViewModel.Interface.Authentication;
using ReserGO.ViewModel.ViewModel.Utils;

namespace ReserGO.ViewModel.ViewModel.Authentication
{
    public class LoginViewModel : LightReserGOViewModel<DTOLoginRequest>, ILoginViewModel
    {
        private readonly IAuthenticationService _authService;
        private readonly NavigationManager _navigationManager;

        public LoginViewModel(IEvent aggregator, ILogger<LoginViewModel> logger, IAuthenticationService authService, NavigationManager navigationManager) : base(aggregator, logger)
        {
            _authService = authService;
            _navigationManager = navigationManager;
            aggregator.Subscribe<ObjectMessage<GenericModalVoid>>(GetType(), OpenModal);
            IsLoading = false;
            IsFirstLoad = true;
        }
        public bool LoginError { get; set; } = false;
        public bool ShowGUI { get; set; }
        public bool IsLoggedIn { get; set; }
        public bool IsOpen { get; set; }
        EventCallback Callback { get; set; }
        public DTOLoginRequest User { get; set; } = new();

        public async void OpenModal(ObjectMessage<GenericModalVoid> message)
        {
            if(message.Value.Event.HasDelegate)
            {
                Callback = message.Value.Event;
                IsOpen = true;
                OnPropertyChanged();
            }
                
        }


        public async Task Login(DTOLoginRequest user = null)
        {
            IsLoading = true;
            if(ShowGUI)
                Loading();
            try
            {
                if(user == null)
                    user = new() { IsGuest = true };

                LoginError = !await _authService.Login(user);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsFirstLoad = false;
                IsLoading = false;
                if (ShowGUI)
                    Loading();
                if(Callback.HasDelegate && !LoginError)
                {
                    IsOpen = false;
                    await Callback.InvokeAsync();
                }
                OnPropertyChanged();
            }
        }

        public async Task CheckUser() => IsLoggedIn=await _authService.IsLoggedIn();

        public async Task Logout()
        {
            await _authService.Logout();
            IsLoggedIn = false;
            OnPropertyChanged();
            await Login();
            _navigationManager.NavigateTo("/", true);

        }
        private void Loading()
        {
            Aggregator.Publish<bool, ObjectMessage<bool>>(new ObjectMessage<bool>(IsLoading), typeof(LoadingSpinnerViewModel));
        }

    }
}
