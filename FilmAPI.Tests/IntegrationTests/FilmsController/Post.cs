using FilmAPI.Core.Entities;
using FilmAPI.DTOs;
using FilmAPI.DTOs.Film;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FilmAPI.Tests.IntegrationTests.FilmsController
{
    public class Post : TestBase
    {
        private readonly HttpClient _client;

        public Post()
        {
            _client = base.GetClient();
        }
        
                
        [Fact]
        public async Task ReturnsOkGivenValidFilmDataAsync()
        {
            string title = "BadMax";
            short year = 2017;
            short length = 123;
            var filmToPost = new BaseFilmDto(title, year, length);
            var jsonString = JsonConvert.SerializeObject(filmToPost);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(filmToPost), Encoding.UTF8, "application/json");
            var response = _client.PostAsync("api/films", jsonContent).Result;
            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<KeyedFilmDto>(stringResponse);

            Assert.Equal(title, model.Title);
            Assert.Equal(year, model.Year);
            Assert.Equal(length, model.Length);
        }
        [Fact]
        public async Task ReturnBadRequestGivenEarlyYearAsync()
        {
            var title = "Gone with the Wind";
            short year = 1849;
            var filmToPost = new KeyedFilmDto(title, year);            
            var jsonContent = new StringContent(JsonConvert.SerializeObject(filmToPost), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/films", jsonContent);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task ReturnBadRequestGivenLateYearAsync()
        {
            var title = "Gone with the Wind";
            short year = 2051;
            var filmToPost = new Film(title, year);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(filmToPost), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/films", jsonContent);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task ReturnBadRequestGivenEmptyTitleAsync()
        {
            var title = "";
            short year = 1957;
            var filmToPost = new Film(title, year);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(filmToPost), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/films", jsonContent);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        }
    }
}
