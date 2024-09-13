using Microsoft.AspNetCore.Components;
using ReserGO.DTO;

namespace ReserGO.ViewModel.Interface.Authentication
{
    public interface ILoginViewModel : ILightReserGOViewModel<DTOLoginRequest>
    {
        Task Login(DTOLoginRequest user = null);
        public bool LoginError { get; set; }
        public bool ShowGUI { get; set; }
        bool IsLoggedIn { get; set; }
        Task CheckUser();
        Task Logout();
        public bool IsOpen { get; set; }
        DTOLoginRequest User { get; set; }
    }
}
