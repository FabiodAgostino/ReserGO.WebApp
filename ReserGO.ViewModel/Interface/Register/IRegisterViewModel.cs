using ReserGO.Miscellaneous.Model;

namespace ReserGO.ViewModel.Interface.Register
{
    public interface IRegisterViewModel : ICompleteReserGOViewModel<object> 
    {
        bool IsOpen { get; set; }
        UserRegister UserRegister { get; set; }
        void Close();
        void Register();
        Task RegistrationConfirm(string username);

    }
}
