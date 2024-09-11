using ReserGO.DTO;

namespace ReserGO.ViewModel.Interface.Authentication
{
    public interface ILoginViewModel : ILightReserGOViewModel<DTOLoginRequest>
    {
        Task Login(DTOLoginRequest user);
        public bool LoginError { get; set; }
    }
}
