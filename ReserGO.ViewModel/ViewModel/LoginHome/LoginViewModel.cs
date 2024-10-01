using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using ReserGO.Service.Interface.Utils;
using ReserGO.Utils.Event;
using ReserGO.ViewModel.Interface.LoginHome;


namespace ReserGO.ViewModel.ViewModel.LoginHome
{
    public class LoginHomeViewModel : CompleteReserGOViewModell<object>, ILoginHomeViewModel
    {

        public LoginHomeViewModel(IEvent aggregator, ILogger<LoginHomeViewModel> logger, INotificationService notificationService, IUserSession userSession, IJSRuntime js) : base(aggregator, logger, notificationService, userSession, js)
        {

        }
    }
}
