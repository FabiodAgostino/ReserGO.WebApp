using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using ReserGO.DTO;
using ReserGO.Miscellaneous.Enum;
using ReserGO.Miscellaneous.Message;
using ReserGO.Miscellaneous.Model;
using ReserGO.Service.Interface;
using ReserGO.Service.Interface.Authentication;
using ReserGO.ViewModel.Interface.Authentication;
using ReserGO.ViewModel.ViewModel.Register;

namespace ReserGO.ViewModel.ViewModel.Authentication
{
    public class LoginViewModel : CompleteReserGOViewModell<DTOLoginRequest, LoginViewModel>, ILoginViewModel
    {
        private readonly IAuthenticationService _authService;
        private readonly NavigationManager _navigationManager;

        public LoginViewModel(IBaseServicesReserGO<LoginViewModel> baseServices, IAuthenticationService authService, NavigationManager navigationManager)
           : base(baseServices)
        {
            _authService = authService;
            _navigationManager = navigationManager;
            Aggregator.Subscribe<ObjectMessage<GenericModalVoid>>(GetType(), OpenModal);
            IsLoading = false;
            IsFirstLoad = true;
            SelectedItem = new();
        }

        public string? LoginError { get; set; }
        public bool IsOpen { get; set; }
        EventCallback Callback { get; set; }

        public async void OpenModal(ObjectMessage<GenericModalVoid> message)
        {
            if(message.Value.Event.HasDelegate)
            {
                LoginError = String.Empty;
                Callback = message.Value.Event;
                IsOpen = true;
                SelectedItem = new();
                OnPropertyChanged();
            }
                
        }
        public async Task FirstLogin()
        {
            try
            {
                var user = new DTOLoginRequest() { IsGuest = true };
                var data = await _authService.Login(user);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message);
            }
            finally
            {
                OnPropertyChanged();
            }
        }

        public async Task Login(DTOLoginRequest user = null)
        {
            IsLoading = true;
            Loading();
            try
            {
                if (user == null)
                    user = new() { IsGuest = true };
                else
                {
                    if (user.Password == null && !(user.Password.Length > 0))
                    {
                        LoginError = "Inserisci una password";
                        return;
                    }
                }
                var data = await _authService.Login(user);
                if (!data.Success)
                    LoginError = data.Message;
                else
                    LoginError = String.Empty;

            }
            catch (Exception ex)
            {
                Notification(ex.Message, NotificationColor.Error);
            }
            finally
            {
                IsFirstLoad = false;
                IsLoading = false;
                Loading();
                if (Callback.HasDelegate && String.IsNullOrEmpty(LoginError))
                {
                    IsOpen = false;
                    await Callback.InvokeAsync();
                }
                OnPropertyChanged();
            }
        }



        public async Task OpenModal()
        {
            Aggregator.Publish<bool, ObjectMessage<bool>>(new ObjectMessage<bool>(true),typeof(RegisterViewModel));
        }
    }
}
