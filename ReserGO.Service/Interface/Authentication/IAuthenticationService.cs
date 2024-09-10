using ReserGO.DTO;
using ReserGO.Utils.Service.Interface;

namespace ReserGO.Service.Interface.Authentication
{
    public interface IAuthenticationService : IClientBaseService<object>
    {
        Task<bool> Login(DTOLoginRequest loginRequest);
    }
}
