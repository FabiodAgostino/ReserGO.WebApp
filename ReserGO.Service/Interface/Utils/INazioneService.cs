using Refit;
using ReserGO.Miscellaneous.Model;

namespace ReserGO.Service.Interface.Utils
{
    public interface INazioneService
    {
        [Get("/v3.1/translation/{name}")]
        Task<List<ComboNazione>> GetNazioneAsync(
        [AliasAs("name")] string nome
    );
    }
}
