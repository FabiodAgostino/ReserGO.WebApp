using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using ReserGO.Miscellaneous.Enum;
using ReserGO.Miscellaneous.Model;
using ReserGO.Service.Interface.Utils;
using ReserGO.Utils.Event;
using ReserGO.ViewModel.Interface.FiltersComponent;

namespace ReserGO.ViewModel.ViewModel.FiltersComponent
{
    public class ComuneComboViewModel : CompleteReserGOViewModell<DTOComuneProvincia>, IComuneComboViewModel
    {
        private readonly IComuneService service;

        public ComuneComboViewModel(IEvent aggregator, ILogger<ComuneComboViewModel> logger, INotificationService notificationService, IUserSession userSession, IJSRuntime js, IComuneService service) : 
            base(aggregator, logger, notificationService, userSession, js)
        {
            this.service = service;
            IsLoading = false;
        }

        private List<DTOComuneProvincia> _comuni { get; set; }
        public List<DTOComuneProvincia> Comuni { get => _comuni; set => _comuni=value; }
        public string SearchString { get; set; }

        private string _oldSearchString { get; set; } = "new";   

        public override async Task Refresh()
        {
            if(!String.IsNullOrEmpty(SearchString) && SearchString.Length>1 && SearchString!=_oldSearchString)
            {
                IsLoading = true;
                try
                {
                    Comuni = await service.GetComuniAsync(SearchString);
                }
                catch (Exception ex)
                {
                    Notification(ex.Message, NotificationColor.Error);
                }
                finally
                {
                    _oldSearchString = SearchString;
                    OnPropertyChanged();
                    IsLoading = false;
                }
            }
        }


    }
}
