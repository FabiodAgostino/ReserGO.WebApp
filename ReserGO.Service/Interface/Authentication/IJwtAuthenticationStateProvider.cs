using Microsoft.AspNetCore.Components.Authorization;
using ReserGO.Utils.DTO.Utils;
using System.Security.Claims;

namespace ReserGO.Service.Interface.Authentication
{
    public interface IJwtAuthenticationStateProvider
    {
        void NotifyUserAuthentication(string token);
        IEnumerable<Claim> ParseClaimsFromJwt(string jwt);
        void NotifyUserLogout();
        Task<AuthenticationState> GetAuthenticationStateAsync();
        public DTOUserSession User { get; set; }
    }
}
