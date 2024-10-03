using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using ReserGO.Miscellaneous.Model;
using ReserGO.Service.Interface.Utils;
using ReserGO.Utils.Event;
using ReserGO.ViewModel.Interface.FiltersComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserGO.ViewModel.ViewModel.FiltersComponent
{
    public class NazioneViewModel : CompleteReserGOViewModell<DTONazione>, INazioneViewModel
    {
        private INazioneService _service;
        public NazioneViewModel(IEvent aggregator, ILogger<NazioneViewModel> logger, INotificationService notificationService, IUserSession userSession, IJSRuntime js, INazioneService service) : base(aggregator, logger, notificationService, userSession, js)
        {
            this._service = service;
            IsLoading = false;
        }

        public List<DTONazione> Nazioni { get ; set ; }
        public string SearchString { get ; set ; }

        public override async Task Refresh()
        {
            var n = await _service.GetNazioneAsync(SearchString);
        }



    }
}
