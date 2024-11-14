using Microsoft.AspNetCore.Components;
using ReserGO.DTO;
using ReserGO.Miscellaneous.Enum;
using ReserGO.Service.Interface;
using ReserGO.Service.Interface.Service;
using ReserGO.Utils.DTO.Service;
using ReserGO.ViewModel.Interface.FiltersComponent;
using System.Linq;

namespace ReserGO.ViewModel.ViewModel.FiltersComponent
{
    public class ServicesComboViewModel : CompleteReserGOViewModell<DTOService, ServicesComboViewModel>, IServicesComboViewModel
    {
        private readonly IServiceService _service;

        public ServicesComboViewModel(IBaseServicesReserGO<ServicesComboViewModel> baseServices, IServiceService service) : base(baseServices)
        {
            _service = service;
            List = new();
        }
        public IEnumerable<string> _servicesChecked { get; set; }
        public IEnumerable<string> ServicesChecked
        {
            get => _servicesChecked; 
            set
            {
                _servicesChecked = value;
                if(Callback.HasDelegate)
                {
                    var services = List.Where(s=> _servicesChecked.ToList().Contains(s.ServiceName)).ToList();
                    Callback.InvokeAsync(services);
                }
            }

        }
        public EventCallback<List<DTOService>> Callback { get; set; }

        public async Task GetServices()
        {
            try
            {
                IsLoading = true;
                var filter = new GenericPagedFilter<DTOServiceFilter>() { Page = 1, PageSize = 10000, Filter = new() };
                var result = await _service.GetServices(filter);
                if (result.Success)
                {
                    List = result.Data.CurrentPageData;
                    OnPropertyChanged();
                }
                else
                {
                    List = new();
                    Notification(result.Message, NotificationColor.Warning);
                }

            }
            catch (Exception ex)
            {
                Notification(ex.Message, NotificationColor.Error);
            }
            finally
            {
                IsLoading = false;
                OnPropertyChanged();
            }
        }
    }
}
