using FilmAPI.Common.DTOs.FilmPerson;
using FilmAPI.Core.SharedKernel;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FilmAPI.Tests.IntegrationTests.FilmPeopleContrüller
{
    public class Post : TestBase
    {
        private readonly HttpClient _client;
        private string _route;

        public Post()
        {
            _client = base.GetClient();
            _route = FilmConstants.FilmPersonUri;
        }
        [Fact]
        public async Task ReturnsOkGivenValidFilmPersonDataAsync()
        {
            var title = "Pretty Woman";
            var year = (short)1990;
            var lastName = "Gere";
            var birthdate = "1949-08-31";
            var role = FilmConstants.Role_Actor;
            var filmPersonToPost = new BaseFilmPersonDto(title, year, lastName, birthdate, role);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(filmPersonToPost), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_route, jsonContent);
            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<KeyedFilmPersonDto>(stringResponse);

            Assert.Equal(title, result.Title);
            Assert.Equal(year, result.Year);
            Assert.Equal(lastName, result.LastName);
            Assert.Equal(birthdate, result.Birthdate);
            Assert.Equal(role, result.Role);
        }
        [Fact]
        public async Task ReturnsBadRequestGivenTooEarlyYearAsync()
        {
            var title = "Pretty Woman";
            var year = (short)1849;
            var lastName = "Gere";
            var birthdate = "1949-08-31";
            var role = FilmConstants.Role_Actor;
            var filmPersonToPost = new BaseFilmPersonDto(title, year, lastName, birthdate, role);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(filmPersonToPost), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_route, jsonContent);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task ReturnsBadRequestGivenTooLateYearAsync()
        {
            var title = "Pretty Woman";
            var year = (short)2051;
            var lastName = "Gere";
            var birthdate = "1949-08-31";
            var role = FilmConstants.Role_Actor;
            var filmPersonToPost = new BaseFilmPersonDto(title, year, lastName, birthdate, role);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(filmPersonToPost), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_route, jsonContent);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
       [Fact]
        public async Task ReturnsBadRequestGivenEmptyTitleAsync()
        {
            var title = "";
            var year = (short)1990;
            var lastName = "Gere";
            var birthdate = "1949-08-31";
            var role = FilmConstants.Role_Actor;
            var filmPersonToPost = new BaseFilmPersonDto(title, year, lastName, birthdate, role);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(filmPersonToPost), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_route, jsonContent);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
