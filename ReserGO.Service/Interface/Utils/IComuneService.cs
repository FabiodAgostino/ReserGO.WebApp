using ReserGO.Miscellaneous.Model;
using static ReserGO.Service.Service.Utils.ComuneService;

namespace ReserGO.Service.Interface.Utils
{
    public interface IComuneService
    {
        Task<List<DTOComuneProvincia>> GetComuniAsync(string nome, int page = 1, int pageSize = 100);
    }
}
