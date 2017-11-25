using FilmAPI.Common.Constants;
using FilmAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FilmAPI.Tests.IntegrationTests.FilmController
{
    public class Put : TestBase
    {
        private readonly HttpClient _client;
        private string _route;
        public Put()
        {
            _client = base.GetClient();
            _route = "/" + FilmConstants.FilmUri;
        }
        [Fact]
        public async Task ReturnsBadRequestGivenEmptyTitle()
        {
            var title = "";
            var year = (short)1990;
            var length = (short)110;
            var response = await PutFilmAsync(title, year, length, _route);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
        [Fact]
        public async Task UpdatesLengthCorrectlyGivenValidInputAsync()
        {
            var title = "Pretty Woman";
            var year = (short)1990;
            var length = (short)111;
            var response = await PutFilmAsync(title, year, length, _route);
            response.EnsureSuccessStatusCode();

            var f = await CompleteGetFilmAsync(title, year, _route);
            Assert.Equal(title, f.Title);
            Assert.Equal(year, f.Year);
            Assert.Equal(length, f.Length);
        }
    }
}
