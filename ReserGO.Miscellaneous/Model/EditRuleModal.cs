using Microsoft.AspNetCore.Components;
using ReserGO.DTO;
using ReserGO.Utils.DTO.Utils;

namespace ReserGO.Miscellaneous.Model
{
    public class EditRuleModal
    {
        public EventCallback<(DTOResource Resource, bool IsChanged)> Event { get; set; }
        public DTOResource Data { get; set; }
        public AvailabilityType Type { get; set; }
        public bool IsSmallView { get; set; }
        public EditRuleModal(EventCallback<(DTOResource Resource,bool IsChanged)> Event, AvailabilityType type, DTOResource data, bool isSmallView)
        {
            this.Event = Event;
            Type = type;
            Data = data;
            IsSmallView = isSmallView;
        }



    }
}
