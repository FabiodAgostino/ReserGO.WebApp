using ReserGO.Utils.DTO.Service;

namespace ReserGO.Service.Enum
{
    public class HomeServiceType : ServiceType
    {
        public HomeServiceType(int key, string value) : base(key, value)
        {
        }

        public static readonly AuthenticationServiceType GetSettingsMenu = new AuthenticationServiceType(1, "GetSettingsMenu");

    }
}
