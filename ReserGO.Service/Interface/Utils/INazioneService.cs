using Refit;
using ReserGO.Miscellaneous.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReserGO.Service.Interface.Utils
{
    public interface INazioneService
    {
        [Get("/v3.1/name/{name}")]
        Task<List<DTONazione>> GetNazioneAsync(
        [AliasAs("name")] string nome
    );
    }
}
