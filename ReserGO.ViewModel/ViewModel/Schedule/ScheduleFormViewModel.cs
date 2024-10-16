using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using ReserGO.DTO;
using ReserGO.Utils.Event;
using ReserGO.ViewModel.Interface.Schedule;

namespace ReserGO.ViewModel.ViewModel.Schedule
{
    public class ScheduleFormViewModel : LightReserGOViewModel<DTOUserLight>, IScheduleFormViewModel
    {
        public ScheduleFormViewModel(IEvent aggregator, ILogger<ScheduleFormViewModel> logger, IJSRuntime js) : base(aggregator, logger, js)
        {
            SelectedItem = new();
        }
    }
}
