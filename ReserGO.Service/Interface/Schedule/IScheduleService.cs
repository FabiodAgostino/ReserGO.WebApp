using Refit;
using ReserGO.DTO;
using ReserGO.Utils.DTO.Service;

namespace ReserGO.Service.Interface.Schedule
{
    public interface IScheduleService
    {
        [Get("/GetResourcesByCompany")]
        Task<ServiceResponse<IEnumerable<DTOResource>>> GetResourcesByCompany();
        [Get("/GetFullResource")]
        Task<ServiceResponse<DTOResource>> GetFullResource(int idResource);
    }
}
