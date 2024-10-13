using ReserGO.DTO;
using ReserGO.Utils.DTO.Service;

namespace ReserGO.ViewModel.Interface.Historical
{
    public interface IHistoricalViewModel : ICompleteReserGOViewModel<DTOBooking>
    {
        public GenericPagedList<DTOBooking> Bookings { get; set; }
    }
}
