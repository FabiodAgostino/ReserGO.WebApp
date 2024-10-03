using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using ReserGO.Service.Interface;
using ReserGO.Service.Interface.Utils;
using ReserGO.Utils.Event;
using ReserGO.ViewModel.Interface.LoginHome;


namespace ReserGO.ViewModel.ViewModel.LoginHome
{
    public class LoginHomeViewModel : CompleteReserGOViewModell<object, LoginHomeViewModel>, ILoginHomeViewModel
    {

        public LoginHomeViewModel(IBaseServicesReserGO<LoginHomeViewModel> baseService) : base(baseService)
        {

        }
    }
}
