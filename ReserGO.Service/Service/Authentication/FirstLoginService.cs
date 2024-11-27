using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using ReserGO.DTO;
using ReserGO.Service.Interface.Authentication;
using ReserGO.Utils.DTO.Service;

namespace ReserGO.Service.Service.Authentication
{
    public class FirstLoginService : IFirstLoginService
    {
        private readonly ISessionStorageService _sessionStorage;
        private readonly ILoginService _loginService;

        public FirstLoginService( ISessionStorageService sessionStorage,
            ILoginService loginService)
        {
            _sessionStorage = sessionStorage;
            _loginService = loginService;
        }

        public async Task<string> Login()
        {
            try
            {
                var response = await _loginService.Login(new DTOLoginRequest() { IsGuest = true });
                if (response.Success && response.Data != null)
                {
                    await _sessionStorage.SetItemAsync("authToken", response.Data);
                    return response.Data;
                }
                else
                    throw new Exception();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
