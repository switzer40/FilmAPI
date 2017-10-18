using FilmAPI.Common.DTOs.Medium;
using FilmAPI.Core.SharedKernel;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FilmAPI.Tests.IntegrationTests.MediaControllr
{
    public class Put : TestBase
    {
        private readonly HttpClient _client;
        private string _route;

        public Put()
        {
            _client = base.GetClient();
            _route = FilmConstants.MediumUri;
        }
        [Fact]
        public async Task ReturnsOKGivenValidMediaDataAsync()
        {
            // Arrange
            // Start with a medium known to be in the DB.
            var title = "Pretty Woman";
            var year = (short)1990;
            var length = (short)109;
            var type = FilmConstants.MediumType_DVD;
            var newLocation = FilmConstants.Location_Right;
            var mediumToUpdate = new BaseMediumDto(title, year, type, newLocation, length);
            var key = _keyService.ConstructMediumSurrogateKey(title, year, type);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(mediumToUpdate), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync(_route, jsonContent);
            response.EnsureSuccessStatusCode();
            var response1 = await _client.GetAsync($"{_route}/{key}");

            // Assert
            response1.EnsureSuccessStatusCode();

            // And now test whether it was properly updated
            var stringResponse = await response1.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<KeyedMediumDto>(stringResponse);
            Assert.Equal(newLocation, result.Location);
        }
    }
}
