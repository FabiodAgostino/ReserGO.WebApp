using ReserGO.Miscellaneous.Model;
using ReserGO.Service.Interface;
using ReserGO.Service.Interface.Utils;
using ReserGO.ViewModel.Interface.FiltersComponent;

namespace ReserGO.ViewModel.ViewModel.FiltersComponent
{
    public class NazioneViewModel : CompleteReserGOViewModell<DTONazione, NazioneViewModel>, INazioneViewModel
    {
        private INazioneService _service;
        public NazioneViewModel(IBaseServicesReserGO<NazioneViewModel> baseService, INazioneService service) : base(baseService)
        {
            this._service = service;
            IsLoading = false;
        }

        public List<DTONazione> Nazioni { get ; set ; }
        public string SearchString { get ; set ; }

        public override async Task Refresh()
        {
            var comboList = await _service.GetNazioneAsync(SearchString);
            List = comboList.Select(x => new DTONazione(x));
        }



    }
}
