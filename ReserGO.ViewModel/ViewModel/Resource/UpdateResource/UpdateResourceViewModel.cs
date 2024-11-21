using Microsoft.AspNetCore.Components.Forms;
using ReserGO.DTO;
using ReserGO.Miscellaneous.Message;
using ReserGO.Service.Interface;
using ReserGO.ViewModel.Interface.Resource.UpdateResource;

namespace ReserGO.ViewModel.ViewModel.Resource.UpdateResource
{
    public class UpdateResourceViewModel : CompleteReserGOViewModell<DTOResource, UpdateResourceViewModel>, IUpdateResourceViewModel
    {
        public UpdateResourceViewModel(IBaseServicesReserGO<UpdateResourceViewModel> baseServices) : base(baseServices)
        {
            Aggregator.Subscribe(GetType(), (ObjectMessage<DTOResource> message) => OpenModal(message));
        }

        private void OpenModal(ObjectMessage<DTOResource> message)
        {
            SelectedItem = message.Value;
            IsOpen = true;
        }
        public bool IsOpen { get; set; }

        public async Task HandleFileSelected(IBrowserFile file)
        {
            if (file != null)
            {
                using var memoryStream = new MemoryStream();
                await file.OpenReadStream().CopyToAsync(memoryStream);
                SelectedItem.ImageData = memoryStream.ToArray();
                SelectedItem.ImageContentType = file.ContentType;
                SelectedItem.ImageName = file.Name;
            }
        }
    }
}
