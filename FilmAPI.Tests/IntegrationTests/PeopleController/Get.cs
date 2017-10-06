using FilmAPI.DTOs.Person;
using FilmAPI.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
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
        public async Task ReturnsÖListOfPeopleAsync()
        {
            var response = await _client.GetAsync("api/people");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<KeyedPersonDto>>(stringResponse);

            Assert.Equal(2, result.Count);
        }
    }
}
