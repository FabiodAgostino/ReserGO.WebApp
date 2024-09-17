using ReserGO.Miscellaneous.Model;

namespace ReserGO.ViewModel.Interface.FiltersComponent
{
    public interface IComuneComboViewModel : ICompleteReserGOViewModel<DTOComuneProvincia>
    {
        public List<DTOComuneProvincia> Comuni {  get; set; }
        public string SearchString { get; set; }
    }
}
