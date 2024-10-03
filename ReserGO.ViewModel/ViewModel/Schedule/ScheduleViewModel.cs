using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using ReserGO.DTO;
using ReserGO.Miscellaneous.Enum;
using ReserGO.Miscellaneous.Message;
using ReserGO.Miscellaneous.Model;
using ReserGO.Service.Interface.Schedule;
using ReserGO.Service.Interface.Utils;
using ReserGO.Utils.Event;
using ReserGO.ViewModel.Interface.Schedule;

namespace ReserGO.ViewModel.ViewModel.Schedule
{
    internal class ScheduleViewModel : CompleteReserGOViewModell<IEnumerable<DTOResource>>, IScheduleViewModel
    {
        private readonly IScheduleService _service;

        public ScheduleViewModel(IEvent aggregator, ILogger<ScheduleViewModel> logger, INotificationService notificationService, IUserSession userSession, IJSRuntime js, IScheduleService service) : base(aggregator, logger, notificationService, userSession, js)
        {
            _service = service;
        }
        public async Task OpenModalCalendar(int idResource)
        {
            var modal = new GenericModal<int>("", idResource);
            Aggregator.Publish<GenericModal<int>, ObjectMessage<GenericModal<int>>>(new ObjectMessage<GenericModal<int>>(modal), typeof(ModalScheduleViewModel));
        }

        public override async Task Refresh()
        {
            try
            {
                IsLoading = true;
                var res = await _service.GetResourcesByCompany();
                if(res.Success)
                    List = res.Data;
                else
                    Notification(res.Message, NotificationColor.Warning);
            }catch(Exception ex)
            {
                Notification(ex.Message, NotificationColor.Error);
            }
            finally
            {
                IsLoading=false;
            }
        }
    }
}
