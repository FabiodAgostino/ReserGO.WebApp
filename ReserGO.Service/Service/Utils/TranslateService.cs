using Blazored.LocalStorage;
using Blazored.SessionStorage;
using ReserGO.Miscellaneous.Message;
using ReserGO.Miscellaneous.Model;
using ReserGO.Service.Interface.Home;
using ReserGO.Service.Interface.Service;
using ReserGO.Utils.DTO.Utils;
using ReserGO.Utils.Event;

namespace ReserGO.Service.Service.Utils
{
    public class TranslateService : ITranslateService
    {
        private readonly ISessionStorageService _localStorage;
        private IHomeService _service { get; }
        private IEvent _event { get; set;}

        public TranslateService(IHomeService service, ISessionStorageService localStorage, IEvent _event)
        {
            this._event = _event;
            _service = service;
            _localStorage = localStorage;
        }
        private DictionaryTranslate<string, string> _words { get; set; }

        public DictionaryTranslate<string, string> Words
        {
            get
            {
                if (_words == null)
                    _words = new();
                return _words;
            }
            set => _words = value;
        }

        public async Task<string> GetCurrentLanguage()
        {
            var lang = await _localStorage.GetItemAsync<string>("culture");
            if (String.IsNullOrEmpty(lang))
                return "Italiano";
            else
            {
                switch(lang)
                {
                    case "it": return "Italiano";
                    case "en": return "English";
                    default: return "";
                }
            }
        }

        public async Task Initialize(bool loading = true)
        {
            var lang = await _localStorage.GetItemAsync<string>("culture");
            if (String.IsNullOrEmpty(lang))
                lang = "it";

            await ReInitialize(lang, loading);
        }

        public async Task ReInitialize(string culture, bool loading=true)
        {
            bool success = false;
            try
            {
                var resources = await _localStorage.GetItemAsync<DictionaryTranslate<string, string>>("translation");
                var lang = await _localStorage.GetItemAsync<string>("culture");
                if ((resources == null || !resources.Any()) || lang != culture)
                {
                    if(loading)
                        _event.Publish<LoadingSpinner, ObjectMessage<LoadingSpinner>>(new ObjectMessage<LoadingSpinner>(new LoadingSpinner(true, "Caricamento lingua in corso")));
                    if (lang == null)
                        lang = "it";
                    else
                        lang = culture;

                    await _localStorage.SetItemAsync<string>("culture", lang);

                    var result = await _service.GetTranslate(culture, null);

                    if (result.Success)
                    {
                        success = true;
                        resources = new DictionaryTranslate<string, string>(result.Data.KeyValueResources);
                        await _localStorage.SetItemAsync("translation", resources);
                        if (loading)
                            _event.Publish<LoadingSpinner, ObjectMessage<LoadingSpinner>>(new ObjectMessage<LoadingSpinner>(new LoadingSpinner(false, "Caricamento lingua in corso")));
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
