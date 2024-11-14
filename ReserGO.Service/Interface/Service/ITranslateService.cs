using ReserGO.Utils.DTO.Utils;

namespace ReserGO.Service.Interface.Service
{
    public interface ITranslateService
    {
        Task Initialize();
        DictionaryTranslate<string, string> Words { get; set; }
        Task<string> GetCurrentLanguage();
        Task ReInitialize(string culture);
    }
}
