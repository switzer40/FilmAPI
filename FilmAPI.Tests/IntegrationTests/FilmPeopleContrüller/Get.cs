using FilmAPI.Core.SharedKernel;
using FilmAPI.DTOs.FilmPerson;
using FilmAPI.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FilmAPI.Tests.IntegrationTests.FilmPeopleContrüller
{
    public class Get : TestBase
    {
        private readonly HttpClient _client;
        private string _route;



        public Get()
        {
            _client = base.GetClient();
            _keyService = new KeyService();
            _route = FilmConstants.FilmPersonUri;
            // mock dependencies here
            //_keyService = ;
        }
        [Fact]
        public async Task ReturnsListOfFilmPeople()
        {
            var response = await _client.GetAsync(_route);
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<KeyedFilmPersonDto>>(stringResponse);

            Assert.Equal(2, result.Count);            
        }
        [Fact]
        public async Task ReturnsPrettyWomanGivenValidSurrogateKey()
        {
            var title = "Pretty Woman";
            var year = (short)1990;
            var lastName = "Roberts";
            var birthdate = "1967-10-28";
            var role = FilmConstants.Role_Actor;
            var key = _keyService.ConstructFilmPersonSurrogateKey(title, year, lastName, birthdate, role);
            var response = await _client.GetAsync($"{_route}/{key}");
            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<KeyedFilmPersonDto>(stringResponse);

            Assert.Equal(title, result.Title);
        }
        [Fact]
        public async Task ReturnsRobertsGivenValidSurrogateKey()
        {
            var title = "Pretty Woman";
            var year = (short)1990;
            var lastName = "Roberts";
            var birthdate = "1967-10-28";
            var role = FilmConstants.Role_Actor;
            var key = _keyService.ConstructFilmPersonSurrogateKey(title, year, lastName, birthdate, role);
            var response = await _client.GetAsync($"{_route}/{key}");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<KeyedFilmPersonDto>(stringResponse);

            Assert.Equal(lastName, result.LastName);
        }
        [Fact]
        public async Task ReturnsBadQuestGivenInvalidSurrogateKey()
        {
            var key = "Howdy";
            var response = await _client.GetAsync($"{_route}/{key}");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        }
        [Fact]
        public async Task ReturnsNotFoundGivenValidDataExceptForTitle()
        {
            var title = "Ugly Woman";
            var year = (short)1990;
            var lastName = "Roberts";
            var birthdate = "1967-10-28";
            var role = FilmConstants.Role_Actor;
            var key = _keyService.ConstructFilmPersonSurrogateKey(title, year, lastName, birthdate, role);
            var response = await _client.GetAsync($"{_route}/{key}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            var stringResponse = await response.Content.ReadAsStringAsync();
            Assert.Contains(title, stringResponse);
        }
        [Fact]
        public async Task ReturnsNotFoundGivenValidDataExceptForYear()
        {
            var title = "Pretty Woman";
            var year = (short)1991;
            var lastName = "Roberts";
            var birthdate = "1967-10-28";
            var role = FilmConstants.Role_Actor;
            var key = _keyService.ConstructFilmPersonSurrogateKey(title, year, lastName, birthdate, role);
            var response = await _client.GetAsync($"{_route}/{key}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            var stringResponse = await response.Content.ReadAsStringAsync();
            Assert.Contains(year.ToString(), stringResponse);
        }
        [Fact]
        public async Task ReturnsNotFoundGivenValidDataExceptForLastName()
        {
            var title = "Pretty Woman";
            var year = (short)1990;
            var lastName = "Robins";
            var birthdate = "1967-10-28";
            var role = FilmConstants.Role_Actor;
            var key = _keyService.ConstructFilmPersonSurrogateKey(title, year, lastName, birthdate, role);
            var response = await _client.GetAsync($"{_route}/{key}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            var stringResponse = await response.Content.ReadAsStringAsync();
            Assert.Contains(lastName, stringResponse);
        }
        [Fact]
        public async Task ReturnsNotFoundGivenValidDataExceptForBirthdate()
        {
            var title = "Pretty Woman";
            var year = (short)1990;
            var lastName = "Roberts";
            var birthdate = "1968-10-28";
            var role = FilmConstants.Role_Actor;
            var key = _keyService.ConstructFilmPersonSurrogateKey(title, year, lastName, birthdate, role);
            var response = await _client.GetAsync($"{_route}/{key}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            var stringResponse = await response.Content.ReadAsStringAsync();
            Assert.Contains(birthdate, stringResponse);
        }
        [Fact]
        public async Task ReturnsNotFoundGivenValidDataExceptForRole()
        {
            var title = "Pretty Woman";
            var year = (short)1990;
            var lastName = "Roberts";
            var birthdate = "1967-10-28";
            var role = FilmConstants.Role_Composer;
            var key = _keyService.ConstructFilmPersonSurrogateKey(title, year, lastName, birthdate, role);
            var response = await _client.GetAsync($"{_route}/{key}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            var stringResponse = await response.Content.ReadAsStringAsync();
            Assert.Contains(role, stringResponse);
        }
    }
}
