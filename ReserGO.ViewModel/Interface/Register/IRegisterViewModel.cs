using ReserGO.DTO;
using ReserGO.Miscellaneous.Model;

namespace ReserGO.ViewModel.Interface.Register
{
    public interface IRegisterViewModel : ICompleteReserGOViewModel<DTOUser> 
    {
        bool IsOpen { get; set; }
        void Close();
        void Register();
        Task RegistrationConfirm(string username);

    }
}
