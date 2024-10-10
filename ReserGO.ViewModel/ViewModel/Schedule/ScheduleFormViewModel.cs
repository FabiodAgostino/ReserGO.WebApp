using Microsoft.Extensions.Logging;
using ReserGO.DTO;
using ReserGO.Utils.Event;
using ReserGO.ViewModel.Interface.Schedule;

namespace ReserGO.ViewModel.ViewModel.Schedule
{
    public class ScheduleFormViewModel : LightReserGOViewModel<DTOUserLight>, IScheduleFormViewModel
    {
        public ScheduleFormViewModel(IEvent aggregator, ILogger<ScheduleFormViewModel> logger) : base(aggregator, logger)
        {
            SelectedItem = new();
        }
    }
}
