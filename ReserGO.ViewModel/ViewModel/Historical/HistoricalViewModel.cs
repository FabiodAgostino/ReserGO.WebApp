using ReserGO.DTO;
using ReserGO.Service.Interface;
using ReserGO.Service.Interface.Schedule;
using ReserGO.Utils.DTO.Service;
using ReserGO.Utils.DTO.Utils;
using ReserGO.ViewModel.Interface.Historical;

namespace ReserGO.ViewModel.ViewModel.Historical
{
    public class HistoricalViewModel : CompleteReserGOViewModell<DTOBooking, HistoricalViewModel>, IHistoricalViewModel
    {
        private readonly IBaseServicesReserGO<HistoricalViewModel> _baseServices;
        private readonly IBookingService _service;

        public HistoricalViewModel(IBaseServicesReserGO<HistoricalViewModel> baseServices, IBookingService service) : base(baseServices)
        {
            _baseServices = baseServices;
            _service = service;
        }
        private GenericPagedList<DTOBooking> _bookings { get; set; }

        public GenericPagedList<DTOBooking> Bookings { get => _bookings; set => _bookings=value; }
        public override async Task Refresh()
        {
            try
            {
                var filter = new GenericPagedFilter<DTOBookingFilter>() { Page = 1, PageSize = 10, Filter = new() {GetAll =  UserIs(RoleConst.ADMIN)} };
                var result = await _service.GetBookings(filter);
                if (result.Success)
                {
                    Bookings = result.Data;
                    if(Bookings.TotalItemCount == 0)
                        Bookings = new GenericPagedList<DTOBooking>() { CurrentPageData = new(), TotalItemCount = 0, Page = 0 };
                }
                else
                {
                    Bookings = new GenericPagedList<DTOBooking>() { CurrentPageData = new(), TotalItemCount = 0, Page = 0 };
                    Notification(result.Message, Miscellaneous.Enum.NotificationColor.Error);
                }

            }
            catch (Exception ex)
            {
                Notification(ex.Message, Miscellaneous.Enum.NotificationColor.Error);
            }
            finally
            {
                OnPropertyChanged();
            }
        }
    }
}
