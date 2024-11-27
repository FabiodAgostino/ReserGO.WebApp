using ReserGO.DTO;

namespace ReserGO.Service.Interface.Authentication
{
    public interface IFirstLoginService
    {
        Task<string> Login();
    }
}
