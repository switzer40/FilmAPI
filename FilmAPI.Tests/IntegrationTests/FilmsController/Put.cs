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
    public class Put : TestBase
    {
        private readonly HttpClient _client;

        public Put()
        {
            _client = base.GetClient();
        }


        [Fact]
        public async Task ReturnsNotFoundGivenNonExistentSurrogateKy()
        {
            string key = "Howdy"; // A surrogate key that certainly does not exist.
            var filmToPut = new FilmViewModel("", 1957, 123);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(filmToPut), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync("api/films", jsonContent);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            var stringResponse = await response.Content.ReadAsStringAsync();
            Assert.Equal("", stringResponse);
        }
    }
}
