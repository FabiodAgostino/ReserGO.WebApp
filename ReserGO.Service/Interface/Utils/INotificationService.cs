
using Microsoft.AspNetCore.Components;
using ReserGO.Miscellaneous.Enum;

namespace ReserGO.Service.Interface.Utils
{
    public interface INotificationService
    {
        void NotifyMessage(string text, NotificationColor color = NotificationColor.Info, string position = null, bool block = false, EventCallback? callback = null);
        Task PushToList(string text, NotificationColor color = NotificationColor.Info, string position = null, bool block = false);
        Task ReadNotification();
    }
}
