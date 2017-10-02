using FilmAPI.DTOs;
using FilmAPI.DTOs.Film;
using Newtonsoft.Json;
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
            var filmToUpdate = new KeyedFilmDto(title, year, newLength);
            filmToUpdate.SurrogateKey = $"{title}*{year}";
            var jsonContent = new StringContent(JsonConvert.SerializeObject(filmToUpdate), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync($"api/films", jsonContent);
            response.EnsureSuccessStatusCode();
            var response1 = await _client.GetAsync($"api/films/{filmToUpdate.SurrogateKey}");

            // Assert
            response1.EnsureSuccessStatusCode();

            // And now test whether it was properly updated
            var stringResponse = await response1.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<KeyedFilmDto>(stringResponse);
            Assert.Equal(newLength, result.Length);
        }
    }
}
