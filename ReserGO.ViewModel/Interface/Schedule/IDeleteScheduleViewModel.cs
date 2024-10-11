using ReserGO.DTO;

namespace ReserGO.ViewModel.Interface.Schedule
{
    public interface IDeleteScheduleViewModel : ILightReserGOViewModel<string>
    {
        Task DeleteSchedule(DTODeleteBooking tokenIdBooking);
    }
}
