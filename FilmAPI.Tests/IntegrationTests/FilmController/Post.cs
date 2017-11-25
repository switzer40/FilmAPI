using FilmAPI.Common.Constants;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FilmAPI.Tests.IntegrationTests.FilmController
{
    public class Post : TestBase
    {
        private readonly HttpClient _client;
        private string _route;
        public Post()
        {
            _client = base.GetClient();
            _route = "/" + FilmConstants.FilmUri;
        }
        [Fact]
        public async Task ReturnsStarWarsGivenValidInput()
        {
            var title = "Star Wars";
            var year = (short)2016;
            var length = (short)167;
            var result = await CompletePostFilmAsync(title, year, length, _route);
            Assert.Equal(title, result.Title);
            Assert.Equal(year, result.Year);
            Assert.Equal(length, result.Length);
        }
        [Fact]
        public async Task ReturnsBadRequestGivenNoTitleAsync()
        {
            var title = "";
            var year = (short)2016;
            var length = (short)167;
            var response = await PostFilmAsync(title, year, length, _route);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task ReturnsBadRequestGiveTooEarlyYearAsync()
        {
            var title = "Star Wars";
            var year = (short)1849;
            var length = (short)167;
             var response = await PostFilmAsync(title, year, length, _route);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task ReturnsBadRequestGiveTooLateYearAsync()
        {
            var title = "Star Wars";
            var year = (short)2051;
            var length = (short)167;
            var response = await PostFilmAsync(title, year, length, _route);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task ReturnsBadRequestGivenDuplicateFilm()
        {
            var title = "Pretty Woman";
            var year = (short)1990;
            var length = (short)111;
            var response = await PostFilmAsync(title, year, length, _route);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            var stringResponse = await response.Content.ReadAsStringAsync();
            Assert.Contains("duplicate", stringResponse);
        }
    }
}
