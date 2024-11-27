using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using ReserGO.DTO;
using ReserGO.Miscellaneous.Enum;
using ReserGO.Service.Interface.Schedule;
using ReserGO.Service.Interface.Service;
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
        private readonly ITranslateService _t;

        public DeleteScheduleViewModel(IEvent aggregator, ILogger<DeleteScheduleViewModel> logger, ITranslateService t,
            IBookingService service, NavigationManager navigationManager, INotificationService notification, IJSRuntime js) : base(aggregator, logger, js)
        {
            this.logger = logger;
            this.service = service;
            _navigationManager = navigationManager;
            _t = t;
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
                    await notification.PushToList(_t.Words["Eliminazione avvenuta con successo"], NotificationColor.Success,null, true);
                else
                    await notification.PushToList(res.Message, NotificationColor.Error, null, true);   
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
