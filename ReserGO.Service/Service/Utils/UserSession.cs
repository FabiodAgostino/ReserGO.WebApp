using ReserGO.Service.Interface.Authentication;
using ReserGO.Service.Interface.Utils;
using ReserGO.Utils.DTO.Utils;

namespace ReserGO.Service.Service.Utils
{
    public class UserSession : IUserSession
    {
        private readonly IJwtAuthenticationStateProvider _service;

        public UserSession(IJwtAuthenticationStateProvider service)
        {
            _service = service;
        }

        public DTOUserSession User { get => _service.User; }

       

    }
}
