namespace ReserGO.ViewModel.Interface.Header
{
    public interface IHeaderViewModel : ICompleteReserGOViewModel<object>
    {
        Task OpenModal();
        bool IsLoggedIn { get; set; }
        Task Logout();
        Task CheckUser();

    }
}
