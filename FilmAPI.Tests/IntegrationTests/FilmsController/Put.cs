using FilmAPI.Core.SharedKernel;
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
        private string _route;

        public Put()
        {
            _client = base.GetClient();
            _route = FilmConstants.FilmUri;
        }

        [Fact]
        public async Task ReturnsOkGivenValidFilmDataAsync()
        {
            // Arrange
            // Start with a film known to be in the DB.
            var title = "Pretty Woman";
            var year = (short)1990;
            var newLength = (short)110;            
            var filmToUpdate = new BaseFilmDto(title, year, newLength);
            var key = _keyService.ConstructFilmSurrogateKey(title, year);
             var jsonContent = new StringContent(JsonConvert.SerializeObject(filmToUpdate), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync(_route, jsonContent);
            response.EnsureSuccessStatusCode();
            var response1 = await _client.GetAsync($"{_route}/{key}");

            // Assert
            response1.EnsureSuccessStatusCode();

            // And now test whether it was properly updated
            var stringResponse = await response1.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<KeyedFilmDto>(stringResponse);
            Assert.Equal(newLength, result.Length);
        }
    }
}
