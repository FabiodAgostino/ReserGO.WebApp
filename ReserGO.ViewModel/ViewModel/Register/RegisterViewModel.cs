using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using ReserGO.DTO;
using ReserGO.Miscellaneous.Enum;
using ReserGO.Miscellaneous.Message;
using ReserGO.Service.Interface;
using ReserGO.Service.Interface.Authentication;
using ReserGO.Service.Interface.Service;
using ReserGO.Service.Interface.Utils;
using ReserGO.Utils.Event;
using ReserGO.ViewModel.Interface.Register;

namespace ReserGO.ViewModel.ViewModel.Register
{
    public class RegisterViewModel : CompleteReserGOViewModell<DTOUser, RegisterViewModel>, IRegisterViewModel
    {
        ILoginService _authService;
        private readonly NavigationManager _navigationManager;
        private readonly ITranslateService _t;

        public bool IsOpen { get; set; }


        public RegisterViewModel(IBaseServicesReserGO<RegisterViewModel> baseService, ITranslateService t, ILoginService authService, NavigationManager navigationManager) 
            : base(baseService)
        {
            _authService = authService;
            _navigationManager = navigationManager;
            _t = t;
            Aggregator.Subscribe<ObjectMessage<bool>>(GetType(), OpenModal);
            SelectedItem = new DTOUser();
        }

        public async Task RegistrationConfirm(string username)
        {
            try
            {
                IsLoading = true;
                Loading("Conferma della mail in corso");
                var result = await _authService.RegistrationConfirm(username);
                if(result.Success)
                    await PushNotification(_t.Words["Conferma della mail effettuata con successo"], NotificationColor.Success,null,true);
                else
                    await PushNotification(result.Message, NotificationColor.Error,null,true);

                _navigationManager.NavigateTo("/", forceLoad: true);
            }
            catch (Exception ex)
            {
                Notification(ex.Message, NotificationColor.Error);
            }
            finally
            {
                IsLoading = false;
                Loading();
            }
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

        public async void Register()
        {
            try
            {
                IsLoading = true;
                Loading();
                var result=await _authService.Register(SelectedItem);
                if(result.Success)
                {
                    Notification(_t.Words["Registrazione effettuata con successo, conferma la mail"], NotificationColor.Success);
                    IsOpen = false;
                }
                else
                {
                    Notification(result.Message, NotificationColor.Error);
                }
            }catch(Exception ex)
            {
                Notification(ex.Message, NotificationColor.Error);
            }
            finally
            {
                IsLoading= false;
                Loading();
                OnPropertyChanged();
            }
        }
    }
}
