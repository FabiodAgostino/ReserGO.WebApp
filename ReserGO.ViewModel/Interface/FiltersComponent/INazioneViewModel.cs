using ReserGO.Miscellaneous.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserGO.ViewModel.Interface.FiltersComponent
{
    public interface INazioneViewModel : ICompleteReserGOViewModel<DTONazione>
    {
        public List<DTONazione> Nazioni { get; set; }
        public string SearchString { get; set; }
    }
}
