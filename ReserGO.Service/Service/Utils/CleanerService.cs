using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using ReserGO.Service.Interface.Utils;

namespace ReserGO.Service.Service.Utils
{
    public class CleanerService : ICleanerService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly ISessionStorageService _sessionStorage;
        private readonly NavigationManager _navigation;
        public CleanerService(ILocalStorageService localStorage, ISessionStorageService sessionStorage, NavigationManager navigation)
        {
            _localStorage = localStorage;
            _sessionStorage = sessionStorage;
            _navigation = navigation;
        }

        public async Task CleanAll()
        {
            await _sessionStorage.ClearAsync();
            await _localStorage.ClearAsync();

        }

        public async Task CleanAllAndRedirect(string url = "/")
        {
            await CleanAll();
            _navigation.NavigateTo("/", forceLoad: true);
        }
    }
}
