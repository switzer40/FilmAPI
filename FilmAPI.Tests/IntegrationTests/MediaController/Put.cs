using FilmAPI.Core.SharedKernel;
using FilmAPI.DTOs;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FilmAPI.Tests.IntegrationTests.MediaController
{
    public class Put : TestBase
    {
          private readonly HttpClient _client;

    public Put()
    {
        _client = base.GetClient();
    }

    [Fact]
    public async Task ReturnsOkGivenValidMediumData()
        {
            // Arrange
            var title = "Pretty Woman";
            var year = (short)1990;
            var type = FilmConstants.MediumType_DVD;
            var newLocation = FilmConstants.Location_Right;
            var model = new MediumDto(title, year, type, newLocation);
            var key = model.SurrogateKey;
            var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync($"api/media", jsonContent);
            response.EnsureSuccessStatusCode();

            var response1 = await _client.GetAsync($"api/nedia/{key}");

            // Assert
            response1.EnsureSuccessStatusCode();

             // And now test whether it was properly updated
            var stringResponse = await response1.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<MediumDto>(stringResponse);
            Assert.Equal(newLocation, result.Location);
        }
}
}
