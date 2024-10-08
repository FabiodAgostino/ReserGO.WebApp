using Refit;
using ReserGO.DTO;
using ReserGO.DTO.Extensions;
using ReserGO.DTO.ListAvailability;
using ReserGO.Miscellaneous.Enum;
using ReserGO.Miscellaneous.Extensions;
using ReserGO.Miscellaneous.Message;
using ReserGO.Miscellaneous.Model;
using ReserGO.Service.Interface;
using ReserGO.Service.Interface.Schedule;
using ReserGO.Utils.DTO.ExtensionMethod;
using ReserGO.Utils.DTO.Utils;
using ReserGO.ViewModel.Interface.Schedule;
using System.Collections.Generic;

namespace ReserGO.ViewModel.ViewModel.Schedule
{
    public class ModalScheduleViewModel : CompleteReserGOViewModell<DTOResource, ModalScheduleViewModel>, IModalScheduleViewModel
    {
        private readonly IScheduleService _service;
        public ModalScheduleViewModel(IBaseServicesReserGO<ModalScheduleViewModel> baseServices, IScheduleService service) : base(baseServices)
        {
            Aggregator.Subscribe<ObjectMessage<GenericModal<DTOResource>>>(GetType(), OpenModal);
            _service = service;
        }
        public bool IsOpen { get; set; }
        public Func<DateTime, bool> DayDisabled { get; set; }
        public List<DTOTimeSlot> TimeSlots { get; set; } = new();
        public DTOBooking Booking { get; set; } = new();

        public async void OpenModal(ObjectMessage<GenericModal<DTOResource>> message)
        {
            IsOpen = true;
            SelectedItem = message.Value.Data;
            Booking.Resource = SelectedItem;
            if(!UserIs(RoleConst.GUEST))
            {
                Booking.User = User.GetFromDTOSession();
            }
            await SetDayDisabled();
            await GetSlot(DateTime.Now);
            OnPropertyChanged();
        }

        public async Task SetDayDisabled()
        {
            DayDisabled = item =>
            {
                bool isSpecificDayUnavailable = SelectedItem.AvailabilityAdv.UnavailableSpecificDays.Data.Contains(item.Date);
                if (isSpecificDayUnavailable)
                    return true;

                bool isDayOfWeekUnavailable = SelectedItem.AvailabilityAdv.UnavailableByDaysOfTheWeek.Data
                    .Contains(item.DayOfWeek.DayToItalianString(), StringComparer.OrdinalIgnoreCase);
                if (isDayOfWeekUnavailable)
                    return true;
                bool oldDays = item.Date < DateTime.Now.AddDays(-1);
                if (oldDays)
                    return true;
                return false;
            };
        }

        public async Task GetSlot(DateTime day)
        {
            Booking.StartDateTime = day.Date;
            Booking.EndDateTime = day.Date;
            var slotOccupati = new List<DTOTimeSlot>();
            TimeSlots = new();
            if (SelectedItem.AvailabilityAdv.UnavailableSpecificDays.Data.Contains(day))
                return;
            if (SelectedItem.AvailabilityAdv.Unavailable)
                return;

            var firstSlot = SelectedItem.AvailabilityAdv.UnavailableRecurringTimeDays;

            if (firstSlot.Count() > 0)
            {
                var recurringSlot = firstSlot.SingleOrDefault(x => x.DayOfTheWeek.Contains(day.DayOfWeek.DayToItalianString()));
                if (recurringSlot != null)
                    slotOccupati = recurringSlot.TimeSlots;
            }

            var secondSlot = SelectedItem.AvailabilityAdv.UnavailableTimeDatesSlot;
            if (secondSlot.Count() > 0)
            {
                var timeDateSlot = secondSlot.SingleOrDefault(x => x.SpecificDate.Date == day.Date);
                if (timeDateSlot != null)
                    slotOccupati.AddRange(timeDateSlot.TimeSlots);
            }
            if (slotOccupati.Count() > 0)
            {
                slotOccupati = DTOResourceExtension.MergeTimeSlots(slotOccupati);
            }
            TimeSlots = DTOResourceExtension.GetAvailableSlots(slotOccupati, SelectedItem.DurationBooking.Value);

        }

        public async Task GetFullResource(int idResource)
        {
            try
            {
                await _service.GetFullResource(idResource);
            }
            catch (Exception ex)
            {
                Notification(ex.Message, NotificationColor.Error);
            }
        }

        public void Close()
        {
            IsOpen = false;
        }
    }
}
