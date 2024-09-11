using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using ReserGO.DTO;
using ReserGO.Service.Interface.Authentication;
using ReserGO.Service.Interface.Home;
using ReserGO.Utils.Event;
using ReserGO.ViewModel.Interface.Home;

namespace ReserGO.ViewModel.ViewModel.Home
{
    public class HomeViewModel : LightReserGOViewModel<object>, IHomeViewModel
    {
        private readonly IHomeService _service;

        public HomeViewModel(IEvent aggregator, ILogger<HomeViewModel> logger, IHomeService service) : base(aggregator, logger)
        {
            _service = service;
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
            try
            {
                var result = await _service.GetSettingsMenu();
                if (result.Success)
                    ItemsMenu = result.Data;
            }
            catch (Exception ex)
            {
            }
            finally
            {
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

        }

    }
}
