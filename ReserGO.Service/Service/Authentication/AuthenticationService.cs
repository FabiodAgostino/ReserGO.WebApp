
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

        public async Task<bool> Login(DTOLoginRequest loginRequest)
        {
            var token = await _sessionStorage.GetItemAsync<string>("authToken");
            if (String.IsNullOrEmpty(token))
            {
                var response = await PostItem<string>(loginRequest, AuthenticationServiceType.Login);

                if (response.Success && response.Data!=null)
                {
                    await _sessionStorage.SetItemAsync("authToken", response.Data);
                    _authProvider.NotifyUserAuthentication(response.Data);
                }
                return response.Success;
            }
            return false;
        }
    }
}
