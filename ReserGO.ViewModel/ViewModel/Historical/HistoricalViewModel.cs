using ReserGO.DTO;
using ReserGO.Miscellaneous.Enum;
using ReserGO.Service.Interface;
using ReserGO.Service.Interface.Schedule;
using ReserGO.Utils.DTO.Service;
using ReserGO.Utils.DTO.Utils;
using ReserGO.ViewModel.Interface.Historical;
using System.Security.Cryptography.X509Certificates;

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
            Pagination = new() { Page = 1, PageSize = 10, Filter = new() { GetAll = UserIs(RoleConst.ADMIN) } };
        }
        private GenericPagedList<DTOBooking> _bookings { get; set; }

        public GenericPagedList<DTOBooking> Bookings { get => _bookings; set => _bookings = value; }
        private GenericPagedFilter<DTOBookingFilter> _pagination { get; set; }
        public GenericPagedFilter<DTOBookingFilter> Pagination { get => _pagination; set => _pagination = value; }
        public override async Task Refresh()
        {
            try
            {
                IsLoading = true;
                if (IsFirstLoad)
                    Loading("Caricamento prenotazioni in corso");
                var result = await _service.GetBookings(Pagination);
                if (result.Success)
                {
                    Bookings = result.Data;
                    if (Bookings.TotalItemCount == 0)
                        Bookings = new GenericPagedList<DTOBooking>() { CurrentPageData = new() };
                    Pagination.TotalCount = Bookings.TotalItemCount;
                }
                else
                {
                    Bookings = new GenericPagedList<DTOBooking>() { CurrentPageData = new() };
                    Pagination.TotalCount = 0;
                    Notification(result.Message, Miscellaneous.Enum.NotificationColor.Error);
                }

            }
            catch (Exception ex)
            {
                Notification(ex.Message, Miscellaneous.Enum.NotificationColor.Error);
            }
            finally
            {
                IsLoading = false;

                if (IsFirstLoad)
                    Loading();

               IsFirstLoad = false;
               OnPropertyChanged();
            }
        }

        public async Task ExecuteAction(GridAction<DTOBooking, DTOBookingFilter> action)
        {
            if (!String.IsNullOrEmpty(action.Error))
            {
                Notification(action.Error, NotificationColor.Error);
                return;
            }
            if (action.Items is object || action.PagingOptions is object || action.Filter is object)
            {
                switch (action.TypeActions)
                {
                    case TypeActionsGRID.PAGE_CHANGED:
                    case TypeActionsGRID.PAGE_SIZE_CHANGED:
                        Pagination.Page = action.PagingOptions.Page;
                        Pagination.PageSize = action.PagingOptions.PageSize;
                        await Refresh();
                        break;
                    case TypeActionsGRID.FILTER:
                        Pagination.Filter = (DTOBookingFilter)action.Filter;
                        await Refresh();
                        break;
                    case TypeActionsGRID.UPDATE:
                        await UpdateStateBooking(action.Items.FirstOrDefault());
                        break;
                    case TypeActionsGRID.SINGLE_DELETE:
                        await DeleteBooking(action.Items.FirstOrDefault());
                        break;

                }
            }
        }

        public async Task UpdateStateBooking(DTOBooking booking)
        {
            try
            {
                IsLoading = true;
                Loading();
                var result=await _service.UpdateBookingState(booking);
                if(result.Success)
                {
                    Notification("Prenotazione validata correttamente", NotificationColor.Success);
                    await Refresh();
                }
            }
            catch(Exception ex)
            {
                Notification(ex.Message, NotificationColor.Error);
            }
            finally
            {
                IsLoading = false;
                Loading();
            }
        }

        public async Task DeleteBooking(DTOBooking booking)
        {
            try
            {
                IsLoading = true;
                Loading();
                var result = await _service.DeleteBooking(booking.Id);
                if (result.Success)
                {
                    Notification("Prenotazione eliminata correttamente", NotificationColor.Success);
                    await Refresh();
                }
                else
                {
                    Notification(result.Message, NotificationColor.Warning);
                }
            }
            catch (Exception ex)
            {
                Notification(ex.Message, NotificationColor.Error);
            }
            finally
            {
                IsLoading = false;
                Loading();
            }
        }

    }
}
