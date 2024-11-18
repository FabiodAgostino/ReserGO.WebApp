using ReserGO.DTO;

namespace ReserGO.ViewModel.Interface.Resource.UpdateResource
{
    public interface IUpdateResourceViewModel : ICompleteReserGOViewModel<DTOResource>
    {
        public bool IsOpen { get; set; }
    }
}
