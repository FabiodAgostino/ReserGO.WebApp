using Refit;
using ReserGO.DTO;
using ReserGO.Utils.DTO.Service;

namespace ReserGO.Service.Interface.Authentication
{
    public interface ILoginService
    {
        [Post("/login")]
        Task<ServiceResponse<string>> Login(DTOLoginRequest loginRequest);

        [Get("/ConfirmUsername")]
        Task<ServiceResponse<string>> RegistrationConfirm(string username);
    }
}
