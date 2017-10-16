using FilmAPI.Core.Entities;
using FilmAPI.Core.SharedKernel;
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
        private string _route;

        public Post()
        {
            _client = base.GetClient();
            _route = FilmConstants.FilmUri;
        }
        
                
        [Fact]
        public async Task ReturnsOkGivenValidFilmDataAsync()
        {
            string title = "BadMax";
            short year = 2017;
            short length = 123;                                    
            var response = await PostFilmAsync(title, year, length, _route);
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
            var length = (short)169;
            var response = await PostFilmAsync(title, year, length, _route);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task ReturnBadRequestGivenLateYearAsync()
        {
            var title = "Gone with the Wind";
            short year = 2051;
            var length = (short)169;
            var response = await PostFilmAsync(title, year, length, _route);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task ReturnBadRequestGivenEmptyTitleAsync()
        {
            var title = "";
            short year = 1957;            
            var length = (short)134;
            var response = await PostFilmAsync(title, year, length, _route)
  ;          Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        }
    }
}
