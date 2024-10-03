using ReserGO.Miscellaneous.Model;

namespace ReserGO.ViewModel.Interface.FiltersComponent
{
    public interface INazioneViewModel : ICompleteReserGOViewModel<DTONazione>
    {
        public string SearchString { get; set; }
    }
}
