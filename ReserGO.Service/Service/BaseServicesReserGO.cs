using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using ReserGO.Service.Interface;
using ReserGO.Service.Interface.Utils;
using ReserGO.Utils.Event;

namespace ReserGO.Service.Service
{
    public class BaseServicesReserGO<T> : IBaseServicesReserGO<T> where T : class
    {
        public INotificationService Notification { get; private set; }
        public IUserSession Session { get; private set; }
        public IJSRuntime JS { get; private set; }
        public ILogger Log { get; private set; }
        public IEvent Aggregator { get; private set; }

        public BaseServicesReserGO(INotificationService notificationService, IUserSession userSession, IJSRuntime jsRuntime, ILogger<T> logger, IEvent aggregator)
        {
            Notification = notificationService;
            Session = userSession;
            JS = jsRuntime;
            Log = logger;
            Aggregator = aggregator;
        }
    }
}
