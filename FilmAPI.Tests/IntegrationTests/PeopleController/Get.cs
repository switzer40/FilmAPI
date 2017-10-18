using FilmAPI.Common.DTOs.Person;
using FilmAPI.Core.SharedKernel;
using FilmAPI.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace FilmAPI.Tests.IntegrationTests.PeopleController
{
    public class Get : TestBase
    {
        private readonly HttpClient _client;
        private string _route;
        public Get()
        {
            _client = base.GetClient();
            _keyService = new KeyService();
            _route = FilmConstants.PersonUri;
        }
        [Fact]
        public async Task ReturnsÖListOfPeopleAsync()
        {
            var response = await _client.GetAsync(_route);
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<KeyedPersonDto>>(stringResponse);

            Assert.Equal(3, result.Count);
        }
        [Fact]
        public async Task ReturnsJuliaRobertsGivenValidSurrogateKey()
        {
            var firstName = "Julia";
            var lastName = "Roberts";
            var birthdate = "1967-10-28";
            var key = _keyService.ConstructPersonSurrogateKey(lastName, birthdate);
            var response = await _client.GetAsync($"{_route}/{key}");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<KeyedPersonDto>(stringResponse);

            Assert.Equal(firstName, result.FirstMidName);
            Assert.Equal(lastName, result.LastName);
            Assert.Equal(birthdate, result.Birthdate);
        }
        [Fact]
        public async Task RetursNotFoundGivenWrongLastNameAsync()
        {            
            var lastName = "Robertson";
            var birthdate = "1967-10-28";
            var key = _keyService.ConstructPersonSurrogateKey(lastName, birthdate);
            var response = await _client.GetAsync($"{_route}/{key}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        [Fact]
        public async Task RetursNotFoundGivenWrongBirthdateAsync()
        {            
            var lastName = "Roberts";
            var birthdate = "1968-10-28";
            var key = _keyService.ConstructPersonSurrogateKey(lastName, birthdate);
            var response = await _client.GetAsync($"{_route}/{key}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        [Fact]
        public async Task ReturnsBadRequestGivenInvalidSurrogateKeyAsync()
        {
            var key = "Howdy";
            var response = await _client.GetAsync($"{_route}/{key}");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
