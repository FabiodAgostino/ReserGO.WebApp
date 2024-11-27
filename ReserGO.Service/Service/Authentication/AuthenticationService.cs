
using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using ReserGO.DTO;
using ReserGO.Service.Interface.Authentication;
using ReserGO.Utils.DTO.Service;
using System.Security.Claims;

namespace ReserGO.Service.Service.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ISessionStorageService _sessionStorage;
        private readonly IJwtAuthenticationStateProvider _authProvider;
        private readonly ILoginService _loginService;
        private readonly ILocalStorageService _localStorageService;
        private readonly NavigationManager _navigationManager;

        public AuthenticationService(IConfiguration Configuration, ISessionStorageService sessionStorage, IJwtAuthenticationStateProvider authProvider,
            ILoginService loginService, ILocalStorageService localStorageService, NavigationManager navigationManager)
        {
            _sessionStorage = sessionStorage;
            _authProvider = authProvider;
            _loginService = loginService;
            _localStorageService = localStorageService;
            _navigationManager = navigationManager;
        }



        public async Task Logout()
        {
            await _sessionStorage.RemoveItemAsync("authToken");
            await _localStorageService.RemoveItemAsync("authToken");
            _authProvider.NotifyUserLogout();
        }
        public async Task<ServiceResponse<string>> Login(DTOLoginRequest loginRequest)
        {
            var isLoggedInUser = await _localStorageService.GetItemAsync<string>("authToken");

            if (String.IsNullOrEmpty(isLoggedInUser))
            {
                var token = await _sessionStorage.GetItemAsync<string>("authToken");
                if (String.IsNullOrEmpty(token) || _authProvider.User.Username == "system")
                {
                    if (_authProvider.User != null && _authProvider.User.Username == "system")
                        await Logout();

                    var response = await _loginService.Login(loginRequest);
                    if (response.Success && response.Data != null)
                    {
                        await _sessionStorage.SetItemAsync("authToken", response.Data);
                        if (!loginRequest.IsGuest)
                            await _localStorageService.SetItemAsync("authToken", response.Data);
                        _authProvider.NotifyUserAuthentication(response.Data);
                        await _authProvider.GetAuthenticationStateAsync();
                    }
                    return response;
                }

            }
            else
            {
                await _sessionStorage.SetItemAsync("authToken", isLoggedInUser);
                _authProvider.NotifyUserAuthentication(isLoggedInUser);
                await _authProvider.GetAuthenticationStateAsync();
            }

            return new ServiceResponse<string>();
        }

        public async Task<bool> IsLoggedIn()
        {
            bool loggedIn = false;
            var token = await _localStorageService.GetItemAsync<string>("authToken");
            if (!String.IsNullOrEmpty(token))
                loggedIn = true;
            var sessionToken = await _sessionStorage.GetItemAsync<string>("authToken");
            if (!String.IsNullOrEmpty(sessionToken) && String.IsNullOrEmpty(token) && !String.IsNullOrEmpty(_authProvider.User.FirstName))
            {
                await _sessionStorage.RemoveItemAsync("authToken");
                await _localStorageService.RemoveItemAsync("authToken");
                await Login(new DTOLoginRequest() { IsGuest = true });
            }
            await _authProvider.GetAuthenticationStateAsync();

            if (_authProvider.User != null && _authProvider.User.Username == "system")
                loggedIn = false;

            return loggedIn;
        }

    }
}
