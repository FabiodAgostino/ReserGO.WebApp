using Microsoft.AspNetCore.Components.Authorization;
using ReserGO.DTO;
using ReserGO.Utils.DTO.Service;
using ReserGO.Utils.Service.Interface;

namespace ReserGO.Service.Interface.Authentication
{
    public interface IAuthenticationService : IClientBaseService<object>
    {
        Task<ServiceResponse<string>> Login(DTOLoginRequest loginRequest);
        Task<bool> IsLoggedIn();
        Task Logout();

    }
}
