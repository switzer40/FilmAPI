using FilmAPI.Interfaces;
using FilmAPI.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FilmAPI.Tests.IntegrationTests.PeopleController
{
    public class Get : TestBase
    {
        private readonly HttpClient _client;
        private readonly IKeyService _keyService;

        public Get(IKeyService keyService)
        {
            _client = base.GetClient();
            _keyService = keyService;
        }

        [Fact]
        public async Task RturnsListOfPeople()
        {
            var response = await _client.GetAsync("api/people");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<PersonViewModel>>(stringResponse);

            Assert.Equal(2, result.Count);
            Assert.Equal(1, result.Where(p => p.LastName.Contains("Hepburn")).Count());
            Assert.Equal(1, result.Where(p => p.LastName.Contains("Roberts")).Count());
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
            var result = JsonConvert.DeserializeObject<PersonViewModel>(stringResponse);

            Assert.Equal(lastName, result.LastName);
            Assert.Equal(birthdate, result.BirthdateString);

        }
    }        
}
