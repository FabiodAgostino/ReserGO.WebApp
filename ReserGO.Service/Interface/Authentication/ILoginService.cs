using Refit;
using ReserGO.DTO;
using ReserGO.Utils.DTO.Service;

namespace ReserGO.Service.Interface.Authentication
{
    public interface ILoginService
    {
        [Post("/login")]
        Task<ServiceResponse<string>> Login(DTOLoginRequest loginRequest);

        [Post("/Register")]
        Task<ServiceResponse<string>> Register(DTOUser loginRequest);

        [Get("/ConfirmEmail")]
        Task<ServiceResponse<string>> RegistrationConfirm(string email);
    }
}
