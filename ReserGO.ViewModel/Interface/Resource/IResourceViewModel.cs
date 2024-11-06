
using ReserGO.DTO;
using ReserGO.Utils.DTO.Service;
using ReserGO.Utils.DTO.Utils;

namespace ReserGO.ViewModel.Interface.Resource
{
    public interface IResourceViewModel : ICompleteReserGOViewModel<DTOResource>
    {
        public GenericPagedList<DTOResource> Resources { get; set; }
        public GenericPagedFilter<DTOResourceFilter> Pagination { get; set; }
        Task GetResourceByCompanyFiltered();
        Task ExecuteAction(GridAction<DTOResource, DTOResourceFilter> action);
    }
}
