using Microsoft.AspNetCore.Components;
using ReserGO.DTO;
using ReserGO.DTO.ListAvailability;
using ReserGO.Miscellaneous.Model;

namespace ReserGO.ViewModel.Interface.Schedule
{
    public interface IModalScheduleViewModel : ICompleteReserGOViewModel<DTOResource>
    {
        public bool IsOpen { get; set; }
        void Close();
        Func<DateTime, bool> DayDisabled { get; set; }
        Task GetSlot(DateTime day);
        List<DTOTimeSlot> TimeSlots { get; set; }
        DTOBooking Booking { get; set; }
        Task InsertBooking(bool submit);
        public int SelectedIndex { get; set; }
        public bool SlotLoading { get; set; }
    }
}
