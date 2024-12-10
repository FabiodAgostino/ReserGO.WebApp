using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ReserGO.DTO;
using ReserGO.Miscellaneous.Enum;
using ReserGO.Miscellaneous.Message;
using ReserGO.Miscellaneous.Model;
using ReserGO.Service.Interface;
using ReserGO.Service.Interface.Authentication;
using ReserGO.Service.Interface.Home;
using ReserGO.Service.Interface.Service;
using ReserGO.Service.Interface.Utils;
using ReserGO.Service.Service.Utils;
using ReserGO.Utils.DTO.Utils;
using ReserGO.ViewModel.Interface.Home;
using ReserGO.ViewModel.ViewModel.Utils;

namespace ReserGO.ViewModel.ViewModel.Home
{
    public class HomeViewModel : CompleteReserGOViewModell<object, HomeViewModel>, IHomeViewModel
    {
        private readonly IHomeService _service;
        private readonly ISessionStorageService _sessionStorage;
        private readonly NavigationManager navigationManager;
        private readonly IAuthenticationService _authService;
        private readonly ICleanerService _cleanerService;
        private const string SettingsMenuKey = "settingsMenu";
        public HomeViewModel(IBaseServicesReserGO<HomeViewModel> baseService, IHomeService service, 
            ISessionStorageService sessionStorage, NavigationManager navigationManager, 
            IAuthenticationService authService, ICleanerService cleanerService) : base(baseService)
        {
            _service = service;
            _sessionStorage = sessionStorage;
            this.navigationManager = navigationManager;
            _authService = authService;
            _cleanerService = cleanerService;
            Aggregator.Subscribe<ObjectMessage<bool>>(GetType(), async (ObjectMessage<bool> message) => await OnInitialize());


        }
        public IEnumerable<DTOSettingMenu> ItemsMenu { get; set; }
        public string PageTitle { get; set; }
        private RenderFragment _content;
        private bool _showFilter;
        private bool thisView {  get; set; }
        public bool Open { get; set; } = true;

        public RenderFragment Content
        {
            get { return _content; }
            set
            {
                _content = value;
                OnPropertyChanged();
            }
        }
        public bool DrawerVisibility { get; set; } = true;

        private DTOSettingMenu _selectedItem { get; set; }

        public DTOSettingMenu SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }

        }


        public void ToggleDrawer() => DrawerVisibility = !DrawerVisibility;


        public async Task OnInitialize()
        {
            if (ConfigurationServer.Manutenzione)
                navigationManager.NavigateTo("/Manutenzione");

            TriggerMethodOnSmall = new EventCallback<bool>(null, (bool IsSmall) => {
                if (thisView != IsSmall)
                {
                    Open = false;
                    thisView = IsSmall;
                }
            });

            await ReadNotification();
            var isLogged=await _authService.IsLoggedIn();
            var savedSettingsMenu = await _sessionStorage.GetItemAsync<IEnumerable<DTOSettingMenu>>(SettingsMenuKey);
            if (savedSettingsMenu == null || !isLogged)
            {
                isLoading = true;
                Loading();
                try
                {
                    var result = await _service.GetSettingsMenu();
                    if (result.Success)
                    {
                        savedSettingsMenu = result.Data;
                        await _sessionStorage.SetItemAsync(SettingsMenuKey, savedSettingsMenu);
                    }

                }
                catch (Exception ex)
                {
                    if(ex.Message.Contains("401"))
                    {
                        await _cleanerService.CleanAllAndRedirect();
                        return;
                    }

                    navigationManager.NavigateTo("/Manutenzione", forceLoad: true);
                    Notification(ex.Message, NotificationColor.Error);
                }
                finally
                {
                    
                    isFirstLoad = false;
                    OnPropertyChanged();
                }
            }
            IsLoading = false;
            Loading();
            ItemsMenu = savedSettingsMenu.Where(menu => menu.Permissions.Any(permission => User.Roles.HasPermission((RoleConst)permission))).OrderBy(x => x.OrderN);
            SelectedItem = ItemsMenu.FirstOrDefault();
            ChangeComponent();
        }

        public void ChangeComponent(string component = null)
        {
            if (!String.IsNullOrEmpty(component))
                SelectedItem = ItemsMenu.SingleOrDefault(x => x.Text.ToLower().StartsWith(component.ToLower()));

            Content = new RenderFragment(builder =>
            {
                builder.OpenComponent(0, Type.GetType(SelectedItem.ComponentType));
                builder.CloseComponent();
            });
            OnPropertyChanged();

        }
    }
}
