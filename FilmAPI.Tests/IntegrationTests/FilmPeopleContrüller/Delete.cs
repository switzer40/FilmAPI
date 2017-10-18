using FilmAPI.Common.DTOs.FilmPerson;
using FilmAPI.Core.SharedKernel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace FilmAPI.Tests.IntegrationTests.FilmPeopleContrüller
{
    public class Delete : TestBase
    {
        private readonly HttpClient _client;
        private string _route;
        public Delete()
        {
            _client = base.GetClient();
            _route = FilmConstants.FilmPersonUri;
        }
        private async Task<int> FilmPersonCount()
        {
            var response = await _client.GetAsync(_route);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var filmPeople = JsonConvert.DeserializeObject<List<KeyedFilmPersonDto>>(stringResponse);
            return filmPeople.Count;
        }
        [Fact]
        public async Task DecrementsCountByOneAsync()
        {
            var before = await FilmPersonCount();
            var title = "Pretty Woman";
            var year = (short)1990;
            var lastName = "Roberts";
            var birthdate = "1967-10-28";
            var role = FilmConstants.Role_Actor;
            var key = _keyService.ConstructFilmPersonSurrogateKey(title, year, lastName, birthdate, role);
            var response = await _client.DeleteAsync($"{_route}/{key}");
            response.EnsureSuccessStatusCode();
            var after = await FilmPersonCount();
            Assert.Equal(before - 1, after);
        }
    }
}
