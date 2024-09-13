using Microsoft.AspNetCore.Components;

namespace ReserGO.Miscellaneous.Model
{
    public class GenericModalVoid : GenericModal<object>
    {
        public GenericModalVoid(EventCallback Event) : base()
        {
            this.Event = Event;
        }
        public new EventCallback Event;
    }
}
