using Refit;
using ReserGO.DTO;
using ReserGO.Utils.DTO.Service;

namespace ReserGO.Service.Interface.Service
{
    public interface IServiceService
    {
        [Post("/InsertService")]
        Task<ServiceResponse<DTOService>> InsertService(DTOService service);
        [Put("/UpdateService")]
        Task<ServiceResponse<DTOService>> UpdateService(DTOService service);
        [Delete("/DeleteService")]
        Task<ServiceResponse<bool>> DeleteService(int idService);
        [Post("/GetServices")]
        Task<ServiceResponse<GenericPagedList<DTOService>>> GetServices(GenericPagedFilter<DTOServiceFilter> data);
    }
}
