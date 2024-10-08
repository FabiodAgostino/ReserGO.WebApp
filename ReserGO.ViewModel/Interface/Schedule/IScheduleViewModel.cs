using ReserGO.DTO;

namespace ReserGO.ViewModel.Interface.Schedule
{
    public interface IScheduleViewModel : ICompleteReserGOViewModel<DTOResource>
    {
        Task OpenModalCalendar();
        Task GetFullResource(int IdResource);
        bool LoadingFullResource { get; set; }
    }
}
