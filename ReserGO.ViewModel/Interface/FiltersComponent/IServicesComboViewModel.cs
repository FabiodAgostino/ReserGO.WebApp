using ReserGO.DTO;

namespace ReserGO.ViewModel.Interface.FiltersComponent
{
    public interface IServicesComboViewModel : ICompleteReserGOViewModel<DTOService>
    {
        public IEnumerable<DTOService> ServicesChecked { get; set; }
        Task GetServices();
    }
}
