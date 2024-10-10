using Refit;
using ReserGO.DTO;
using ReserGO.DTO.ListAvailability;
using ReserGO.Utils.DTO.Service;

namespace ReserGO.Service.Interface.Schedule
{
    public interface IBookingService
    {
        [Post("/InsertBooking")]
        Task<ServiceResponse<bool>> InsertBooking(DTOBooking booking);
        [Get("/GetBookingSlotUnavailableFromResource")]
        Task<ServiceResponse<IEnumerable<DTOTimeSlot>>> GetBookingSlotUnavailableFromResource(int idResource, DateTime day);
    }
}
