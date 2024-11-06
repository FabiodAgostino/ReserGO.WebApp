using ReserGO.DTO;
using ReserGO.Miscellaneous.Enum;
using ReserGO.Miscellaneous.Model;
using ReserGO.Service.Interface;
using ReserGO.Service.Interface.Schedule;
using ReserGO.Utils.DTO.Service;
using ReserGO.Utils.DTO.Utils;
using ReserGO.ViewModel.Interface.Resource.InsertResource;
using static MudBlazor.CategoryTypes;

namespace ReserGO.ViewModel.ViewModel.Resource.InsertResource
{
    public class InsertResourceViewModel : CompleteReserGOViewModell<DTOResource, InsertResourceViewModel>, IInsertResourceViewModel
    {
        private readonly IResourceService _service;

        public InsertResourceViewModel(IBaseServicesReserGO<InsertResourceViewModel> baseServices, IResourceService service) : base(baseServices)
        {
            SelectedItem = new();
            Stepper = new();
            _service = service;
        }
        public DTOResourceStepper Stepper { get; set; }

        

        public async Task InsertResource()
        {
            try
            {
                var result = await _service.InsertResource(SelectedItem);
                if (result.Success)
                    Notification("Risorsa inserita con successo!", NotificationColor.Info);
                else
                    Notification(result.Message, NotificationColor.Warning);
            }
            catch (Exception ex)
            {
                Notification(ex.Message, NotificationColor.Error);
            }
        }

      
    }
}
