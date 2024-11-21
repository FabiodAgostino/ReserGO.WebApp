using ReserGO.Utils.DTO.Utils;

namespace ReserGO.Service.Interface.Service
{
    public interface ITranslateService
    {
        Task Initialize(bool loading = true);
        DictionaryTranslate<string, string> Words { get; set; }
        Task<string> GetCurrentLanguage();
        Task ReInitialize(string culture, bool loading = true);
    }
}
