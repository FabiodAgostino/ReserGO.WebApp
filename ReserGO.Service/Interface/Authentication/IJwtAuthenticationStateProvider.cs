namespace ReserGO.Service.Interface.Authentication
{
    public interface IJwtAuthenticationStateProvider
    {
        void NotifyUserAuthentication(string token);
    }
}
