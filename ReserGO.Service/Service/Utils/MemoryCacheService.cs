using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using ReserGO.Miscellaneous.Message;
using ReserGO.Service.Interface.Utils;

namespace ReserGO.Service.Service.Utils
{
    public class MemoryCacheService : IMemoryCacheService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IHubContext<NotificationHub> _hubContext;

        public MemoryCacheService(IMemoryCache memoryCache, IHubContext<NotificationHub> hubContext)
        {
            _memoryCache = memoryCache;
            _hubContext = hubContext;
        }

        public async Task AddReservation(Guid reservationId, string details)
        {
            // Aggiungi la prenotazione alla cache (chiave unica basata sull'ID della prenotazione)
            _memoryCache.Set(reservationId, details, TimeSpan.FromMinutes(30));
            var message = new NotificationMemoryCache() { Identifier = reservationId, Message=details };
            // Invia notifica all'amministratore
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", message);
        }
    }
}
