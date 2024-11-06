using ReserGO.DTO;
using ReserGO.Miscellaneous.Model;
using ReserGO.Utils.DTO.Service;
using ReserGO.Utils.DTO.Utils;

namespace ReserGO.ViewModel.Interface.Resource.InsertResource
{
    public interface IInsertResourceViewModel : ICompleteReserGOViewModel<DTOResource>
    {
        public DTOResourceStepper Stepper { get; set; }
       
    }
}
