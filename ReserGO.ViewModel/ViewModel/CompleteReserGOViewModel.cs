using Microsoft.Extensions.Logging;
using ReserGO.Miscellaneous.Enum;
using ReserGO.Service.Interface.Utils;
using ReserGO.Utils.Event;
using ReserGO.ViewModel.Interface;

namespace ReserGO.ViewModel.ViewModel
{
    public class CompleteReserGOViewModell<TModel> : LightReserGOViewModel<TModel>, ICompleteReserGOViewModel<TModel> where TModel : class
    {
        private readonly INotificationService _notificationService;

        public CompleteReserGOViewModell(IEvent aggregator, ILogger logger, INotificationService notificationService) : base(aggregator, logger)
        {
            _notificationService = notificationService;
        }

        public virtual void Notification(string text, NotificationColor color = NotificationColor.Info, string position = null)
        {
            _notificationService.NotifyMessage(text, color, position);
        }
    }
}
