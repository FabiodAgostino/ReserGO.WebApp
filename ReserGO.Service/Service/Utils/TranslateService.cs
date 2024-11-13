using Blazored.LocalStorage;
using Blazored.SessionStorage;
using ReserGO.Service.Interface.Home;
using ReserGO.Service.Interface.Service;
using ReserGO.Utils.DTO.Utils;

namespace ReserGO.Service.Service.Utils
{
    public class TranslateService : ITranslateService
    {
        private readonly ISessionStorageService _localStorage;
        private IHomeService _service { get; }

        public TranslateService(IHomeService service, ISessionStorageService localStorage)
        {
            _service = service;
            _localStorage = localStorage;
        }
        private DictionaryTranslate<string, string> _words { get; set; }

        public DictionaryTranslate<string, string> Words
        {
            get => _words;
            set => _words = value;
        }

        public async Task Initialize(string culture)
        {
            try
            {
                var resources = await _localStorage.GetItemAsync<DictionaryTranslate<string, string>>("translation");
                var lang = await _localStorage.GetItemAsync<string>("culture");

                if ((resources == null || !resources.Any()) || lang!=culture)
                {
                    if (lang == null)
                        lang = "it";

                    await _localStorage.SetItemAsync<string>("culture", lang);

                    var result = await _service.GetTranslate(culture, null);

                    if (result.Success)
                    {
                        resources = new DictionaryTranslate<string, string>(result.Data.KeyValueResources);
                        await _localStorage.SetItemAsync("translation", resources);
                    }
                    else
                        resources = new();
                }


                Words = resources;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
