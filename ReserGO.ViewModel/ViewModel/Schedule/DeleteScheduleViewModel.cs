using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using ReserGO.DTO;
using ReserGO.Miscellaneous.Enum;
using ReserGO.Service.Interface.Schedule;
using ReserGO.Service.Interface.Utils;
using ReserGO.Utils.Event;
using ReserGO.ViewModel.Interface.Schedule;

namespace ReserGO.ViewModel.ViewModel.Schedule
{
    public class DeleteScheduleViewModel : LightReserGOViewModel<string>, IDeleteScheduleViewModel
    {
        private readonly ILogger<DeleteScheduleViewModel> logger;
        private readonly IBookingService service;
        private readonly NavigationManager _navigationManager;
        private readonly INotificationService notification;

        public DeleteScheduleViewModel(IEvent aggregator, ILogger<DeleteScheduleViewModel> logger, IBookingService service, NavigationManager navigationManager, INotificationService notification) : base(aggregator, logger)
        {
            this.logger = logger;
            this.service = service;
            _navigationManager = navigationManager;
            this.notification = notification;
        }

        public async Task DeleteSchedule(DTODeleteBooking deleteBooking)
        {
            try
            {
                IsLoading = true;
                Loading("Eliminazione della prenotazione in corso");
                var res = await service.DeleteBookingEmail(deleteBooking);
                if (res.Success)
                    notification.NotifyMessage("Eliminazione avvenuta con successo", NotificationColor.Success);
                else
                    notification.NotifyMessage(res.Message, NotificationColor.Error);   
                await Task.Delay(750);
                _navigationManager.NavigateTo("/", forceLoad: true);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
            finally
            {
                IsLoading = false;
                Loading();
            }
        }
    }
}
