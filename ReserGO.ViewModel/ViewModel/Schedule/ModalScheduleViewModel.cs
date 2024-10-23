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
        private readonly IBookingService _bookService;
        private readonly IResourceService _service;

        public ModalScheduleViewModel(IBaseServicesReserGO<ModalScheduleViewModel> baseServices, IBookingService bookService, IResourceService service) : base(baseServices)
        {
            Aggregator.Subscribe<ObjectMessage<GenericModal<DTOResource>>>(GetType(), OpenModal);
            _bookService = bookService;
            _service = service;
            IsFirstLoad = false;
        }
        public bool IsOpen { get; set; }
        public Func<DateTime, bool> DayDisabled { get; set; }
        public List<DTOTimeSlot> TimeSlots { get; set; } = new();
        public DTOBooking Booking { get; set; } = new();
        public int SelectedIndex { get; set; } = 0;
        public bool SlotLoading { get; set; }

        public async void OpenModal(ObjectMessage<GenericModal<DTOResource>> message)
        {
            IsFirstLoad = true;
            SelectedIndex = 0;
            Booking = new();
            IsOpen = true;
            SelectedItem = message.Value.Data;
            Booking.Resource = (DTOResource)SelectedItem.Clone();
            Booking.Services = new();
            TimeSlots = new();

            if (!UserIs(RoleConst.GUEST))
            {
                Booking.User = User.GetFromDTOSession();
            }
            OnPropertyChanged();
            await SetDayDisabled(true);
        }

        public async Task SetDayDisabled(bool firstLoad=false)
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
                if (!TimeSlots.Any() && item.Date == DateTime.Now.Date && !firstLoad)
                    return true;
                return false;
            };

        }

        public async Task GetSlot(DateTime day)
        {
            if (IsFirstLoad)
                IsFirstLoad = false;
            SlotLoading = true;
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
                    slotOccupati.AddRange(recurringSlot.TimeSlots);
            }

            var secondSlot = SelectedItem.AvailabilityAdv.UnavailableTimeDatesSlot;
            if (secondSlot.Count() > 0)
            {
                var timeDateSlot = secondSlot.SingleOrDefault(x => x.SpecificDate.Date == day.Date);
                if (timeDateSlot != null)
                    slotOccupati.AddRange(timeDateSlot.TimeSlots);
            }

            var timeSLotsBooking = await GetBookingSlot(day);
            if(timeSLotsBooking.Count() > 0)
            {
                slotOccupati.AddRange(timeSLotsBooking);
            }

            if (slotOccupati.Count() > 0)
            {
                slotOccupati = DTOResourceExtension.MergeTimeSlots(slotOccupati);
            }
            var duration = Booking.Services
             .Sum(s => s.Duration);
            duration = duration == 0 ? SelectedItem.DurationBooking.HasValue ? SelectedItem.DurationBooking.Value: 25 : duration;

            TimeSlots = DTOResourceExtension.GetAvailableSlots(slotOccupati, duration, day);
            SlotLoading = false;
        }

        public async Task<List<DTOTimeSlot>> GetBookingSlot(DateTime day)
        {
            var list = new List<DTOTimeSlot>();
            try
            {
                var r = await _bookService.GetBookingSlotUnavailableFromResource(SelectedItem.Id.Value, day);
                list = r.Data.ToList();
            }
            catch (Exception ex)
            {
            }
            return list;

        }

        public async Task InsertBooking(bool submit)
        {
            if(submit)
            {
                try
                {
                    var bookingToInsert = (DTOBooking)Booking.Clone();
                    bookingToInsert.ResourceId= SelectedItem.Id.Value;
                    bookingToInsert.Resource.AvailabilityAdv = null;
                    bookingToInsert.Resource.ResourcesAvailability = null;

                    var result = await _bookService.InsertBooking(bookingToInsert);
                    if(result.Success)
                    {
                        Notification("Prenotazione inserita correttamente", NotificationColor.Success);
                        IsOpen = false;
                    }
                    else
                        Notification(result.Message, NotificationColor.Warning);

                }
                catch (Exception ex)
                {
                    Notification(ex.Message, NotificationColor.Error);
                }
            }
            else
            {
                Notification("Prenotazione non salvata!", NotificationColor.Warning);
                IsOpen = false;
            }
        }

        public void Close()
        {
            IsOpen = false;
        }
    }
}
