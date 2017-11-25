using FilmAPI.Common.Constants;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Newtonsoft.Json;
using FilmAPI.Common.DTOs;
using System.Linq;
using System.Net;

namespace FilmAPI.Tests.IntegrationTests.FilmController
{
     public class Get : TestBase
    {
        private HttpClient _client;
        private string _route = "";
        public Get()
        {
            _client = base.GetClient();
            _route += FilmConstants.FilmUri;
        }
        [Fact]
        public async Task ReturnsTiffanyAndPrettyAsync()
        {
            var l = await GetFilmAsync(_route);

            Assert.Equal(2,l.Count);
            Assert.True(l.Any(f => f.Title.Contains("Tiffany")));
            Assert.True(l.Any(f => f.Title.Contains("Pretty")));
        }
        [Fact]
        public async Task ReturnsPrettyWomanGivenValidInputAsync()
        {
            var title = "Pretty Woman";
            var year = (short)1990;
            var key = _keyService.ConstructFilmKey(title, year);
            var response = await _client.GetAsync($"{_route}/{key}");
            response.EnsureSuccessStatusCode();
            var stringReponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<KeyedFilmDto>(stringReponse);

            Assert.Equal(title, result.Title);
            Assert.Equal(year, result.Year);
        }
        [Fact]
        public async Task ReturnsNotFoundGivenWrongTitleAsync()
        {
            var title = "Ugly Woman";
            var year = (short)1990;
            var key = _keyService.ConstructFilmKey(title, year);
            var response = await _client.GetAsync($"{_route}/{key}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);            
        }
        [Fact]
        public async Task ReturnsNotFoundGivenWrongYearAsync()
        {
            var title = "Pretty Woman";
            var year = (short)1991;
            var key = _keyService.ConstructFilmKey(title, year);
            var response = await _client.GetAsync($"{_route}/{key}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        [Fact]
        public async Task ReturnsBadRequestGivenInvalidKeyAsync()
        {            
            var key = "Howdy";
            var response = await _client.GetAsync($"{_route}/{key}");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
