using FilmAPI.Interfaces;
using FilmAPI.Services;
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
    class Put : TestBase
    {
        private readonly HttpClient _client;
        private readonly IKeyService service = new KeyService();
        public Put()
        {
            _client = base.GetClient();
        }
        [Fact]
        public async Task ReturnsBadRequestGivenNonExistentSurrogateKy()
        {
            string key = "Howdy"; // A surrogate key that certainly does not exist
            var firstName = "Alice";
            var lastName = "King";
            var birthdate = "1940-02-14";
            var personToPut = new PersonViewModel(lastName, birthdate, firstName);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(personToPut), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"api/people/{key}", jsonContent);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var stringResponse = await response.Content.ReadAsStringAsync();
            Assert.Equal("Howdy", stringResponse);
        }
    }
}
