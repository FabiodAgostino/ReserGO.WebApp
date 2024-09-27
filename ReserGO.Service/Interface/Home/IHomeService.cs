using Refit;
using ReserGO.DTO;
using ReserGO.Utils.DTO.Service;
namespace ReserGO.Service.Interface.Home
{
    public interface IHomeService
    {
        [Get("/GetSettingsMenu")]
        Task<ServiceResponse<IEnumerable<DTOSettingMenu>>> GetSettingsMenu();
    }
}
