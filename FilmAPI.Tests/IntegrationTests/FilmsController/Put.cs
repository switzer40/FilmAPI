using FilmAPI.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        public async Task ReturnsOkHivenValidFilmDataAsync()
        {
            // Arrange
            // Start with a film knowen to be in the DB.
            var title = "Pretty Woman";
            var year = (short)1990;
            var newLength = (short)110;
            var filmToUpdate = new FilmViewModel(title, year, newLength);
            var key = filmToUpdate.SurrogateKey;
            var jsonContent = new StringContent(JsonConvert.SerializeObject(filmToUpdate), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync($"api/films", jsonContent);
            response.EnsureSuccessStatusCode();
            var response1 = await _client.GetAsync($"api/films/{key}");

            // Assert
            response1.EnsureSuccessStatusCode();

            // And now test whether it was properly updated
            var stringResponse = await response1.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<FilmViewModel>(stringResponse);
            Assert.Equal(newLength, result.Length);
        }
    }
}
