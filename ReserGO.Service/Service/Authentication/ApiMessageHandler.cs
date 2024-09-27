using Blazored.SessionStorage;
using System.Net.Http.Headers;

namespace ReserGO.Service.Service.Authentication
{
    public class ApiMessageHandler : DelegatingHandler
    {
        private readonly ISessionStorageService _sessionStorage;

        public ApiMessageHandler(ISessionStorageService sessionStorage) : base(new HttpClientHandler())
        {
            _sessionStorage = sessionStorage;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _sessionStorage.GetItemAsync<string>("authToken");

            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }

    }
}