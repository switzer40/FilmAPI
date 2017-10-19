using FilmAPI.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using FilmAPI.Core.SharedKernel;
using FilmAPI.Common.DTOs.Film;
using FilmAPI.Common.Services;

namespace FilmAPI.Tests.IntegrationTests.FilmsController
{
    public class Get : TestBase
    {
        private readonly HttpClient _client;
        private string _route;
        

        public Get()
        {
            _client = base.GetClient();
            _keyService = new KeyService();
            _route = FilmConstants.FilmUri;
            // mock dependencies here
            //_keyService = ;
        }

        [Fact]
        public async Task ReturnsListOfFilms()
        {
            var response = await _client.GetAsync(_route);
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<KeyedFilmDto>>(stringResponse);

            Assert.Equal(2, result.Count);
            Assert.Equal(1, result.Where(f => f.Title.Contains("Tiffany")).Count());
            Assert.Equal(1, result.Where(f => f.Title.Contains("Woman")).Count());
        }
        [Fact]
        public async Task ReturnsBadRequestGivenNonexistentSurrogateKey()
        {
            string badKey = "Howdy";
            var response = await _client.GetAsync($"{_route}/{badKey}");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task ReturnPrettyWomanGivenValidSurrogateKey()
        {
            string title = "Pretty Woman";
            short year = 1990;            
            var response = await GetFilmAsync(title, year, _route);
            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<KeyedFilmDto>(stringResponse);

            Assert.Equal(title, result.Title);
            Assert.Equal(year, result.Year);
        }
        [Fact]
        public async Task ReturnNotFoundGivenSurrogateKeyOfUnknownFilm()
        {
            string title = "Star Wars";
            short year = 2017;            
            var response = await GetFilmAsync(title, year, _route);
            AssemblyTraitAttribute.Equals(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
