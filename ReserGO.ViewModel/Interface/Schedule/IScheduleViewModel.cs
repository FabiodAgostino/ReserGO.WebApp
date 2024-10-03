using ReserGO.DTO;

namespace ReserGO.ViewModel.Interface.Schedule
{
    public interface IScheduleViewModel : ICompleteReserGOViewModel<IEnumerable<DTOResource>>
    {
        Task OpenModalCalendar(int idResource);
    }
}
