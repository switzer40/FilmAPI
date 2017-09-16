using FilmAPI.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FilmAPI.Tests.IntegrationTests.PeopleController
{
    public class Delete : TestBase
    {
        private readonly HttpClient _client;

        public Delete()
        {
            _client = base.GetClient();
        }
        [Fact]
        public async Task ReturnBadRequestGivenInvalidPersonAsyncSurrogateKey()
        {
            string key = "Howdy";
            var response = await _client.DeleteAsync($"api/people/{key}");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            var stringResponse = await response.Content.ReadAsStringAsync();
            Assert.Equal(key, stringResponse);
        }

        [Fact]
        public async Task ReturnsOkGivenValidSurrogateKey()
        {
            var lastName = "Roberts";
            var birthdate = "1967-10-28";
            var key = _keyService.ConstructPersonSurrogateKey(lastName, birthdate);
            var response = await _client.GetAsync($"api/people/{key}");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<PersonViewModel>(stringResponse);
            var keyToDelete = model.SurrogateKey;
            keyToDelete = Uri.EscapeUriString(keyToDelete);
            var response2 = await _client.DeleteAsync($"api/people/{keyToDelete}");
            response2.EnsureSuccessStatusCode();
        }
    }
}
