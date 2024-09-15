using ReserGO.DTO;
using ReserGO.Miscellaneous.Model;
using ReserGO.ViewModel.Interface.Header;
using ReserGO.ViewModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserGO.ViewModel.Interface.Register
{
    public interface IRegisterViewModel : ICompleteReserGOViewModel<object> 
    {
        bool IsOpen { get; set; }
        UserRegister UserRegister { get; set; }
        void Close();
        void Register();

    }
}
