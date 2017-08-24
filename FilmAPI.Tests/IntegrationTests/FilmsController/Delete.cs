using FilmAPI.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FilmAPI.Tests.IntegrationTests.FilmsController
{
    public class Delete : TestBase
    {
        private readonly HttpClient _client;

        public Delete()
        {
            _client = base.GetClient();
        }

        [Fact]
        public async Task ReturnsNotFoundGivenNonExistentSurrogateKey()
        {
            string key = "Howdy";
            var response = await _client.DeleteAsync($"api/films/{key}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            var stringResponse = await response.Content.ReadAsStringAsync();
            Assert.Equal(key, stringResponse);
        }
        [Fact]
        public async Task ReturnsOkGivenValidSurrogateKey()
        {
            var response = await _client.GetAsync("api/films");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var films = JsonConvert.DeserializeObject<List<FilmViewModel>>(stringResponse);
            var keyToDelete = films[0].SurrogateKey;
            var response2 = await _client.DeleteAsync($"apifilms/{keyToDelete}");
            response2.EnsureSuccessStatusCode();
        }                        
    }
}
