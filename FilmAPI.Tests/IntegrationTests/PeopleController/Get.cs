using FilmAPI.DTOs;
using FilmAPI.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace FilmAPI.Tests.IntegrationTests.PeopleController
{
    public class Get : TestBase
    {
        private readonly HttpClient _client;
        

        public Get()
        {
            _client = base.GetClient();
            _keyService = new KeyService();
        }

        [Fact]
        public async Task ReturnsListOfPeople()
        {
            var response = await _client.GetAsync("api/people");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<PersonDto>>(stringResponse);

            Assert.Equal(2, result.Count);
            Assert.Equal(1, result.Where(p => p.LastName.Contains("Hepburn")).Count());
            Assert.Equal(1, result.Where(p => p.LastName.Contains("Roberts")).Count());
        }
        [Fact]
        public async Task ReturnBadRequestGivenInvalidPersonSurrogateKeyAsync()
        {
            string badKey = "Howdy";
            var response = await _client.GetAsync($"/api/people/{badKey}");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task ReturnsHepburnGivenValidSurrogateKey()
        {
            string lastName = "Hepburn";
            string birthdate = "1929-05-04";
            string key = _keyService.ConstructPersonSurrogateKey(lastName, birthdate);
            var response = await _client.GetAsync($"api/people/{key}");
            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PersonDto>(stringResponse);

            Assert.Equal(lastName, result.LastName);
            Assert.Equal(birthdate, result.Birthdate);

        }
    }        
}
