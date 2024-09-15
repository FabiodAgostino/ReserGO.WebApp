using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using ReserGO.DTO;
using ReserGO.Miscellaneous.Message;
using ReserGO.Miscellaneous.Model;
using ReserGO.Service.Interface.Authentication;
using ReserGO.Service.Interface.Home;
using ReserGO.Service.Interface.Utils;
using ReserGO.Utils.Event;
using ReserGO.ViewModel.Interface.Register;
using ReserGO.ViewModel.ViewModel.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserGO.ViewModel.ViewModel.Register
{
    public class RegisterViewModel : CompleteReserGOViewModell<object>, IRegisterViewModel
    {
        IAuthenticationService _authService;
        public bool IsOpen { get; set; } = false;

        public UserRegister UserRegister {  get; set; }

        public RegisterViewModel(IEvent aggregator, ILogger<RegisterViewModel> logger, INotificationService notification, IUserSession session, IAuthenticationService authService) : base(aggregator, logger, notification, session)
        {
            _authService = authService;
            aggregator.Subscribe<ObjectMessage<bool>>(GetType(), OpenModal);
            UserRegister = new UserRegister();
        }



        public async void OpenModal(ObjectMessage<bool> message)
        {
            IsOpen = true;
            OnPropertyChanged();
        }

        public void Close()
        {
            IsOpen = false;
        }

        public void Register()
        {
            //chiamata
        }
    }
}
