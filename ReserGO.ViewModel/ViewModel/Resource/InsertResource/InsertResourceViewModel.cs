using Microsoft.AspNetCore.Components.Forms;
using ReserGO.DTO;
using ReserGO.Miscellaneous.Enum;
using ReserGO.Miscellaneous.Message;
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
            Aggregator.Subscribe<ObjectMessage<bool>>(GetType(),(ObjectMessage<bool> open) => IsOpen = true);
        }
        public DTOResourceStepper Stepper { get; set; }
        public bool IsOpen { get; set; }

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

        public async Task HandleFileSelected(IBrowserFile file)
        {
            if (file != null)
            {
                using var memoryStream = new MemoryStream();
                await file.OpenReadStream().CopyToAsync(memoryStream);
                SelectedItem.ImageData = memoryStream.ToArray();
                SelectedItem.ImageContentType = file.ContentType;
            }
        }
    }
}
