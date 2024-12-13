using Refit;
using ReserGO.DTO;
using ReserGO.Utils.DTO.Service;

namespace ReserGO.Service.Interface.Schedule
{
    public interface IResourceService
    {
        [Get("/GetResourcesByCompany")]
        Task<ServiceResponse<IEnumerable<DTOResource>>> GetResourcesByCompany();
        [Post("/GetResourcesByCompanyFiltered")]
        Task<ServiceResponse<GenericPagedList<DTOResource>>> GetResourceByCompanyFiltered(GenericPagedFilter<DTOResourceFilter> data);
        [Get("/GetFullResource")]
        Task<ServiceResponse<DTOResource>> GetFullResource(int idResource, DateTime? monthDate);
        [Get("/GetDetailResource")]
        Task<ServiceResponse<DTOResource>> GetDetailResource(int idResource);
        [Post("/InsertResource")]
        Task<ServiceResponse<DTOResource>> InsertResource(DTOResource resource);
        [Delete("/DeleteResource")]
        Task<ServiceResponse<bool>> DeleteResource(int idResource);
        [Put("/UpdateResource")]
        Task<ServiceResponse<DTOResource>> UpdateResource(DTOResource resource);
    }
}
