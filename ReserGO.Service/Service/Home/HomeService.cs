using Microsoft.Extensions.Configuration;
using ReserGO.DTO;
using ReserGO.Service.Enum;
using ReserGO.Service.Interface.Home;
using ReserGO.Utils.DTO.Service;
using ReserGO.Utils.Service.Service;

namespace ReserGO.Service.Service.Home
{
    public class HomeService : ClientBaseService<object>, IHomeService
    {
        public HomeService(HttpClient Http, IConfiguration Configuration) : base(Http, Configuration)
        {
            ExtraBaseUrl = "Utils";
        }

        public async Task<ServiceResponse<IEnumerable<DTOSettingMenu>>> GetSettingsMenu()
        {
            return await RequestGet<IEnumerable<DTOSettingMenu>>(HomeServiceType.GetSettingsMenu, "");
        }

    }
}
