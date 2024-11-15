using Microsoft.AspNetCore.Components.Forms;
using ReserGO.DTO;
using ReserGO.Miscellaneous.Model;

namespace ReserGO.ViewModel.Interface.Resource.InsertResource
{
    public interface IInsertResourceViewModel : ICompleteReserGOViewModel<DTOResource>
    {
        public DTOResourceStepper Stepper { get; set; }
        Task HandleFileSelected(IBrowserFile file);
        public bool IsOpen { get; set; }
        public Task InsertResource();
        public bool EnableResource { get; set; }
    }
}
