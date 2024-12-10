namespace ReserGO.Service.Interface.Utils
{
    public interface ICleanerService
    {
        Task CleanAllAndRedirect(string url = "/");
    }
}
