using ReserGO.Utils.DTO.Utils;

namespace ReserGO.Service.Interface.Service
{
    public interface ITranslateService
    {
        Task Initialize(string culture);
        DictionaryTranslate<string, string> Words { get; set; }
    }
}
