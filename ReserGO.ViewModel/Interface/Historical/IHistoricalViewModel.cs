using ReserGO.DTO;
using ReserGO.Utils.DTO.Service;
using ReserGO.Utils.DTO.Utils;

namespace ReserGO.ViewModel.Interface.Historical
{
    public interface IHistoricalViewModel : ICompleteReserGOViewModel<DTOBooking>
    {
        public GenericPagedList<DTOBooking> Bookings { get; set; }
        public GenericPagedFilter<DTOBookingFilter> Pagination { get; set; }
        Task ExecuteAction(GridAction<DTOBooking, DTOBookingFilter> action);
    }
}
