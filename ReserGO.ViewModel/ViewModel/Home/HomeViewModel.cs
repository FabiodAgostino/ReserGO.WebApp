using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using ReserGO.DTO;
using ReserGO.Service.Interface.Authentication;
using ReserGO.Utils.Event;
using ReserGO.ViewModel.Interface.Home;

namespace ReserGO.ViewModel.ViewModel.Home
{
    public class HomeViewModel : LightReserGOViewModel<object>, IHomeViewModel
    {

        public HomeViewModel(IEvent aggregator, ILogger<HomeViewModel> logger) : base(aggregator, logger)
        {
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
