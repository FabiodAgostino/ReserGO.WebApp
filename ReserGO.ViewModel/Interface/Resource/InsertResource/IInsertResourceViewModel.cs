using ReserGO.DTO;

namespace ReserGO.ViewModel.Interface.Resource.InsertResource
{
    public interface IInsertResourceViewModel : ICompleteReserGOViewModel<DTOResource>
    {
        public int SelectedIndex { get; set; }
    }
}
