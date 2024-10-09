using Microsoft.AspNetCore.Components;
using ReserGO.DTO;

namespace ReserGO.Miscellaneous.Model
{
    public class DTOScheduleCardDetailsModal
    {
        public EventCallback<int> CallbackOpenModal { get; set; }
        public DTOResource? Resource { get; set; }
    }
}
