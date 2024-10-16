using MudBlazor;
using ReserGO.Miscellaneous.Enum;
using ReserGO.Service.Interface.Utils;

namespace ReserGO.Service.Service.Utils
{
    public class NotificationService : INotificationService
    {
        private readonly ISnackbar _service;

        public NotificationService(ISnackbar service)
        {
            _service = service;
        }


        public void NotifyMessage(string text, NotificationColor color =NotificationColor.Info, string position = null)
        {
            _service.Clear();

            var severity = (Severity)color;

            if (position == null)
                position = Defaults.Classes.Position.TopRight;

            _service.Configuration.PositionClass = position;
            _service.Configuration.VisibleStateDuration = 1;
            _service.Add(text, severity);
        }

    }
}
