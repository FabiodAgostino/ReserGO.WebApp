
using Microsoft.AspNetCore.Components;
using ReserGO.DTO;

namespace ReserGO.ViewModel.Interface.Home
{
    public interface IHomeViewModel : ICompleteReserGOViewModel<object>
    {
        IEnumerable<DTOSettingMenu> ItemsMenu { get; set; }
        DTOSettingMenu SelectedItem { get; set; }
        void ToggleDrawer();
        string PageTitle { get; set; }

        RenderFragment Content { get; set; }
        void ChangeComponent();
        bool DrawerVisibility { get; set; }
        Task OnInitialize();
    }
}
