using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using ReserGO.Service.Interface.Utils;
using ReserGO.Utils.Event;

namespace ReserGO.Service.Interface
{
    public interface IBaseServicesReserGO<T> where T : class
    {
        INotificationService Notification { get; }
        IUserSession Session { get; }
        IJSRuntime JS { get; }
        ILogger Log { get; }
        IEvent Aggregator { get; }
    }
}
