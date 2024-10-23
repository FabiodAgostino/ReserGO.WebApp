using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using ReserGO.DTO;
using ReserGO.Miscellaneous.Model;
using ReserGO.Utils.Event;
using ReserGO.ViewModel.Interface.Schedule;

namespace ReserGO.ViewModel.ViewModel.Schedule
{
    public class ScheduleServicesViewModel : LightReserGOViewModel<DTOService>, IScheduleServicesViewModel
    {
        public ScheduleServicesViewModel(IEvent aggregator, ILogger<ScheduleServicesViewModel> logger, IJSRuntime js) : base(aggregator, logger, js)
        {
            List = new();
        }

        public List<DTOServiceSelectable> ServicesListCheckable { get; set; }

    }
}
