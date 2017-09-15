using FilmAPI.Core.Entities;
using FilmAPI.Interfaces;
using FilmAPI.Services;
using FilmAPI.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FilmAPI.Tests.IntegrationTests.FilmsController
{
    public class Get : TestBase
    {
        private readonly HttpClient _client;
        private readonly IKeyService _keyService;

        public Get()
        {
            _client = base.GetClient();
            _keyService = new KeyService();
            // mock dependencies here
            //_keyService = ;
        }

        [Fact]
        public async Task ReturnsListOfFilms()
        {
            var response = await _client.GetAsync("/api/films");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<FilmViewModel>>(stringResponse);

            Assert.Equal(2, result.Count);
            Assert.Equal(1, result.Where(f => f.Title.Contains("Tiffany")).Count());
            Assert.Equal(1, result.Where(f => f.Title.Contains("Woman")).Count());
        }
        [Fact]
        public async Task ReturnsNotFoundGivenNonexistentSurrogateKey()
        {
            string badKey = "Howdy";
            var response = await _client.GetAsync($"/api/films/{badKey}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        [Fact]
        public async Task ReturnPrettyWomanGivenValidSurrogateKey()
        {
            string title = "Pretty Woman";
            short year = 1990;
            string key = _keyService.ConstructFilmSurrogateKey(title, year);
            var response = await _client.GetAsync($"api/films/{key}");
            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<FilmViewModel>(stringResponse);

            Assert.Equal(title, result.Title);
            Assert.Equal(year, result.Year);
        }
        [Fact]
        public async Task ReturnNotFoundGivenSurrogateKeyOfUnknownFilm()
        {
            string title = "Star Wars";
            short year = 2017;
            string key = _keyService.ConstructFilmSurrogateKey(title, year);
            var response = await _client.GetAsync($"api/films/{key}");
            AssemblyTraitAttribute.Equals(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
