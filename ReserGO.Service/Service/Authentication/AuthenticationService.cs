
using Blazored.SessionStorage;
using Microsoft.Extensions.Configuration;
using ReserGO.DTO;
using ReserGO.Service.Enum;
using ReserGO.Service.Interface.Authentication;
using ReserGO.Utils.DTO.Service;
using ReserGO.Utils.Service.Service;

namespace ReserGO.Service.Service.Authentication
{
    public class AuthenticationService : ClientBaseService<object>, IAuthenticationService
    {
        private readonly ISessionStorageService _sessionStorage;
        private readonly IJwtAuthenticationStateProvider _authProvider;

        public AuthenticationService(HttpClient Http, IConfiguration Configuration, ISessionStorageService sessionStorage, IJwtAuthenticationStateProvider authProvider)
            : base(Http, Configuration)
        {
            ExtraBaseUrl = "Auth";
            _sessionStorage = sessionStorage;
            _authProvider = authProvider;
        }



        public async Task Logout()
        {
            await _sessionStorage.RemoveItemAsync("authToken");
            _authProvider.NotifyUserLogout();
        }
        public async Task<ServiceResponse<string>> Login(DTOLoginRequest loginRequest)
        {
            var token = await _sessionStorage.GetItemAsync<string>("authToken");
            if (String.IsNullOrEmpty(token) || _authProvider.User.Username=="system")
            {
                if (_authProvider.User!=null && _authProvider.User.Username == "system")
                    await Logout();
                var response = await PostItem<string>(loginRequest, AuthenticationServiceType.Login);

                if (response.Success && response.Data!=null)
                {
                    await _sessionStorage.SetItemAsync("authToken", response.Data);
                    _authProvider.NotifyUserAuthentication(response.Data);
                    await _authProvider.GetAuthenticationStateAsync();
                }
                return response;
            }
            return new ServiceResponse<string>();
        }

        public async Task<ServiceResponse<string>> RegistrationConfirm(string username) => await RequestGet<string>(AuthenticationServiceType.ConfirmUsername, $"username={username}");

        public async Task<bool> IsLoggedIn()
        {
            var token = await _sessionStorage.GetItemAsync<string>("authToken");
            if (String.IsNullOrEmpty(token))
                return false;
            await _authProvider.GetAuthenticationStateAsync();

            if (_authProvider.User != null && _authProvider.User.Username == "system")
                return false;
            return true;
        }

    }
}
