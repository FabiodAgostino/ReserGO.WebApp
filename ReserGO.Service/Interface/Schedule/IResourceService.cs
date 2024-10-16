using Refit;
using ReserGO.DTO;
using ReserGO.Utils.DTO.Service;

namespace ReserGO.Service.Interface.Schedule
{
    public interface IResourceService
    {
        [Get("/GetResourcesByCompany")]
        Task<ServiceResponse<IEnumerable<DTOResource>>> GetResourcesByCompany();

        [Get("/GetFullResource")]
        Task<ServiceResponse<DTOResource>> GetFullResource(int idResource, DateTime? monthDate);
        [Get("/GetDetailResource")]
        Task<ServiceResponse<DTOResource>> GetDetailResource(int idResource);
        [Post("/InsertResource")]
        Task<ServiceResponse<DTOResource>> InsertResource(DTOResource resource);
    }
}
