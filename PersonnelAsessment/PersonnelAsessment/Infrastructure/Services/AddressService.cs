using Application.Abstractions.ServiceAbstractions;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Infrastructure.Services
{
    public class AddressService : IAddressService
    {
        public async Task<bool> CheckAddress(string? address)
        {
            var addr = address!.TrimEnd();
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", "1320bd5561321158463692d9c33b3495ce2f6209");

            using var response = await httpClient
                .PostAsJsonAsync("http://suggestions.dadata.ru/suggestions/api/4_1/rs/suggest/address",
                    new { query = addr });
            var result = await response.Content.ReadFromJsonAsync<Response>();
            var correct = result!.Suggestions!.Any(x => x.Value!.Equals(addr));

            return correct;
        }
    }

    class Response
    {
        public List<Item>? Suggestions { get; set; }
    }

    class Item
    {
        public string? Value { get; set; }
    }
}
