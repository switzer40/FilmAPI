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
    public class Delete : TestBase
    {        
        private readonly string _route;
        public Delete()
        {            
            _route = "/" + FilmConstants.FilmUri;
        }
        [Fact]
        public async Task ReturnsOkGivenValidKey()
        {
            var title = "Pretty Woman";
            var year = (short)1990;            
            var response = await DeleteFilmAsync(title, year, _route);
            response.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task ReturnsNotFoundGivenUnknownKey()
        {
            var title = "Gone with the Wind";
            var year = (short)1957;
            var response = await DeleteFilmAsync(title, year, _route);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        [Fact]
        public async Task ReturnsBadRequestGivenInvalidKey()
        {
            var key = "Howdy";
            var response = await DeleteFilmWithKeyAsync(key, _route);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
