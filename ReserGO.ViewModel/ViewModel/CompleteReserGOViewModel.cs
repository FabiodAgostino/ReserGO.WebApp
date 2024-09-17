using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using ReserGO.Miscellaneous.Enum;
using ReserGO.Service.Interface.Authentication;
using ReserGO.Service.Interface.Utils;
using ReserGO.Utils.DTO.Utils;
using ReserGO.Utils.Event;
using ReserGO.ViewModel.Interface;

namespace ReserGO.ViewModel.ViewModel
{
    public class CompleteReserGOViewModell<TModel> : LightReserGOViewModel<TModel>, ICompleteReserGOViewModel<TModel> where TModel : class
    {
        private readonly INotificationService _notificationService;
        private readonly IUserSession _userSession;
        private readonly IJSRuntime _js;

        public CompleteReserGOViewModell(IEvent aggregator, ILogger logger, INotificationService notificationService, IUserSession userSession, IJSRuntime js) : base(aggregator, logger)
        {
            _notificationService = notificationService;
            _userSession = userSession;
            _js = js;
        }

        public DTOUserSession User
        {
            get
            {
                if (_userSession.User == null || _userSession.User.Username == null)
                {
                    return new DTOUserSession();
                }
                return _userSession.User;
            }
        }
        private bool _isSmallView { get;set; }
        public bool IsSmallView { get => _isSmallView; set=> _isSmallView = value; }

        public virtual async Task RegisterOnScreenResize(int width=1200)
        {
            await _js.InvokeVoidAsync("onScreenResize.addResizeListener", DotNetObjectReference.Create(this), width);
        }

        [JSInvokable]
        public void OnScreenResize(bool isSmall)
        {
            IsSmallView = isSmall;
            OnPropertyChanged();
        }

        public virtual void Notification(string text, NotificationColor color = NotificationColor.Info, string position = null)
        {
            _notificationService.NotifyMessage(text, color, position);
        }

      

        public virtual bool UserIs(RoleConst role)
        {
            if(User!=null && User.Roles!=null)
            {
                switch (role)
                {
                    case RoleConst.ADMIN:
                        return User.Roles.HasPermission(RoleConst.ADMIN);
                    case RoleConst.CUSTOMER:
                        return User.Roles.HasPermission(RoleConst.CUSTOMER) && !User.Roles.HasPermission(RoleConst.ADMIN);
                    case RoleConst.GUEST:
                        return User.Roles.HasPermission(RoleConst.GUEST) && !User.Roles.HasPermission(RoleConst.CUSTOMER);
                    default: return false;
                }
            }
            return false;
            
        }



    }
}
