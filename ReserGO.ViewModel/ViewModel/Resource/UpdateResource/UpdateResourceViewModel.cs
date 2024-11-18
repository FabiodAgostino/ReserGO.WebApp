using ReserGO.DTO;
using ReserGO.Service.Interface;
using ReserGO.ViewModel.Interface.Resource.UpdateResource;

namespace ReserGO.ViewModel.ViewModel.Resource.UpdateResource
{
    public class UpdateResourceViewModel : CompleteReserGOViewModell<DTOResource, UpdateResourceViewModel>, IUpdateResourceViewModel
    {
        public UpdateResourceViewModel(IBaseServicesReserGO<UpdateResourceViewModel> baseServices) : base(baseServices)
        {
        }

        public bool IsOpen { get; set; }
    }
}
