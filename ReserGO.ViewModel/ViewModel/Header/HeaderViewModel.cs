using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using ReserGO.DTO;
using ReserGO.Miscellaneous.Message;
using ReserGO.Miscellaneous.Model;
using ReserGO.Service.Interface.Authentication;
using ReserGO.Utils.Event;
using ReserGO.ViewModel.Interface.Header;
using ReserGO.ViewModel.ViewModel.Home;

namespace ReserGO.ViewModel.ViewModel.Header
{
    public class HeaderViewModel : LightReserGOViewModel<object>, IHeaderViewModel
    {
        private readonly IAuthenticationService _authService;

        public HeaderViewModel(IEvent aggregator, ILogger<HeaderViewModel> logger, IAuthenticationService authService) : base(aggregator, logger)
        {
            _authService = authService;
        }
        public bool IsLoggedIn { get; set; }

        public async Task OpenModal()
        {
            var modal = new GenericModalVoid(new EventCallbackFactory().Create(this, () => Refresh()));
            Aggregator.Publish<GenericModalVoid,ObjectMessage<GenericModalVoid>>(new ObjectMessage<GenericModalVoid>(modal));
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
            await _authService.Logout();
            await Login();
            await CheckUser();
            Aggregator.Publish<bool, ObjectMessage<bool>>(new ObjectMessage<bool>(true), typeof(HomeViewModel));
            OnPropertyChanged();

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
