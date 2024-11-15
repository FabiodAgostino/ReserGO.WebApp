using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using ReserGO.Miscellaneous.Enum;
using ReserGO.Service.Interface;
using ReserGO.Utils.DTO.Utils;
using ReserGO.ViewModel.Interface;

namespace ReserGO.ViewModel.ViewModel
{
    public class CompleteReserGOViewModell<TModel, T> : LightReserGOViewModel<TModel>, ICompleteReserGOViewModel<TModel> where TModel : class where T : class
    {
        private readonly IBaseServicesReserGO<T> _baseServices;

        public CompleteReserGOViewModell(IBaseServicesReserGO<T> baseServices) : base(baseServices.Aggregator, baseServices.Log, baseServices.JS)
        {
            _baseServices = baseServices;
        }

        public DTOUserSession User
        {
            get
            {
                if (_baseServices.Session.User == null || _baseServices.Session.User.Username == null)
                {
                    return new DTOUserSession();
                }
                return _baseServices.Session.User;
            }
        }

        public virtual async Task ReadNotification()
        {
            await _baseServices.Notification.ReadNotification();
        }

        public virtual async Task PushNotification(string text, NotificationColor color = NotificationColor.Info, string position = null, bool block=false)
        {
            if (color == NotificationColor.Warning || color == NotificationColor.Error)
                _baseServices.Log.LogError(text);

            await _baseServices.Notification.PushToList(text, color, position, block);
        }

        public virtual void Notification(string text, NotificationColor color = NotificationColor.Info, string position = null, EventCallback? callback=null)
        {
            if (color == NotificationColor.Warning || color == NotificationColor.Error)
                _baseServices.Log.LogError(text);

            _baseServices.Notification.NotifyMessage(text, color, position, false, callback);
        }

        public virtual bool IsLoggedIn() => !String.IsNullOrEmpty(User.FirstName);

        public virtual bool UserIs(RoleConst role)
        {
            var roles = _baseServices.Session.User.Roles;
            if (User!=null && User.Roles!=null)
            {
                switch (role)
                {
                    case RoleConst.ADMIN:
                        return roles.HasPermission(RoleConst.ADMIN);
                    case RoleConst.CUSTOMER:
                        return roles.HasPermission(RoleConst.CUSTOMER) && !roles.HasPermission(RoleConst.ADMIN);
                    case RoleConst.GUEST:
                        return roles.HasPermission(RoleConst.GUEST) && !roles.HasPermission(RoleConst.CUSTOMER);
                    default: return false;
                }
            }
            return false;
            
        }



    }
}
