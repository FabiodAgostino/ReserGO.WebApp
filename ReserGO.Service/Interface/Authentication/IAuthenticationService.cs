﻿using Microsoft.AspNetCore.Components.Authorization;
using ReserGO.DTO;
using ReserGO.Utils.DTO.Service;

namespace ReserGO.Service.Interface.Authentication
{
    public interface IAuthenticationService
    {
        Task<ServiceResponse<string>> Login(DTOLoginRequest loginRequest);
        Task<bool> IsLoggedIn();
        Task Logout();
    }
}
