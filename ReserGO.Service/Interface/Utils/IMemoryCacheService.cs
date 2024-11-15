namespace ReserGO.Service.Interface.Utils
{
    public interface IMemoryCacheService
    {
        Task AddReservation(Guid reservationId, string details);
    }
}
