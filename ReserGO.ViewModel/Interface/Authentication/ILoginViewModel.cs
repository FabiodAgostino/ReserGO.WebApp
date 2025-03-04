﻿using Microsoft.AspNetCore.Components;
using ReserGO.DTO;

namespace ReserGO.ViewModel.Interface.Authentication
{
    public interface ILoginViewModel : ICompleteReserGOViewModel<DTOLoginRequest>
    {
        Task Login(DTOLoginRequest user = null);
        Task FirstLogin();
        public string? LoginError { get; set; }
        public bool IsOpen { get; set; }
        Task OpenModal();
    }
}
