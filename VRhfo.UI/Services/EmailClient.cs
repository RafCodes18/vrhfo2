using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace VRhfo.UI.Services
{
    public class EmailClient
    {
        private readonly HttpClient _httpClient;

        public EmailClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var request = new { To = to, Subject = subject, Body = body };
            var response = await _httpClient.PostAsJsonAsync("api/emails", request);
            response.EnsureSuccessStatusCode();
        }
    }
}