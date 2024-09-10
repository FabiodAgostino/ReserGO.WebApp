using ReserGO.DTO;

namespace ReserGO.ViewModel.Interface.Home
{
    public interface ILoginViewModel : ILightReserGOViewModel<DTOLoginRequest>
    {
        Task Login(DTOLoginRequest user);
        public bool LoginError { get; set; }
    }
}
