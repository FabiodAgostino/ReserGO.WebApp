using Microsoft.Extensions.Logging;
using ReserGO.Miscellaneous.Model;
using ReserGO.Utils.Event;
using ReserGO.ViewModel.Interface.FiltersComponent;

namespace ReserGO.ViewModel.ViewModel.FiltersComponent
{
    public class DayOfWeekViewModel : LightReserGOViewModel<DTODayOfWeekWizard>, IDayOfWeekViewModel
    {
        public DayOfWeekViewModel(IEvent aggregator, ILogger<DayOfWeekViewModel> logger) : base(aggregator, logger)
        {
            List = new();
        }
    }
}
