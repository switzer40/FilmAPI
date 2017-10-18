using FilmAPI.Common.DTOs.Person;
using FilmAPI.Core.SharedKernel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace FilmAPI.Tests.IntegrationTests.PeopleController
{
    public class Delete : TestBase
    {
        private readonly HttpClient _client;
        private string _route = "";

        public Delete()
        {
            _client = base.GetClient();
            _keyService = new Services.KeyService();
            _route = FilmConstants.PersonUri;
        }
        private async Task<int> PersonCountAsync()
        {
            var response = await _client.GetAsync(_route);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<List<KeyedPersonDto>>(stringResponse);
            return list.Count;
        }
        [Fact]
        public async Task ReturnsBadRequestGivenInvalidSurrogateKeyAsync()
        {
            var key = "Howdy";
            var response = await _client.DeleteAsync($"{_route}/{key}");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task ReturnsNotFoundGivenWrongNameAndRightBirthdateAsync()
        {
            var lastName = "Robins";
            var birthdate = "1967-10-28";
            var key = _keyService.ConstructPersonSurrogateKey(lastName, birthdate);
            var response = await _client.DeleteAsync($"{_route}/{key}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        [Fact]
        public async Task ReturnsNotFoundGivenRightNameAndWrongBirthdateAsync()
        {
            var lastName = "Roberts";
            var birthdate = "1968-10-28";
            var key = _keyService.ConstructPersonSurrogateKey(lastName, birthdate);
            var response = await _client.DeleteAsync($"{_route}/{key}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        [Fact]
        public async Task ReturnsOkGivenValidPersonDataAsync()
        {
            // Delete a person known to be in the DB
            var lastName = "Roberts";
            var birthdate = "1967-10-28";
            var before = await PersonCountAsync();
            var key = _keyService.ConstructPersonSurrogateKey(lastName, birthdate);
            var response = await _client.DeleteAsync($"{_route}/{key}");
            response.EnsureSuccessStatusCode();
            var after = await PersonCountAsync();
            Assert.Equal(before - 1, after);
        }
    }
}