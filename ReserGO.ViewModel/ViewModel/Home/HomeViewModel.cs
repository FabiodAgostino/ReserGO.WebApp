using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using ReserGO.DTO;
using ReserGO.Miscellaneous.Enum;
using ReserGO.Miscellaneous.Message;
using ReserGO.Service.Interface.Home;
using ReserGO.Service.Interface.Utils;
using ReserGO.Utils.Event;
using ReserGO.ViewModel.Interface.Home;
using ReserGO.ViewModel.ViewModel.Utils;

namespace ReserGO.ViewModel.ViewModel.Home
{
    public class HomeViewModel : CompleteReserGOViewModell<object>, IHomeViewModel
    {
        private readonly IHomeService _service;

        public HomeViewModel(IEvent aggregator, ILogger<HomeViewModel> logger, INotificationService notification, IHomeService service) : base(aggregator, logger, notification)
        {
            _service = service;
            aggregator.Subscribe<ObjectMessage<bool>>(GetType(), async (ObjectMessage<bool> message) => await OnInitialize());
        }

        public IEnumerable<DTOSettingMenu> ItemsMenu { get; set; }
        public string PageTitle { get; set; }
        private RenderFragment _content;
        private bool _showFilter;

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
            IsLoading = true;
            try
            {
                var result = await _service.GetSettingsMenu();
                if (result.Success)
                {
                    ItemsMenu = result.Data;
                    SelectedItem = result.Data.FirstOrDefault();
                    ChangeComponent();
                }

            }
            catch (Exception ex)
            {
                Notification(ex.Message, NotificationColor.Error);
            }
            finally
            {
                IsLoading = false;
                Loading();
                OnPropertyChanged();
            }
        }

        public void ChangeComponent()
        {
            Content = new RenderFragment(builder =>
            {
                builder.OpenComponent(0, Type.GetType(SelectedItem.ComponentType));
                builder.CloseComponent();
            });
            OnPropertyChanged();

        }

        private void Loading()
        {
            Aggregator.Publish<bool,ObjectMessage<bool>>(new ObjectMessage<bool>(IsLoading), typeof(LoadingSpinnerViewModel));
        }

    }
}
