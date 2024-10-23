using ReserGO.DTO;
using ReserGO.Miscellaneous.Model;

namespace ReserGO.ViewModel.Interface.Schedule
{
    public interface IScheduleServicesViewModel : ILightReserGOViewModel<DTOService>
    {
        public List<DTOServiceSelectable> ServicesListCheckable { get; set; }
    }
}
