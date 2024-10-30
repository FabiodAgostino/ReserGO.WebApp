using ReserGO.DTO;
using ReserGO.Miscellaneous.Model;

namespace ReserGO.ViewModel.Interface.Resource.InsertResource
{
    public interface IInsertResourceViewModel : ICompleteReserGOViewModel<DTOResource>
    {
        public DTOResourceStepper Stepper { get; set; }
    }
}
