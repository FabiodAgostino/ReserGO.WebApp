using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using ReserGO.Service.Interface.Authentication;
using ReserGO.Utils.DTO.Utils;
using System.IdentityModel.Tokens.Jwt;
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
        private readonly ILocalStorageService _localStorageService;
        private readonly IFirstLoginService _authService;

        private DTOUserSession _user { get; set; }
        public DTOUserSession User { get => _user; set => _user = value; }


        public JwtAuthenticationStateProvider(ISessionStorageService sessionStorage, HttpClient httpClient, ILogger<JwtAuthenticationStateProvider> logger, ILocalStorageService localStorageService, IFirstLoginService authService)
        {
            _sessionStorage = sessionStorage;
            _httpClient = httpClient;
            _logger = logger;
            _localStorageService = localStorageService;
            _authService = authService;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string token = String.Empty;
            token = await _localStorageService.GetItemAsync<string>("authToken");

            if (String.IsNullOrEmpty(token))
                token = await _sessionStorage.GetItemAsync<string>("authToken");
            else
                await _sessionStorage.SetItemAsync("authToken", token);


            _logger.LogInformation("Token retrieved from session storage: {Token}", token);

            if (string.IsNullOrEmpty(token))
                token = await _authService.Login();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var claims = ParseClaimsFromJwt(token);
            var identity = new ClaimsIdentity(claims, "jwtAuthType");
            var user = new ClaimsPrincipal(identity);
            if (User == null) User = new();
            User.Username = claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
            User.FirstName = claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.GivenName)?.Value;
            User.LastName = claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.FamilyName)?.Value;
            User.Roles = claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).FirstOrDefault().Trim(new char[] { '[', ']' }).Split(',').ToList().ToList();

            var state = new AuthenticationState(user);
            return state;
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
