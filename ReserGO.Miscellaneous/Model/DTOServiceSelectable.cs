using ReserGO.DTO;

namespace ReserGO.Miscellaneous.Model
{
    public class DTOServiceSelectable
    {
        public bool IsSelected { get; set; }
        public DTOService Service { get; set; }
        public DTOServiceSelectable(DTOService service)
        {
            Service = service;
        }

    }
}
