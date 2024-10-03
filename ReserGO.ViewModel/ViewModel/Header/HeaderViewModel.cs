using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using ReserGO.DTO;
using ReserGO.Miscellaneous.Enum;
using ReserGO.Miscellaneous.Message;
using ReserGO.Miscellaneous.Model;
using ReserGO.Service.Interface;
using ReserGO.Service.Interface.Authentication;
using ReserGO.Service.Interface.Utils;
using ReserGO.Utils.Event;
using ReserGO.ViewModel.Interface.Authentication;
using ReserGO.ViewModel.Interface.Header;
using ReserGO.ViewModel.ViewModel.Authentication;
using ReserGO.ViewModel.ViewModel.Home;

namespace ReserGO.ViewModel.ViewModel.Header
{
    public class HeaderViewModel : CompleteReserGOViewModell<object, HeaderViewModel>, IHeaderViewModel
    {
        private readonly IAuthenticationService _authService;

        public HeaderViewModel(IBaseServicesReserGO<HeaderViewModel> baseService,IAuthenticationService authService) : base(baseService)
        {
            _authService = authService;
        }
        public bool IsLoggedIn { get; set; }

        public async Task OpenModal()
        {
            var modal = new GenericModalVoid(new EventCallbackFactory().Create(this, () => Refresh()));
            Aggregator.Publish<GenericModalVoid,ObjectMessage<GenericModalVoid>>(new ObjectMessage<GenericModalVoid>(modal), typeof(LoginViewModel));
        }

        public async Task Refresh()
        {
            await CheckUser();
            Aggregator.Publish<bool,ObjectMessage<bool>>(new ObjectMessage<bool>(true), typeof(HomeViewModel)); 
            OnPropertyChanged();
        }

        public async Task CheckUser() => IsLoggedIn = await _authService.IsLoggedIn();

        public async Task Logout()
        {
            IsLoading = true;
            Loading();
            await _authService.Logout();
            await Login();
            await CheckUser();
            Aggregator.Publish<bool, ObjectMessage<bool>>(new ObjectMessage<bool>(true), typeof(HomeViewModel));
            OnPropertyChanged();
            IsLoading = false;
            Loading();

        }

        public async Task Login()
        {
            IsLoading = true;
            try
            {
                var user = new DTOLoginRequest() { IsGuest = true };
                await _authService.Login(user);
            }
            catch (Exception ex)
            {
               Notification(ex.Message, NotificationColor.Error);
            }
            finally
            {
                IsFirstLoad = false;
                IsLoading = false;
                OnPropertyChanged();
            }
        }

    }
}
