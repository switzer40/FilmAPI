using FilmAPI.Core.SharedKernel;
using FilmAPI.DTOs.Medium;
using FilmAPI.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FilmAPI.Tests.IntegrationTests.MediaControllr
{
    public class Get : TestBase
    {
        private readonly HttpClient _client;
        private string _route;
        public Get()
        {
            _client = base.GetClient();
            _keyService = new KeyService();
            _route = FilmConstants.MediumUri;
        }
        [Fact]
        public async Task ReturnsListOfMediaAsync()
        {
            var response = await _client.GetAsync(_route);
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<KeyedMediumDto>>(stringResponse);

            Assert.Equal(2, result.Count);
        }
        [Fact]
        public async Task ReturnsPrettyWomanGivenValidSurrogateKeyAsync()
        {
            var title = "Pretty Woman";
            var year = (short)1990;
            var type = FilmConstants.MediumType_DVD;
            var key = _keyService.ConstructMediumSurrogateKey(title, year, type);
            var response = await _client.GetAsync($"{_route}/{key}");
            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<KeyedMediumDto>(stringResponse);

            Assert.Equal(title, result.Title);
            Assert.Equal(year, result.Year);
            Assert.Equal(type, result.MediumType);
        }
        [Fact]
        public async Task ReturnsNotFoundGivenSurrogateKeyWithWrongMediumTypeAsync()
        {
            var title = "Pretty Woman";
            var year = (short)1990;
            var type = FilmConstants.MediumType_BD;
            var key = _keyService.ConstructMediumSurrogateKey(title, year, type);
            var response = await _client.GetAsync($"{_route}/{key}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        [Fact]
        public async Task ReturnsNotFoundGivenWrongFilmAsync()
        {
            var title = "Star Wars";
            var year = (short)1990;
            var type = FilmConstants.MediumType_BD;
            var key = _keyService.ConstructMediumSurrogateKey(title, year, type);
            var response = await _client.GetAsync($"{_route}/{key}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        [Fact]
        public async Task ReturnsNotFoundGivenWrongYearAsync()
        {
            var title = "Pretty Woman";
            var year = (short)1991;
            var type = FilmConstants.MediumType_DVD;
            var key = _keyService.ConstructMediumSurrogateKey(title, year, type);
            var response = await _client.GetAsync($"{_route}/{key}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        [Fact]
        public async Task ReturnsBadRequestGivenInvalidSurrogateKeyAsync()
        {                       
            var key = "Howdy";
            var response = await _client.GetAsync($"{_route}/{key}");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
