using ReserGO.DTO;
using ReserGO.Utils.DTO.Service;
using ReserGO.Utils.DTO.Utils;

namespace ReserGO.ViewModel.Interface.Service
{
    public interface IServiceViewModel : ICompleteReserGOViewModel<DTOService>
    {
        public GenericPagedList<DTOService> Services { get; set; }
        public GenericPagedFilter<DTOServiceFilter> Pagination { get; set; }
        Task ExecuteAction(GridAction<DTOService, DTOServiceFilter> action);
        Task GetServices();
    }
}
