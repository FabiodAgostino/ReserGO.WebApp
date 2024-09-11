using Microsoft.Extensions.Logging;
using ReserGO.DTO;
using ReserGO.Miscellaneous.Enum;
using ReserGO.Miscellaneous.Message;
using ReserGO.Service.Interface.Authentication;
using ReserGO.Utils.Event;
using ReserGO.ViewModel.Interface.Authentication;
using ReserGO.ViewModel.ViewModel.Utils;

namespace ReserGO.ViewModel.ViewModel.Authentication
{
    public class LoginViewModel : LightReserGOViewModel<DTOLoginRequest>, ILoginViewModel
    {
        private readonly IAuthenticationService _authService;

        public LoginViewModel(IEvent aggregator, ILogger<LoginViewModel> logger, IAuthenticationService authService) : base(aggregator, logger)
        {
            _authService = authService;
            IsLoading = false;
        }
        public bool LoginError { get; set; } = false;
        public async Task Login(DTOLoginRequest user)
        {
            IsLoading = true;
            Loading();
            try
            {
                LoginError = !await _authService.Login(user);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsLoading = false;
                Loading();
                OnPropertyChanged();
            }

        }


        private void Loading()
        {
            Aggregator.Publish<bool, ObjectMessage<bool>>(new ObjectMessage<bool>(IsLoading), typeof(LoadingSpinnerViewModel));
        }
    }
}
