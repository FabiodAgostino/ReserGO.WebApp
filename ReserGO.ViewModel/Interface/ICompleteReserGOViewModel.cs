using ReserGO.Miscellaneous.Enum;
using ReserGO.Utils.DTO.Utils;

namespace ReserGO.ViewModel.Interface
{
    public interface ICompleteReserGOViewModel<TModel> : ILightReserGOViewModel<TModel>
    {
        void Notification(string text, NotificationColor color = NotificationColor.Info, string position = null);
        DTOUserSession User { get; }
        bool UserIs(RoleConst role);
    }
}
