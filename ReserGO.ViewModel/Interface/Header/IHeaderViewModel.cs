namespace ReserGO.ViewModel.Interface.Header
{
    public interface IHeaderViewModel : ILightReserGOViewModel<object>
    {
        Task OpenModal();
        bool IsLoggedIn { get; set; }
        Task Logout();
        Task CheckUser();
    }
}
