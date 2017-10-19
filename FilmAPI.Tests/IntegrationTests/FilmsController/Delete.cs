using FilmAPI.Common.DTOs.Film;
using FilmAPI.Core.SharedKernel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace FilmAPI.Tests.IntegrationTests.FilmsController
{
    public class Delete : TestBase
    {
        private readonly HttpClient _client;
        private string _route;

        public Delete()
        {
            _client = base.GetClient();
            _route = FilmConstants.FilmUri;
        }
        private async Task<int> FilmCountAsync()
        {
            var response = await _client.GetAsync(_route);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var films = JsonConvert.DeserializeObject<List<KeyedFilmDto>>(stringResponse);
            return films.Count;
        }
        [Fact]
        public async Task ReturnsNotFoundGivenRightTitelAndWrongYearAsync()
        {
            var rightTitel = "Pretty Woman";
            var wrongYear = (short)1991;
            string key = _keyService.ConstructFilmKey(rightTitel, wrongYear);
            var response = await _client.DeleteAsync($"{_route}/{key}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            var stringResponse = await response.Content.ReadAsStringAsync();
            Assert.Equal(key, stringResponse);
        }
        [Fact]
        public async Task ReturnBadRequestGivenInvalidFilmSurrogateKeyAsync()
        {
            var key = "Howdy";
            var response = await _client.DeleteAsync($"{_route}/{key}");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            var stringResponse = await response.Content.ReadAsStringAsync();
            Assert.Equal(key, stringResponse);

        }
        [Fact]
        public async Task ReturnMNotFoundGivenWrongTitleAndRightYearAsync()
        {
            var wrongTitel = "Ugly Woman";
            var rightYear = (short)1990;
            string key = _keyService.ConstructFilmKey(wrongTitel, rightYear);
            var response = await _client.DeleteAsync($"{_route}/{key}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            var stringResponse = await response.Content.ReadAsStringAsync();
            Assert.Equal(key, stringResponse);
        }
        [Fact]
        public async Task ReturnsOkGivenValidSurrogateKey()
        {
            var title = "Pretty Woman";
            var year = (short)1990;
            var before = await FilmCountAsync();
            var key = _keyService.ConstructFilmKey(title, year);
            var response = await _client.DeleteAsync($"{_route}/{key}");
            var after = await FilmCountAsync();
            Assert.Equal(before - 1, after);
        }
    }
}
