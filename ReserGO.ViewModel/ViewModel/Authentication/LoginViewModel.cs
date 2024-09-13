﻿using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using ReserGO.DTO;
using ReserGO.Miscellaneous.Enum;
using ReserGO.Miscellaneous.Message;
using ReserGO.Miscellaneous.Model;
using ReserGO.Service.Interface.Authentication;
using ReserGO.Service.Interface.Utils;
using ReserGO.Utils.Event;
using ReserGO.ViewModel.Interface.Authentication;
using ReserGO.ViewModel.ViewModel.Utils;

namespace ReserGO.ViewModel.ViewModel.Authentication
{
    public class LoginViewModel : CompleteReserGOViewModell<DTOLoginRequest>, ILoginViewModel
    {
        private readonly IAuthenticationService _authService;
        private readonly NavigationManager _navigationManager;

        public LoginViewModel(IEvent aggregator, ILogger<LoginViewModel> logger, INotificationService notification, IAuthenticationService authService, NavigationManager navigationManager) : base(aggregator, logger, notification)
        { 
            _authService = authService;
            _navigationManager = navigationManager;
            aggregator.Subscribe<ObjectMessage<GenericModalVoid>>(GetType(), OpenModal);
            IsLoading = false;
            IsFirstLoad = true;
        }
        public bool LoginError { get; set; } = false;
        public bool IsOpen { get; set; }
        EventCallback Callback { get; set; }
        public DTOLoginRequest User { get; set; } = new();

        public async void OpenModal(ObjectMessage<GenericModalVoid> message)
        {
            if(message.Value.Event.HasDelegate)
            {
                Callback = message.Value.Event;
                IsOpen = true;
                OnPropertyChanged();
            }
                
        }


        public async Task Login(DTOLoginRequest user = null)
        {
            IsLoading = true;
            Loading();
            try
            {
                if(user == null)
                    user = new() { IsGuest = true };

                LoginError = !await _authService.Login(user);
            }
            catch (Exception ex)
            {
                Notification(ex.Message, NotificationColor.Error);
            }
            finally
            {
                IsFirstLoad = false;
                IsLoading = false;
                Loading();
                if(Callback.HasDelegate && !LoginError)
                {
                    IsOpen = false;
                    await Callback.InvokeAsync();
                }
                OnPropertyChanged();
            }
        }

        private void Loading()
        {
            Aggregator.Publish<bool, ObjectMessage<bool>>(new ObjectMessage<bool>(IsLoading), typeof(LoadingSpinnerViewModel));
        }

    }
}
