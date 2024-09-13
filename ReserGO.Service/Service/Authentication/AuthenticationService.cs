
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using ReserGO.DTO;
using ReserGO.Service.Enum;
using ReserGO.Service.Interface.Authentication;
using ReserGO.Utils.DTO.Service;
using ReserGO.Utils.Service.Service;
using System.Security.Claims;

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
            bool isGuest = false;
            var token = await _sessionStorage.GetItemAsync<string>("authToken");
            if(!string.IsNullOrEmpty(token))
            {
                var claims=_authProvider.ParseClaimsFromJwt(token);
                isGuest = IsGuest(claims);
            }
            if (String.IsNullOrEmpty(token) || isGuest)
            {
                if (isGuest)
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

        private bool IsGuest(IEnumerable<Claim> claims)
        {
            var username = claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            return username == "system";
        }

        public async Task<bool> IsLoggedIn()
        {
            var token = await _sessionStorage.GetItemAsync<string>("authToken");
            if (String.IsNullOrEmpty(token))
                return false;
            var claims = _authProvider.ParseClaimsFromJwt(token);
            if (IsGuest(claims))
                return false;
            return true;
        }

        public async Task<AuthenticationState> GetAuthenticationStateAsync() => await _authProvider.GetAuthenticationStateAsync();

    }
}
