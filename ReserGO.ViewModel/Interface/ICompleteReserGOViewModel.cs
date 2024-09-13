using ReserGO.Miscellaneous.Enum;

namespace ReserGO.ViewModel.Interface
{
    public interface ICompleteReserGOViewModel<TModel> : ILightReserGOViewModel<TModel>
    {
        void Notification(string text, NotificationColor color = NotificationColor.Info, string position = null);
    }
}
