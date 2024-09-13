using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using ReserGO.Service.Interface.Authentication;
using ReserGO.Utils.DTO.Utils;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using static MudBlazor.Colors;

namespace ReserGO.Service.Service.Authentication
{
    public class JwtAuthenticationStateProvider : AuthenticationStateProvider, IJwtAuthenticationStateProvider
    {
        private readonly ISessionStorageService _sessionStorage;
        private readonly HttpClient _httpClient;
        private readonly ILogger<JwtAuthenticationStateProvider> _logger;

        private DTOUserSession _user { get; set; }
        public DTOUserSession User { get => _user; set => _user = value; }


        public JwtAuthenticationStateProvider(ISessionStorageService sessionStorage, HttpClient httpClient, ILogger<JwtAuthenticationStateProvider> logger)
        {
            _sessionStorage = sessionStorage;
            _httpClient = httpClient;
            _logger = logger;
        }


        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _sessionStorage.GetItemAsync<string>("authToken");
            _logger.LogInformation("Token retrieved from session storage: {Token}", token);

            if (string.IsNullOrEmpty(token))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var claims = ParseClaimsFromJwt(token);
            var identity = new ClaimsIdentity(claims, "jwtAuthType");
            var user = new ClaimsPrincipal(identity);
            if (User == null) User = new();
            User.Username = claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            User.Roles = claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).FirstOrDefault().Trim(new char[] { '[', ']' }).Split(',').ToList().ToList();
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
            return new AuthenticationState(user);
        }

        public void NotifyUserAuthentication(string token)
        {
            _logger.LogInformation("Notifying authentication with token: {Token}", token);
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwtAuthType"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public void NotifyUserLogout()
        {
            _logger.LogInformation("Notifying user logout.");
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(anonymousUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }


}
