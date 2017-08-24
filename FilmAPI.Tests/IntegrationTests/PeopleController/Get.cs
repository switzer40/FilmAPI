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

        public Get()
        {
            _client = base.GetClient();
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
    }        
}
