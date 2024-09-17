using ReserGO.Miscellaneous.Model;
using ReserGO.Service.Interface.Utils;
using System.Text.Json;

namespace ReserGO.Service.Service.Utils
{
    public class ComuneService : IComuneService
    {
    private readonly HttpClient _httpClient;

    public ComuneService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<DTOComuneProvincia>> GetComuniAsync(string nome, int page = 1, int pageSize = 100)
        {
            var url = $"https://axqvoqvbfjpaamphztgd.functions.supabase.co/comuni?page={page}&pagesize={pageSize}&nome={nome}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error fetching data: {response.ReasonPhrase}");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();

            // Aggiungi questo per verificare il JSON restituito
            Console.WriteLine(jsonResponse);

            var comuni = JsonSerializer.Deserialize<List<DTOComuneProvincia>>(jsonResponse, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return comuni;
        }

       

    }
}
