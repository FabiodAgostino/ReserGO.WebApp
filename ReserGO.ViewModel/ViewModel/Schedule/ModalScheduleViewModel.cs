using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using Microsoft.VisualBasic;
using ReserGO.Miscellaneous.Message;
using ReserGO.Miscellaneous.Model;
using ReserGO.Service.Interface.Utils;
using ReserGO.Utils.Event;
using ReserGO.ViewModel.Interface.Schedule;

namespace ReserGO.ViewModel.ViewModel.Schedule
{
    public class ModalScheduleViewModel : CompleteReserGOViewModell<GenericModal<int>>, IModalScheduleViewModel
    {
        public ModalScheduleViewModel(IEvent aggregator, ILogger<ModalScheduleViewModel> logger, 
            INotificationService notificationService, IUserSession userSession, IJSRuntime js) : base(aggregator, logger, notificationService, userSession, js)
        {
            aggregator.Subscribe<ObjectMessage<GenericModal<int>>>(GetType(), OpenModal);
        }
        public bool IsOpen { get; set; }

        public async void OpenModal(ObjectMessage<GenericModal<int>> message)
        {
            IsOpen = true;
            OnPropertyChanged();
        }

        public void Close()
        {
            IsOpen = false;
        }
    }
}
