using Microsoft.Extensions.Logging;
using ReserGO.DTO;
using ReserGO.Service.Interface.Authentication;
using ReserGO.Utils.Event;
using ReserGO.ViewModel.Interface.Home;

namespace ReserGO.ViewModel.ViewModel.Home
{
    public class LoginViewModel : LightReserGOViewModel<DTOLoginRequest>, ILoginViewModel
    {
        private readonly IAuthenticationService _authService;

        public LoginViewModel(IEvent aggregator, ILogger<LoginViewModel> logger, IAuthenticationService authService) : base(aggregator, logger)
        {
            _authService = authService;
        }

        public async Task Login()
        {
            try
            {
                var login = new DTOLoginRequest() { Username = "danielone", Password = "123stella." };
                await _authService.Login(login);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
