using FilmAPI.Common.DTOs.Person;
using FilmAPI.Core.SharedKernel;
using FilmAPI.Services;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FilmAPI.Tests.IntegrationTests.PeopleController
{
    public class Put : TestBase
    {
        private readonly HttpClient _client;
        private string _route;

        public Put()
        {
            _client = base.GetClient();
            _keyService = new KeyService();
            _route = FilmConstants.PersonUri;
        }
        [Fact]
        public async Task ReturnsOkGivenValidPersonData()
        {
            var firstName = "Juliet";
            var lastName = "Roberts";
            var birthdate = "1967-10-28";
            var key = _keyService.ConstructPersonSurrogateKey(lastName, birthdate);
            var personToUpdate = new BasePersonDto(lastName, birthdate, firstName);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(personToUpdate), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(_route, jsonContent);
            response.EnsureSuccessStatusCode();

            var response1 = await _client.GetAsync($"{_route}/{key}");
            response1.EnsureSuccessStatusCode();

            var stringResponse = await response1.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<KeyedPersonDto>(stringResponse);
            Assert.Equal(firstName, result.FirstMidName);
        }

    }
}
