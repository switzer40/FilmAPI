using FilmAPI.Core.Entities;
using FilmAPI.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        public async Task ReturnsBadRequestGivenNoFilmTitle()
        {
            var filmToPost = new Film("", 2017, 123);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(filmToPost), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/films", jsonContent);

            var stringResponse = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("Title", stringResponse);
        }
        [Fact]
        public async Task ReturnsBNadResultGivenFilmTitelOver50Chars()
        {
            var filmToPost = new Film("12345678901234567890123456789012345678901234567890x", 2017, 123);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(filmToPost), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/films", jsonContent);

            
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task ReturnsBadRequestGivenYearOutsideRange1850To2100()
        {
            var filmToPost = new Film("MadMax", 2500, 123);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(filmToPost), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/films", jsonContent);

            var stringResponse = await response.Content.ReadAsStringAsync();
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("Year", stringResponse);
        }
        [Fact]
        public async Task ReturnsOkGivenValidFilmData()
        {
            string title = "MadMax";
            short year = 2017;
            short length = 123;
            var filmToPost = new Film(title, year, length);
            var jsonContent = new StringContent(JsonConvert.SerializeObject(filmToPost), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/films", jsonContent);
            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<FilmViewModel>(stringResponse);

            Assert.Equal(title, model.Title);
            Assert.Equal(year, model.Year);
            Assert.Equal(length, model.Length);
        }
    }
}
