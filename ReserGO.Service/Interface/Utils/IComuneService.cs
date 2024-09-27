using Refit;
using ReserGO.Miscellaneous.Model;

namespace ReserGO.Service.Interface.Utils
{
    public interface IComuneService
    {
    [Get("/comuni")]
    Task<List<DTOComuneProvincia>> GetComuniAsync(
      [AliasAs("nome")] string nome,
      [AliasAs("page")] int page = 1,
      [AliasAs("pagesize")] int pageSize = 100
  );
    }
}
