using ReserGO.DTO;
using ReserGO.Utils.DTO.Service;
using ReserGO.Utils.Service.Interface;

namespace ReserGO.Service.Interface.Home
{
    public interface IHomeService : IClientBaseService<object>
    {
        Task<ServiceResponse<IEnumerable<DTOSettingMenu>>> GetSettingsMenu();
    }
}
