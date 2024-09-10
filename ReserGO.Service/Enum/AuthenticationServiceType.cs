using ReserGO.Utils.DTO.Service;

namespace ReserGO.Service.Enum
{
    public class AuthenticationServiceType : ServiceType
    {
        public static readonly AuthenticationServiceType Login = new AuthenticationServiceType(1, "Login");

        public AuthenticationServiceType(int key, string value) : base(key, value)
        {
        }
    }
}
