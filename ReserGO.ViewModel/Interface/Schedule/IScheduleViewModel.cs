using ReserGO.DTO;

namespace ReserGO.ViewModel.Interface.Schedule
{
    public interface IScheduleViewModel : ICompleteReserGOViewModel<DTOResource>
    {
        Task OpenModalCalendar(int idResource);
        Task GetFullResource(int IdResource);
        bool LoadingFullResource { get; set; }
    }
}
