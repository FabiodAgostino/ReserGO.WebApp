using Blazored.SessionStorage;
using MudBlazor;
using ReserGO.Miscellaneous.Enum;
using ReserGO.Miscellaneous.Model;
using ReserGO.Service.Interface.Utils;

namespace ReserGO.Service.Service.Utils
{
    public class NotificationService : INotificationService
    {
        private readonly ISnackbar _service;
        private readonly ISessionStorageService _sessionStorage;
        public NotificationService(ISnackbar service, ISessionStorageService sessionStorage)
        {
            _service = service;
            _sessionStorage = sessionStorage;
        }


        public void NotifyMessage(string text, NotificationColor color =NotificationColor.Info, string position = null, bool block=false)
        {
            _service.Clear();

            var severity = (Severity)color;

            if (position == null)
                position = Defaults.Classes.Position.TopRight;

            _service.Configuration.PositionClass = position;
            _service.Configuration.VisibleStateDuration = 1;
            if (block)
                _service.Configuration.VisibleStateDuration = int.MaxValue;

            _service.Add(text, severity);
        }

        public async Task PushToList(string text, NotificationColor color = NotificationColor.Info, string position = null, bool block = false)
        {
            var notifications = await _sessionStorage.GetItemAsync<List<DTONotification>>("notification");
            if (notifications == null)
                notifications = new();

            if (notifications != null)
                notifications.Add(new DTONotification(text, color, position, block));

            await _sessionStorage.SetItemAsync("notification", notifications);
        }

        public async Task ReadNotification()
        {
            var notifications = await _sessionStorage.GetItemAsync<List<DTONotification>>("notification");
            if(notifications!=null)
            {
                notifications.ForEach(notification => NotifyMessage(notification.Text, notification.Color, notification.Position, notification.Block));
                await _sessionStorage.RemoveItemAsync("notification");
            }
        }

    }
}
